using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Dtos;
using System;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Quote;

public static class QuoteExtension
{
    public static InvoiceBundle ToBundle(this QuoteDto dto, IdentityBundle identity)
    {
        throw new NotImplementedException();
    }

    public static QuoteDto ToDto(this InvoiceBundle bundle)
    {
        throw new NotImplementedException();
    }
}
