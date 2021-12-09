using CatalogueOfParticipants;



byte menuAction;
var continueMenu = true;

var listOfParticipants = new List<Participant>();
var managerTGH = new Participant();

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
    string path = @"C:\TheGreatHappening\TGHjson.txt";

    switch (menuAction)
    {
        case 1: Console.WriteLine("Anmäl deltagare");
            listOfParticipants = managerTGH.CreateParticipant(listOfParticipants);
            break;
        case 2: Console.WriteLine("Visa lista med deltagare");
            managerTGH.ShowList(listOfParticipants);
            break;
        case 3: Console.WriteLine("Ta bort deltagare");
            listOfParticipants = managerTGH.RemoveParticipantFromList(listOfParticipants);
            break;
        case 4: Console.WriteLine("Generera rabattkod");
            managerTGH.GenerateDiscountCode(listOfParticipants);
            break;
        case 5: Console.WriteLine("Spara listan till textfil");
            managerTGH.WriteToTextFile(listOfParticipants);
            break;
        case 6: Console.WriteLine("Skriv json fil att spara");

            path = managerTGH.WriteToTextFileAsJson(listOfParticipants);
            break;
        case 7: Console.WriteLine("Ladda lista");
            listOfParticipants = managerTGH.ReadJsonFromTxtFile(path);
            break;
        case 9: Console.WriteLine("SE TILL ATT DU SKRIVER EN SIFFRA FRÅN VALMENYN");
            break;
        case 0:
            Console.WriteLine("Avslutar programmet"); 
            continueMenu = false;
            
            break;
        default: continue;
    }
}
while (continueMenu == true);












