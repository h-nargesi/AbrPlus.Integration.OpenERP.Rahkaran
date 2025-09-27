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
    private readonly static Dictionary<string, PropertyInfo> PaymentCashMoneysType;
    private readonly static Dictionary<string, PropertyInfo> PaymentDepositDataType;
    private readonly static Dictionary<string, PropertyInfo> PaymentPayableChequeDataType;

    static PaymentExtension()
    {
        PaymentCashMoneysType = typeof(PaymentCashMoneyData).GetProperties().ToDictionary(k => k.Name);
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

        dto.PaymentCashMoneys.InjectTo('C', PaymentCashMoneysType.Values, properties);
        dto.PaymentDeposits.InjectTo('D', PaymentDepositDataType.Values, properties);
        dto.PaymentPayableCheques.InjectTo('P', PaymentPayableChequeDataType.Values, properties);

        return bundle;
    }

    public static PaymentDto ToDto(this PaymentBundle bundle)
    {
        throw new NotImplementedException();
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
}