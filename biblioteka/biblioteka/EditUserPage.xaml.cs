using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace biblioteka
{
    
    public partial class EditUserPage : ContentPage
    {
        protected internal ObservableCollection<User> Users { get; set; }
        public EditUserPage()
        {
            InitializeComponent();
            Users = new ObservableCollection<User>
            {
                new User {Name="Альмира", Date = "21.06.2004", Role="Клиент", Login = "almira", Password = "123"},
                new User {Name="Светлана", Date = "20.05.2000", Role="Библиотекарь", Login = "sveta123", Password = "123"},
            };
            userList.BindingContext = Users;
        }
        private async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            User selecteduser = args.SelectedItem as User;
            if (selecteduser != null)
            {
                userList.SelectedItem = null;
                await Navigation.PushAsync(new EditUser(selecteduser));
            }
        }
        private async void AddButton_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditBook(null));
        }
        protected internal void AddUser(User user)
        {
            Users.Add(user);
        }
    }
}