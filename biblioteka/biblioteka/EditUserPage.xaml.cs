using MySqlConnector;
using System;
using System.Collections.ObjectModel;
using System.Data;
using Xamarin.Forms;

namespace biblioteka
{
    
    public partial class EditUserPage : ContentPage
    {
        protected internal ObservableCollection<User> Users { get; set; }
        public EditUserPage()
        {
            InitializeComponent();
            Users = new ObservableCollection<User>{};
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("select * from user", conn);
                MySqlDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string name = Convert.ToString(reader["name"]);
                    string date = Convert.ToString(reader["date"]);
                    string role = Convert.ToString(reader["role"]);
                    string login = Convert.ToString(reader["login"]);
                    string password = Convert.ToString(reader["password"]);
                    Users.Add(new User { Id = id, Name = name, Date = date, Role = role, Login = login, Password = password});
                }
                reader.Close();
            }
            userList.BindingContext = Users;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Users = new ObservableCollection<User> { };
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("select * from user", conn);
                MySqlDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string name = Convert.ToString(reader["name"]);
                    string date = Convert.ToString(reader["date"]);
                    string role = Convert.ToString(reader["role"]);
                    string login = Convert.ToString(reader["login"]);
                    string password = Convert.ToString(reader["password"]);
                    Users.Add(new User { Id = id, Name = name, Date = date, Role = role, Login = login, Password = password });
                }
                reader.Close();
            }
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
            await Navigation.PushAsync(new EditUser(null));
        }
        protected internal void AddUser(User user)
        {
            Users.Add(user);
        }
    }
}