using System;
using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Dtos;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Payment;

public static class PaymentExtension
{
    public static PaymentBundle ToBundle(this PaymentDto dto)
    {
        var bundle = new PaymentBundle();
        throw new NotImplementedException();
    }

    public static PaymentDto ToDto(this PaymentBundle bundle)
    {
        var dto = new PaymentDto();
        throw new NotImplementedException();
    }
}