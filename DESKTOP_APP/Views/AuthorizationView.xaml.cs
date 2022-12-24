using APPLICATION.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DESKTOP_APP.Views
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationView.xaml
    /// </summary>
    public partial class AuthorizationView : Window
    {
        private readonly AuthorizationService _authorizationService;
        public AuthorizationView()
        {
            InitializeComponent();
            _authorizationService = new AuthorizationService();
        }

        private void authorizeButton_Click(object sender, RoutedEventArgs e)
        {
            var login = loginTextBox.Text;
            var password = passwordBox.Password;

            try
            {
                var user = _authorizationService.Auth(login, password);

                //MessageBox.Show("Успешная авторизация!");
                //var view = new DriverView(new Guid("05DA1F47-39FA-4ADF-A824-004223E51CF8"));
                //var view = new FineView(1);
                //var view = new ExportFSSPView();
                var view = new RegNumberView(1);
                view.Show();
                Close();
            }
            catch (UserNotFoundException)
            {
                MessageBox.Show("Пользователь не найден");
            }
            catch (TooManyAuthorizationAttempts)
            {
                MessageBox.Show("Превышено число попыток. Попробуйте позже");
            }
            catch (AuthenticationFail)
            {
                MessageBox.Show("Неверный пароль");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _authorizationService.Dispose();
        }
    }
}
