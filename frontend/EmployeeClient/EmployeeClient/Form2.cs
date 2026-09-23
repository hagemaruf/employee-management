using EmployeeClient.Models;
using EmployeeClient.Services;

namespace EmployeeClient;

public partial class Form2 : Form
{
    private readonly ApiClient _apiClient;

    public Form2(ApiClient apiClient)
    {
        InitializeComponent();

        _apiClient = apiClient;
    }

    private async void Form2_Load(object sender, EventArgs e)
    {
        await LoadEmployeesAsync();
    }

    private async void btnAddEmployee_Click(object sender, EventArgs e)
    {
        using var form3 = new Form3(_apiClient);

        if (form3.ShowDialog(this) == DialogResult.OK)
        {
            await LoadEmployeesAsync();
        }
    }

    private async Task LoadEmployeesAsync()
    {
        try
        {
            var employees = await _apiClient.GetEmployeesAsync();

            dgvEmployees.DataSource = employees;

            dgvEmployees.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
        }
        catch (HttpRequestException ex)
        {
            MessageBox.Show(
                $"Gagal mengambil data employee:\n{ex.Message}",
                "API Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                $"Error:\n{ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private async void btnEditEmployee_Click(object sender, EventArgs e)
    {
        if (dgvEmployees.CurrentRow == null)
        {
            MessageBox.Show(
                "Pilih employee yang ingin diedit.",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            return;
        }

        if (dgvEmployees.CurrentRow.DataBoundItem is not Employee employee)
        {
            MessageBox.Show(
                "Data employee tidak valid.",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            return;
        }

        using var form3 = new Form3(
            _apiClient,
            employee);

        if (form3.ShowDialog(this) == DialogResult.OK)
        {
            await LoadEmployeesAsync();
        }
    }

    private async void btnDeleteEmployee_Click(object sender, EventArgs e)
    {
        if (dgvEmployees.CurrentRow == null)
        {
            MessageBox.Show(
                "Pilih employee yang ingin dihapus.",
                "Information",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            return;
        }

        if (dgvEmployees.CurrentRow.DataBoundItem is not Employee employee)
        {
            MessageBox.Show(
                "Data employee tidak valid.",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            return;
        }

        if (!employee.Id.HasValue)
        {
            MessageBox.Show(
                "ID employee tidak valid.",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);

            return;
        }

        var confirm = MessageBox.Show(
            $"Yakin ingin menghapus employee berikut?\n\n" +
            $"ID: {employee.Id}\n" +
            $"Name: {employee.Name}\n" +
            $"Email: {employee.Email}",
            "Confirm Delete",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning);

        if (confirm != DialogResult.Yes)
        {
            return;
        }

        try
        {
            await _apiClient.DeleteEmployeeAsync(employee.Id.Value);

            MessageBox.Show(
                "Employee berhasil dihapus.",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            await LoadEmployeesAsync();
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
                $"Gagal menghapus employee:\n{ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    private void btnLogout_Click(object sender, EventArgs e)
    {
        var confirm = MessageBox.Show(
            "Yakin ingin logout?",
            "Confirm Logout",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

        if (confirm != DialogResult.Yes)
        {
            return;
        }

        _apiClient.Logout();

        Close();
    }
}