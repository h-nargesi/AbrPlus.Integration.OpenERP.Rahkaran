using Refit;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Invoice;

[Headers("Content-Type: application/json")]
internal interface IInvoiceRmsWebService
{
    public const string BasePath = "/Retail/eSalesApi/ESalesService.svc";

    [Get("/Invoice")]
    Task<GetInvoicesResponse> GetInvoiceById(object key, [Header("Cookie")] string cookie);

    [Post("/Invoice")]
    Task<SaveInvoiceResponse> SaveInvoice(object dto, [Header("Cookie")] string cookie);
}

[Headers("Content-Type: application/json")]
internal interface IInvoiceSlsWebService
{
    public const string BasePath = "/Sales/InvoiceManagement/Services/InvoiceManagementService.svc";

    [Get("/Invoice")]
    Task<GetInvoicesResponse> GetInvoiceById(object key, [Header("Cookie")] string cookie);

    [Post("/Invoice")]
    Task<SaveInvoiceResponse> SaveInvoice(object dto, [Header("Cookie")] string cookie);
}