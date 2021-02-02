using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AdoDotNetConsoleAssignment
{
    class WithOutSqlPerameter
    {
        SqlConnection cn = null;
        SqlCommand cmd;
        SqlDataReader dr;
        public int InsertWithOut()
        {
            try
            {
                Console.WriteLine("Enter the data to be Inserted");
                Console.WriteLine("Enter employee name");
                var ename = Console.ReadLine();
                Console.WriteLine("Enter Employee salary");
                var esal = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine("Enter Employee Department Id");
                var did = Convert.ToInt32(Console.ReadLine());
                cn = new SqlConnection("Data Source=DESKTOP-I1OCHSV;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("Insert into EmployeeTable values('" + ename + "'," + esal + "," + did + ")", cn);
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                Console.WriteLine("One row inserted to the table....");
                return i;
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

        public int UpdateWithOut()
        {
            try
            {
                Console.WriteLine("Enter the data to be updated");
                Console.WriteLine("Enter Department ID");
                int did = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Employee Id");
                int eid = Convert.ToInt32(Console.ReadLine());
                cn = new SqlConnection("Data Source=DESKTOP-I1OCHSV;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("update EmployeeTable set Deptid=" + did + " where empid=" + eid + "", cn);
                cn.Open();
                int u = cmd.ExecuteNonQuery();
                Console.WriteLine("One row updated to the table....");

                return u;
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

        public int DeleteWithOut()
        {
            try
            {
                Console.WriteLine("Enter the data to be Deleted");
                Console.WriteLine("Enter employee id");
                int eid = Convert.ToInt32(Console.ReadLine());
                cn = new SqlConnection("Data Source=DESKTOP-I1OCHSV;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("Delete from EmployeeTable where empid="+eid+"", cn);
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
                Console.WriteLine("Enter Employee id");
                var empid = Convert.ToInt32(Console.ReadLine());
                cn = new SqlConnection("Data Source=DESKTOP-I1OCHSV;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("select * from EmployeeTable e,DepartmentTable d where e.deptId=d.deptid and Empid ="+empid+"", cn);
                
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
                Console.WriteLine("Exception is "+ex.Message);
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
    class Program
    {
        static void Main(string[] args)
        {
            WithOutSqlPerameter wos = new WithOutSqlPerameter();
            bool w = true;
            while (w)
            {
                Console.WriteLine("....................\n1.Insert\n2.Update\n3.Delete\n4.Search\n5.Show Table\n6.Exit\nEnter Your choice...");
                int ch = Convert.ToInt32(Console.ReadLine());
                
                switch (ch)
                {
                    case 1: wos.InsertWithOut();
                        break;
                    case 2:wos.UpdateWithOut();
                        break;
                    case 3:wos.DeleteWithOut();
                        break;
                    case 4:wos.Search();
                        break;
                    case 5: wos.ShowTable();
                        break;
                    case 6:
                        w = false;
                        break;
                }

            }
        }
    }
}
