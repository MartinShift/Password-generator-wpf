using Microsoft.Win32;
using My.BaseViewModels;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Pass_generator_wpf.ViewModels
{
    public class MainWindowViewModel : NotifyPropertyChangedBase
    {
        public MainWindowViewModel()
        {
            Passwords = new ObservableCollection<string>();
        }
        private bool _numbersCheck = false;
        public bool NumbersCheck
        {
            get => _numbersCheck;
            set { _numbersCheck = value; OnPropertyChanged(nameof(NumbersCheck)); }
        }
        private bool _smallLettersCheck = false;
        public bool SmallLettersCheck
        {
            get => _smallLettersCheck;
            set
            {
                _smallLettersCheck = value;
                OnPropertyChanged(nameof(SmallLettersCheck));
            }
        }
        private bool _largeLettersCheck = false ;
        public bool LargeLettersCheck
        {
            get => _largeLettersCheck;
            set { _largeLettersCheck = value; OnPropertyChanged(nameof(LargeLettersCheck)); }
        }
        private bool _symbolsCheck = false;
        public bool SymbolsCheck
        {
            get => _symbolsCheck;
            set { _symbolsCheck = value; OnPropertyChanged(nameof(SymbolsCheck)); }

        }
        private string _addSymbols = "";
        public string AddSymbols
        {
            get => _addSymbols;
            set { _addSymbols = value; OnPropertyChanged(nameof(AddSymbols)); }
        }
        private int _length = 0;
        public int Length
        {
            get => _length;
            set { _length = value; OnPropertyChanged(nameof(Length)); }
        }
        public ObservableCollection<string> Passwords { get; set; }
        private string _selected = "";
        public string Selected { get => _selected; set { _selected = value; OnPropertyChanged(nameof(Selected)); } }
        public ICommand Generate => new RelayCommand(
            x =>
            {
                Passwords.Clear();
                try
                {
                    string from = "";
                    string res = "";
                    from += _addSymbols;
                    if (NumbersCheck)
                    {
                        from += "1234567890";
                    }
                    if (SmallLettersCheck)
                    {
                        from += "abcdefghijklmnopqrstuvwxyz";
                    }
                    if (LargeLettersCheck)
                    {
                        from += "abcdefghijklmnopqrstuvwxyz".ToUpper();
                    }
                    if (SymbolsCheck)
                    {
                        from += "!@#$%^&*()_+=-[]{};:\'\"\\|/?.>,<`~";
                    }
                    from = string.Join("", from.Distinct());
                    var temp = new Random();
                    for (int a = 0; a < 15; a++)
                    {
                        for (int i = 0; i < Length; i++)
                        {
                            res += from[temp.Next(0, from.Length)];
                        }
                        Passwords.Add(res);
                        res = "";
                    }
                    OnPropertyChanged(nameof(Passwords));
                }
                catch { }
            },
            x => Length >= 4 && Length <= 32);
        public ICommand Copy => new RelayCommand(
        x =>
        {
            Clipboard.SetText(Selected);
        }, x => Selected != "");
        public ICommand Save => new RelayCommand(
        x =>
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text file (*.txt)|*.txt|C# file (*.cs)|*.cs";
            saveFileDialog.InitialDirectory = @"D:\Mein progectos\Password generator";
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllText(saveFileDialog.FileName,Selected);
        }, x => Selected != "");

        
    }
}
