using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MovieBookingManagement.Models
{
    public class MovieDBHandle
    {
        public List<MovieModel> GetMovies()
        {
            List<MovieModel> movieList = new List<MovieModel>();

            using (SqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT 
                        m.Movie_ID,
                        m.Movie_name,
                        m.Release_Date,
                        m.Cat_ID,
                        m.rate,
                        c.Cat_Type
                    FROM Tbl_Movie m
                    INNER JOIN Tbl_Movie_Category c
                        ON m.Cat_ID = c.Cat_ID
                    ORDER BY m.Movie_ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            movieList.Add(new MovieModel
                            {
                                Movie_ID = Convert.ToInt32(reader["Movie_ID"]),
                                Movie_name = reader["Movie_name"].ToString(),
                                Release_Date = Convert.ToDateTime(reader["Release_Date"]),
                                Cat_ID = Convert.ToInt32(reader["Cat_ID"]),
                                rate = Convert.ToDecimal(reader["rate"]),
                                Cat_Type = reader["Cat_Type"].ToString()
                            });
                        }
                    }
                }
            }

            return movieList;
        }


        public MovieModel GetMovieById(int id)
        {
            MovieModel movie = null;

            using (SqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT 
                        m.Movie_ID,
                        m.Movie_name,
                        m.Release_Date,
                        m.Cat_ID,
                        m.rate,
                        c.Cat_Type
                    FROM Tbl_Movie m
                    INNER JOIN Tbl_Movie_Category c
                        ON m.Cat_ID = c.Cat_ID
                    WHERE m.Movie_ID = @Movie_ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Movie_ID", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            movie = new MovieModel
                            {
                                Movie_ID = Convert.ToInt32(reader["Movie_ID"]),
                                Movie_name = reader["Movie_name"].ToString(),
                                Release_Date = Convert.ToDateTime(reader["Release_Date"]),
                                Cat_ID = Convert.ToInt32(reader["Cat_ID"]),
                                rate = Convert.ToDecimal(reader["rate"]),
                                Cat_Type = reader["Cat_Type"].ToString()
                            };
                        }
                    }
                }
            }

            return movie;
        }


        public bool AddMovie(MovieModel model)
        {
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = @"
                    INSERT INTO Tbl_Movie
                    (Movie_name, Release_Date, Cat_ID, rate)
                    VALUES
                    (@Movie_name, @Release_Date, @Cat_ID, @rate)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Movie_name", model.Movie_name);
                    command.Parameters.AddWithValue("@Release_Date", model.Release_Date);
                    command.Parameters.AddWithValue("@Cat_ID", model.Cat_ID);
                    command.Parameters.AddWithValue("@rate", model.rate);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }


        public bool UpdateMovie(MovieModel model)
        {
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = @"
                    UPDATE Tbl_Movie
                    SET 
                        Movie_name = @Movie_name,
                        Release_Date = @Release_Date,
                        Cat_ID = @Cat_ID,
                        rate = @rate
                    WHERE Movie_ID = @Movie_ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Movie_ID", model.Movie_ID);
                    command.Parameters.AddWithValue("@Movie_name", model.Movie_name);
                    command.Parameters.AddWithValue("@Release_Date", model.Release_Date);
                    command.Parameters.AddWithValue("@Cat_ID", model.Cat_ID);
                    command.Parameters.AddWithValue("@rate", model.rate);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }


        public bool DeleteMovie(int id)
        {
            using (SqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = @"
                    DELETE FROM Tbl_Movie
                    WHERE Movie_ID = @Movie_ID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Movie_ID", id);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }


        public List<MovieCategoryModel> GetCategories()
        {
            List<MovieCategoryModel> categoryList =
                new List<MovieCategoryModel>();

            using (SqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = @"
                    SELECT Cat_ID, Cat_Type
                    FROM Tbl_Movie_Category
                    ORDER BY Cat_Type";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            categoryList.Add(new MovieCategoryModel
                            {
                                Cat_ID = Convert.ToInt32(reader["Cat_ID"]),
                                Cat_Type = reader["Cat_Type"].ToString()
                            });
                        }
                    }
                }
            }

            return categoryList;
        }
    }
}