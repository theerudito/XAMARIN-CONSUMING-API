using API.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace API.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page_My_API : ContentPage
    {
        public Page_My_API()
        {
            InitializeComponent();
            BindingContext = new VM_My_API(Navigation);
        }
    }
}