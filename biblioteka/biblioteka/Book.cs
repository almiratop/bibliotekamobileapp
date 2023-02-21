using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace biblioteka
{
    public class Book : INotifyPropertyChanged
    {
        private string name;
        private string author;
        private string text;
        private string saw;
        private string save;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }
        public string Author
        {
            get { return author; }
            set
            {
                if (author != value)
                {
                    author = value;
                    OnPropertyChanged("Author");
                }
            }
        }
        public string Text
        {
            get { return text; }
            set
            {
                if (text != value)
                {
                    text = value;
                    OnPropertyChanged("Text");
                }
            }
        }
        public string Saw
        {
            get { return saw; }
            set
            {
                if (saw != value)
                {
                    saw = value;
                    OnPropertyChanged("Saw");
                }
            }
        }
        public string Save
        {
            get { return save; }
            set
            {
                if (save != value)
                {
                    save = value;
                    OnPropertyChanged("Save");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
