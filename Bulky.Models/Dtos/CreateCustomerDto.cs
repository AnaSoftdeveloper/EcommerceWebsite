namespace Bulky.Models.Dtos
{
    public class CreateCustomerDto
    {
        public string customerName { get; set; } = string.Empty;
        public string customerEmail { get; set; }
        public string password { get; set; }
        public int customerPhone { get; set; }
        public string pronoun { get; set; }
        public string ageRange { get; set; }
        public string province { get; set; }
    }
}
