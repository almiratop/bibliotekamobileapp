using System;
using Xamarin.Forms;

namespace biblioteka
{
    public partial class MainPage : ContentPage
    {
        Label textLabel;
        Entry loginEntry, passwordEntry;
        Picker picker;
        StackLayout stackLayout;
        Button button, button2;
        public MainPage()
        {
            stackLayout = new StackLayout();
            stackLayout.BackgroundColor = Color.White;
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
                Text = "",
                Margin = new Thickness(10),
                TextColor = Color.Black
            };

            passwordEntry = new Entry
            {
                Placeholder = "Password",
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

            stackLayout.Children.Add(picker);
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
            if(loginEntry.Text != "" && passwordEntry.Text != "" && Convert.ToString(picker.SelectedItem) == "Клиент")
            {
                await Navigation.PushModalAsync(new NavigationPage(new ClientPage()));
            }
            else if (loginEntry.Text != "" && passwordEntry.Text != "" && Convert.ToString(picker.SelectedItem) == "Библиотекарь")
            {
                await Navigation.PushModalAsync(new NavigationPage(new BookerPage()));
            }
            else if (loginEntry.Text != "" && passwordEntry.Text != "" && Convert.ToString(picker.SelectedItem) == "Администратор")
            {
                await Navigation.PushModalAsync(new NavigationPage(new AdminPage()));
            }
            else
            {
                textLabel.Text = "Введите все данные";
            }
        }



    }
}
