using System.Threading.Tasks;
using Refit;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Payment;

[Headers("Content-Type: application/json")]
public interface IPaymentWebService
{
    [Get("Payment")]
    Task<GetPaymentResponse> GetPaymentById(object key, [Header("Cookie")] string cookie);

    [Post("Payment")]
    Task<SavePaymentResponse> SavePayment(object dto, [Header("Cookie")] string cookie);
}