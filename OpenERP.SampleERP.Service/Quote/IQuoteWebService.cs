using Refit;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Quote;

[Headers("Content-Type: application/json")]
internal interface IQuoteWebService
{
    [Get("Quote")]
    Task<GetQuotesResponse> GetQuoteById(object key, [Header("Cookie")] string cookie);

    [Post("Quote")]
    Task<SaveQuoteResponse> SaveQuote(QuoteSaveDocument dto, [Header("Cookie")] string cookie);
}
