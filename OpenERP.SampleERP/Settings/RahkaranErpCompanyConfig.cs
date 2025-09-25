using AbrPlus.Integration.OpenERP.Settings;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AbrPlus.Integration.OpenERP.SampleERP.Settings
{
    public class RahkaranErpCompanyConfig : BaseFlatCompanyConfig
    {
        public string RahkaranWebServiceUrl { get; set; }
        public string RahkaranUsername { get; set; }
        public string RahkaranPassword { get; set; }
    }
}
