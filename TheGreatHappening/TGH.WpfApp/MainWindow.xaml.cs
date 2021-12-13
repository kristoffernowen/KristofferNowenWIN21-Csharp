using CatalogueOfParticipants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TGH.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Participant> listOfParticipants = new List<Participant>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddParticipant_Click(object sender, RoutedEventArgs e)
        {
            spanelShowList.Visibility = Visibility.Collapsed;
            spanelListOptions.Visibility = Visibility.Collapsed;
            spanelCreateParticipant.Visibility = Visibility.Visible;
        }

       
        private void btnShowList_Click(object sender, RoutedEventArgs e)
        {
            spanelShowList.Visibility = Visibility.Visible;
            ShowList(listOfParticipants);
        }

        private void ShowList(List<Participant> list)
        {
            spanelCreateParticipant.Visibility = Visibility.Collapsed;
            spanelListOptions.Visibility = Visibility.Visible;
            tblockShowList.Text = "";

            if (list.Count > 0)
            {
                    foreach (var participant in list)
                    {
                    tblockShowList.Text += $"Deltagare nummer {list.IndexOf(participant) + 1} \n";
                    tblockShowList.Text += $"Namn: { participant.FullName} \n";
                    tblockShowList.Text += $"Epostadress: { participant.Email}\n";
                    }
            }
            else if (list.Count == 0)
            {
                    
                tblockShowList.Text = "Listan är tom";
            }
            
        }

        private void btnAddToList_Click(object sender, RoutedEventArgs e)
        {
            
            Participant newParticipant = new Participant();

            tblockOutput.Text = "Fyll i data för deltagaren. Tryck spara till lista efteråt.";
            newParticipant.FirstName = tbFirstName.Text;
            newParticipant.LastName = tbLastName.Text;
            newParticipant.Email = tbEmail.Text;
            newParticipant.SpecialRequirements = tbSpecialRequirements.Text;

            listOfParticipants.Add(newParticipant);

            tbFirstName.Text = "";
            tbLastName.Text = "";
            tbEmail.Text = "";
            tbSpecialRequirements.Text = "";

            spanelCreateParticipant.Visibility=Visibility.Collapsed;
        }


        private async void btnSaveList_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonSerializer.Serialize(listOfParticipants);

            await File.WriteAllTextAsync(@"C:\TheGreatHappening\TGHwpf.txt", json);
        }

        private void btnReadList_Click(object sender, RoutedEventArgs e)
        {
            var path = @"C:\TheGreatHappening\TGHwpf.txt";
            using (StreamReader r = new StreamReader(path))
            {

                string json = r.ReadToEnd();
                listOfParticipants = JsonSerializer.Deserialize<List<Participant>>(json);
                //List<Participant> listUpdated = JsonConvert.DeserializeObject<List<Participant>>(json);
                //list = listUpdated.ToList();
            }
            ShowList(listOfParticipants);
        }

        private void btnRemoveParticipant_Click(object sender, RoutedEventArgs e)
        {
            lviewRight.Visibility = Visibility.Collapsed;
            spanelRemoveParticipant.Visibility=Visibility.Visible;

            tblockRemoveParticipant.Text = "Välj deltagare att ta bort, skriv nummer på deltagare";


        }
        private void btnRemoveThisParticipant_Click(object sender, RoutedEventArgs e)
        {
            listOfParticipants = RemoveParticipantFromList(listOfParticipants);
            ShowList(listOfParticipants);
            tboxRemoveParticipant.Text = "";
        }
        public List<Participant> RemoveParticipantFromList(List<Participant> list)
        {
            int val;
            bool result = int.TryParse(tboxRemoveParticipant.Text, out val);
            if (!result)
                tblockRemoveParticipant.Text = "Nåt gick fel!"; //something has gone wrong
                        //OK, continue using val


           // var removeThisOne = Convert.ToInt32(tboxRemoveParticipant.Text);
            
            if (list.Count > 1)
                list.Remove(list[val-1]);

            else if(list.Count == 1)
                list.Clear();

            else
                tblockRemoveParticipant.Text = "Deltagaren fanns inte.";
            return list;
        }

        private void btnChangeSpecialRequirement_Click(object sender, RoutedEventArgs e)
        {
            spanelRemoveParticipant.Visibility = Visibility.Collapsed;
            lviewRight.Visibility = Visibility.Visible;

           
        }

        private void btnViewThisParticipant_Click(object sender, RoutedEventArgs e)
        {
            ViewParticipant();
        }
        public void ViewParticipant()
        {
            int val;
            bool result = int.TryParse(tboxViewParticipant.Text, out val);
            if (!result)
                tblockRemoveParticipant.Text = "Nåt gick fel!";

            if (listOfParticipants.Count >= val)
            {
                var i = Convert.ToInt32(val - 1);
                tblockVRFirstName.Text = $"Förnamn: {listOfParticipants[i].FirstName}";
                tblockVRLastName.Text = $"Efternamn: {listOfParticipants[i].LastName}";
                tblockVREmail.Text = $"Email: {listOfParticipants[i].Email}";
                tblockVRSpecialRequirement.Text = $"Speciella önskemål: {listOfParticipants[i].SpecialRequirements}";
                tblockVRDiscountCode.Text = $"Rabattkod: {listOfParticipants[i].DiscountCode}";
            }

            else
                tblockVRFirstName.Text = "Deltagaren fanns inte.";
        }
    }
}
