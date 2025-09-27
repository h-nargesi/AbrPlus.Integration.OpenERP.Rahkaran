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