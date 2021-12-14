using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheConference_UWP.Services
{
    internal class UwpParticipant
    {
        Random random = new Random();


        public UwpParticipant()
        {
            var dateHere = DateTime.Now;
            DiscountCode = $"TGH{dateHere.Year}{random.Next(0, 100)}";
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string SpecialRequirements { get; set; }
        public string DiscountCode { get; }
        public string FullName => $"{FirstName} {LastName }";

        public static UwpParticipant CreateParticipant(string firstName, string lastName, string email, string specialRequirements)
        {
            var newParticipant = new UwpParticipant();
            newParticipant.FirstName = firstName;
            newParticipant.LastName = lastName;
            newParticipant.Email = email;
            newParticipant.SpecialRequirements = specialRequirements;

            return newParticipant;
        }

        public static List<UwpParticipant> ShowList(List<UwpParticipant> list)
        {
            return list;

        }

        
    }
}
