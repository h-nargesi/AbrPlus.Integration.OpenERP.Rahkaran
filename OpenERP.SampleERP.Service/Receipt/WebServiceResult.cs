namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Receipt;

public class SaveReceiptResult
{
    public long ID { get; set; }
    public string Number { get; set; }
    public string[] ValidationErrors { get; set; }
}
