using System;
using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.Enums;

namespace AbrPlus.Integration.OpenERP.SampleERP.Dtos;

public class InvoiceDto
{
    public string Id { get; set; }

    public string Subject { get; set; }

    public string Description { get; set; }

    public IdentityBundle Customer { get; set; }

    public string Number { get; set; }

    public decimal Discount { get; set; }

    public DateTime? ExpireDate { get; set; }

    public DateTime? InvoiceDate { get; set; }

    public InvoiceType InvoiceType { get; set; }

    public string PriceListName { get; set; }

    public decimal Tax { get; set; }

    public decimal Toll { get; set; }

    public int TaxPercent { get; set; }

    public int TollPercent { get; set; }

    public decimal TotalValue { get; set; }

    public InvoiceDetail[] Details { get; set; }

    public PaymentBundle[] Payments { get; set; }

    public decimal FinalValue { get; set; }

    public string SubInvoiceId { get; set; }

    public string SubInvoiceType { get; set; }

    public decimal? AdditionalCosts { get; set; }

    public string CrmObjectTypeCode { get; set; }

    public string Creator { get; set; }
}