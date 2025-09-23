using System;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service;

public interface IRahkaranSessionService
{
    public IDisposable GetSession();
}
