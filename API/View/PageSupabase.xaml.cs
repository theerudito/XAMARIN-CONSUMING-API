using API.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace API.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSupabase : ContentPage
    {
        public PageSupabase()
        {
            InitializeComponent();

            BindingContext = new VM_PageSupabase(Navigation);
        }
    }
}