﻿using System;
using System.Collections.Generic;

namespace RCM.ViewModels
{

    public class ReceivableVM : ReceivableCM
    {
        public int Id { get; set; }
        public int? ClosedDay { get; set; }
        public int? PayableDay { get; set; }
        public bool IsConfirmed { get; set; }
        public IEnumerable<ContactVM> Contacts { get; set; }
    }

    public class ReceivableDM : ReceivableVM
    {
        public CollectionProgressDM CollectionProgress { get; set; }

        public AssignedCollectorVM assignedCollector { get; set; }
    }
    public class ReceivableCM
    {
        public long PrepaidAmount { get; set; }
        public long DebtAmount { get; set; }
        public int CustomerId { get; set; }
        public int? LocationId { get; set; }
    }
    public class ReceivableUM : ReceivableCM
    {
        public int Id { get; set; }
    }
    //Import succes model
    public class ReceivableISM : ReceivableCM
    {
        public int Id{ get; set; }
        public IEnumerable<ContactIM> Contacts { get; set; }
        public int? PayableDay { get; set; }
        public int ProfileId { get; set; }
        public string CollectorId { get; set; }
    }
    public class ReceivableIM : ReceivableCM
    {
        public IEnumerable<ContactIM> Contacts { get; set; }
        public int? PayableDay { get; set; }
        public int ProfileId { get; set; }
        public string CollectorId { get; set; }
    }

    public class ReceivableLM : ReceivableCM
    {
        public int Id { get; set; }
        public int CollectionProgressStatus { get; set; }
        public int CollectionProgressId { get; set; }
        public int? PayableDay { get; set; }
        public int? ClosedDay { get; set; }
        public string AssignedCollectorId { get; set; }
        public string CustomerName { get; set; }
        public string DebtorName { get; set; }
        public int DebtorId { get; set; }
        public int ProgressPercent { get; set; }
        public bool HaveLateAction { get; set; }
        public bool IsConfirmed { get; set; }
        public string Stage { get; set; }
        public string Action { get; set; }

    }

    public class ReceivableMobileLM
    {
        public int Id { get; set; }
        public long PrepaidAmount { get; set; }
        public long DebtAmount { get; set; }
        public int? LocationId { get; set; }
        public int CollectionProgressStatus { get; set; }
        public int? PayableDay { get; set; }
        public int ExpectationClosedDay { get; set; }
        public int? ClosedDay { get; set; }
        public double TimeRate { get; set; }
        public string CustomerName { get; set; }
        public string DebtorName { get; set; }
        public int DebtorId { get; set; }
        public bool IsConfirmed { get; set; }
        public IEnumerable<ContactVM> Contacts { get; set; }
        public int AssignDate { get; set; }
        public string Stage { get; set; }
        public LastAction Action { get; set; }
        
    }
    public class LastAction
    {
        public string Name { get; set; }
        public DateTime Time { get; set; } 
    }
    public class ReceivableCloseModel
    {
        public int Id { get; set; }
        public bool isPayed { get; set; }
    }

    public class ReceivableOpenModel
    {
        public int Id { get; set; }
    }

    public class ReceivableAssignModel
    {
        public int Id { get; set; }
        public string CollectorId { get; set; }
        public int PayableDay { get; set; }

    }
}
