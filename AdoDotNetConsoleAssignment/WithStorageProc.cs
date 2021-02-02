using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AdoDotNetConsoleAssignment
{
    class WithSP
    {
        SqlConnection cn = null;
        SqlCommand cmd;
        SqlDataReader dr;
        public int InsertWithsp()
        {
            try
            {
                Console.WriteLine("Employee Name:");
                var empname = Console.ReadLine();
                Console.WriteLine("Employee Salary:");
                var salary = Convert.ToDecimal(Console.ReadLine());
                Console.WriteLine("Employee department no:");
                var DeptNo = Convert.ToInt32(Console.ReadLine());
                cn = new SqlConnection("Data Source=DESKTOP-I1OCHSV;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("sp_InsertEmployeeTable", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empname", empname);
                cmd.Parameters.AddWithValue("@salary", salary);
                cmd.Parameters.AddWithValue("@deptno", DeptNo);
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
        public int UpdateWithsp()
        {
            try
            {
                Console.WriteLine("Enter empid");
                var eid = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter employee name");
                var ename = Console.ReadLine();
                Console.WriteLine("Enter Employee salary");
                var esal = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine("Enter Employee Dept Id");
                var did = Convert.ToInt32(Console.ReadLine());

                cn = new SqlConnection("Data Source=DESKTOP-I1OCHSV;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("sp_UpdateEmployeeTable", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@empid", SqlDbType.Int).Value = eid;
                cmd.Parameters.Add("@empname", SqlDbType.VarChar, 20).Value = ename;
                cmd.Parameters.Add("@esal", SqlDbType.Float).Value = esal;
                cmd.Parameters.Add("@deptid", SqlDbType.Int).Value = did;
                cn.Open();
                int u = cmd.ExecuteNonQuery();
                Console.WriteLine("One row updated to the table....");
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
        public int Search()
        {
            try
            {
                Console.WriteLine("Enter empid");
                var eid = Convert.ToInt32(Console.ReadLine());
                cn = new SqlConnection("Data Source=DESKTOP-I1OCHSV;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("sp_SearchEmployeeTable", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@empid", SqlDbType.Int).Value = eid;
                cn.Open();
                dr = cmd.ExecuteReader();
                if (dr != null && dr.HasRows)
                    while (dr.Read())
                    {
                        Console.WriteLine($"{dr["EmpId"]}\t{dr["empName"]}\t{dr["salary"]}\t{dr["deptname"]}");
                    }
                else
                    Console.WriteLine("No data found..");
                dr.Close();
                int s = cmd.ExecuteNonQuery();
                return s;
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
        public int DeleteWithsp()
        {
            try
            {
                Console.WriteLine("Enter the data to be Deleted");
                Console.WriteLine("Enter employee id");
                int eid = Convert.ToInt32(Console.ReadLine());
                cn = new SqlConnection("Data Source=DESKTOP-I1OCHSV;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("sp_DeleteEmployeeTable", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@empid", SqlDbType.Int).Value = eid;
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
        public int ShowTable()
        {
            try
            {
                Console.WriteLine("Data from the table after the Dml Command");
                cn = new SqlConnection("Data Source=DESKTOP-I1OCHSV;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("sp_ShowEmployeeTable", cn);
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
    class WithStorageProc
    {
        static void Main()
        {
            WithSP wp = new WithSP();
            bool w = true;
            while (w)
            {
                Console.WriteLine("....................\n1.Insert\n2.Update\n3.Delete\n4.Search\n5.Show Table\n6.Exit\nEnter Your choice...");
                int ch = Convert.ToInt32(Console.ReadLine());

                switch (ch)
                {
                    case 1:
                        wp.InsertWithsp();
                        break;
                    case 2:
                        wp.UpdateWithsp();
                        break;
                    case 3:
                        wp.DeleteWithsp();
                        break;
                    case 4:
                        wp.Search();
                        break;
                    case 5:
                        wp.ShowTable();
                        break;
                    case 6:w = false;
                        break;
                   
                }

            }
        }
    }
}
