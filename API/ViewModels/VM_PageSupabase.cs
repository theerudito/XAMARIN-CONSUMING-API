using API.Data;
using API.Models;
using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json;
using Supabase;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace API.ViewModels
{
    public class VM_PageSupabase : BaseViewModel
    {
        public VM_PageSupabase(INavigation navigation)
        {
            Navigation = navigation;

            Task.Run(async () => await Get());

            //Get();
        }

        #region Properties

        private ObservableCollection<MSupabase> _listContact;

        public ObservableCollection<MSupabase> ListClient
        {
            get { return _listContact; }
            set { _listContact = value; OnPropertyChanged(); }
        }

        private static string _hour = DateTime.Now.ToString("HH:mm");
        private static string _date = DateTime.Now.ToString("dd/MM/yyyy");
        private string _dateNow = $"Fecha {_date} - Hour {_hour} ";

        private string _name;
        private string _database = "Firebase";
        private string _description;
        private float _saldoInitial;
        private bool _status = true;

        #endregion Properties

        #region Objetos

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public float Saldo_Initial
        {
            get => _saldoInitial;
            set => SetProperty(ref _saldoInitial, value);
        }

        public string DateNow
        {
            get => _dateNow;
            set => SetProperty(ref _dateNow, value);
        }

        public string Database
        {
            get => _database;
            set => SetProperty(ref _database, value);
        }

        #endregion Objetos

        #region Methods

        public async Task Add()
        {
            switch (Database)
            {
                case "Sqlite":
                    await Add_Sqlite();

                    break;

                case "Supabase":
                    await Add_Supabase();
                    break;

                case "Firebase":
                    await Add_Firebase();
                    break;

                case "Web":
                    await Add_Web();
                    break;

                default:
                    await DisplayAlert("Alert", "Data Base not found", "OK");
                    break;
            }

            await Get_Web();
        }

        public async Task Add_Sqlite()
        {
        }

        public async Task Add_Supabase()
        {
            var options = new SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            var supabase = new Supabase.Client(FetchData.urlSupabase, FetchData.keySupabase, options);
            await supabase.InitializeAsync();

            var newClient = new MSupabase { Name = Name.ToUpper().Trim(), Saldo_Inicial = Saldo_Initial, Description = Description, Status = _status, Fecha = _dateNow };

            await supabase.From<MSupabase>().Insert(newClient);

            ResetValue();

            await DisplayAlert("Alert", "Added Successfully", "OK");
        }

        public async Task Add_Firebase()
        {
            FirebaseClient firebase = new FirebaseClient(FetchData.urlFirebase.ToString());

            await firebase.Child("Clients").PostAsync(new MSupabase()
            {
                Name = Name.ToUpper().Trim(),
                Saldo_Inicial = Saldo_Initial,
                Description = Description,
                Status = _status,
                Fecha = _dateNow
            });

            ResetValue();

            await DisplayAlert("Alert", "Added Successfully", "OK");
        }

        public async Task Add_Web()
        {
            var fetch = new HttpClient();

            var newClient = new MSupabase { Name = Name.ToUpper().Trim(), Saldo_Inicial = Saldo_Initial, Description = Description, Status = _status, Fecha = _dateNow };

            var json = JsonConvert.SerializeObject(newClient);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await fetch.PostAsync(FetchData.urlWeb + "/api/ControllerClient", content);

            ResetValue();

            await DisplayAlert("Alert", "Added Successfully", "OK");
        }

        public async Task Get()
        {
            switch (Database)
            {
                case "Sqlite":
                    await Get_Sqlite();
                    break;

                case "Supabase":
                    await Get_Supabase();
                    break;

                case "Firebase":
                    await Get_Firebase();
                    break;

                case "Web":
                    await Get_Web();
                    break;

                default:
                    await DisplayAlert("Alert", "Data Base not found", "OK");
                    break;
            }
        }

        public async Task Get_Sqlite()
        {
        }

        public async Task Get_Supabase()
        {
            var options = new SupabaseOptions
            {
                AutoConnectRealtime = true
            };

            var supabase = new Supabase.Client(FetchData.urlSupabase, FetchData.keySupabase, options);

            await supabase.InitializeAsync();

            var result = await supabase.From<MSupabase>().Get();
            var data = result.Models;

            foreach (var item in data)
            {
                await DisplayAlert("Alert", item.Name, "OK");
            }
        }

        public async Task Get_Firebase()
        {
            FirebaseClient firebase = new FirebaseClient(FetchData.urlFirebase.ToString());
            var loadDataFirebase = await firebase.Child("Clients").OnceAsync<MSupabase>();

            ListClient = new ObservableCollection<MSupabase>();

            foreach (var item in loadDataFirebase)
            {
                if (item.Object.Status == true)
                {
                    ListClient.Add(new MSupabase
                    {
                        ClientId = item.Key,
                        Name = item.Object.Name,
                        Saldo_Inicial = item.Object.Saldo_Inicial,
                        Description = item.Object.Description,
                        Status = item.Object.Status,
                        Fecha = item.Object.Fecha
                    });
                }
                else
                {
                    await DisplayAlert("Alert", "Client not found", "OK");
                }
            }
        }

        public async Task Get_Web()
        {
            var fetch = new HttpClient();

            var response = await fetch.GetAsync(FetchData.urlWeb + "/api/ControllerClient");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<MSupabase[]>(result);

                ListClient = new ObservableCollection<MSupabase>(data);
            }
            else
            {
                await DisplayAlert("Alert", "Error", "OK");
            }
        }

        public void ResetValue()
        {
            Name = string.Empty;
            Description = string.Empty;
            Saldo_Initial = 0;
        }

        #endregion Methods

        #region Commands

        public ICommand btnAdd => new Command(async () => await Add());
        public ICommand deleteClientCommand => new Command(async () => await Add());
        public ICommand getOneClientCommand => new Command(async () => await Add());

        #endregion Commands
    }
}