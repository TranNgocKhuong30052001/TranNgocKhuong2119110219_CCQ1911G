using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Cau_1.BAL;
using Cau_1.Model;

namespace Cau_1
{
    public partial class Form1 : Form
    {
        EmployeeBAL EmployBAL = new EmployeeBAL();
        DepartmentBAL DepartmentBAL = new DepartmentBAL();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Employee> lstEmploy = EmployBAL.ReadCustomer();
            foreach (Employee Emp in lstEmploy)
            {
                dgvEmployee.Rows.Add(Emp.IdEmployee, Emp.Name, Emp.DateBirth, Emp.Gender, Emp.PlaceBirth, Emp.Depart);

            }
            List<Department> lstDepart = DepartmentBAL.ReadAreaList();
            foreach (Department depart in lstDepart)
            {
                cbDepart.Items.Add(depart);
            }
            cbDepart.DisplayMember = "Name_department";
        }

        private void dgvEmployee_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int idx = e.RowIndex;
            DataGridViewRow row = dgvEmployee.Rows[idx];
            if (row.Cells[0].Value != null)
            {
                tbId.Text = row.Cells[0].Value.ToString();
                tbName.Text = row.Cells[1].Value.ToString();
                dtBirth.Text = row.Cells[2].Value.ToString();
                //   tbGender.Text = row.Cells[3].Value.ToString();
                if (row.Cells[3].Value.ToString() == "NAM")
                {
                    cbGender.Checked = true;
                }
                else
                {
                    cbGender.Checked = false;
                }

                tbPlace.Text = row.Cells[4].Value.ToString();
                cbDepart.Text = row.Cells[5].Value.ToString();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (checkData())
            {
                Employee emp = new Employee();
                emp.IdEmployee = tbId.Text;
                emp.Name = tbName.Text;
                emp.DateBirth = DateTime.Parse(dtBirth.Value.Date.ToString());
                // emp.Gender = tbGender.Text;
                if (cbGender.Checked)
                {
                    emp.Gender = "NAM";
                }
                else
                {
                    emp.Gender = "NU";
                }

                emp.PlaceBirth = tbPlace.Text;
                emp.Department = (Department)cbDepart.SelectedItem;



                EmployBAL.NewEmployee(emp);

                dgvEmployee.Rows.Add(emp.IdEmployee, emp.Name, emp.DateBirth, emp.Gender, emp.PlaceBirth, emp.Department.Name_Department);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (checkData())
            {
                DataGridViewRow row = dgvEmployee.CurrentRow;

                Employee empp = new Employee();
                empp.IdEmployee = tbId.Text;
                empp.Name = tbName.Text;
                empp.DateBirth = DateTime.Parse(dtBirth.Value.Date.ToString());


                if (cbGender.Checked)
                {
                    empp.Gender = "NAM";
                }
                else
                {
                    empp.Gender = "NU";
                }
                empp.PlaceBirth = tbPlace.Text;
                empp.Department = (Department)cbDepart.SelectedItem;

                EmployBAL.EditEmployee(empp);

                row.Cells[0].Value = empp.IdEmployee;
                row.Cells[1].Value = empp.Name;
                row.Cells[2].Value = empp.DateBirth;
                row.Cells[3].Value = empp.Gender;
                row.Cells[4].Value = empp.PlaceBirth;
                row.Cells[5].Value = empp.Depart;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult f = MessageBox.Show("Ban co thuc su muon xoa?", "Thong Bao", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (f == DialogResult.Yes)
            {
                Employee emp = new Employee();
                emp.IdEmployee = tbId.Text;
                emp.Name = tbName.Text;
                emp.DateBirth = DateTime.Parse(dtBirth.Value.Date.ToString());
                emp.Gender = cbGender.Text;
                emp.PlaceBirth = tbPlace.Text;


                EmployBAL.DeleteEmployee(emp);
                int idx = dgvEmployee.CurrentCell.RowIndex;
                dgvEmployee.Rows.RemoveAt(idx);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult f = MessageBox.Show("Ban co thuc su muon thoat?", "Thong Bao", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (f == DialogResult.Yes)
            {
                this.Close();
                Application.Exit();
            }
        }
        public bool checkData()
        {
            if (string.IsNullOrWhiteSpace(tbId.Text))
            {
                MessageBox.Show("Chưa Nhập Mã", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("Chưa nhập Tên", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (string.IsNullOrWhiteSpace(tbPlace.Text))
            {
                MessageBox.Show("Chưa nhập Nơi Sinh", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (string.IsNullOrWhiteSpace(dtBirth.Text))
            {
                MessageBox.Show("Chưa nhập Ngày Sinh", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (string.IsNullOrWhiteSpace(cbGender.Text))
            {
                MessageBox.Show("Chưa nhập Giới Tính", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (string.IsNullOrWhiteSpace(cbDepart.Text))
            {
                MessageBox.Show("Chưa nhập Đơn Vị", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }
    }
}
