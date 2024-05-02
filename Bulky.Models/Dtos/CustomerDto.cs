using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models.Dtos
{
    public class CustomerDto
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
