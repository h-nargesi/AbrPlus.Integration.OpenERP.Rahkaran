using AbrPlus.Integration.OpenERP.Service.Configuration;
using System;
using System.ComponentModel.DataAnnotations;

namespace AbrPlus.Integration.OpenERP.SampleERP.Settings
{
    [Serializable]
    public class RahkaranErpSetting : IFinancialSystemSetting
    {
        [Display(GroupName = "وب سرور", Name = "مسیر", Description = "مسیر وب سرور راهکاران")]
        [UIHint("Text")]
        public string RahkaranWebServiceUrl { get; set; }

        [Display(GroupName = "وب سرور", Name = "کاربر", Description = "نام کاربر وب سرویس راهکاران")]
        [UIHint("Text")]
        public string RahkaranUsername { get; set; }

        [Display(GroupName = "وب سرور", Name = "کلمه عبور", Description = "کلمه عبور")]
        [UIHint("Text")]
        public string RahkaranPassword { get; set; }
    }
}
