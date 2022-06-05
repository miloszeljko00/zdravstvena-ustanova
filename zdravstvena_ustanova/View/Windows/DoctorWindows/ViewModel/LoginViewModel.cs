using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using zdravstvena_ustanova.Commands;
using zdravstvena_ustanova.View.Windows.DoctorWindows.View;

namespace zdravstvena_ustanova.View.Windows.DoctorWindows.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public LoginView LoginView { get; set; }
        public RegisterUserView RegisterUserView { get; set; }
        public RelayCommand OpenAnotherWindowCommand { get; set; }
        public RelayCommand OpenDoctorHomePageWindow { get; set; }

        #region PropertyChangedNotifier
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        public LoginViewModel(LoginView loginView)
        {
            LoginView = loginView;
            var app = Application.Current as App;
            OpenAnotherWindowCommand = new RelayCommand(param => ExecuteOpen());
            OpenDoctorHomePageWindow = new RelayCommand(param => ExecuteOpenDoctorHomePageWindow());
        }

        private void ExecuteOpenDoctorHomePageWindow()
        {
            var openDoctorHomePageWindow = new DoctorHomePageWindow();
            LoginView.Close();
            openDoctorHomePageWindow.Show();
        }

        private void ExecuteOpen()
        {
            RegisterUserView = new RegisterUserView();
            RegisterUserView.ShowDialog();
        }
    }
}
