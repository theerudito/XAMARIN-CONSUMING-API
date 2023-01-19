using API.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace API.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Regres_Page : ContentPage
    {
        public Regres_Page()
        {
            InitializeComponent();
            BindingContext = new VM_Reqres_Page(Navigation);
        }
    }
}