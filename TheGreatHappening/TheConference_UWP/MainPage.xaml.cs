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
using Newtonsoft.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TheConference_UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        List<UwpParticipant> listOfUwpParticipants = new List<UwpParticipant>();

        public MainPage()
        {
            this.InitializeComponent();
        }

        
        /// <summary>
        /// Saves a participants data to a List<UwpParticipant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnSaveParticipantToList_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tboxFirstName.Text) && !string.IsNullOrEmpty(tboxLastName.Text) &&
                !string.IsNullOrEmpty(tboxEmail.Text) && !string.IsNullOrEmpty(tboxSpecialRequirements.Text))
            {
                UwpParticipant.SaveParticipantToList(listOfUwpParticipants, new UwpParticipant(tboxFirstName.Text, tboxLastName.Text, tboxEmail.Text, tboxSpecialRequirements.Text));

                tboxFirstName.Text = "";
                tboxLastName.Text = "";
                tboxEmail.Text = "";
                tboxSpecialRequirements.Text = "";
                tblockMessageLeft.Text = "";
            }
            else
                tblockMessageLeft.Text = "Deltagaren sparades inte, du måste fylla i alla fälten.";

            DisplayParticipantsList(listOfUwpParticipants);
        }


        /// <summary>
        /// Displays the participants in a List<UwpParticipants> in the xaml listview
        /// </summary>
        /// <param name="list">
        /// </param>

        private void DisplayParticipantsList(List<UwpParticipant> list)
        {
            listOfUwpParticipants = list.ToList();
            lvListOfParticipants.ItemsSource = listOfUwpParticipants;
            tblockDiscountCode.Text = "";
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var obj = (Button)sender;
            var item = (UwpParticipant)obj.DataContext;

           listOfUwpParticipants = UwpParticipant.DeleteParticipantFromList(listOfUwpParticipants, item);

            DisplayParticipantsList(listOfUwpParticipants);
        }

        private async void btnSaveList_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await UwpParticipant.SaveList(listOfUwpParticipants);
            }
            catch
            { }
        }


        /// <summary>
        /// Reads a list from a textfile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnReadList_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            picker.FileTypeFilter.Add(".txt");
            Windows.Storage.StorageFile pickedFile = await picker.PickSingleFileAsync();
            if (pickedFile == null)
                this.tblockDiscountCode.Text = "Operation cancelled.";

            if (pickedFile != null)
            {
                var data = await pickedFile.OpenReadAsync();

                using (StreamReader r = new StreamReader(data.AsStream()))
                {
                    string text = r.ReadToEnd();
                    listOfUwpParticipants = JsonConvert.DeserializeObject<List<UwpParticipant>>(text);
                }

                DisplayParticipantsList(listOfUwpParticipants);
            }
        }

        /// <summary>
        /// Shows the discountcode of the participant
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowDiscountCode_Click(object sender, RoutedEventArgs e)
        {
            var obj = (Button)sender;
            var item = (UwpParticipant)obj.DataContext;

            tblockDiscountCode.Text = UwpParticipant.ChooseDiscountMessage(item);
        }
    }
}
