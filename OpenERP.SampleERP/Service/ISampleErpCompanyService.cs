using AbrPlus.Integration.OpenERP.SampleERP.Enums;
using AbrPlus.Integration.OpenERP.SampleERP.Settings;
using AbrPlus.Integration.OpenERP.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service
{
    public interface ISampleErpCompanyService : ICompanyService<SampleErpVersion, SampleErpCompanyConfig>
    {
    }
}
