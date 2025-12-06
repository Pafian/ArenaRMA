namespace ArenaRMA.Models
{
    public class Email
    {
        public int Id { get; set; }
        public string Sender { get; set; }
        public DateTime Date { get; set; }
        public string MessageId { get; set; }
        public int StatusID { get; set; }
    }
}
