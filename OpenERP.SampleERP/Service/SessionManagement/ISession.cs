using System;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.SessionManagement;

public interface ISession : IDisposable
{
    IToken Token { get; }
    
    Task TryCall(Func<IToken, Task> action);
}