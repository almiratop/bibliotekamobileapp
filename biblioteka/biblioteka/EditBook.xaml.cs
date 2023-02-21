using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace biblioteka
{
    public partial class EditBook : ContentPage
    {
        bool edited = true;
        public Book Book { get; set; }
        public EditBook(Book book)
        {
            InitializeComponent();

            Book = book;
            if (book == null)
            {
                Book = new Book();
                edited = false;
            }
            this.BindingContext = Book;
        }

        async void SaveBook(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

            if (edited == false)
            {
                NavigationPage navPage = (NavigationPage)Application.Current.MainPage;
                IReadOnlyList<Page> navStack = navPage.Navigation.NavigationStack;
                EditBookPage homePage = navStack[navPage.Navigation.NavigationStack.Count - 1] as EditBookPage;

                if (homePage != null)
                {
                    homePage.AddBook(Book);
                }
            }
        }

    }
}