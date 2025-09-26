namespace AbrPlus.Integration.OpenERP.SampleERP.Dtos;

public class IdentityDto
{
    public string Alias { get; set; }
    public string CompanyName { get; set; }
    public string Company_EN { get; set; }
    public string EconomicCode { get; set; }
    public string FirstName { get; set; }
    public string FirstName_EN { get; set; }
    public int Gender { get; set; }
    public long ID { get; set; }
    public string LastName { get; set; }
    public string LastName_EN { get; set; }
    public string NationalID { get; set; }
    public PartyAddressDataDto[] PartyAddresses { get; set; }
    public int Type { get; set; }
}

public class PartyAddressDataDto
{
    public string Details { get; set; }
    public string Details_En { get; set; }
    public string Email { get; set; }
    public string Fax { get; set; }
    public long ID { get; set; }
    public bool IsMainAddress { get; set; }
    public string Name { get; set; }
    public string Phone { get; set; }
    public long RegionalDivisionRef { get; set; }
    public string WebPage { get; set; }
    public string ZipCode { get; set; }
}