using CatalogueOfParticipants;
using Newtonsoft.Json;
using System.Text.Json;
//using System.Text.Json.Serialization;

byte menuAction;
var continueMenu = true;

var listOfParticipants = new List<Participant>();

do
{
    Console.WriteLine(@"
Vad vill du göra?
Välj med siffra:

1 : Anmäla deltagare
2 : Visa lista med deltagare
3 : Ta bort deltagare
4 : Generera rabattkod
5 : Spara deltagarlista till textfil som text
6 : Spara deltagarlista till textfil som json
7 : Läs in sparad lista
0 : Avsluta programmet

");
    try
    {
        menuAction = byte.Parse(Console.ReadLine());
    }
    catch
    {
        menuAction = 9;
    }
    switch (menuAction)
    {
        case 1: Console.WriteLine("Anmäl deltagare");
            CreateParticipant();
            break;
        case 2: Console.WriteLine("Visa lista med deltagare");
            ShowList();
            break;
        case 3: Console.WriteLine("Ta bort deltagare");
            RemoveParticipantFromList();
            break;
        case 4: Console.WriteLine("Generera rabattkod");
            GenerateDiscountCode();
            break;
        case 5: Console.WriteLine("Spara listan till textfil");
            WriteToTextFile();
            break;
        case 6: Console.WriteLine("Skriv json fil att spara");
            WriteToTextFileAsJson();
            break;
        case 7: Console.WriteLine("Ladda lista");
            ReadJsonFromTxtFile();
            break;
        case 9: Console.WriteLine("SE TILL ATT DU SKRIVER EN SIFFRA FRÅN VALMENYN");
            break;
        case 0: continueMenu = false;
            Console.WriteLine("Avsluta programmet");
            break;
        default: continue;
    }
}
while (continueMenu == true);

 List<Participant> CreateParticipant()
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

    if (!String.IsNullOrEmpty(participant.FirstName) && !String.IsNullOrEmpty(participant.LastName) && !String.IsNullOrEmpty(participant.Email) && !String.IsNullOrEmpty(participant.SpecialRequirements))
    {
    listOfParticipants.Add(participant);
    }
    else
    {
        Console.WriteLine("Du måste fylla i alla fält. Deltagaren sparades inte.");
        
    }

    return listOfParticipants;
}
void ShowList()
{
    foreach(var participant in listOfParticipants)
        Console.WriteLine($@"Deltagare nummer: {listOfParticipants.IndexOf(participant) + 1}
Namn: {participant.FullName}
Epostadress: {participant.Email}
");
}
List<Participant> RemoveParticipantFromList()
{
    Console.WriteLine("Ta bort deltagare. Välj nummer från listan.");
    foreach (var participant in listOfParticipants)
        Console.WriteLine($@"Deltagare nummer: {listOfParticipants.IndexOf(participant) + 1}
Namn: {participant.FullName}
Epostadress: {participant.Email}
");
    Console.WriteLine("Skriv nummer på deltagare att ta bort:");
    listOfParticipants.Remove(listOfParticipants[Convert.ToByte(Console.ReadLine())-1]);

    return listOfParticipants;
}

void GenerateDiscountCode()
{
    Console.WriteLine("Välj deltagare att generera rabattkod till.");
    foreach (var participant in listOfParticipants)
        Console.WriteLine($@"Deltagare: {listOfParticipants.IndexOf(participant) + 1}
 {participant.FullName}");
    var a = Convert.ToByte(Console.ReadLine());
    Console.WriteLine($"Rabattkod: {listOfParticipants[a-1].DiscountCode}");
}

void WriteToTextFile()
{

    string path = @"C:\Users\krist\OneDrive\Documents\Dagbok C#\24nov\Lektion_7\MyFolder\SubFolder\GH.txt";
    var lines = new List<string>();
    foreach (var participant in listOfParticipants)
    {
        lines.Add($"Förnamn: {participant.FirstName}");
        lines.Add(participant.LastName);
        lines.Add(participant.Email);
        lines.Add(participant.SpecialRequirements);
    }
    File.WriteAllLines(path, lines );
}



void WriteToTextFileAsJson()
{
    string path = @"C:\Users\krist\OneDrive\Documents\Dagbok C#\24nov\Lektion_7\MyFolder\SubFolder\TGH.txt";
    string json = System.Text.Json.JsonSerializer.Serialize(listOfParticipants);
    File.WriteAllText(path, json);
}

void ReadJsonFromTxtFile()
{
    string path = @"C:\Users\krist\OneDrive\Documents\Dagbok C#\24nov\Lektion_7\MyFolder\SubFolder\TGH.txt";
    using (StreamReader r = new StreamReader(path))
    {

        string json = r.ReadToEnd();
        List<Participant> listUpdated = JsonConvert.DeserializeObject<List<Participant>>(json);
        listOfParticipants = listUpdated.ToList();
    }
}