
using pz6RPM_Nikolenko.models;
using pz6RPM_Nikolenko.services;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pz6RPM_Nikolenko.Pages
{
    public partial class Autho : Page
    {
        int click;

        public Autho()
        {
            InitializeComponent();
            click = 0;
            tbCaptcha.Visibility = Visibility.Collapsed;
            tblCaptcha.Visibility = Visibility.Collapsed;
        }

        private void btnEnterGuest_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Client(null, null));
        }

        private void GenerateCaptcha()
        {
            tbCaptcha.Visibility = Visibility.Visible;
            tblCaptcha.Visibility = Visibility.Visible;
            string captchaText = CaptchaGenerator.GenerateCaptchaText(6);
            tblCaptcha.Text = captchaText;
            tblCaptcha.TextDecorations = TextDecorations.Strikethrough;
            tbCaptcha.Clear();
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            click += 1;
            string login = tbLogin.Text.Trim();
            string password = tbPassword.Text.Trim();

            string hashedPassword = Hash.HashPassword(password);

            using (RullDBEntities db = new RullDBEntities())
            {
                var user = db.Users
                    .FirstOrDefault(x => x.LoginUser == login && x.PasswordHash == hashedPassword);

                if (click == 1)
                {
                    if (user != null)
                    {
                        MessageBox.Show("Вы вошли под: " + user.Role.NameRole);
                        LoadPage(user.Role.NameRole, user);
                    }
                    else
                    {
                        MessageBox.Show("Вы ввели логин или пароль неверно!");
                        tbPassword.Clear();
                        GenerateCaptcha();
                    }
                }
                else if (click > 1)
                {
                    if (user != null && tbCaptcha.Text.Trim() == tblCaptcha.Text)
                    {
                        MessageBox.Show("Вы вошли под: " + user.Role.NameRole);
                        LoadPage(user.Role.NameRole, user);
                    }
                    else
                    {
                        MessageBox.Show("Введите данные заново или неверная капча!");
                        tbPassword.Clear();
                        tbCaptcha.Clear();
                        GenerateCaptcha();
                    }
                }
            }
        }

        private void LoadPage(string _role, User user)
        {
            click = 0;
            tbLogin.Clear();
            tbPassword.Clear();
            tbCaptcha.Clear();
            tbCaptcha.Visibility = Visibility.Collapsed;
            tblCaptcha.Visibility = Visibility.Collapsed;

            switch (_role)
            {
                case "Клиент":
                case "Менеджер":
                case "Администратор":
                    NavigationService.Navigate(new Client(user, _role));
                    break;
                default:
                    MessageBox.Show("Роль не определена!");
                    break;
            }
        }
    }
}