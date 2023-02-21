using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace biblioteka
{
    public partial class Statistika : ContentPage
    {
        protected internal ObservableCollection<Book> Books { get; set; }
        public Statistika()
        {
            InitializeComponent();
            Books = new ObservableCollection<Book>
            {
                new Book {Name="Преступление и наказание", Saw="308", Save = "5"},
                new Book {Name="Герой нашего времени", Saw="250", Save = "10"},
                new Book {Name="Мастер и Маргарита", Saw="105", Save = "1"}
            };
            booksList1.BindingContext = Books;
        }
        private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            Book selectedBook = args.SelectedItem as Book;
            if (selectedBook != null)
            {
                booksList1.SelectedItem = null;
                await Navigation.PushAsync(new EditBook(selectedBook));
            }
        }
        private async void AddButton_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditBook(null));
        }

        protected internal void AddBook(Book book)
        {
            Books.Add(book);
        }
    }
}