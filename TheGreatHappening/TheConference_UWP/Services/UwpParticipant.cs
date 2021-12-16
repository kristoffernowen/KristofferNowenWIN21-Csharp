using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheConference_UWP.Services
{
    internal class UwpParticipant 
    {
        // Random random = new Random();


        // public UwpParticipant()
        //{
        //    var dateHere = DateTime.Now;
        //    DiscountCode = $"TGH{dateHere.Year}{random.Next(0, 100)}";
        //}
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string SpecialRequirements { get; set; }
        public string DiscountCode { get; set; }
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

        public static async Task SaveList(List<UwpParticipant> list)
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            
            savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
            
            savePicker.SuggestedFileName = "The Great Happening";

            Windows.Storage.StorageFile pickedFile = await savePicker.PickSaveFileAsync();

            string json = JsonConvert.SerializeObject(list);

            await Windows.Storage.FileIO.WriteTextAsync(pickedFile, json);

            // kan jag read pickedFile list och append to json/list och sedan writefile(appendend list) ?
        }

        public static string GenerateDiscountCode(UwpParticipant person)
        {
            // kan jag sätta in den i konstruktorns get eller set del (tror det var kört på set delen), med en if sats, om discountcode redan har ett värde skapa inget nytt    ?

            Random random = new Random();

            var dateHere = DateTime.Now;
            person.DiscountCode = $"TGH{dateHere.Year}{random.Next(0, 100)}";

            return person.DiscountCode;
        }

    }
}
