using EmployeeClient.Services;
using System.Linq;

namespace EmployeeClient
{

    public partial class Form1 : Form
    {
        private readonly ApiClient _apiClient = new ApiClient("http://localhost:8080");

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender,  EventArgs e)
        {
            bool success =
                await _apiClient.LoginAsync(
                    txtUsername.Text,
                    txtPassword.Text);

            if (!success)
            {
                MessageBox.Show(
                    "Username atau password salah.",
                    "Login",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return;
            }

            MessageBox.Show(
                "Login berhasil.",
                "Login",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            var form2 = new Form2(_apiClient);

            form2.Show();

            Hide();
        }
    }
}
