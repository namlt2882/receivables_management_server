namespace RCM.ViewModels
{
    public class ContactVM : ContactCM
    {
        public int Id { get; set; }
    }

    public class ContactCM
    {
        public int Type { get; set; }
        public string IdNo { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    public class ContactIM : ContactCM
    {
        public int? ReceivableId { get; set; }
    }
}
