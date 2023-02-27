
using System.Data;
using System;
using Xamarin.Forms;
using MySqlConnector;
using System.Xml.Linq;


namespace biblioteka
{
    public partial class BooksPage : ContentPage
    {
        StackLayout stackLayout;
        Button button, button2;
        public BooksPage()
        {
            Title = "Книги";
            stackLayout = new StackLayout();
            stackLayout.BackgroundColor = Color.White;

            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("select * from book", conn);
                MySqlDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    button = new Button
                    {
                        Text = Convert.ToString(reader["name"]),
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                        BorderWidth = 1,
                        HorizontalOptions = LayoutOptions.Fill,
                        Margin = new Thickness(20),
                        TextColor = Color.White,
                        BackgroundColor = Color.Black,
                        ClassId = "But" + Convert.ToString(reader["id"])
                    };
                    button.Clicked += OnButtonClicked;
                    stackLayout.Children.Add(button);
                }
                reader.Close();
            }
            this.Content = stackLayout;
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
                int saw = 0;
                MySqlCommand cmd1 = new MySqlCommand("select saw from book where id = @id", conn);
                cmd1.Parameters.AddWithValue("@id", get);
                if (cmd1.ExecuteScalar() != null)
                {
                    saw = Convert.ToInt32(cmd1.ExecuteScalar());
                }
                saw = saw + 1;
                MySqlCommand cmd = new MySqlCommand("UPDATE book SET saw = @saw where id = @id;", conn);
                cmd.Parameters.AddWithValue("@saw", saw);
                cmd.Parameters.AddWithValue("@id", get);
                cmd.ExecuteNonQuery();
                await Navigation.PushAsync(new BookPage(get));
            }   
        }
        
    }
}