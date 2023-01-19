using API.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace API.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageHome : ContentPage
    {
        public PageHome()
        {
            InitializeComponent();
            BindingContext = new VM_PageHome(Navigation);
        }
    }
}