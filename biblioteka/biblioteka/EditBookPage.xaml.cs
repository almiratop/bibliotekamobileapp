using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace biblioteka
{
    public partial class EditBookPage : ContentPage
    {
        protected internal ObservableCollection<Book> Books { get; set; }

        public EditBookPage()
        {
            InitializeComponent();
            Books = new ObservableCollection<Book>
            {
                new Book {Name="Преступление и наказание", Author="Федор Достоевский", Text = "что то"},
                new Book {Name="Герой нашего времени", Author="Михаил Лермонтов", Text = "что то"},
                new Book {Name="Мастер и Маргарита", Author="Михаил Булгаков", Text = "что то"}
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