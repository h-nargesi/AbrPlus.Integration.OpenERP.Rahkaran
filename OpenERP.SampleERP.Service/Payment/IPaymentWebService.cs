using System.Threading.Tasks;
using Refit;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Payment;

[Headers("Content-Type: application/json")]
public interface IPaymentWebService
{
    public const string BasePath = "/ReceiptAndPayment/PaymentManagement/Services/PaymentManagementService.svc";

    [Post("/RegisterPayment")]
    Task<SavePaymentResult> RegisterPayment(object dto, [Header("Cookie")] string cookie);
}
