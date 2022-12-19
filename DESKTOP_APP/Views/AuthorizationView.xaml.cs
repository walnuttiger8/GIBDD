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
                MessageBox.Show("Успешная авторизация!");
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
    }
}
