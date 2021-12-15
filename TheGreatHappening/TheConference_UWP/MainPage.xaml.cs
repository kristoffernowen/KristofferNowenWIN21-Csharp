using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TheConference_UWP.Services;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TheConference_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<UwpParticipant> listOfUwpParticipants = new List<UwpParticipant>();
        ObservableCollection<UwpParticipant> OBlista = new ObservableCollection<UwpParticipant>();
        

        public MainPage()
        {
            this.InitializeComponent();
          //  lvListOfParticipants.ItemsSource = OBlista;
            
        }

        private ObservableCollection<UwpParticipant> MyList
        {
            get
            {
                if (MyList == null)
                {
                    MyList = new ObservableCollection<UwpParticipant>();
                }
                return MyList;
            }
            set
            {
                MyList = value;
            }
        }
        

        
        private  void ViewList(List<UwpParticipant> list)
        {
            listOfUwpParticipants = list.ToList();
           
            lvListOfParticipants.ItemsSource = list;
        }
        private void btnSaveParticipantToList_Click(object sender, RoutedEventArgs e)
        {


            // OBlista
            listOfUwpParticipants.Add(UwpParticipant.CreateParticipant(tboxFirstName.Text, tboxLastName.Text, tboxEmail.Text, tboxSpecialRequirements.Text));

            //  listOfUwpParticipants = listOfUwpParticipants.Where(participant => participant == item).ToList();




            ViewList(listOfUwpParticipants);
        }

        private void btnShowList_Click(object sender, RoutedEventArgs e)
        {
            // låser här också, den uppdaterar inte, första går bra

            
            
            

            
            
        }

        private void btnClearList_Click(object sender, RoutedEventArgs e)
        {
            listOfUwpParticipants.Clear();
            lvListOfParticipants.ItemsSource = listOfUwpParticipants;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            
            var obj =(Button)sender;
            var item =(UwpParticipant)obj.DataContext;
            
            //OBlista.Remove(item);

            listOfUwpParticipants = listOfUwpParticipants.Where(participant => participant != item).ToList();
            
            ViewList(listOfUwpParticipants);
            
            // kan inte uppdatera listvy här, då låser jag den för showlist(), är det därför jag ska ha en uppdateande klocka?   is funky...
            
            
        }
    }
}
