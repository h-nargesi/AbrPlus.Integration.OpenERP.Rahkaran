using AbrPlus.Integration.OpenERP.SampleERP.Dtos;
using Refit;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Customer;

[Headers("Content-Type: application/json")]
internal interface IPartyWebService
{
    [Post("/FetchParty")]
    Task<IdentityDto> FetchParty(object key, [Header("Cookie")] string cookie);

    [Post("/GenerateParty")]
    Task<PartyDataSaveResult> GenerateParty(IdentityDto dto, [Header("Cookie")] string cookie);

    [Post("/EditParty")]
    Task<PartyDataSaveResult> EditParty(IdentityDto dto, [Header("Cookie")] string cookie);
}
