using AbrPlus.Integration.OpenERP.SampleERP.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service
{
    public interface IStockService
    {
        List<StockQuantity> GetItemQuantityInAllStock(string itemCode);
    }
}
