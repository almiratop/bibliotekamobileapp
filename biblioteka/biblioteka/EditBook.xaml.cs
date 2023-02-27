using MySqlConnector;
using System;
using System.Data;
using System.IO;
using Windows.Storage;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Runtime;
using System.Collections.Generic;

namespace biblioteka
{
    public partial class EditBook : ContentPage
    {
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
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
            int idd;
            if (edited == true)
            {
                if (nameEntry.Text != null && authorEntry.Text != null)
                {
                    MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                        idd = Book.Id;
                        MySqlCommand cmd = new MySqlCommand("UPDATE book SET name = @name, author = @author, text = @text, file = @file where id = @id;", conn);
                        cmd.Parameters.AddWithValue("@name", nameEntry.Text);
                        cmd.Parameters.AddWithValue("@author", authorEntry.Text);
                        cmd.Parameters.AddWithValue("@text", textEntry.Text);
                        cmd.Parameters.AddWithValue("@file", fileEntry.Text);
                        cmd.Parameters.AddWithValue("@id", idd);
                        cmd.ExecuteNonQuery();
                        await Navigation.PopAsync();
                    }
                }
                else { await DisplayAlert("Пустые поля", "Заполните все поля", "OK"); }
            }

            if (edited == false)
            {
                if (nameEntry.Text != null && authorEntry.Text != null)
                {
                    MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                        MySqlCommand cmd = new MySqlCommand("insert into book (name, author, text, saw, save, file) values (@name, @author, @text, @saw, @save, @file);", conn);
                        cmd.Parameters.AddWithValue("@name", nameEntry.Text);
                        cmd.Parameters.AddWithValue("@author", authorEntry.Text);
                        cmd.Parameters.AddWithValue("@text", textEntry.Text);
                        cmd.Parameters.AddWithValue("@saw", 0);
                        cmd.Parameters.AddWithValue("@save", 0);
                        cmd.Parameters.AddWithValue("@file", fileEntry.Text);
                        cmd.ExecuteNonQuery();
                        await Navigation.PopAsync();
                    }
                }
                else { await DisplayAlert("Пустые поля", "Заполните все поля", "OK"); }
            }
        }

        async void DeleteBook(object sender, EventArgs e)
        {
            int idd;
            MySqlConnection conn = new MySqlConnection("server=127.0.0.1;port=3306;database=mydb;user id=root;password=1234;charset=utf8;Pooling=false;SslMode=None;");
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
                idd = Book.Id;
                MySqlCommand cmd = new MySqlCommand("DELETE FROM BOOK where id = @id;", conn);
                cmd.Parameters.AddWithValue("@id", idd);
                cmd.ExecuteNonQuery();
                await Navigation.PopAsync();
            }
        }


        async void Open(object sender, EventArgs e)
        {
            //string filename = fileEntry.Text;
            //if (String.IsNullOrEmpty(filename)) return;
            //if (File.Exists(Path.Combine(folderPath, filename)))
            //{
            //    bool isRewrited = await DisplayAlert("Подтверждение", "Файл уже существует, перезаписать его?", "Да", "Нет");
            //    if (isRewrited == false) return;
            //}
            //File.WriteAllText(Path.Combine(folderPath, filename), textEntry.Text);
            
            try
            {
                var result = await FilePicker.PickAsync();
                if (result != null)
                {
                    fileEntry.Text = Convert.ToString(result.FileName);
                    string path = result.FullPath;
                    string text = File.ReadAllText(Path.Combine(folderPath, fileEntry.Text));
                    text = text.Substring(0, 1000);
                    textEntry.Text = text;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.ToString(), "OK");
            }
        }
    }
}