using System;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;

public interface IToken
{
    string SessionId { get; }
    
    string Cookie { get; }
    
    bool IsExpired { get; }
}
