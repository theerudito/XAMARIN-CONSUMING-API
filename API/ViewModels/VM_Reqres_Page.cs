using API.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace API.ViewModels
{
    public class VM_Reqres_Page : BaseViewModel
    {
        #region VARIABLES
        private ObservableCollection<Reqres_Model.Datum> _reqres_Models;
        private int _Page;
        #endregion



        #region OBJECS
        public ObservableCollection<Reqres_Model.Datum> Reqres_Models
        {
            get { return _reqres_Models; }
            set { _reqres_Models = value; OnPropertyChanged(); }
        }
        public int Page
        {
            get { return _Page; }
            set { _Page = value; OnPropertyChanged(); }
        }
        #endregion



        #region CONSTRUCTOR
        public VM_Reqres_Page(INavigation navigation)
        {
            Navigation = navigation;
            Page = 1;
            Get_Reqres_Page();
        }
        #endregion



        #region METHODS
        public async void Get_Reqres_Page()
        {
            var client = new HttpClient();
            var response = await client.GetAsync($"https://reqres.in/api/users?page={Page}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                var json = JsonConvert.DeserializeObject<Reqres_Model.Root>(content);
                Reqres_Models = new ObservableCollection<Reqres_Model.Datum>(json.data);
            }
        }

        public async Task PrewPage()
        {
            Page = 1;
            Get_Reqres_Page();
        }
        public async Task NextPage()
        {
            Page = 2;
            Get_Reqres_Page();
        }
        #endregion

        #region COMMMANDS
        public ICommand btnPreviewCommand => new Command(async () => await PrewPage());
        public ICommand btnNextCommand => new Command(async () => await NextPage());
        #endregion
    }

}

