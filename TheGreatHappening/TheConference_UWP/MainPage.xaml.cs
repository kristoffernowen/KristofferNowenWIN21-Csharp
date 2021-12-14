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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TheConference_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<UwpParticipant> listOfUwpParticipants = new List<UwpParticipant>();
        string name = "Vivo";
        public MainPage()
        {
            this.InitializeComponent();
            
        }

        private void StateTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += TickAction;
            timer.Start();
        }

        public void TickAction(object sender, object e)
        {
            lvListOfParticipants.ItemsSource = listOfUwpParticipants;
        }
        private void btnSaveParticipantToList_Click(object sender, RoutedEventArgs e)
        {

            listOfUwpParticipants.Add(UwpParticipant.CreateParticipant(tboxFirstName.Text, tboxLastName.Text, tboxEmail.Text, tboxSpecialRequirements.Text));
            
        }

        private void btnShowList_Click(object sender, RoutedEventArgs e)
        {
            List<UwpParticipant> nextList = new List<UwpParticipant>();
            nextList = listOfUwpParticipants;

            IEnumerable<UwpParticipant> listU = listOfUwpParticipants as IEnumerable<UwpParticipant>;
            
            lvListOfParticipants.ItemsSource = nextList;

            name = "måns";
            
        }

        private void btnClearList_Click(object sender, RoutedEventArgs e)
        {
            listOfUwpParticipants.Clear();
            lvListOfParticipants.ItemsSource = listOfUwpParticipants;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<UwpParticipant> listU = listOfUwpParticipants as IEnumerable<UwpParticipant>;
            var obj =(Button)sender;
            var item =(UwpParticipant)obj.DataContext;
            

            listOfUwpParticipants = listU.Where(participant => participant != item).ToList();
            
            // kan inte uppdatera listvy här, då låser jag den för showlist(), är det därför jag ska ha en uppdateande klocka?   is funky...
            name = "Fran";
            tblockTest.Text = name;
        }
    }
}
