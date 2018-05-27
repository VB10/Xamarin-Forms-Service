using Xamarin.Forms;
using XamarinWebservice.Model;
using XamarinWebservice.ViewModel;

namespace XamarinWebservice.View
{
    public partial class ListPage : ContentPage
    {
        ListPageMVVM ListPageMVVM;
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ListPageMVVM.get_ItemList();
        }
        public ListPage()
        {
            InitializeComponent();
            BindingContext = ListPageMVVM = new ListPageMVVM(this);

        }
        async void Clicked_Delete(object sender, System.EventArgs e)
        {
            var menuItem = sender as MenuItem;
            await ListPageMVVM.delete_Item((string)menuItem.CommandParameter);
        }
        async void Clicked_Update(object sender, System.EventArgs e)
        {
            var menuItem = sender as MenuItem;
            await ListPageMVVM.update_Item((Book)menuItem.CommandParameter);
        }
    }
}
