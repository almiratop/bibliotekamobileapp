using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace biblioteka
{
    public partial class EditUser : ContentPage
    {
        bool edited = true;
        public User User { get; set; }
        public EditUser(User user)
        {
            InitializeComponent();
            User = user;
            if (user == null)
            {
                User = new User();
                edited = false;
            }
            this.BindingContext = User;
        }
        async void SaveUser(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

            if (edited == false)
            {
                NavigationPage navPage = (NavigationPage)Application.Current.MainPage;
                IReadOnlyList<Page> navStack = navPage.Navigation.NavigationStack;
                EditUserPage homePage = navStack[navPage.Navigation.NavigationStack.Count - 1] as EditUserPage;

                if (homePage != null)
                {
                    homePage.AddUser(User);
                }
            }
        }
    }
}