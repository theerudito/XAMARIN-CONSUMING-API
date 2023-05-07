using API.Data;
using API.Models;
using Supabase;
using System;
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
        }

        #region Properties

        private static string _hour = DateTime.Now.ToString("HH:mm");
        private static string _date = DateTime.Now.ToString("dd/MM/yyyy");
        private string _dateNow = $"Fecha {_date} - Hour {_hour} ";

        private string _name;
        private string _database = "Supabas";
        private string _description;
        private float _value;
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
            get => _value;
            set => SetProperty(ref _value, value);
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

            await DisplayAlert("Alert", "Added Successfully", "OK");
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

            var cliens = new MSupabase

            {
                Name = Name,
                Description = Description,
                Saldo_Inicial = Saldo_Initial,
                Status = _status,
                Fecha = _dateNow,
            };

            await supabase.From<MSupabase>().Insert(cliens);
        }

        public async Task Add_Firebase()
        {
        }

        public async Task Add_Web()
        {
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
        }

        public async Task Get_Web()
        {
        }

        #endregion Methods

        #region Commands

        public ICommand btnAdd => new Command(async () => await Add());

        #endregion Commands
    }
}