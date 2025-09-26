using System.Collections.Generic;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Customer;

internal class PartyDataSaveResult
{
    public long ID { get; set; }
    public string Title { get; set; }
    public Dictionary<string, string> ValidationErrors { get; set; }
}
