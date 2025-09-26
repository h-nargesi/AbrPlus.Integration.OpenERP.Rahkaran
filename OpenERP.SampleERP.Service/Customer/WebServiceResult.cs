using AbrPlus.Integration.OpenERP.SampleERP.Dtos;
using System.Collections.Generic;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Customer;

public class PartyDataSaveResult
{
    public long ID { get; set; }
    public string Title { get; set; }
    public string[] ValidationErrors { get; set; }
}

public class PartyByRefDataResult
{
    public IdentityDto GetPartyResult { get; set; }
}

public class FetchPartyDataResult
{
    public IdentityDto FetchPartyResult { get; set; }
}
