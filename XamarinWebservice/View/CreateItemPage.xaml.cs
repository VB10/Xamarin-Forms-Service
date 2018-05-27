using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinWebservice.ViewModel;

namespace XamarinWebservice.View
{
    public partial class CreateItemPage : ContentPage
    {
        CreateItemMVVM createItemMVVM;
        public CreateItemPage()
        {
            InitializeComponent();
            BindingContext = createItemMVVM = new CreateItemMVVM(this);
            //IsBusy = createItemMVVM.loadingPost;
        }
    }
}
