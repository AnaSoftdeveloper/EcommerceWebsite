namespace Bulky.Models
{
    public class Customer
    {
        public int id { get; set; }
        public string customerName { get; set; } = string.Empty;
        public string customerEmail { get; set; }
        public string password { get; set; }
        public string ageRange { get; set; }
        public int customerPhone { get; set; }
        public string pronoun { get; set; }

        public string province { get; set; }
    }
}
