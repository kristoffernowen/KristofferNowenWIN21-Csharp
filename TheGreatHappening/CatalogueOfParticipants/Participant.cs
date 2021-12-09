using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogueOfParticipants
{
    internal class Participant
    {
        Random random = new Random();

        public Participant()
        {

            DiscountCode = $"TGH{DateTime.Now}{random.Next(0, 100)}";
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string SpecialRequirements { get; set; }
        public string DiscountCode { get; set; }
        public string FullName => $"{FirstName} {LastName }";

        
    }
}
