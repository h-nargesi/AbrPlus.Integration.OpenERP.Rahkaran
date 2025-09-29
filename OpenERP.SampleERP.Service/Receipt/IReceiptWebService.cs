using Refit;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Receipt;

[Headers("Content-Type: application/json")]
public interface IReceiptWebService
{
    public const string BasePath = "/ReceiptAndPayment/ReceiptManagement/Services/ReceiptManagementService.svc";

    [Post("/RegisterReceipt")]
    Task<SaveReceiptResult> RegisterReceipt(object dto, [Header("Cookie")] string cookie);
}
