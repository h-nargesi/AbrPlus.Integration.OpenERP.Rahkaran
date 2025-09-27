using AbrPlus.Integration.OpenERP.SampleERP.Dtos;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Quote;

public class QuoteSaveDocument
{
    public QuoteDto document { get; set; }
}

public class GetQuotesResponse
{
    public QuoteDto GetQuoteByIdResult { get; set; }
}

public class SaveQuoteResponse
{
    public object SaveQuoteResult { get; set; }
}