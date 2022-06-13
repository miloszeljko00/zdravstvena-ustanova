using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace zdravstvena_ustanova.View.ManagerMVVM.Model
{
    public class ManagerModel : BindableBase
    {
        private string _fullName;
        public string FullName
        {
            get => _fullName;
            set => SetProperty(ref _fullName, value);
        }
        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        private ImageSource _profileImage;
        public ImageSource ProfileImage
        {
            get => _profileImage;
            set => SetProperty(ref _profileImage, value);
        }

        public ManagerModel(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
            ProfileImage = new BitmapImage(new Uri(App.ProjectPath + "/Resources/img/manager-icon.png"));
        }
    }
}
