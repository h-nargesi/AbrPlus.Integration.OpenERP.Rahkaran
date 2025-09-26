using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.Enums;
using AbrPlus.Integration.OpenERP.SampleERP.Dtos;
using System.Collections.Generic;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Customer;

internal static class CustomerExtension
{
    public static IdentityBundle ToBundle(this IdentityDto dto)
    {
        var bundle = new IdentityBundle
        {
            EconomicCode = dto.EconomicCode,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Id = dto.ID.ToString(),
            IdentityType = dto.Type == 1 ? IdentityType.Corporate : IdentityType.Person,
            NationalCode = dto.NationalID,
            NickName = dto.Alias,
            OrganizationName = dto.CompanyName,
        };

        if (dto.Gender.HasValue)
        {
            bundle.Gender = dto.Gender == 1 ? "M" : "F";
        }

        if (dto.PartyAddresses == null) return bundle;

        var addresses = new List<ContactAddress>();
        var phones = new List<ContactPhone>();
        foreach (var contacts in dto.PartyAddresses)
        {
            if (!string.IsNullOrEmpty(contacts.Details))
                addresses.Add(new ContactAddress
                {
                    Address = contacts.Details,
                    IsDefault = contacts.IsMainAddress,
                    Key = contacts.ID.ToString(),
                    ZipCode = contacts.ZipCode,
                });

            if (!string.IsNullOrEmpty(contacts.Phone))
                phones.Add(new ContactPhone
                {
                    IsDefault = contacts.IsMainAddress,
                    Key = contacts.ID.ToString(),
                    Number = contacts.Phone,
                });

            if (!string.IsNullOrEmpty(contacts.WebPage))
            {
                if (contacts.IsMainAddress || string.IsNullOrEmpty(bundle.Website))
                    bundle.Website = contacts.WebPage;
            }

            if (!string.IsNullOrEmpty(contacts.Email))
            {
                if (contacts.IsMainAddress || string.IsNullOrEmpty(bundle.Email))
                    bundle.Email = contacts.Email;
            }
        }

        bundle.Addresses = [.. addresses];
        bundle.Phones = [.. phones];

        return bundle;
    }

    public static IdentityDto ToDto(this IdentityBundle bundle)
    {
        if (!long.TryParse(bundle.Id, out var id))
        {
            id = 0;
        }

        var dto = new IdentityDto
        {
            EconomicCode = bundle.EconomicCode,
            FirstName = bundle.FirstName,
            LastName = bundle.LastName,
            ID = id,
            Type = bundle.IdentityType == IdentityType.Corporate ? 1 : 0,
            NationalID = bundle.NationalCode,
            Alias = bundle.NickName,
            CompanyName = bundle.OrganizationName,
        };

        if (!string.IsNullOrEmpty(bundle.Gender))
        {
            dto.Gender = bundle.Gender == "M" ? 1 : 2;
        }
        else dto.Gender = null;

        PartyAddressDataDto main = null;
        var contacts = new Dictionary<long, PartyAddressDataDto>();
        var idGen = 0;

        foreach (var address in bundle.Addresses)
        {
            if (!long.TryParse(address.Key, out var cid))
            {
                cid = --idGen;
            }

            if (!contacts.TryGetValue(cid, out var contact))
            {
                if (address.IsDefault)
                {
                    contact = main ??= new PartyAddressDataDto
                    {
                        IsMainAddress = true,
                        ID = cid,
                    };
                }
                else
                {
                    contact = new PartyAddressDataDto()
                    {
                        ID = cid,
                    };
                }

                contacts.Add(cid, contact);
            }

            contact.Details = address.Address;
            contact.ID = cid;
            contact.ZipCode = address.ZipCode;
        }

        foreach (var phone in bundle.Phones)
        {
            if (!long.TryParse(phone.Key, out var cid))
            {
                cid = --idGen;
            }

            if (!contacts.TryGetValue(cid, out var contact))
            {
                if (phone.IsDefault)
                {
                    contact = main ??= new PartyAddressDataDto
                    {
                        IsMainAddress = true,
                        ID = cid,
                    };
                }
                else
                {
                    contact = new PartyAddressDataDto()
                    {
                        ID = cid,
                    };
                }

                contacts.Add(cid, contact);
            }

            contact.Phone = phone.Number;
        }

        if (!string.IsNullOrEmpty(bundle.Website))
        {
            if (main != null)
            {
                main.WebPage = bundle.Website;
            }
            else
            {
                contacts.Add(--idGen, main = new PartyAddressDataDto
                {
                    WebPage = bundle.Website,
                    IsMainAddress = true,
                });
            }
        }

        if (!string.IsNullOrEmpty(bundle.Email))
        {
            if (main != null)
            {
                main.Email = bundle.Email;
            }
            else
            {
                contacts.Add(--idGen, new PartyAddressDataDto
                {
                    Email = bundle.Email,
                    IsMainAddress = true,
                });
            }
        }

        return dto;
    }
}
