using Refit;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Quote;

[Headers("Content-Type: application/json")]
internal interface IQuoteRmsWebService
{
    public const string BasePath = "/Retail/eSalesApi/ESalesService.svc";

    [Get("Quote")]
    Task<QuoteDtoRmsResponse> GetSalesOrderById(object key, [Header("Cookie")] string cookie);

    [Post("Quote")]
    Task<QuoteDtoRmsResponse> SaveSalesOrder(object dto, [Header("Cookie")] string cookie);
}

[Headers("Content-Type: application/json")]
internal interface IQuoteSlsWebService
{
    [Get("Quote")]
    Task<QuoteDtoRmsResponse> GetQuoteById(object key, [Header("Cookie")] string cookie);

    [Post("Quote")]
    Task<QuoteDtoRmsResponse> SaveQuote(object dto, [Header("Cookie")] string cookie);
}
