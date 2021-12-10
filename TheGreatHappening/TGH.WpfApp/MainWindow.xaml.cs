using CatalogueOfParticipants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            spanelCreateParticipant.Visibility = Visibility.Visible;
        }

       
        private void btnShowList_Click(object sender, RoutedEventArgs e)
        {
            spanelShowList.Visibility = Visibility.Visible;
            ShowList(listOfParticipants);
        }

        private void ShowList(List<Participant> list)
        {
            
                if (list.Count > 0)
                {
                    foreach (var participant in list)
                    {
                    tblockShowList.Text += @$"Deltagare nummer {list.IndexOf(participant) + 1} 
                        Namn: { participant.FullName} 
                Epostadress: { participant.Email}";
                    


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
            spanelCreateParticipant.Visibility=Visibility.Collapsed;
        }

        private void btnCloseList_Click(object sender, RoutedEventArgs e)
        {
            spanelShowList.Visibility=Visibility.Collapsed;
        }
    }
}
