using AbrPlus.Integration.OpenERP.SampleERP.Dtos;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Customer;

public class PartyDataSaveResult
{
    public long ID { get; set; }
    public string Title { get; set; }
    public string[] ValidationErrors { get; set; }
}

public class PartyByRefResponse
{
    public IdentityDto GetPartyResult { get; set; }
}

public class FetchPartyResponse
{
    public IdentityDto FetchPartyResult { get; set; }
}
