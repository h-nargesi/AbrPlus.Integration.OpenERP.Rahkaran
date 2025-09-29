using AbrPlus.Integration.OpenERP.SampleERP.Dtos;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Invoice;

public class GetInvoicesResponse
{
    public InvoiceRmsDto GetInvoiceByIdResult { get; set; }
}

public class SaveInvoiceResponse
{
    public object SaveInvoiceResult { get; set; }
}