﻿using AbrPlus.Integration.OpenERP.Api.Contracts;
using AbrPlus.Integration.OpenERP.Api.DataContracts;
using AbrPlus.Integration.OpenERP.SampleERP.Service;
using SeptaKit.DI;

namespace AbrPlus.Integration.OpenERP.SampleERP.Api.Api
{
    public class InvoiceApi(IInvoiceService invoiceService, IRahkaranSessionService sessionService) : IInvoiceApi, IApi
    {
        private readonly IInvoiceService _invoiceService = invoiceService;
        private readonly IRahkaranSessionService _sessionService = sessionService;

        public string[] GetAllIds(int? companyId)
        {
            using var session = _sessionService.GetSession();
            return _invoiceService.GetAllIds();
        }
        public InvoiceBundle GetBundle(string key, int? companyId)
        {
            return _invoiceService.GetBundle(key);
        }
        public ChangeInfo GetChanges(string lastTrackedVersion, int? companyId)
        {
            return _invoiceService.GetChanges(lastTrackedVersion);
        }
        public bool Save(InvoiceBundle item, int? companyId)
        {
            return _invoiceService.Save(item);
        }
        public void SetTrackingStatus(bool enabled, int? companyId)
        {
            _invoiceService.SetTrackingStatus(enabled);
        }
        public bool SyncWithCrmObjectTypeCode(int companyId)
        {
            return _invoiceService.SyncWithCrmObjectTypeCode();
        }
        public bool Validate(InvoiceBundle item)
        {
            return _invoiceService.Validate(item);
        }
    }
}
