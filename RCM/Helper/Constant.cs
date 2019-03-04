namespace RCM.Helper
{
    public static class Constant
    {
        public const string SCHEDULER_CRON = "0 0/5 7-22 * * ?";

        public const string CONTACT_DEBTOR = "Debtor";
        public const int CONTACT_DEBTOR_CODE = 0;

        public const string CONTACT_RELATION = "Relation";
        public const int CONTACT_RELATION_CODE = 1;

        public const string ACTION_SMS = "SMS";
        public const int ACTION_SMS_CODE = 0;

        public const string ACTION_PHONECALL = "PhoneCall";
        public const int ACTION_PHONECALL_CODE = 1;

        public const string ACTION_NOTIFICATION = "Notification";
        public const int ACTION_NOTIFICATION_CODE = 2;

        public const string ACTION_VISIT = "Notification";
        public const int ACTION_VISIT_CODE = 4;

        public const string ACTION_REPORT = "Report";
        public const int ACTION_REPORT_CODE = 3;

        //Action is executed but response result is bad.
        public const string COLLECTION_STATUS_CANCEL = "Cancel";
        public const int COLLECTION_STATUS_CANCEL_CODE = 0;

        //Action is in order to be executed.
        //Stage is in order to be executed.
        //Collection Progress is in order to be done.
        public const string COLLECTION_STATUS_COLLECTION = "Collection";
        public const int COLLECTION_STATUS_COLLECTION_CODE = 1;

        //Action is executed.
        //Stage's actions are all executed.
        //All stages of Collection Progress are executed.
        public const string COLLECTION_STATUS_DONE = "Done";
        public const int COLLECTION_STATUS_DONE_CODE = 2;

        //Action is not executed on time.
        public const string COLLECTION_STATUS_LATE = "Late";
        public const int COLLECTION_STATUS_LATE_CODE = 3;

        //Collection progress is still pending for assgined.
        public const string COLLECTION_STATUS_WAIT = "Wait";
        public const int COLLECTION_STATUS_WAIT_CODE = 4;

        //Collection progress is closed, debt has been recovered.
        public const string COLLECTION_STATUS_CLOSED = "Closed";
        public const int COLLECTION_STATUS_CLOSED_CODE = 5;

        public const string ASSIGNED_STATUS_ACTIVE = "Active";
        public const int ASSIGNED_STATUS_ACTIVE_CODE = 1;

        public const string ASSIGNED_STATUS_DEACTIVE = "Deactive";
        public const int ASSIGNED_STATUS_DEACTIVE_CODE = 0;

        public const string MESSAGE_PARAMETER_NAME = "[NAME]";
        public const string MESSAGE_PARAMETER_DEBTAMOUNT = "[AMOUNT]";

        public const string MESSAGE_TYPE_SMS = "SMS";
        public const string MESSAGE_TYPE_SMS_CODE = "0";

        public const string MESSAGE_TYPE_CALL = "CALL";
        public const string MESSAGE_TYPE_CALL_CODE = "1";

        public const string DATE_FORMAT = "yyyyMMdd";

        public const string TIME_FORMAT = "HHmm";
    }

}
