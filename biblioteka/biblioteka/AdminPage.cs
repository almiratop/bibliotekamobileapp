using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace biblioteka
{
    public class AdminPage : ContentPage
    {
        int Id;
        Label textLabel;
        StackLayout stackLayout;
        Button button, button2, button3;
        Image img;
        Button takePhotoBtn;
        Button getPhotoBtn;
        Entry nameEntry, dateEntry;
        public AdminPage(int id)
        {
            Id = id;
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            string name = "";
            string date = "";
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("select name from user where id = @id", conn);
                cmd1.Parameters.AddWithValue("@id", Id);
                if (cmd1.ExecuteScalar() != null)
                {
                    name = cmd1.ExecuteScalar().ToString();
                }
                MySqlCommand cmd = new MySqlCommand("select date from user where id = @id", conn);
                cmd.Parameters.AddWithValue("@id", Id);
                if (cmd.ExecuteScalar() != null)
                {
                    date = cmd.ExecuteScalar().ToString();
                }
            }
            stackLayout = new StackLayout();
            stackLayout.BackgroundColor = Color.White;

            textLabel = new Label
            {
                Text = " Добро пожаловать в личный кабинет, " + name + " \n Вы можете изменить личные данные",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                Margin = new Thickness(30, 20, 0, 0),
                TextColor = Color.Black
            };
            nameEntry = new Entry
            {
                Placeholder = "Имя",
                Text = name,
                Margin = new Thickness(5),
                TextColor = Color.Black
            };
            dateEntry = new Entry
            {
                Placeholder = "Дата рождения",
                Text = date,
                Margin = new Thickness(5),
                TextColor = Color.Black
            };

            button2 = new Button
            {
                Text = "Сохранить",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button)),
                BorderWidth = 3,
                HorizontalOptions = LayoutOptions.Start,
                Margin = new Thickness(5),
                TextColor = Color.White,
                BackgroundColor = Color.Black

            };

            button = new Button
            {
                Text = "Пользователи",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button)),
                BorderWidth = 3,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.EndAndExpand,
                Margin = new Thickness(5),
                TextColor = Color.White,
                BackgroundColor = Color.Black

            };
            button3 = new Button
            {
                Text = "Статистика скачиваний и просмотров",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button)),
                BorderWidth = 3,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.EndAndExpand,
                Margin = new Thickness(5),
                TextColor = Color.White,
                BackgroundColor = Color.Black

            };

            takePhotoBtn = new Button
            {
                Text = "Сделать фото",
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Button)),
                HorizontalOptions = LayoutOptions.End,
                Margin = new Thickness(5),
                TextColor = Color.White,
                BackgroundColor = Color.Black
            };
            getPhotoBtn = new Button
            {
                Text = "Выбрать фото",
                FontSize = Device.GetNamedSize(NamedSize.Default, typeof(Button)),
                HorizontalOptions = LayoutOptions.End,
                Margin = new Thickness(5),
                TextColor = Color.White,
                BackgroundColor = Color.Black
            };

            string text = "";
            MySqlCommand cmd3 = new MySqlCommand("select photo from user where id = @id", conn);
            cmd3.Parameters.AddWithValue("@id", Id);
            if (cmd3.ExecuteScalar() != null)
            {
                text = cmd3.ExecuteScalar().ToString();

            }
            img = new Image()
            {
                Source = ImageSource.FromFile(text),
                WidthRequest = 150,
                HeightRequest = 100
            };
            getPhotoBtn.Clicked += GetPhotoAsync;
            takePhotoBtn.Clicked += TakePhotoAsync;

            button.Clicked += OnButtonClicked;
            button2.Clicked += OnButton2Clicked;
            button3.Clicked += OnButton3Clicked;
            stackLayout.Children.Add(textLabel);
            stackLayout.Children.Add(nameEntry);
            stackLayout.Children.Add(dateEntry);
            stackLayout.Children.Add(button2);
            stackLayout.Children.Add(img);
            stackLayout.Children.Add(takePhotoBtn);
            stackLayout.Children.Add(getPhotoBtn);
            stackLayout.Children.Add(button);
            stackLayout.Children.Add(button3);
            this.Content = stackLayout;
            
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditUserPage());
        }
        private async void OnButton2Clicked(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("UPDATE user SET name = @name, date = @date where id = @id;", conn);
                cmd.Parameters.AddWithValue("@name", nameEntry.Text);
                cmd.Parameters.AddWithValue("@date", dateEntry.Text);
                cmd.Parameters.AddWithValue("@id", Id);
                cmd.ExecuteNonQuery();
                await Navigation.PushModalAsync(new NavigationPage(new AdminPage(Id)));
            }
        }
        private async void OnButton3Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Statistika());
        }
        async void GetPhotoAsync(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                img.Source = ImageSource.FromFile(photo.FullPath);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE user SET photo = @photo where id = @id;", conn);
                    cmd.Parameters.AddWithValue("@photo", photo.FullPath);
                    cmd.Parameters.AddWithValue("@id", Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }

        async void TakePhotoAsync(object sender, EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = $"xamarin.{DateTime.Now.ToString("dd.MM.yyyy_hh.mm.ss")}.png"
                });
                var newFile = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);
                using (var stream = await photo.OpenReadAsync())
                using (var newStream = File.OpenWrite(newFile))
                    await stream.CopyToAsync(newStream);
                img.Source = ImageSource.FromFile(photo.FullPath);

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE user SET photo = @photo where id = @id;", conn);
                    cmd.Parameters.AddWithValue("@photo", photo.FullPath);
                    cmd.Parameters.AddWithValue("@id", Id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }
    }
}