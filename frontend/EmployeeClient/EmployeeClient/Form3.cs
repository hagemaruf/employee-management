using EmployeeClient.Models;
using EmployeeClient.Services;

namespace EmployeeClient;

public partial class Form3 : Form
{
    private readonly ApiClient _apiClient;
    private readonly long? _employeeId;

    public Form3(ApiClient apiClient, Employee? employee = null)
    {
        InitializeComponent();

        _apiClient = apiClient;

        if (employee != null)
        {
            _employeeId = employee.Id;

            txtName.Text = employee.Name;
            txtEmail.Text = employee.Email;
            txtDepartment.Text = employee.Department;

            Text = "Edit Employee";
            btnSave.Text = "Update";
        }
        else
        {
            Text = "Add Employee";
            btnSave.Text = "Save";
        }
    }

    private async void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            var employee = new Employee
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                Department = txtDepartment.Text
            };

            Employee result;

            if (_employeeId.HasValue)
            {
                result = await _apiClient.UpdateEmployeeAsync(
                    _employeeId.Value,
                    employee);
            }
            else
            {
                result = await _apiClient.CreateEmployeeAsync(
                    employee);
            }

            MessageBox.Show(
                $"Employee berhasil disimpan.\n\n" +
                $"ID: {result.Id}\n" +
                $"Name: {result.Name}",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            DialogResult = DialogResult.OK;
            Close();
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
                $"Gagal menyimpan employee:\n{ex.Message}",
                "Error",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
    
    private void btnCancel_Click(object sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
    
}
