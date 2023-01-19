using API.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace API.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JPH_Page : ContentPage
    {
        public JPH_Page()
        {
            InitializeComponent();
            BindingContext = new VM_JPH_Page(Navigation);
        }
    }
}