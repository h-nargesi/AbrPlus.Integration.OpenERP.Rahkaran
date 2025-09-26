using AbrPlus.Integration.OpenERP.Settings;

namespace AbrPlus.Integration.OpenERP.SampleERP.Settings
{
    public class RahkaranErpCompanyConfig : BaseFlatCompanyConfig
    {
        public string BaseUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int IdleTimeout { get; set; }
    }
}
