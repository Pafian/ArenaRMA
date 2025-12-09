public class Customer
{
    public int Id { get; set; }

    public string CustomerCode { get; set; }  // CÍMSZÁM
    public string CompanyName { get; set; }   // CÉGNÉV
    public string PhoneNumber { get; set; }   // TELEFONSZÁM
    public string Email { get; set; }         // E-MAIL

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
