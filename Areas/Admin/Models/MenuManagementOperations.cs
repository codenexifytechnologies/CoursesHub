using CoursesHub.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CoursesHub.Areas.Admin.Models
{
    public class MenuManagement
    {
        public List<Menu> lstMenu { get; set; }
        public List<Emp> lstEmp { get; set; }
        public int UserId { get; set; }
        public List<CheckedMenu> lstUserMenuMapping { get; set; }

    }
    public class Menu
    {
        public int id { get; set; }
        public string MainMenu { get; set; }
        public string SubMenu { get; set; }
        public int LoginId { get; set; }
        public int IsVisible { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }


    public class Emp
    {
        public int id { get; set; }
        public string Name { get; set; }
    }


    public class CheckedMenu
    {
        public int id { get; set; }
        public int MenuId { get; set; }
    }

    public class CheckboxData
    {
        public string ID { get; set; }
        public bool Checked { get; set; }
    }
    public class MenuManagementOperations
    {
        string con = ConfigurationManager.ConnectionStrings["DBCon"].ToString();
        public List<Menu> MenuManagement()
        {
            MenuManagement objMenuManagement = new MenuManagement();
            List<Menu> lstMenu = new List<Menu>();
            try
            {
                DataTable userDataTable = new DataTable();
                using (SqlConnection connection = new SqlConnection(con))
                {
                    using (SqlCommand command = new SqlCommand("usp_get_MenuDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            connection.Open();
                            adapter.Fill(userDataTable);
                        }
                    }
                }
                if (userDataTable.Rows.Count > 0)
                {
                    foreach (DataRow row in userDataTable.Rows)
                    {
                        Menu objMenu = new Menu
                        {
                            id = Convert.ToInt32(row["id"]),
                            MainMenu = row["MainMenu"].ToString(),
                            SubMenu = row["SubMenu"].ToString(),
                            IsVisible = Convert.ToInt32(row["IsVisible"].ToString()),
                        };
                        lstMenu.Add(objMenu);
                    }
                }
            }
            catch (Exception Ex)
            {
                Common.errorlog(Convert.ToString(Ex.Message) + " Inner Msg:" + Convert.ToString(Ex.StackTrace), "MenuManagement", "MenuManagementOperations");
            }
            return lstMenu;
        }

        public List<Emp> EmpData()
        {
            MenuManagement objMenuManagement = new MenuManagement();
            List<Emp> lstEmp = new List<Emp>();
            try
            {
                DataTable userDataTable = new DataTable();
                using (SqlConnection connection = new SqlConnection(con))
                {
                    using (SqlCommand command = new SqlCommand("usp_get_UserRegistraionDetails", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            connection.Open();
                            adapter.Fill(userDataTable);
                        }
                    }
                }
                if (userDataTable.Rows.Count > 0)
                {
                    Emp objEmp1 = new Emp
                    {
                        id = 0,
                        Name = "Select Employees",
                    };
                    lstEmp.Add(objEmp1);

                    foreach (DataRow row in userDataTable.Rows)
                    {
                        Emp objEmp = new Emp
                        {
                            id = Convert.ToInt32(row["userId"]),
                            Name = row["name"].ToString(),
                        };
                        lstEmp.Add(objEmp);
                    }
                }
            }
            catch (Exception Ex)
            {
                Common.errorlog(Convert.ToString(Ex.Message) + " Inner Msg:" + Convert.ToString(Ex.StackTrace), "EmpData", "MenuManagementOperations");
            }
            return lstEmp;
        }

        public List<CheckedMenu> UserMenuMapping(int Id)
        {
            MenuManagement objMenuManagement = new MenuManagement();
            List<CheckedMenu> lstMenu = new List<CheckedMenu>();
            try
            {
                DataTable userDataTable = new DataTable();
                using (SqlConnection connection = new SqlConnection(con))
                {
                    using (SqlCommand command = new SqlCommand("usp_get_EmpMenuMapping", connection))
                    {
                        command.Parameters.AddWithValue("@LoginId", Id);
                        command.CommandType = CommandType.StoredProcedure;
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            connection.Open();
                            adapter.Fill(userDataTable);
                        }
                    }
                }
                if (userDataTable.Rows.Count > 0)
                {
                    lstMenu = userDataTable.AsEnumerable().Select(row =>
                 new CheckedMenu
                 {
                     id = Convert.ToInt32(row["id"]),
                     MenuId = Convert.ToInt32(row["MenuId"]),
                 }).ToList();
                }
            }
            catch (Exception Ex)
            {
                Common.errorlog(Convert.ToString(Ex.Message) + " Inner Msg:" + Convert.ToString(Ex.StackTrace), "UserMenuMapping", "MenuManagementOperations");
            }
            return lstMenu;
        }

        public string SaveUserMenuRole(string MenuId, int LoginId)
        {
            string Result = string.Empty;
            try
            {
                using (SqlConnection connection = new SqlConnection(con))
                {
                    using (SqlCommand command = new SqlCommand("insertUserMenuRole", connection))
                    {
                        connection.Open();
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@MenuId", MenuId);
                        command.Parameters.AddWithValue("@LoginId", LoginId);
                        Result = (string)command.ExecuteScalar();
                        connection.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                Common.errorlog(Convert.ToString(Ex.Message) + " Inner Msg:" + Convert.ToString(Ex.StackTrace), "SaveUserMenuRole", "MenuManagementOperations");
            }
            return Result;
        }
    }
}