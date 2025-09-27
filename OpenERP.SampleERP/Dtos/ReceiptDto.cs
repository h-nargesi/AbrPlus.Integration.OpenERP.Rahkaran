using System;

namespace AbrPlus.Integration.OpenERP.SampleERP.Dtos;

public class ReceiptDto
{
    public long BranchID { get; set; }
    public string Number { get; set; }
    public string SecondNumber { get; set; }
    public DateTime Date { get; set; }
    public long CashID { get; set; }
    public string CounterPartDLCode { get; set; }
    public string Description { get; set; }
    public string Description_En { get; set; }
    public ReceiptCashMoneyData[] ReceiptCashMoneys { get; set; }
    public ReceiptDepositData[] ReceiptDeposits { get; set; }
    public ReceiptReceivableChequeData[] ReceiptReceivableCheques { get; set; }
    public bool IsApproved { get; set; }
}

public class ReceiptCashMoneyData
{
    public string CounterPartDLCode { get; set; }
    public long AccountingOperationID { get; set; }
    public long CashFlowFactorID { get; set; }
    public string CurrencyAbbreviation { get; set; }
    public decimal Amount { get; set; }
    public decimal OperationalCurrencyExchangeRate { get; set; }
    public string BaseCurrencyAbbreviation { get; set; }
    public decimal BaseCurrencyExchangeRate { get; set; }
    public string Description { get; set; }
    public string Description_En { get; set; }
}

public class ReceiptDepositData
{
    public string Number { get; set; }
    public DateTime Date { get; set; }
    public long BankAccountID { get; set; }
    public string CounterPartDLCode { get; set; }
    public long AccountingOperationID { get; set; }
    public long CashFlowFactorID { get; set; }
    public decimal Amount { get; set; }
    public decimal OperationalCurrencyExchangeRate { get; set; }
    public string BaseCurrencyAbbreviation { get; set; }
    public decimal BaseCurrencyExchangeRate { get; set; }
    public string Description { get; set; }
    public string Description_En { get; set; }
}

public class ReceiptReceivableChequeData
{
    public string CounterPartDLCode { get; set; }
    public long BankID { get; set; }
    public string BankBranchName { get; set; }
    public string BankBranchCode { get; set; }
    public string AccountNumber { get; set; }
    public string SerialNumber { get; set; }
    public string SayadNumber { get; set; }
    public DateTime DueDate { get; set; }
    public DateTime AgreementDate { get; set; }
    public long AccountingOperationID { get; set; }
    public long CashFlowFactorID { get; set; }
    public string CurrencyAbbreviation { get; set; }
    public decimal Amount { get; set; }
    public decimal OperationalCurrencyExchangeRate { get; set; }
    public string BaseCurrencyAbbreviation { get; set; }
    public decimal BaseCurrencyExchangeRate { get; set; }
    public string Description { get; set; }
    public string Description_En { get; set; }
    public long CityID { get; set; }
    public string InternationalNumber { get; set; }
}
