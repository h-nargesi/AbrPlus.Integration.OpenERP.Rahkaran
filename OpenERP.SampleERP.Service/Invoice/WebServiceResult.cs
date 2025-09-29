using AbrPlus.Integration.OpenERP.SampleERP.Dtos;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Invoice;

public class GetInvoicesResponse
{
    public InvoiceRmsDto GetInvoiceByIdResult { get; set; }
}

public class SaveInvoiceResponse
{
    public InvoiceRmsDto Result { get; set; }
    public Metadata Metadata { get; set; }
}

public class Metadata
{
    public bool IsSuccessfull { get; set; }
    public string ErrorMessage { get; set; }
    public string StackTrace { get; set; }
}