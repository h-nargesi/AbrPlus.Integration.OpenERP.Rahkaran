using System;
using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Dtos;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Receipt;

public static class ReceiptExtension
{
    public static PaymentBundle ToBundle(this ReceiptDto dto)
    {
        var bundle = new PaymentBundle();
        throw new NotImplementedException();
    }

    public static ReceiptDto ToDto(this PaymentBundle bundle)
    {
        var dto = new ReceiptDto();
        throw new NotImplementedException();
    }
}