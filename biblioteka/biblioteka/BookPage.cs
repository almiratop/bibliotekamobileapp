using MySqlConnector;
using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Xml.Linq;
using Xamarin.Forms;


namespace biblioteka
{
    public class BookPage : ContentPage
    {
        int Id;
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        Label textLabel, textLabel2;
        Button button;
        StackLayout stackLayout;

        public BookPage(int id)
        {
            Id = id;
            Title = "Книга";
            stackLayout = new StackLayout();
            stackLayout.BackgroundColor = Color.White;

            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("select * from book where id=@id", conn);
                cmd1.Parameters.AddWithValue("@id", Id);
                MySqlDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    string filen = Convert.ToString(reader["file"]);

                    textLabel = new Label
                    {
                        Text = Convert.ToString(reader["name"]),
                        FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                        TextColor = Color.Black,
                        Margin = new Thickness(10),
                        HorizontalOptions = LayoutOptions.Center
                    };

                    button = new Button
                    {
                        Text = "Скачать",
                        FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button)),
                        BorderWidth = 1,
                        HorizontalOptions = LayoutOptions.Center,
                        Margin = new Thickness(20),
                        TextColor = Color.White,
                        BackgroundColor = Color.Black,
                        ClassId = "But" + Convert.ToString(reader["id"])
                    };
                    button.Clicked += OnButtonClicked;

                    textLabel2 = new Label
                    {
                        Text = File.ReadAllText(Path.Combine(folderPath, filen)),
                        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                        TextColor = Color.Black,
                        Margin = new Thickness(10),
                        HorizontalOptions = LayoutOptions.Center
                    };

                    stackLayout.Children.Add(textLabel);
                    stackLayout.Children.Add(button);
                    stackLayout.Children.Add(textLabel2);
                    ScrollView scrollView = new ScrollView();
                    scrollView.Content = stackLayout;
                    this.Content = scrollView;
                }
                reader.Close();
            }

        }
        

        private async void OnButtonClicked(object sender, System.EventArgs e)
        {
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                Button button = (Button)sender;
                string text = button.ClassId;
                text = text.Replace("But", "");
                int get = Convert.ToInt32(text);
                int save = 0;
                MySqlCommand cmd1 = new MySqlCommand("select save from book where id = @id", conn);
                cmd1.Parameters.AddWithValue("@id", get);
                if (cmd1.ExecuteScalar() != null)
                {
                    save = Convert.ToInt32(cmd1.ExecuteScalar());
                }
                save = save + 1;
                MySqlCommand cmd = new MySqlCommand("UPDATE book SET save = @save where id = @id;", conn);
                cmd.Parameters.AddWithValue("@save", save);
                cmd.Parameters.AddWithValue("@id", get);
                cmd.ExecuteNonQuery();
                await DisplayAlert("Успешное скачивание", "Проверьте папку загрузки", "ОК");
            }
        }
    }
}