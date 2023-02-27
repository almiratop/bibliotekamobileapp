using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
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
            int idd;
            if (edited == true)
            {
                if (nameEntry.Text != null && dateEntry.Text != null && loginEntry.Text != null && passwordEntry.Text != null && roleEntry.Text != null)
                {
                    MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                        idd = User.Id;
                        MySqlCommand cmd = new MySqlCommand("UPDATE user SET name = @name, date = @date, role = @role, password = @password, login = @login where id = @id;", conn);
                        cmd.Parameters.AddWithValue("@name", nameEntry.Text);
                        cmd.Parameters.AddWithValue("@date", dateEntry.Text);
                        cmd.Parameters.AddWithValue("@role", roleEntry.Text);
                        cmd.Parameters.AddWithValue("@login", loginEntry.Text);
                        cmd.Parameters.AddWithValue("@password", passwordEntry.Text);
                        cmd.Parameters.AddWithValue("@id", idd);
                        cmd.ExecuteNonQuery();
                        await Navigation.PopAsync();
                    }
                }
                else { await DisplayAlert("Пустые поля", "Заполните все поля", "OK"); }
                await Navigation.PopAsync();
            }

            if (edited == false)
            {
                if (nameEntry.Text != null && dateEntry.Text != null && loginEntry.Text != null && passwordEntry.Text != null && roleEntry.Text != null)
                {
                    MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("insert into user (name, date, role, login, password) values (@name, @date, @role, @login, @password);", conn);
                        cmd.Parameters.AddWithValue("@name", nameEntry.Text);
                        cmd.Parameters.AddWithValue("@date", dateEntry.Text);
                        cmd.Parameters.AddWithValue("@role", roleEntry.Text);
                        cmd.Parameters.AddWithValue("@login", loginEntry.Text);
                        cmd.Parameters.AddWithValue("@password", passwordEntry.Text);
                        cmd.ExecuteNonQuery();
                        await Navigation.PopAsync();
                    }
                }
                else { await DisplayAlert("Пустые поля", "Заполните все поля", "OK"); }
            }
        }

        async void DeleteUser(object sender, EventArgs e)
        {
            int idd;
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                idd = User.Id;
                MySqlCommand cmd = new MySqlCommand("DELETE FROM user where id = @id;", conn);
                cmd.Parameters.AddWithValue("@id", idd);
                cmd.ExecuteNonQuery();
                await Navigation.PopAsync();
            }
        }
    }
}