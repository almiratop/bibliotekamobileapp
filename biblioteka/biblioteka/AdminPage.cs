﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace biblioteka
{
    public class AdminPage : ContentPage
    {
        Label textLabel;
        StackLayout stackLayout;
        Button button, button2, button3;
        Image img;
        Button takePhotoBtn;
        Button getPhotoBtn;
        Entry nameEntry, dateEntry;
        public AdminPage()
        {
            stackLayout = new StackLayout();
            stackLayout.BackgroundColor = Color.White;

            textLabel = new Label
            {
                Text = " Добро пожаловать в личный кабинет, Анатолий \n Ваш статус: Администратор \n Вы можете изменить личные данные",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label)),
                Margin = new Thickness(30, 20, 0, 0),
                TextColor = Color.Black
            };
            nameEntry = new Entry
            {
                Placeholder = "Имя",
                Text = "Анатолий",
                Margin = new Thickness(5),
                TextColor = Color.Black
            };
            dateEntry = new Entry
            {
                Placeholder = "Дата рождения",
                Text = "04.12.1992",
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
            img = new Image()
            {
                Source = "",
                WidthRequest = 150,
                HeightRequest = 100
            };
            getPhotoBtn.Clicked += GetPhotoAsync;
            takePhotoBtn.Clicked += TakePhotoAsync;

            button.Clicked += OnButtonClicked;
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
        private async void OnButton3Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Statistika());
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