using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AdoDotNetConsoleAssignment
{
    class WithPara
    {
        SqlConnection cn = null;
        SqlCommand cmd;
        SqlDataReader dr;
        public int InsertWith()
        {
            try
            {
                Console.WriteLine("Enter employee name");
                var ename = Console.ReadLine();
                Console.WriteLine("Enter Employee salary");
                var esal = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine("Enter Employee Dept Id");
                var did = Convert.ToInt32(Console.ReadLine());

                cn = new SqlConnection("Data Source=DESKTOP-I1OCHSV;Initial Catalog=WFA3DotNet;Integrated Security=True");

                cmd = new SqlCommand("Insert into employeetable values(@empname,@salary,@deptid)", cn);
                cmd.Parameters.Add("@empname", SqlDbType.VarChar, 20).Value = ename;
                cmd.Parameters.Add("@salary", SqlDbType.Float).Value = esal;
                cmd.Parameters.Add("@deptid", SqlDbType.Int).Value = did;

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                Console.WriteLine("One row inserted to the table....");
                return i;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }
        public int UpdateWith()
        {
            try
            {
                Console.WriteLine("Enter the data to be updated");
                Console.WriteLine("Enter Emp Name");
                var ename = Console.ReadLine();
                Console.WriteLine("Enter Department ID");
                int did = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Employee Id");
                int eid = Convert.ToInt32(Console.ReadLine());
                cn = new SqlConnection("Data Source=DESKTOP-I1OCHSV;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("update EmployeeTable set empname=@ename,Deptid = @did where empid = @eid", cn);
                cmd.Parameters.Add("@ename", SqlDbType.VarChar, 20).Value = ename;
                cmd.Parameters.Add("@did", SqlDbType.Int).Value = did;
                cmd.Parameters.Add("@eid", SqlDbType.Int).Value = eid;

                cn.Open();
                int u = cmd.ExecuteNonQuery();
                Console.WriteLine("One row Updated to the table....");
                return u;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }
        public int DeleteWith()
        {
            try
            {
                Console.WriteLine("Enter the data to be Deleted");
                Console.WriteLine("Enter employee id");
                int eid = Convert.ToInt32(Console.ReadLine());
                cn = new SqlConnection("Data Source=DESKTOP-I1OCHSV;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("Delete from EmployeeTable where empid=@eid", cn);
                cmd.Parameters.Add("@eid", SqlDbType.Int).Value = eid;
                cn.Open();
                int d = cmd.ExecuteNonQuery();
                Console.WriteLine("One row deleted to the table....");

                return d;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }
        public int Search()
        {
            try
            {
                Console.WriteLine("Enter the data to be selected");
                Console.WriteLine("Enter employee id");
                int eid = Convert.ToInt32(Console.ReadLine());
                cn = new SqlConnection("Data Source=DESKTOP-I1OCHSV;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("select * from EmployeeTable e,DepartmentTable d where e.deptId=d.deptid and Empid =@eid", cn);
                cmd.Parameters.Add("@eid", SqlDbType.Int).Value = eid;
                cn.Open();
                dr = cmd.ExecuteReader();
                if (dr != null && dr.HasRows)
                    while (dr.Read())
                        Console.WriteLine($"Id : {dr["EmpId"]}\nName : {dr["empName"]}\nSalary : {dr["salary"]}\nDepartment Id : {dr["deptid"]}\nDepartment Name : {dr["DeptName"]}");
                else
                    Console.WriteLine("No data found..");
                dr.Close();
                int s = cmd.ExecuteNonQuery();
                return s;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }
        public int ShowTable()
        {
            try
            {
                Console.WriteLine("Data from the table after the Dml Command");
                cn = new SqlConnection("Data Source=DESKTOP-I1OCHSV;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("select * from EmployeeTable", cn);
                cn.Open();
                dr = cmd.ExecuteReader();
                Console.WriteLine("EmpId\tEmpName\t  Salary\tDeptId");
                Console.WriteLine("-----\t-------\t  ------\t------");
                while (dr.Read())
                    Console.WriteLine($"{dr["EmpId"]}\t{dr["empName"]}\t{dr["salary"]}\t{dr["deptid"]}");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }
    }

    class WithSqlParameter
    {
        static void Main()
        {
            WithPara wp = new WithPara();
            bool w = true;
            while (w)
            {
                Console.WriteLine("....................\n1.Insert\n2.Update\n3.Delete\n4.Search\n5.Show Table\n6.Exit\nEnter Your choice...");
                int ch = Convert.ToInt32(Console.ReadLine());

                switch (ch)
                {
                    case 1:
                        wp.InsertWith();
                        break;
                    case 2:
                        wp.UpdateWith();
                        break;
                    case 3:
                        wp.DeleteWith();
                        break;
                    case 4:wp.Search();
                        break;
                    case 5:wp.ShowTable();
                        break;
                    case 6:
                        w = false;
                        break;
                }

            }
        }
    }
}
