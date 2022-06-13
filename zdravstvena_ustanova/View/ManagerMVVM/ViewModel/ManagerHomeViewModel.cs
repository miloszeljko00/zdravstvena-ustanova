using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using zdravstvena_ustanova.View.ManagerMVVM.Model;

namespace zdravstvena_ustanova.View.ManagerMVVM.ViewModel
{
    public class ManagerHomeViewModel
    {
        #region fields
        public ManagerModel ManagerModel { get; set; }
        public MovementGraphModel MovementGraphModel { get; set; }
        #endregion

        #region constructors
        public ManagerHomeViewModel()
        {
            var app = Application.Current as App;

            var name = app.LoggedInUser.Name;
            var surname = app.LoggedInUser.Surname;
            var email = app.LoggedInUser.Email;

            ManagerModel = new ManagerModel(name + " " + surname, email);
            MovementGraphModel = new MovementGraphModel(DateTime.Now.AddDays(-30), DateTime.Now);
        }
        #endregion

        #region commands

        

        #endregion
    }
}
