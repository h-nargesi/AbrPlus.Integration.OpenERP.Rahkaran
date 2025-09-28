using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Receipt;

public static class ReceiptExtension
{
    private readonly static Dictionary<string, PropertyInfo> ReceiptCashMoneyData;
    private readonly static Dictionary<string, PropertyInfo> ReceiptDepositData;
    private readonly static Dictionary<string, PropertyInfo> ReceiptReceivableChequeData;

    static ReceiptExtension()
    {
        ReceiptCashMoneyData = typeof(ReceiptCashMoneyData).GetProperties().ToDictionary(k => k.Name);
        ReceiptDepositData = typeof(ReceiptDepositData).GetProperties().ToDictionary(k => k.Name);
        ReceiptReceivableChequeData = typeof(ReceiptReceivableChequeData).GetProperties().ToDictionary(k => k.Name);
    }

    public static PaymentBundle ToBundle(this ReceiptDto dto)
    {
        var bundle = new PaymentBundle
        {
            BranchCode = dto.BranchID.ToString(),
            Description = dto.Description,
            IsReceipt = true,
            Number = dto.Number,
            SettledDate = dto.Date,
            ChequeBank = dto.ReceiptReceivableCheques?[0].BankID.ToString(),
            ChequeNo = dto.ReceiptReceivableCheques?[0].SerialNumber,
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

        dto.ReceiptCashMoneys.InjectTo('C', ReceiptCashMoneyData.Values, properties);
        dto.ReceiptDeposits.InjectTo('D', ReceiptDepositData.Values, properties);
        dto.ReceiptReceivableCheques.InjectTo('P', ReceiptReceivableChequeData.Values, properties);

        return bundle;
    }

    public static ReceiptDto ToDto(this PaymentBundle bundle)
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

        var dto = new ReceiptDto
        {
            BranchID = branchId,
            Description = bundle.Description,
            Number = bundle.Number,
            Date = bundle.SettledDate.Value,
        };

        if (bundle.ExtendedProperties == null) return dto;

        var receiptCashMoneys = new Dictionary<string, ReceiptCashMoneyData>();
        var receiptDeposits = new Dictionary<string, ReceiptDepositData>();
        var receiptReceivableCheques = new Dictionary<string, ReceiptReceivableChequeData>();

        foreach (var prop in bundle.ExtendedProperties)
        {
            if (!string.IsNullOrEmpty(prop.Key)) continue;

            switch (prop.Key[0])
            {
                case 'C':
                    ReceiptCashMoneyData.ReadFrom(prop, receiptCashMoneys);
                    break;
                case 'D':
                    ReceiptDepositData.ReadFrom(prop, receiptDeposits);
                    break;
                case 'P':
                    ReceiptReceivableChequeData.ReadFrom(prop, receiptReceivableCheques);
                    break;
            }
        }

        dto.ReceiptCashMoneys = [.. receiptCashMoneys.OrderBy(o => o.Key).Select(c => c.Value)];
        dto.ReceiptDeposits = [.. receiptDeposits.OrderBy(o => o.Key).Select(c => c.Value)];
        dto.ReceiptReceivableCheques = [.. receiptReceivableCheques.OrderBy(o => o.Key).Select(c => c.Value)];

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