using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cau_1.Model;

namespace Cau_1.DAL
{
    public class EmployeeDAL : DBConnection
    {
        public List<Employee> ReadCustomer()
        {
            SqlConnection conn = CreateConnection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SelectAll", conn);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();

            EmployeeDAL employee = new EmployeeDAL();
            DepartmentDAL department = new DepartmentDAL();
            List<Employee> lstEmployee = new List<Employee>();
            while (reader.Read())
            {
                Employee emp = new Employee();
                emp.IdEmployee = reader["IdEmployee"].ToString();
                emp.Name = reader["Name"].ToString();
                emp.DateBirth = DateTime.Parse(reader["DateBirth"].ToString());
                emp.Gender = reader["Gender"].ToString();
                emp.PlaceBirth = reader["PlaceBirth"].ToString();
                emp.Department = department.ReadArea(int.Parse(reader["IdDepartment"].ToString()));
                lstEmployee.Add(emp);
            }
            conn.Close();
            return lstEmployee;
        }
        public void NewEmployee(Employee emp)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("InsertEmployee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IdEmployee", emp.IdEmployee));
            cmd.Parameters.Add(new SqlParameter("@Name", emp.Name));
            cmd.Parameters.Add(new SqlParameter("@DateBirth", emp.DateBirth));
            cmd.Parameters.Add(new SqlParameter("@Gender", emp.Gender));
            cmd.Parameters.Add(new SqlParameter("@PlaceBirth", emp.PlaceBirth));
            cmd.Parameters.Add(new SqlParameter("@IdDepartment", emp.Department.IdDepartment));


            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void EditEmployee(Employee emp)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("UpdateEmployee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IdEmployee", emp.IdEmployee));
            cmd.Parameters.Add(new SqlParameter("@Name", emp.Name));
            cmd.Parameters.Add(new SqlParameter("@DateBirth", emp.DateBirth));
            cmd.Parameters.Add(new SqlParameter("@Gender", emp.Gender));
            cmd.Parameters.Add(new SqlParameter("@PlaceBirth", emp.PlaceBirth));
            cmd.Parameters.Add(new SqlParameter("@IdDepartment", emp.Department.IdDepartment));
            cmd.ExecuteNonQuery();
            conn.Close();
        }
        public void DeleteEmployee(Employee emp)
        {
            SqlConnection conn = CreateConnection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("DeleteEmployee", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@IdEmployee", emp.IdEmployee));
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}

