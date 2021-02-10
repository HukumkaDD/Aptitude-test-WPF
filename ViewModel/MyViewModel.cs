using Project.Controls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace Project
{
    public class MyItemComponentViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<MyItemComponent> myItemComponentsFiltred;
        private MyCommand findCommand;
        private string searchString;

        private TextBoxWithPlaceholder textBoxWithPlaceholder;
        public TextBoxWithPlaceholder TextBoxWithPlaceholder
        {
            get { return textBoxWithPlaceholder; }
            set
            {
                textBoxWithPlaceholder = value;
                OnPropertyChanged("TextBoxWithPlaceholder");
            }
        }
        public ObservableCollection<MyItemComponent> MyItemComponents { get; set; }
        public ObservableCollection<MyItemComponent> MyItemComponentsFiltred {
            get { return myItemComponentsFiltred; }
            set
            {
                myItemComponentsFiltred = value;
                OnPropertyChanged("MyItemComponentsFiltred");
            }
        }

        public string SearchString
        {
            get { return searchString; }
            set
            {
                searchString=value;
                OnPropertyChanged("SearchString");
            }
        }

        public MyCommand FindCommand
        {
            get
            {
                return findCommand ?? (findCommand = new MyCommand(obj => FindByText()));
            }
        }
        public void FindByText()
        {
            MyItemComponentsFiltred = new ObservableCollection<MyItemComponent> { };
            foreach (var item in MyItemComponents.Where(x => x.FirstText.Contains(TextBoxWithPlaceholder.SearchString ?? "") || x.SecondText.Contains(TextBoxWithPlaceholder.SearchString ?? "")))
            {
                MyItemComponentsFiltred.Add(item);
            }
            if (MyItemComponentsFiltred.Count == 0) MessageBox.Show("Нет элементов элементов, удовлетворяющих условиям поиска.");
        }
        public MyItemComponentViewModel()
        {

            TextBoxWithPlaceholder = new TextBoxWithPlaceholder();
            MyItemComponentsFiltred = new ObservableCollection<MyItemComponent> {};
            MyItemComponents = new ObservableCollection<MyItemComponent>
            {
                new MyItemComponent { ImageName="Resources/image1.png",FirstText="Текст11", SecondText="Текст12"  },
                new MyItemComponent { ImageName="Resources/image2.jpeg",FirstText="Текст21", SecondText="Текст22"  },
                new MyItemComponent { ImageName="Resources/image3.png",FirstText="Текст31", SecondText="Текст32"  },
            };
            MyItemComponentsFiltred = MyItemComponents;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }

    public class MyCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public MyCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }
    }
}