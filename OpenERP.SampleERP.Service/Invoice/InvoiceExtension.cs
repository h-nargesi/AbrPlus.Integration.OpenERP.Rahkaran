using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.Enums;
using AbrPlus.Integration.OpenERP.SampleERP.Dtos;
using System;

namespace AbrPlus.Integration.OpenERP.SampleERP.Service.Invoice;

internal static class InvoiceExtension
{
    public static InvoiceBundle ToBundle(this InvoiceRmsDto dto, IdentityBundle identity)
    {
        var bundle = new InvoiceBundle
        {
            Customer = identity,
            Description = dto.Description,
            Discount = dto.CashierDiscount ?? 0,
            FinalValue = dto.NetPrice,
            InvoiceDate = dto.DateTime,
            InvoiceType = InvoiceType.Invoice,
            TotalValue = dto.Price,
        };

        if (dto.Items != null)
        {
            var i = 0;
            bundle.Details = new InvoiceDetail[dto.Items.Count];
            foreach (var item in dto.Items)
            {
                bundle.Details[i++] = new InvoiceDetail
                {
                    BaseUnitPrice = item.Fee,
                    Count = item.Quantity,
                    DetailDescription = item.Description,
                    FinalUnitPrice = item.NetPrice,
                    ProductName = item.ProductTitle,
                    ProductKey = item.ProductId.ToString(),
                    TotalDiscount = item.CashierDiscount ?? 0,
                    TotalUnitPrice = item.Price,
                };
            }
        }

        return bundle;
    }

    public static InvoiceRmsDto ToDto(this InvoiceBundle bundle)
    {
        if (!long.TryParse(bundle.Customer?.Id, out var customerRef))
        {
            // TODO use custom exception
            throw new Exception("Unkown Customer-Id");
        }

        if (!bundle.InvoiceDate.HasValue)
        {
            // TODO use custom exception
            throw new Exception("Unkown InvoiceDate");
        }

        if (bundle.InvoiceType != InvoiceType.Invoice)
        {
            // TODO use custom exception
            throw new Exception("Invalid InvoiceType");
        }

        var dto = new InvoiceRmsDto
        {
            CustomerId = customerRef,
            Description = bundle.Description,
            CashierDiscount = bundle.Discount == 0 ? null : bundle.Discount,
            NetPrice = bundle.FinalValue,
            DateTime = bundle.InvoiceDate.Value,
            Price = bundle.TotalValue,
            CurrencyId = 1
        };

        if (bundle.Details != null)
        {
            foreach (var item in bundle.Details)
            {
                if (!long.TryParse(item.ProductKey, out var pid))
                {
                    // TODO use custom exception
                    throw new Exception("Invalid product-key");
                }

                dto.Items.Add(new ESalesDocumentItemDto
                {
                    ProductTitle = item.ProductName,
                    ProductId = pid,
                    Fee = item.BaseUnitPrice,
                    Quantity = item.Count,
                    Description = item.DetailDescription,
                    NetPrice = item.FinalUnitPrice,
                    CashierDiscount = item.TotalDiscount == 0 ? null : item.TotalDiscount,
                    Price = item.TotalUnitPrice,
                });
            }
        }

        return dto;
    }
}
