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

        private void ViewList(List<UwpParticipant> list)
        {
            listOfUwpParticipants = list.ToList();

            lvListOfParticipants.ItemsSource = listOfUwpParticipants;
        }




        private void btnSaveParticipantToList_Click(object sender, RoutedEventArgs e)
        {



            //  listOfUwpParticipants.Add(UwpParticipant.CreateParticipant(tboxFirstName.Text, tboxLastName.Text, tboxEmail.Text, tboxSpecialRequirements.Text));

            if (!string.IsNullOrEmpty(tboxFirstName.Text) && !string.IsNullOrEmpty(tboxLastName.Text) && !string.IsNullOrEmpty(tboxEmail.Text) && !string.IsNullOrEmpty(tboxSpecialRequirements.Text))
            {
                listOfUwpParticipants.Add(new UwpParticipant(tboxFirstName.Text, tboxLastName.Text, tboxEmail.Text, tboxSpecialRequirements.Text));
                tboxFirstName.Text = "";
                tboxLastName.Text = "";
                tboxEmail.Text = "";
                tboxSpecialRequirements.Text = "";
            }
            else

                tblockMessageLeft.Text = "Deltagaren sparades inte, du måste fylla i alla fälten.";

            

            ViewList(listOfUwpParticipants);
        }

        

        

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            
            var obj =(Button)sender;
            var item =(UwpParticipant)obj.DataContext;
            
            

            listOfUwpParticipants = listOfUwpParticipants.Where(participant => participant != item).ToList();
            
            ViewList(listOfUwpParticipants);
            
            
            
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
    

        private async void btnReadList_Click(object sender, RoutedEventArgs e)
        {
            // Den här borde förstås inte ligga här, men jag fick konvertingsproblem när jag flyttade den och kanske inte hinner lösa de innan uppgiftens huvudmoment är avklarade
            
                var picker = new Windows.Storage.Pickers.FileOpenPicker();
                picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
                picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
                picker.FileTypeFilter.Add(".txt");


                Windows.Storage.StorageFile pickedFile = await picker.PickSingleFileAsync();
                if (pickedFile == null)
                    this.tblockDiscountCode.Text = "Operation cancelled.";


                List<UwpParticipant> list = new List<UwpParticipant>();


            if (pickedFile != null)
            {
                var data = await pickedFile.OpenReadAsync();


                using (StreamReader r = new StreamReader(data.AsStream()))
                {
                    string text = r.ReadToEnd();
                    list = JsonConvert.DeserializeObject<List<UwpParticipant>>(text);


                }





                ViewList(list);
            }
            
        }

        private void btnShowDiscountCode_Click(object sender, RoutedEventArgs e)
        {
            var obj = (Button)sender;
            var item = (UwpParticipant)obj.DataContext;

            tblockDiscountCode.Text = $"{item.FullName}s rabattkod är {item.DiscountCode}.";

        }
    }
}
