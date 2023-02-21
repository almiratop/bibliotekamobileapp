using System;
using Xamarin.Forms;

namespace biblioteka
{
    public class RegistationPage : ContentPage
    {
        Label textLabel, textLabel2;
        Entry loginEntry, passwordEntry, passwordEntry2, nameEntry, dateEntry;
        Picker picker;
        StackLayout stackLayout;
        Button button;
        public RegistationPage()
        {
            Title = "Регистрация";
            stackLayout = new StackLayout();
            stackLayout.BackgroundColor = Color.White;
            nameEntry = new Entry
            {
                Placeholder = "Имя",
                Margin = new Thickness(10),
                TextColor = Color.Black

            };
            dateEntry = new Entry
            {
                Placeholder = "Дата рождения",
                Margin = new Thickness(10),
                TextColor = Color.Black
            };
            picker = new Picker
            {
                Title = "Роль",
                Margin = new Thickness(10),
                TextColor = Color.Black
            };
            picker.Items.Add("Клиент");
            picker.Items.Add("Библиотекарь");
            picker.Items.Add("Администратор");


            loginEntry = new Entry
            {
                Placeholder = "Login",
                Margin = new Thickness(10),
                TextColor = Color.Black
            };

            passwordEntry = new Entry
            {
                Placeholder = "Password",
                IsPassword = true,
                Margin = new Thickness(10),
                TextColor = Color.Black

            };

            passwordEntry2 = new Entry
            {
                Placeholder = "Password",
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
            passwordEntry.TextChanged += passwordEntry_TextChanged;
            passwordEntry2.TextChanged += passwordEntry_TextChanged;

            button = new Button
            {
                Text = "Регистрация",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(20),
                TextColor = Color.White,
                BackgroundColor = Color.Black

            };
            button.Clicked += OnButtonClicked;


            textLabel2 = new Label
            {
                Text = "",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Black
            };

            stackLayout.Children.Add(nameEntry);
            stackLayout.Children.Add(dateEntry);
            stackLayout.Children.Add(picker);
            stackLayout.Children.Add(loginEntry);
            stackLayout.Children.Add(passwordEntry);
            stackLayout.Children.Add(passwordEntry2);
            stackLayout.Children.Add(textLabel);
            stackLayout.Children.Add(button);
            stackLayout.Children.Add(textLabel2);
            this.Content = stackLayout;

        }
        void passwordEntry_TextChanged(object sender, EventArgs e)
        {
            if (passwordEntry.Text != passwordEntry2.Text) { textLabel.Text = "Пароли не совпадают"; }
            else { textLabel.Text = ""; }
        }

        private async void OnButtonClicked(object sender, System.EventArgs e)
        {
            if (nameEntry.Text != "" && dateEntry.Text != "" && loginEntry.Text != "" && passwordEntry.Text != "" && passwordEntry.Text == passwordEntry2.Text && picker.SelectedItem != null)
            {
                await Navigation.PopAsync();
            }
            else { textLabel.Text = "Проверьте данные"; }
        }

    }
}