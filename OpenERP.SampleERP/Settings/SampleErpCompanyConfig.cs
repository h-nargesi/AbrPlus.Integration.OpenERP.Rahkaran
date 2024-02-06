using AbrPlus.Integration.OpenERP.Settings;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AbrPlus.Integration.OpenERP.SampleERP.Settings
{
    public class SampleErpCompanyConfig : BaseFlatCompanyConfig
    {
        public bool CheckBoxSetting { get; set; }
        public string DropdownSetting { get; set; }
        public string StringSetting { get; set; }
    }
}
