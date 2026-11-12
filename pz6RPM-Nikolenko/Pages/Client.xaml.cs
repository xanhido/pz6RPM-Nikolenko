using pz6RPM_Nikolenko.models;
using System.Windows.Controls;

namespace pz6RPM_Nikolenko.Pages
{
    public partial class Client : Page
    {
        public Client(User user, string role)
        {
            InitializeComponent();

            if (user != null)
            {
                tbWelcome.Text = $"Добро пожаловать, {user.LoginUser}!";
                tbUserInfo.Text = $"Роль: {role}";
            }
            else
            {
                tbWelcome.Text = "Вы вошли как гость";
                tbUserInfo.Text = "Роль: Гость";
            }
        }
    }
}