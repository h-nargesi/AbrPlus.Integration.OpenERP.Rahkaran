using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Dtos;
using SeptaKit.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Payment;

public static class PaymentExtension
{
    private readonly static Dictionary<string, PropertyInfo> PaymentCashMoneyDataType;
    private readonly static Dictionary<string, PropertyInfo> PaymentDepositDataType;
    private readonly static Dictionary<string, PropertyInfo> PaymentPayableChequeDataType;

    static PaymentExtension()
    {
        PaymentCashMoneyDataType = typeof(PaymentCashMoneyData).GetProperties().ToDictionary(k => k.Name);
        PaymentDepositDataType = typeof(PaymentDepositData).GetProperties().ToDictionary(k => k.Name);
        PaymentPayableChequeDataType = typeof(PaymentPayableChequeData).GetProperties().ToDictionary(k => k.Name);
    }

    public static PaymentBundle ToBundle(this PaymentDto dto)
    {
        var bundle = new PaymentBundle
        {
            BranchCode = dto.BranchID.ToString(),
            Description = dto.Description,
            IsReceipt = false,
            Number = dto.Number,
            SettledDate = dto.Date,
            ChequeBank = dto.PaymentPayableCheques?[0].BankAccountID.ToString(),
            ChequeNo = dto.PaymentPayableCheques?[0].ChequeNumber,
        };

        var properties = new List<ExtendedProperty>
        {
            new() {
                Key = nameof(dto.CashID),
                Value = dto.CashID.ToString(),
            },
            new() {
                Key = nameof(dto.CounterPartDLCode),
                Value = dto.CounterPartDLCode,
            },
        };

        dto.PaymentCashMoneys.InjectTo('C', PaymentCashMoneyDataType.Values, properties);
        dto.PaymentDeposits.InjectTo('D', PaymentDepositDataType.Values, properties);
        dto.PaymentPayableCheques.InjectTo('P', PaymentPayableChequeDataType.Values, properties);

        return bundle;
    }

    public static PaymentDto ToDto(this PaymentBundle bundle)
    {
        if (!long.TryParse(bundle.BranchCode, out var branchId))
        {
            // TODO use custom exception
            throw new Exception("Unkonw Branch-ID.");
        }

        if (!bundle.SettledDate.HasValue)
        {
            // TODO use custom exception
            throw new Exception("The SettledDate is not set.");
        }

        var dto = new PaymentDto
        {
            BranchID = branchId,
            Description = bundle.Description,
            Number = bundle.Number,
            Date = bundle.SettledDate.Value,
        };

        if (bundle.ExtendedProperties == null) return dto;

        var paymentCashMoneys = new Dictionary<string, PaymentCashMoneyData>();
        var paymentDeposits = new Dictionary<string, PaymentDepositData>();
        var paymentPayableCheques = new Dictionary<string, PaymentPayableChequeData>();

        foreach (var prop in bundle.ExtendedProperties)
        {
            if (!string.IsNullOrEmpty(prop.Key)) continue;

            switch (prop.Key[0])
            {
                case 'C':
                    PaymentCashMoneyDataType.ReadFrom(prop, paymentCashMoneys);
                    break;
                case 'D':
                    PaymentDepositDataType.ReadFrom(prop, paymentDeposits);
                    break;
                case 'P':
                    PaymentPayableChequeDataType.ReadFrom(prop, paymentPayableCheques);
                    break;
            }
        }

        dto.PaymentCashMoneys = [.. paymentCashMoneys.OrderBy(o => o.Key).Select(c => c.Value)];
        dto.PaymentDeposits = [.. paymentDeposits.OrderBy(o => o.Key).Select(c => c.Value)];
        dto.PaymentPayableCheques = [.. paymentPayableCheques.OrderBy(o => o.Key).Select(c => c.Value)];

        return dto;
    }

    private static void InjectTo(this object[] sources, char code, IEnumerable<PropertyInfo> props, List<ExtendedProperty> properties)
    {
        if (sources == null || sources.Length < 1) return;

        var i = 0;
        foreach (var deposit in sources)
        {
            foreach (var p in props)
            {
                var value = p.GetValue(deposit)?.ToString();
                if (value == null) continue;

                properties.Add(new ExtendedProperty
                {
                    Key = $"{code}_{p.Name}_{i}",
                    Value = value,
                });
            }
            i++;
        }
    }

    private static void ReadFrom<T>(this Dictionary<string, PropertyInfo> type, ExtendedProperty prop, Dictionary<string, T> data) where T : class, new()
    {
        var keys = prop.Key.Split('_');
        if (keys.Length != 3) return;

        if (type.TryGetValue(keys[1], out var pType))
        {
            if (!data.TryGetValue(keys[2], out var record))
            {
                record = new T();
            }

            pType.SetValue(record, prop.Value);
        }
    }
}