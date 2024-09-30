using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace Employee_Manager.Database
{
    public class EmployeeRepository
    {
        public ObservableCollection<Employee> GetAllEmployees()
        {
            var employees = new ObservableCollection<Employee>();

            using (SqlConnection connection = DatabaseHelper.GetConnection()) {
                connection.Open();
                string query = "SELECT * FROM Employees";
                SqlCommand command = new SqlCommand(query, connection);
                using (SqlDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        var employee = new Employee
                        {
                            Id = (int)reader["Id"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Position = reader["Position"].ToString(),
                            Salary = (decimal)reader["Salary"]
                        };
                        employees.Add(employee);
                    }
                }
            }

            return employees;
        }

        public void AddEmployee(Employee employee)
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection()) {
                connection.Open();
                string query = "INSERT INTO Employees (FirstName, LastName, Position, Salary) VALUES (@FirstName, @LastName, @Position, @Salary)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@Position", employee.Position);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection()) {
                connection.Open();
                string query = "UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Position = @Position, Salary = @Salary WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", employee.Id);
                command.Parameters.AddWithValue("@FirstName", employee.FirstName);
                command.Parameters.AddWithValue("@LastName", employee.LastName);
                command.Parameters.AddWithValue("@Position", employee.Position);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteEmployee(int id)
        {
            using (SqlConnection connection = DatabaseHelper.GetConnection()) {
                connection.Open();
                string query = "DELETE FROM Employees WHERE Id = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
            }
        }
    }

}
