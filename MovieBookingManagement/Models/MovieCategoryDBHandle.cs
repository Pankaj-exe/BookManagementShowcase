using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MovieBookingManagement.Models
{
    public class MovieCategoryDBHandle
    {
        public List<MovieCategoryModel> GetCategories()
        {
            List<MovieCategoryModel> categories = new List<MovieCategoryModel>();

            using (SqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = "SELECT Cat_ID, Cat_Type FROM Tbl_Movie_Category ORDER BY Cat_ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categories.Add(new MovieCategoryModel
                            {
                                Cat_ID = Convert.ToInt32(reader["Cat_ID"]),
                                Cat_Type = reader["Cat_Type"].ToString()
                            });
                        }
                    }
                }
            }

            return categories;
        }

        public bool AddCategory(MovieCategoryModel model)
        {
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = "INSERT INTO Tbl_Movie_Category (Cat_Type) VALUES (@Cat_Type)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Cat_Type", model.Cat_Type);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool UpdateCategory(MovieCategoryModel model)
        {
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = "UPDATE Tbl_Movie_Category SET Cat_Type = @Cat_Type WHERE Cat_ID = @Cat_ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Cat_ID", model.Cat_ID);
                    command.Parameters.AddWithValue("@Cat_Type", model.Cat_Type);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool DeleteCategory(int id)
        {
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = "DELETE FROM Tbl_Movie_Category WHERE Cat_ID = @Cat_ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Cat_ID", id);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}