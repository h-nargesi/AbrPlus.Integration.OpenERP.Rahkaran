using System;
using System.Collections.Generic;

namespace AbrPlus.Integration.OpenERP.SampleERP.Dtos;

public class InvoiceDto
{
    public DateTime DateTime { get; set; }
    public long CustomerId { get; set; }
    public long CurrencyId { get; set; }
    public long SettlementPolicyId { get; set; }
    public long StoreId { get; set; }
    public long DocumentPatternId { get; set; }
    public long? SalesAreaId { get; set; }
    public decimal Price { get; set; }
    public decimal NetPrice { get; set; }
    public decimal? CashierDiscount { get; set; }
    public List<ESalesDocumentItemDto> Items { get; set; }
    public List<ESalesPolicyResultDto> Policies { get; set; }
    public string Description { get; set; }
    public long? CustomerAddressId { get; set; }
    public long? ReturnReasonId { get; set; }
    public long? SalesAgentId { get; set; }
    public long? LoyaltyProgramId { get; set; }
}

public class ESalesDocumentItemDto
{
    public long ProductId { get; set; }
    public string ProductTitle { get; set; }
    public long UnitId { get; set; }
    public decimal Quantity { get; set; }
    public long StoreId { get; set; }
    public decimal Fee { get; set; }
    public decimal? ConsumerFee { get; set; }
    public decimal Price { get; set; }
    public decimal NetPrice { get; set; }
    public decimal? CashierDiscount { get; set; }
    public string TrackingFactorValue { get; set; }
    public string TrackingFactor1 { get; set; }
    public string TrackingFactor2 { get; set; }
    public string TrackingFactor3 { get; set; }
    public string TrackingFactor4 { get; set; }
    public string TrackingFactor5 { get; set; }
    public long? PartTrackingFactorId1 { get; set; }
    public long? PartTrackingFactorId2 { get; set; }
    public long? PartTrackingFactorId3 { get; set; }
    public long? PartTrackingFactorId4 { get; set; }
    public long? PartTrackingFactorId5 { get; set; }
    public bool? IsTrackingFactorInputModeManual1 { get; set; }
    public bool? IsTrackingFactorInputModeManual2 { get; set; }
    public bool? IsTrackingFactorInputModeManual3 { get; set; }
    public bool? IsTrackingFactorInputModeManual4 { get; set; }
    public bool? IsTrackingFactorInputModeManual5 { get; set; }
    public bool? TrackingFactorHasQuantity1 { get; set; }
    public bool? TrackingFactorHasQuantity2 { get; set; }
    public bool? TrackingFactorHasQuantity3 { get; set; }
    public bool? TrackingFactorHasQuantity4 { get; set; }
    public bool? TrackingFactorHasQuantity5 { get; set; }
    public string Description { get; set; }
    public long? CustomerAddressId { get; set; }
    public long? IderenceId { get; set; }
    public int IderenceType { get; set; }
}

public class ESalesPolicyResultDto
{
    public long PolicyId { get; set; }
    public string Title { get; set; }
    public decimal Price { get; set; }
    public string Effect { get; set; }
    public string LookupValue { get; set; }
    public bool? BooleanValue { get; set; }
    public string StringValue { get; set; }
    public string DateTimeValueDateString { get; set; }
}
