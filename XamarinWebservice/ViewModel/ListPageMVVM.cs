using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinWebservice.Data;
using XamarinWebservice.Model;
using XamarinWebservice.View;

namespace XamarinWebservice.ViewModel
{
    public class ListPageMVVM : INotifyPropertyChanged
    {
        BookManager bookManager;
        Page currentPage;
        public ListPageMVVM(Page currentPage)
        {
            bookManager = new BookManager();
            this.currentPage = currentPage;
            currentPage.Title = "List Page";
        }

        ObservableCollection<Book> _bookList;

        public ObservableCollection<Book> bookList
        {
            get
            {
                return _bookList;
            }

            set
            {
                //değişiklikleri yakalamak için
                _bookList = value;
                OnPropertyChanged();
            }
        }
        bool _refreshList;

        public bool refreshList
        {
            get
            {
                return _refreshList;
            }

            set
            {
                _refreshList = value;
                OnPropertyChanged();
            }
        }
        public ICommand ItemAddCommand{
            get {
                return new Command(async () =>
                {
                    await currentPage.Navigation.PushAsync(new CreateItemPage(),true);
                });
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await get_ItemList();
                });
            }
        }

        public async Task get_ItemList(){
            refreshList = true;
            var list =await bookManager.GetAll();
            refreshList = false;
            bookList = list;
        }
        public async Task delete_Item(string isbn)
        {
                refreshList = true;
                await bookManager.Delete(isbn);
                bookList.Remove(bookList.FirstOrDefault(obj => obj.isbn == isbn));
                refreshList = false;
        }
        public async Task update_Item(Book item)
        {
            await currentPage.Navigation.PushAsync(new CreateItemPage(), true);
            MessagingCenter.Send<ListPageMVVM, Book>(this, BaseEnum.updateBook.ToString(), item);

        }

        #region Değişiklikleri yakalama
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
