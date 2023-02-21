using Xamarin.Forms;

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
            button = new Button
            {
                Text = "Преступление и наказание",
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
                Text = "Герой нашего времени",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Fill,
                Margin = new Thickness(20),
                TextColor = Color.White,
                BackgroundColor = Color.Black
            };

            button2.Clicked += OnButton2Clicked;
            stackLayout.Children.Add(button);
            stackLayout.Children.Add(button2);
            this.Content = stackLayout;
        }
        private async void OnButtonClicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new BookPage(button.Text));
        }
        private async void OnButton2Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new BookPage(button2.Text));
        }
    }
}