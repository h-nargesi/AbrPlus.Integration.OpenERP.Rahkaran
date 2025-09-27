using Refit;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Invoice;

[Headers("Content-Type: application/json")]
internal interface IInvoiceWebService
{
    [Get("Invoice")]
    Task<GetInvoicesResponse> GetInvoiceById(object key, [Header("Cookie")] string cookie);

    [Post("Invoice")]
    Task<SaveInvoiceResponse> SaveInvoice(InvoiceSaveDocument dto, [Header("Cookie")] string cookie);
}
