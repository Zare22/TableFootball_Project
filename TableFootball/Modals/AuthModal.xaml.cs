using System.Windows;

namespace TableFootball.Modals
{
    public partial class AuthModal : Window
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public AuthModal()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Username = txtUsername.Text.Trim();
            Password = txtPassword.Password;
            
            this.Close();
        }
    }
}
