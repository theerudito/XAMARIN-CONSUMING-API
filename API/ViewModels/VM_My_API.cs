
using API.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace API.ViewModels
{
    public class VM_My_API : BaseViewModel
    {
        MyAPI_Model myContact = new MyAPI_Model();
        HttpClient clientHTTP = new HttpClient();

        #region VARIABLES
        private string URL = "https://backend-react-mypage.up.railway.app/api/contactos";
        private ObservableCollection<MyAPI_Model> _contacts;
        private MyAPI_Model data { get; set; }
        private bool EditingContact = true;
        private string _changeText;
        private string _name;
        private string _email;
        private int _phone;
        private string _message;
        private string _pic;
        private string _createdAt;
        private string _updatedAt;
        private string _Label;
        #endregion

        #region OBJECS
        public ObservableCollection<MyAPI_Model> Contacts
        {
            get { return _contacts; }
            set { _contacts = value; OnPropertyChanged(); }
        }
        public string Label
        {
            get { return _Label; }
            set { SetValue(ref _Label, value); }
        }
        public string changeText
        {
            get { return _changeText; }
            set { SetValue(ref _changeText, value); }
        }
        public string name
        {
            get { return _name; }
            set { SetValue(ref _name, value); }
        }
        public string email
        {
            get { return _email; }
            set { SetValue(ref _email, value); }
        }
        public int phone
        {
            get { return _phone; }
            set { SetValue(ref _phone, value); }
        }
        public string message
        {
            get { return _message; }
            set { SetValue(ref _message, value); }
        }
        public string pic
        {
            get { return _pic; }
            set { SetValue(ref _pic, value); }
        }
        public string createdAt
        {
            get { return _createdAt; }
            set { SetValue(ref _createdAt, value); }
        }
        public string updatedAt
        {
            get { return _updatedAt; }
            set { SetValue(ref _updatedAt, value); }
        }

        #endregion

        #region CONSTRUCTOR
        public VM_My_API(INavigation navigation)
        {
            Navigation = navigation;
            changeText = "SAVE CONTACTS";
            EditingContact = false;
            getContacts();
        }
        #endregion

        #region METHODS
        public async Task create_Or_Edit()
        {
            if (EditingContact == true)
            {
                await updateContact();
            }
            else
            {
                await createContact();
            }
        }
        public async Task createContact()
        {
            myContact.pic = $"https://avatars.dicebear.com/api/micah/{name}.svg";
            myContact.name = name;
            myContact.email = email;
            myContact.phone = phone;
            myContact.message = message;

            var json = JsonConvert.SerializeObject(myContact);
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var postData = await clientHTTP.PostAsync(URL, contentJson);

            if (postData.StatusCode == HttpStatusCode.OK)
            {
                name = "";
                email = "";
                phone = 0;
                message = "";
                await DisplayAlert("info", "creado con exito", "ok");
                getContacts();
            }
            else
            {
                await DisplayAlert("error", "no se puedo duardar", "ok");
            }
        }
        public async Task getOneContact(MyAPI_Model getData)
        {
            EditingContact = true;
            changeText = "EDIT CONTACTS";
            data = getData;
            name = getData.name;
            email = getData.email;
            phone = getData.phone;
            message = getData.message;
            Label = getData._id;
        }
        public async Task getContacts()
        {
            var response = await clientHTTP.GetAsync(URL);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                Contacts = JsonConvert.DeserializeObject<ObservableCollection<MyAPI_Model>>(json);
            }
            else
            {
                await DisplayAlert("info", "Sin DATA", "ok");
            }
        }
        public async Task deleteContact(MyAPI_Model deleteContact)
        {
            var deleteData = await clientHTTP.DeleteAsync($"{URL}/{deleteContact._id}");
            if (deleteData.IsSuccessStatusCode)
            {
                getContacts();
                await DisplayAlert("infor", "Eliminado con exito", "ok");
            }
            else
            {
                await DisplayAlert("error", "no se puedo eliminar", "ok");
            }
        }
        public async Task updateContact()
        {
            MyAPI_Model c = new MyAPI_Model();

            c.name = name;
            c.email = email;
            c.phone = phone;
            c.message = message;


            //myContact.name = name;
            //myContact.email = email;
            //myContact.phone = phone;
            //myContact.message = message;

            var json = JsonConvert.SerializeObject(c);
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var putData = await clientHTTP.PutAsync(URL + data._id, contentJson);
            if (putData.IsSuccessStatusCode)
            {
                name = "";
                email = "";
                phone = 0;
                message = "";
                await DisplayAlert("info", "actualizado con exito", "ok");
                changeText = "SAVE CONTACTS";
                EditingContact = false;
                getContacts();
            }
            else
            {
                await DisplayAlert("error", "no se puedo actualizar", "ok");
            }
        }
        #endregion

        #region COMMMANDS
        public ICommand createContactCommand => new Command(async () => await create_Or_Edit());
        public ICommand deleteContactCommand => new Command<MyAPI_Model>(async (c) => await deleteContact(c));
        public ICommand getOneContactCommand => new Command<MyAPI_Model>(async (c) => await getOneContact(c));
        #endregion
    }
}
