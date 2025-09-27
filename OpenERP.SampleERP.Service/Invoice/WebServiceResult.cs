using AbrPlus.Integration.OpenERP.SampleERP.Dtos;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Invoice;

public class InvoiceSaveDocument
{
    public InvoiceDto document { get; set; }
}

public class GetInvoicesResponse
{
    public InvoiceDto GetInvoiceByIdResult { get; set; }
}

public class SaveInvoiceResponse
{
    public object SaveInvoiceResult { get; set; }
}