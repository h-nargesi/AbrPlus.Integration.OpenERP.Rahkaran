namespace AbrPlus.Integration.OpenERP.SampleERP.Options;

public class RahkaranUrlInfo
{
    public string ConnectionString { get; set; }

    public string BaseUrl { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public BasePathOptions BasePath { get; set; }

    public class BasePathOptions
    {
        public string Authentication {  get; set; }
    }
}
