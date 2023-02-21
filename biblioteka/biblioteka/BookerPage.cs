﻿using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace biblioteka
{
    public class BookerPage : ContentPage
    {
        Label textLabel;
        StackLayout stackLayout;
        Button button, button2;
        Image img;
        Button takePhotoBtn;
        Button getPhotoBtn;
        Entry nameEntry, dateEntry;
        
        public BookerPage()
        {
            stackLayout = new StackLayout();
            stackLayout.BackgroundColor = Color.White;

            textLabel = new Label
            {
                Text = " Добро пожаловать в личный кабинет, Светлана \n Ваш статус: Библиотекарь \n Вы можете изменить личные данные",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                Margin = new Thickness(30, 20, 0, 0),
                TextColor = Color.Black
            };
            nameEntry = new Entry
            {
                Placeholder = "Имя",
                Text = "Светлана",
                Margin = new Thickness(5),
                TextColor = Color.Black
            };
            dateEntry = new Entry
            {
                Placeholder = "Дата рождения",
                Text = "24.02.2000",
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
                Text = "Книги",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Button)),
                BorderWidth = 3,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.EndAndExpand,
                Margin = new Thickness(10),
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
            img = new Image()
            {
                Source = "",
                WidthRequest = 150,
                HeightRequest = 100
            };
            getPhotoBtn.Clicked += GetPhotoAsync;
            takePhotoBtn.Clicked += TakePhotoAsync;

            StackLayout Content2 = new StackLayout
            {
                HorizontalOptions = LayoutOptions.Center,
                Children = {
                    new StackLayout
                    {
                         Children = {takePhotoBtn, getPhotoBtn},
                         Orientation =StackOrientation.Horizontal,
                         HorizontalOptions = LayoutOptions.CenterAndExpand
                    },
                    img
                }
            };

            button.Clicked += OnButtonClicked;
            stackLayout.Children.Add(textLabel);
            stackLayout.Children.Add(nameEntry);
            stackLayout.Children.Add(dateEntry);
            stackLayout.Children.Add(button2);
            stackLayout.Children.Add(Content2);
            stackLayout.Children.Add(button);
            this.Content = stackLayout;
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new EditBookPage());
        }
        async void GetPhotoAsync(object sender, EventArgs e)
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                img.Source = ImageSource.FromFile(photo.FullPath);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }


        async void TakePhotoAsync(object sender, EventArgs e)
        {
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
            }
            catch (Exception ex)
            {
                await DisplayAlert("Сообщение об ошибке", ex.Message, "OK");
            }
        }
    }
}