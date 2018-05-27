using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinWebservice.Data;
using XamarinWebservice.Model;

namespace XamarinWebservice.ViewModel
{
    public class CreateItemMVVM : INotifyPropertyChanged
    {
        BookManager bookManager;
        Page currentPage;
        bool update;
        public CreateItemMVVM(Page navigation)
        {
            bookManager = new BookManager();
            currentPage = navigation;
            navigation.Title = "Update Page";

            MessagingCenter.Subscribe<ListPageMVVM, Book>(this, BaseEnum.updateBook.ToString(), (page, data) =>
            {
                Console.WriteLine(BaseEnum.updateBook.ToString());
                newBook = data;
                newBook.authors.ForEach((string obj) =>
                {
                    authors += obj + " ";
                });
                update = true;
                MessagingCenter.Unsubscribe<ListPageMVVM, Book>(this, BaseEnum.updateBook.ToString());

            });
        }
        Book _newBook;

        public Book newBook
        {
            get
            {
                if (_newBook == null) _newBook = new Book();
                return _newBook;
            }

            set
            {
                _newBook = value;
                OnPropertyChanged();
            }
        }
        string _authors;

        public string authors
        {
            get
            {
                return _authors;
            }

            set
            {
                _authors = value;
                OnPropertyChanged();
            }
        }
        bool _loadingPost;

        public bool loadingPost
        {
            get
            {
                return _loadingPost;
            }

            set
            {
                _loadingPost = value;
                OnPropertyChanged();
            }
        }

		public ICommand AddCommand
        {
            get
            {
                return new Command(async () =>
                {
                    loadingPost = true;
                    // use this process to be able to list
                    newBook.authors = _authors.Split(',').ToList();
                    newBook.isbn = Guid.NewGuid().ToString();
                    if (update) await bookManager.Update(newBook);
                    else await bookManager.Add(newBook);
                    loadingPost = false;
                    await currentPage.Navigation.PopAsync(true);
                });
            }
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
