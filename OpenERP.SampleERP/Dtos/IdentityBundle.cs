using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.Enums;
using System;

namespace AbrPlus.Integration.OpenERP.SampleERP.Dtos;

public class IdentityDto : BundleBase
{
    public string Id { get; set; }

    public IdentityType IdentityType { get; set; }

    public string BusinessType { get; set; }

    public decimal? Balance { get; set; }

    public ContactAddress[] Addresses { get; set; }

    public DateTime? CustomerDate { get; set; }

    public string CustomerNo { get; set; }

    public ContactPhone[] Phones { get; set; }

    public string Description { get; set; }

    public string Email { get; set; }

    public string Gender { get; set; }

    public string NationalCode { get; set; }

    public string EconomicCode { get; set; }

    public string NickName { get; set; }

    public string OrganizationName { get; set; }

    public DateTime? RegisterBirthDate { get; set; }

    public string RegisterNo { get; set; }

    public string TradeMark { get; set; }

    public string Website { get; set; }

    public string LastName { get; set; }

    public string FirstName { get; set; }

    public DateTime? BirthDate { get; set; }

    public string CategoryPathToRoot { get; set; }

    public string SubSystemKey { get; set; }
}
