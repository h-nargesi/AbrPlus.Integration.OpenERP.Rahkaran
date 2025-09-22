using System;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service;

public interface IRahkaranCurrentUserService
{
    public IDisposable GetSessionId();
}
