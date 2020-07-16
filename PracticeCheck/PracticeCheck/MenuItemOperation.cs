using PracticeCheck.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeCheck
{
    public class MenuItemOperation
    {
        public static string sqlDataSource = "Server=DESKTOP-HF3JAM2;Database=MenuItems;Trusted_Connection=True;MultipleActiveResultSets=true;";
        //private List<string> list = new List<string>{ "Starter", "Main Course", "Drinks", "Dessert" };
        public static IEnumerable<MenuItem> GetConnection()
        {
            List<MenuItem> Items = new List<MenuItem>();
            var list = new List<string> { "Starter", "Main Course", "Drinks", "Dessert" };

            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "select * from MenuItem";
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        Items.Add(new MenuItem
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            Name = rd["Name"].ToString(),
                            Price = Convert.ToInt32(rd["Price"]),
                            Active = Convert.ToBoolean(rd["Active"]),
                            DateOfLaunch = Convert.ToDateTime(rd["DateOfLaunch"]),
                            CategoryId = Convert.ToInt32(rd["CategoryId"]),
                            CategoryName = list[Convert.ToInt32(rd["CategoryId"]) - 1].ToString(),
                            FreeDelivery = Convert.ToBoolean(rd["FreeDelivery"]),
                        }); ; ;
                    }
                    con.Close();
                }


                return Items;
            }
        }
        public static void Update(int id, MenuItem menuitem)
        {
            var list = new List<string> { "Starter", "Main Course", "Drinks", "Dessert" };
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                SqlCommand cmd = new SqlCommand("UpdateItem", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", menuitem.Name);
                cmd.Parameters.AddWithValue("@Price", menuitem.Price);
                cmd.Parameters.AddWithValue("@Active", menuitem.Active);
                cmd.Parameters.AddWithValue("@DateOfLaunch", menuitem.DateOfLaunch);
                cmd.Parameters.AddWithValue("@CategoryId", menuitem.CategoryId);
                cmd.Parameters.AddWithValue("@FreeDelivery", menuitem.FreeDelivery);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }
        public static void Insert(User user)
        {
            SqlConnection con = new SqlConnection(sqlDataSource);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO UserDetails(Id,UserName,FirstName,LastName,Password,ConfirmPassword) Values (@Id,@UserName,@FirstName,@LastName,@Password,@ConfirmPassword)";
            sqlCmd.Connection = con;


            sqlCmd.Parameters.AddWithValue("@Id", user.Id);
            sqlCmd.Parameters.AddWithValue("@UserName", user.UserName);
            sqlCmd.Parameters.AddWithValue("@FirstName", user.FirstName);
            sqlCmd.Parameters.AddWithValue("@LastName", user.LastName);
            sqlCmd.Parameters.AddWithValue("@Password", user.Password);
            sqlCmd.Parameters.AddWithValue("@ConfirmPassword", user.ConfirmPassword);

            con.Open();
            sqlCmd.ExecuteNonQuery();
            con.Close();

        }
        public static List<User>UserList()
        {
            List<User> users = new List<User>();

            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "select * from UserDetails";
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        users.Add(new User
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            UserName = rd["UserName"].ToString(),
                            FirstName = rd["FirstName"].ToString(),
                            LastName = rd["LastName"].ToString(),
                            Password = rd["Password"].ToString(),
                            ConfirmPassword = rd["ConfirmPassword"].ToString()

                        });
                    }
                    con.Close();
                }

            }
            return users;


        }
        public static void InsertIntoCart(List<Cart> cart)
        {
            SqlConnection con = new SqlConnection(sqlDataSource);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO Cart(Id,MenuItemId,UserId) Values (@Id,@MenuItemId,@UserId)";
            sqlCmd.Connection = con;
            con.Open();
            foreach (var i in cart)
            {
                sqlCmd.Parameters.AddWithValue("@Id", i.Id);
                sqlCmd.Parameters.AddWithValue("@MenuItemId", i.MenuItemId);
                sqlCmd.Parameters.AddWithValue("@UserId", i.UserId);
                sqlCmd.ExecuteNonQuery();
                sqlCmd.Parameters.Clear();
            }
            
             con.Close();
        }
        public static List<MenuItem> CartList(int userid,ref int totalprice)
        {

            List<MenuItem> Items = new List<MenuItem>();
            List<int> list = new List<int>();
                
            var l = new List<string> { "Starter", "Main Course", "Drinks", "Dessert" };


            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "select MenuItemId from Cart where Userid = @userid";
                    cmd.Parameters.AddWithValue("@userid",userid);
                    SqlDataReader rd = cmd.ExecuteReader();
                     while (rd.Read())
                     {
                         list.Add(Convert.ToInt32(rd["MenuItemId"]));
                     }
                    rd.Close();
                      foreach (var i in list)
                      {
                        
                       cmd.CommandText = "select * from MenuItem where Id = @i";
                       cmd.Parameters.AddWithValue("@i", i);
                       SqlDataReader rd1 = cmd.ExecuteReader();
                        while (rd1.Read())
                         {
                             Items.Add(new MenuItem
                             {
                                 Id = Convert.ToInt32(rd1["Id"]),
                                 Name = rd1["Name"].ToString(),
                                 Price = Convert.ToInt32(rd1["Price"]),
                                 Active = Convert.ToBoolean(rd1["Active"]),
                                 DateOfLaunch = Convert.ToDateTime(rd1["DateOfLaunch"]),
                                 CategoryId = Convert.ToInt32(rd1["CategoryId"]),
                                 CategoryName = l[Convert.ToInt32(rd1["CategoryId"]) - 1].ToString(),
                                 FreeDelivery = Convert.ToBoolean(rd1["FreeDelivery"]),
                         });
                           totalprice += Convert.ToInt32(rd1["Price"]);

                         }
                        cmd.Parameters.Clear();
                        rd1.Close();
                    }
                    con.Close();

                 }

             }
            return Items;
           
        }
        public static string Delete(int cartid)
        {
            SqlConnection con = new SqlConnection(sqlDataSource);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from Cart where Id =@cartid";
            sqlCmd.Connection = con;
            con.Open();
            sqlCmd.Parameters.AddWithValue("@cartid",cartid);
            int i = sqlCmd.ExecuteNonQuery();
            if (i >= 1)
                return "record deleted";
            else
                return "no record";

        }
    }
}
