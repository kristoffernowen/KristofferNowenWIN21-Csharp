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

        

         Random random = new Random();
        

        //public UwpParticipant()
        //{
        //    var dateHere = DateTime.Now;
        //    OtherCode = $"TGH{dateHere.Year}{random.Next(0, 100)}";
        //}

        public UwpParticipant(string firstName, string lastName, string email, string specialRequirements)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            SpecialRequirements = specialRequirements;
            var dateHere = DateTime.Now;
            DiscountCode = $"TGH{dateHere.Year}{random.Next(0, 100)}";
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
        public string SpecialRequirements { get; set; }
        // public string DiscountCode => OtherCode;
        public string FullName => $"{FirstName} {LastName }";
        public string DiscountCode { get; set; }

        
        

       

        //public static UwpParticipant CreateParticipant(string firstName, string lastName, string email, string specialRequirements)
        //{
        //    var newParticipant = new UwpParticipant();
        //    newParticipant.FirstName = firstName;
        //    newParticipant.LastName = lastName;
        //    newParticipant.Email = email;
        //    newParticipant.SpecialRequirements = specialRequirements;
           

        //    return newParticipant;
        //}

        public static async Task SaveList(List<UwpParticipant> list)
        {
            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
           
                savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });

                savePicker.SuggestedFileName = "The Great Happening";


                Windows.Storage.StorageFile pickedFile = await savePicker.PickSaveFileAsync();

                string json = JsonConvert.SerializeObject(list);
                
                if (pickedFile != null)
                await Windows.Storage.FileIO.WriteTextAsync(pickedFile, json);
            
               

            // kan jag read pickedFile list och append to json/list och sedan writefile(appendend list) ?
        }

        

    }
}
