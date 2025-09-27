using AbrPlus.Integration.OpenERP.SampleERP.Dtos;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Payment;

public class PaymentSaveDocument
{
    public PaymentDto Data { get; set; }
}

public class GetPaymentResponse
{
    public PaymentDto Data { get; set; }
}

public class SavePaymentResponse
{
    public PaymentDto Data { get; set; }
}