using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheConference_UWP.Services
{
    /// <summary>
    /// This is a participant of the conference and methods to handle participant data
    /// </summary>
    public class UwpParticipant 
    {
        /// <summary>
        /// Create a new participant of the conference
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="specialRequirements"></param>
        
        public UwpParticipant(string firstName, string lastName, string email, string specialRequirements)
        {
            Random random = new Random();
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
        
        public string FullName => $"{FirstName} {LastName }";
        public string DiscountCode { get; set; }


        /// <summary>
        /// Save a participant to the list of participants
        /// </summary>
        /// <param name="list"></param>
        /// <param name="participant"></param>
        /// <returns> updated list of participants</returns>

        public static List<UwpParticipant> SaveParticipantToList(List<UwpParticipant> list, UwpParticipant participant)
        {
            list.Add(participant);

            return list;
        }


        /// <summary>
        /// Deletes a participant from the list of participants
        /// </summary>
        /// <param name="list"></param>
        /// <param name="participantToRemove"></param>
        /// <returns>Updated list of participants</returns>
        /// 
        public static List<UwpParticipant> DeleteParticipantFromList(List<UwpParticipant> list, UwpParticipant participantToRemove)
        {
            var updatedList = list.Where(participant => participant != participantToRemove).ToList();

            return updatedList;
        }


        /// <summary>
        /// Save a list of participants to a location of your choosing
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>

        public static async Task SaveList(List<UwpParticipant> list)
        {
            // Choose location to save

            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
           
                savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });

                savePicker.SuggestedFileName = "The Great Happening";

                Windows.Storage.StorageFile pickedFile = await savePicker.PickSaveFileAsync();

            // Convert list to json

                string json = JsonConvert.SerializeObject(list);

            // save in chosen location
                
                if (pickedFile != null)
                await Windows.Storage.FileIO.WriteTextAsync(pickedFile, json);
        }


        /// <summary>
        /// Puts together a message with fullname and discount code
        /// </summary>
        /// <param name="participant"></param>
        /// <returns>the message</returns>
        public static string ChooseDiscountMessage(UwpParticipant participant)
        {
            var fullNameLastCharacter = participant.FullName[participant.FullName.Length - 1].ToString();
            string discountMessage;
            if (fullNameLastCharacter.Equals("s"))
                discountMessage = $"{participant.FullName} rabattkod är {participant.DiscountCode}.";

            else
                discountMessage = $"{participant.FullName}s rabattkod är {participant.DiscountCode}.";

            return discountMessage;
        }
    }
}
