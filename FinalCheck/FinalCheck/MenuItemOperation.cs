using FinalCheck.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace FinalCheck
{
    public class MenuItemOperation
    {
        public static string sqlDataSource = "Server=DESKTOP-HF3JAM2;Database=MovieCruiser;Trusted_Connection=True;MultipleActiveResultSets=true;";
        
        public static IEnumerable<Movie> GetConnection()
        {
            List<Movie> Items = new List<Movie>();
            var list = new List<string> { "Science Fiction", "Superhero", "Romance", "Comedy","Adventure","Thriller"};

            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "select * from Movie";
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        Items.Add(new Movie
                        {
                            Id = Convert.ToInt32(rd["Id"]),
                            Title = rd["Title"].ToString(),
                            BoxOffice = rd["BoxOffice"].ToString(),
                            Active = Convert.ToBoolean(rd["Active"]),
                            DateOfLaunch = Convert.ToDateTime(rd["DateOfLaunch"]),
                            GenreId = Convert.ToInt32(rd["GenreId"]),
                            GenreType = list[Convert.ToInt32(rd["GenreId"]) - 1].ToString(),
                            HasTeaser = Convert.ToBoolean(rd["HasTeaser"]),
                        }); 
                    }
                    con.Close();
                }


                return Items;
            }
        }
        public static void Update(int id, Movie movieitem)
        {
            var list = new List<string> { "Science Fiction", "Superhero", "Romance", "Comedy", "Adventure", "Thriller" };
            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                SqlCommand cmd = new SqlCommand("UpdateMovie", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Title", movieitem.Title);
                cmd.Parameters.AddWithValue("@BoxOffice", movieitem.BoxOffice);
                cmd.Parameters.AddWithValue("@Active", movieitem.Active);
                cmd.Parameters.AddWithValue("@DateOfLaunch", movieitem.DateOfLaunch);
                cmd.Parameters.AddWithValue("@GenreId", movieitem.GenreId);
                cmd.Parameters.AddWithValue("@HasTeaser", movieitem.HasTeaser);

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
        public static List<User> UserList()
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
        public static void InsertIntoFavorites(List<Favorites> favorite)
        {
            SqlConnection con = new SqlConnection(sqlDataSource);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO Favorites(Id,MovieId,UserId) Values (@Id,@MovieId,@UserId)";
            sqlCmd.Connection = con;
            con.Open();
            foreach (var i in favorite)
            {
                sqlCmd.Parameters.AddWithValue("@Id", i.Id);
                sqlCmd.Parameters.AddWithValue("@MovieId", i.MovieId);
                sqlCmd.Parameters.AddWithValue("@UserId", i.UserId);
                sqlCmd.ExecuteNonQuery();
                sqlCmd.Parameters.Clear();
            }

            con.Close();
        }
        public static List<Movie> favoriteList(int userid, ref int count)
        {

            List<Movie> Items = new List<Movie>();
            List<int> list = new List<int>();

            var l = new List<string> { "Science Fiction", "Superhero", "Romance", "Comedy", "Adventure", "Thriller" };


            using (SqlConnection con = new SqlConnection(sqlDataSource))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "select MovieId from Favorites where Userid = @userid";
                    cmd.Parameters.AddWithValue("@userid", userid);
                    SqlDataReader rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        list.Add(Convert.ToInt32(rd["MovieId"]));
                    }
                    rd.Close();
                    foreach (var i in list)
                    {

                        cmd.CommandText = "select * from Movie where Id = @i";
                        cmd.Parameters.AddWithValue("@i", i);
                        SqlDataReader rd1 = cmd.ExecuteReader();
                        while (rd1.Read())
                        {
                            count++;
                            Items.Add(new Movie
                            {
                                Id = Convert.ToInt32(rd1["Id"]),
                                Title = rd1["Title"].ToString(),
                                BoxOffice = rd1["BoxOffice"].ToString(),
                                Active = Convert.ToBoolean(rd1["Active"]),
                                DateOfLaunch = Convert.ToDateTime(rd1["DateOfLaunch"]),
                                GenreId = Convert.ToInt32(rd1["GenreId"]),
                                GenreType = l[Convert.ToInt32(rd1["GenreId"]) - 1].ToString(),
                                HasTeaser = Convert.ToBoolean(rd1["HasTeaser"]),
                            });
                            

                        }
                        cmd.Parameters.Clear();
                        rd1.Close();
                    }
                    con.Close();

                }

            }
            return Items;

        }
        public static string Delete(int Favoriteid)
        {
            SqlConnection con = new SqlConnection(sqlDataSource);
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from Favorites where Id =@favoriteid";
            sqlCmd.Connection = con;
            con.Open();
            sqlCmd.Parameters.AddWithValue("@favoriteid", Favoriteid);
            int i = sqlCmd.ExecuteNonQuery();
            if (i >= 1)
                return "record deleted";
            else
                return "no record";

        }
    }
}
