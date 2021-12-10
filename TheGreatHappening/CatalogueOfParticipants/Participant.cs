
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CatalogueOfParticipants
{
    public class Participant
    {
        Random random = new Random();
        
        
        public Participant()
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


        public List<Participant> CreateParticipant(List<Participant> list)
        {
            var participant = new Participant();
            Console.WriteLine("Skriv förnamn på deltagaren:");
            participant.FirstName = Console.ReadLine();
            Console.WriteLine("Skriv efternamn på deltagaren:");
            participant.LastName = Console.ReadLine();
            Console.WriteLine("Skriv e-postadress till deltagaren:");
            participant.Email = Console.ReadLine();
            Console.WriteLine("Önskas specialkost? Skriv här:");
            participant.SpecialRequirements = Console.ReadLine();

            if (!String.IsNullOrEmpty(participant.FirstName) && !String.IsNullOrEmpty(participant.LastName)  
                && !String.IsNullOrEmpty(participant.Email) && !String.IsNullOrEmpty(participant.SpecialRequirements))
            {
                list.Add(participant);
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("Du måste fylla i alla fält. Deltagaren sparades inte.");
                Console.WriteLine("");
                Console.WriteLine("");
            }

            return list;
        }

        public void ShowList(List<Participant> list)
        {
            if (list.Count > 0)
            {
                foreach (var participant in list)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Deltagare nummer {list.IndexOf(participant) + 1}");
                    Console.WriteLine($"Namn: {participant.FullName}");
                    Console.WriteLine($"Epostadress: {participant.Email}");
                }
            }
            else if (list.Count == 0)
            {
                Console.WriteLine("");
                Console.WriteLine("Listan är tom");
            }
        }

        public List<Participant> RemoveParticipantFromList(List<Participant> list)
        {
            Console.WriteLine("Ta bort deltagare. Välj nummer från listan.");
            foreach (var participant in list)
            {
                Console.WriteLine($"Deltagare nummer {list.IndexOf(participant) + 1}");
                Console.WriteLine($"Namn: {participant.FullName}");
                Console.WriteLine($"Epostadress: {participant.Email}");
            }
            Console.WriteLine("");
            Console.WriteLine("Skriv nummer på deltagare att ta bort:");
            list.Remove(list[Convert.ToByte(Console.ReadLine()) - 1]);

            return list;
        }

        public void GenerateDiscountCode(List<Participant> list)
        {

            Console.WriteLine("Välj deltagare att generera rabattkod till.");
            Console.WriteLine("");
            foreach (var participant in list)
            {
                Console.WriteLine($"Deltagare: {list.IndexOf(participant) + 1}");
                Console.WriteLine($"{participant.FullName}");
            }
            var a = Convert.ToByte(Console.ReadLine());
            Console.WriteLine($"Rabattkod: {list[a - 1].DiscountCode}");
        }

        public void WriteToTextFile(List<Participant> list)
        {

            string path = @"C:\TheGreatHappening\TGH.txt";
            var lines = new List<string>();
            foreach (var participant in list)
            {
                lines.Add($"Förnamn: {participant.FirstName}");
                lines.Add(participant.LastName);
                lines.Add(participant.Email);
                lines.Add(participant.SpecialRequirements);
            }
            File.WriteAllLines(path, lines);
        }

        public string WriteToTextFileAsJson(List<Participant> list)
        {
            Console.WriteLine("Välj sökväg att spara filen. Använd existerande folder.");
            var pathOfHans = Console.ReadLine();
            string path = @"C:\TheGreatHappening\TGHjson.txt";
            
            string json = JsonSerializer.Serialize(list);
            if (!string.IsNullOrEmpty(pathOfHans))
            {
                File.WriteAllText(pathOfHans, json);

                return pathOfHans;
            }
            else if (string.IsNullOrEmpty(pathOfHans))
            {
                File.WriteAllText(path, json);

                return path;
            }
            else
            {
                File.WriteAllText(path, json);

                return path;
            }
        }

        public List<Participant> ReadJsonFromTxtFile(string path)
        {
            var list = new List<Participant>();

            Console.WriteLine("Skriv en sökväg att ladda fil eller lämna tomt för förinställd plats.");
            var pathOfHans = Console.ReadLine();
            if(!string.IsNullOrEmpty(pathOfHans))
                path = pathOfHans;
            
            
            using (StreamReader r = new StreamReader(path))
            {

                string json = r.ReadToEnd();
                list = JsonSerializer.Deserialize<List<Participant>>(json);
                //List<Participant> listUpdated = JsonConvert.DeserializeObject<List<Participant>>(json);
                //list = listUpdated.ToList();
            }
            ShowList(list);

            return list;    
        }
    }
}
