using EmployeeClient.Services;
using System.Linq;

namespace EmployeeClient
{

    public partial class Form1 : Form
    {
        private readonly ApiClient _apiClient = new ApiClient();

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                var result = await _apiClient.LoginAsync(
                    txtUsername.Text,
                    txtPassword.Text);

                MessageBox.Show(
                    "Login berhasil!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                txtUsername.Text = "";
                txtPassword.Text = "";

                var form2 = new Form2(_apiClient);

                form2.FormClosed += (_, _) =>
                {
                    this.Show();
                };

                this.Hide();

                form2.Show();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show(
                    $"Gagal menghubungi API:\n{ex.Message}",
                    "API Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Login gagal:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
