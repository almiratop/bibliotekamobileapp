using MySqlConnector;
using Mysqlx;
using System;
using System.Collections.ObjectModel;
using System.Data;
using Xamarin.Forms;

namespace biblioteka
{
    public partial class EditBookPage : ContentPage
    {
        protected internal ObservableCollection<Book> Books { get; set; }

        public EditBookPage()
        {
            InitializeComponent();
            Books = new ObservableCollection<Book> { };
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("select * from book", conn);
                MySqlDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string name = Convert.ToString(reader["name"]);
                    string author = Convert.ToString(reader["author"]);
                    string text = Convert.ToString(reader["text"]);
                    int saw = Convert.ToInt32(reader["saw"]);
                    int save = Convert.ToInt32(reader["save"]);
                    string file = Convert.ToString(reader["file"]);
                    Books.Add(new Book { Id = id, Name = name, Author = author, Text = text, Saw = saw, Save = save, File = file });
                }
                reader.Close();
            }
            booksList1.BindingContext = Books;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Books = new ObservableCollection<Book> { };
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("select * from book", conn);
                MySqlDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string name = Convert.ToString(reader["name"]);
                    string author = Convert.ToString(reader["author"]);
                    string text = Convert.ToString(reader["text"]);
                    int saw = Convert.ToInt32(reader["saw"]);
                    int save = Convert.ToInt32(reader["save"]);
                    string file = Convert.ToString(reader["file"]);
                    Books.Add(new Book { Id = id, Name = name, Author = author, Text = text, Saw = saw, Save = save, File = file });
                }
                reader.Close();
            }
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
    }
}