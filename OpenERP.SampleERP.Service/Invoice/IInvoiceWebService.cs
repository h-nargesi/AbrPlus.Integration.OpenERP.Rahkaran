using Refit;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Invoice;

[Headers("Content-Type: application/json")]
internal interface IInvoiceWebService
{
    public const string BasePathRms = "Retail/eSalesApi/ESalesService.svc";
    public const string BasePathSls = "Sales/InvoiceManagement/Services/InvoiceManagementService.svc";

    [Get("Invoice")]
    Task<GetInvoicesResponse> GetInvoiceById(object key, [Header("Cookie")] string cookie);

    [Post("Invoice")]
    Task<SaveInvoiceResponse> SaveInvoice(object dto, [Header("Cookie")] string cookie);
}
