using API.Models;
using Newtonsoft.Json;
using System;
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
        private string URL = "";
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
        private string _Id;
        #endregion

        #region OBJECS
        public ObservableCollection<MyAPI_Model> Contacts
        {
            get { return _contacts; }
            set { _contacts = value; OnPropertyChanged(); }
        }
        public string Id
        {
            get { return _Id; }
            set { SetValue(ref _Id, value); }
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
            myContact.pic = $"https://api.dicebear.com/5.x/micah/svg?seed={name}";

            myContact.name = name;
            myContact.email = email;
            myContact.phone = phone;
            myContact.message = message;

            // hacer una peticion post a la api
            var json = JsonConvert.SerializeObject(myContact);
            // haceptar mayusculas y minusculas

            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");

            var postData = await clientHTTP.PostAsync($"{URL}/Clients", contentJson);

            if (postData.IsSuccessStatusCode)
            {
                await DisplayAlert("info", "the contact was created", "ok");
                name = "";
                email = "";
                phone = 0;
                message = "";
                getContacts();
            }
            else
            {
                await DisplayAlert("error", "the contact was not created", "ok");
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
            Id = getData.id;
        }
        public async Task getContacts()
        {
            var response = await clientHTTP.GetAsync($"{URL}/Clients");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var json = await response.Content.ReadAsStringAsync();
                Contacts = JsonConvert.DeserializeObject<ObservableCollection<MyAPI_Model>>(json);
            }
            else
            {
                await DisplayAlert("info", "don't have contacts", "ok");
            }
        }
        public async Task deleteContact(MyAPI_Model deleteContact)
        {
            var deleteData = await clientHTTP.DeleteAsync($"{URL}/Clients/{deleteContact.id}");
            if (deleteData.IsSuccessStatusCode)
            {
                getContacts();
                await DisplayAlert("infor", "the contact was eliminated", "ok");
            }
            else
            {
                await DisplayAlert("error", "the contact was not eliminated", "ok");
            }
        }
        public async Task updateContact()
        {
            myContact.pic = $"https://api.dicebear.com/5.x/micah/svg?seed={name}";
            myContact.name = name;
            myContact.email = email;
            myContact.phone = phone;
            myContact.message = message;

            var json = JsonConvert.SerializeObject(myContact);
            // haceptar mayusculas y minusculas
            var contentJson = new StringContent(json, Encoding.UTF8, "application/json");
            var putData = await clientHTTP.PutAsync($"{URL}/Clients/{Id}", contentJson);

            if (putData.IsSuccessStatusCode)
            {
                await DisplayAlert("info", "the contact was updated", "ok");
                name = "";
                email = "";
                phone = 0;
                message = "";
<<<<<<< HEAD
=======
                Id = "";
                changeText = "SAVE CONTACTS";
                EditingContact = false;
                await DisplayAlert("info", "the contact was updated", "ok");
>>>>>>> dfad841a9d73bff9e7a6149860344fdd34ddb384
                getContacts();
                EditingContact = false;
                changeText = "SAVE CONTACTS";
            }
            else
            {
                await DisplayAlert("error", "the contact was not updated", "ok");
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


