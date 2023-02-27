using MySqlConnector;
using System;
using System.Data;
using System.Xml.Linq;
using Windows.System;
using Xamarin.Forms;

namespace biblioteka
{
    public partial class MainPage : ContentPage
    {
        Label textLabel;
        Entry loginEntry, passwordEntry;
        StackLayout stackLayout;
        Button button, button2;
        public MainPage()
        {
            stackLayout = new StackLayout();
            stackLayout.BackgroundColor = Color.White;
            
            loginEntry = new Entry
            {
                Placeholder = "Логин",
                Text = "",
                Margin = new Thickness(10),
                TextColor = Color.Black
            };

            passwordEntry = new Entry
            {
                Placeholder = "Пароль",
                Text = "",
                IsPassword = true,
                Margin = new Thickness(10),
                TextColor = Color.Black

            };

            textLabel = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                TextColor = Color.Black
            };

            button = new Button
            {
                Text = "Регистрация",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Fill,
                Margin = new Thickness(20),
                TextColor = Color.White,
                BackgroundColor = Color.Black

            };
            button.Clicked += OnButtonClicked;

            button2 = new Button
            {
                Text = "Вход",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Fill,
                Margin = new Thickness(20),
                TextColor = Color.White,
                BackgroundColor = Color.Black
            };

            button2.Clicked += OnButton2Clicked;

            stackLayout.Children.Add(loginEntry);
            stackLayout.Children.Add(passwordEntry);
            stackLayout.Children.Add(textLabel);
            stackLayout.Children.Add(button2);
            stackLayout.Children.Add(button);
            this.Content = stackLayout;
        }
        private async void OnButtonClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new RegistationPage());
        }
        private async void OnButton2Clicked(object sender, System.EventArgs e)
        {
            //await Navigation.PushModalAsync(new NavigationPage(new AdminPage(Convert.ToInt32(3))));
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            string name = "";
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    int id = 0;
                    string role = "";
                    conn.Open();
                    if (loginEntry.Text != "" && passwordEntry.Text != "")
                    {
                        MySqlCommand cmd1 = new MySqlCommand("select * from user where login = @login and password = @password", conn);
                        cmd1.Parameters.AddWithValue("@login", loginEntry.Text);
                        cmd1.Parameters.AddWithValue("@password", passwordEntry.Text);
                        MySqlDataReader reader = cmd1.ExecuteReader();
                        while (reader.Read())
                        {
                            id = Convert.ToInt32(reader["id"]);
                            role = Convert.ToString(reader["role"]);
                        }
                        reader.Close();
                        if (role != "")
                        {
                            if (role == "Клиент") { await Navigation.PushModalAsync(new NavigationPage(new ClientPage(Convert.ToInt32(id)))); }
                            if (role == "Библиотекарь") { await Navigation.PushModalAsync(new NavigationPage(new BookerPage(Convert.ToInt32(id)))); }
                            if (role == "Администратор") { await Navigation.PushModalAsync(new NavigationPage(new AdminPage(Convert.ToInt32(id)))); }
                        }
                        else { await DisplayAlert("Пользователя не существует", "", "OK"); }
                    }
                    else
                    {
                        await DisplayAlert("Пустые поля", "Заполните все поля", "OK");
                    }
                }

            }
            catch (MySqlException ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.ToString(), "OK");
            }
        }

    }
}
