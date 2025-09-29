using AbrPlus.Integration.OpenERP.SampleERP.Dtos;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Quote;

public class QuoteDtoRmsResponse
{
    public QuoteDto Result { get; set; }
    public Metadata Metadata { get; set; }
}

public class Metadata
{
    public bool IsSuccessfull { get; set; }
    public string ErrorMessage { get; set; }
    public string StackTrace { get; set; }
}