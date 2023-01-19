using API.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace API.ViewModels
{
    public class VM_JPH_Page : BaseViewModel
    {
        #region CONSTRUCTOR
        public VM_JPH_Page(INavigation navigation)
        {
            Navigation = navigation;
            getData();
        }
        #endregion


        #region VARIABLES
        private ObservableCollection<JPH_Model.Root> _jPH_Models;
        #endregion


        #region OBJECTS
        public ObservableCollection<JPH_Model.Root> JPH_Models
        {
            get { return _jPH_Models; }
            set { _jPH_Models = value; OnPropertyChanged(); }
        }
        #endregion


        #region METHODS
        public async Task getData()
        {
            var client = new HttpClient();
            var response = await client.GetAsync("https://jsonplaceholder.typicode.com/users");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                JPH_Models = JsonConvert.DeserializeObject<ObservableCollection<JPH_Model.Root>>(content);
            }
        }
        #endregion
    }
}

#region VARIABLES

#endregion

#region OBJECS

#endregion

#region CONSTRUCTOR

#endregion

#region METHODS

#endregion



#region COMMMANDS

#endregion