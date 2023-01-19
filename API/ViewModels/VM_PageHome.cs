using API.View;
using System.Windows.Input;
using Xamarin.Forms;

namespace API.ViewModels
{
    public class VM_PageHome : BaseViewModel
    {
        public VM_PageHome(INavigation navigation)
        {
            Navigation = navigation;
        }

        #region COMMAND
        public ICommand btnGoJPHCommand => new Command(async () =>
        {
            await Navigation.PushAsync(new JPH_Page());
        });
        public ICommand btnGoReqresCommand => new Command(async () =>
        {
            await Navigation.PushAsync(new Regres_Page());
        });
        public ICommand btnGoMyAPICommand => new Command(async () =>
        {
            await Navigation.PushAsync(new Page_My_API());
        });
        #endregion
    }
}
