using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace biblioteka
{
    public class Book : INotifyPropertyChanged
    {
        public int id;
        private string name;
        private string author;
        private string text;
        private int saw;
        private int save;
        private string file;

        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged("Id");
                }
            }
        }
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
        public int Saw
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
        public int Save
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

        public string File
        {
            get { return file; }
            set
            {
                if (file != value)
                {
                    file = value;
                    OnPropertyChanged("File");
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
