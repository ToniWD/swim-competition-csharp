using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using Model.Domain;
using Persistence.Repository.Interfaces;
using log4net;

namespace Persistence.Repository.DBRepositories
{
    public class UsersDbRepo(string url) : UsersRepository
    {
        private readonly DBUtils _dbUtils = new(url);
        private static readonly ILog Log = LogManager.GetLogger(typeof(DBUtils));
        public User FindOne(long id)
        {
            User user = null!;

            try
            {
                Log.Info("Finding user");
                SQLiteConnection con = _dbUtils.GetConnection();

                using (var command = new SQLiteCommand(
                           "SELECT username, password FROM users where id = @id", con
                       ))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string username = reader["username"].ToString();
                            string password = reader["password"].ToString();

                            user = new User(username, password);
                            user.Id = id;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Log.Error(ex);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

            return user;
        }

        public IEnumerable<User> FindAll()
        {
            List<User> users = new List<User>();

            try
            {
                Log.Info("Finding users");
                SQLiteConnection con = _dbUtils.GetConnection();

                using (var command = new SQLiteCommand(
                           "SELECT id, username, password FROM users", con
                       ))
                {

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string username = reader["username"].ToString();
                            string password = reader["password"].ToString();
                            long id = Convert.ToInt64(reader["id"]);

                            User user = new User(username, password);
                            user.Id = id;

                            users.Add(user);
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Log.Error(ex);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

            return users;
        }

        public User Save(User entity)
        {
            try
            {
                Log.Info("Saving user");
                SQLiteConnection con = _dbUtils.GetConnection();

                using (var command = new SQLiteCommand(
                           "INSERT INTO users(username, password) VALUES (@username, @password)", con
                       ))
                {
                    command.Parameters.AddWithValue("@username", entity.username);
                    command.Parameters.AddWithValue("@password", entity.password);
                    int affectedRows = command.ExecuteNonQuery();

                    if (affectedRows == 1)
                    {
                        entity = null!;
                    }
                }
            }
            catch (SqlException ex)
            {
                Log.Error(ex);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

            return entity;
        }

        public bool Delete(long id)
        {
            try
            {
                Log.Info("Deleting user");
                SQLiteConnection con = _dbUtils.GetConnection();

                using (var command = new SQLiteCommand(
                           "DELETE FROM users WHERE id = @id", con
                       ))
                {
                    command.Parameters.AddWithValue("@id", id);
                    int affectedRows = command.ExecuteNonQuery();

                    return affectedRows == 1;
                }
            }
            catch (SqlException ex)
            {
                Log.Error(ex);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

            return false;
        }

        public User findByUsername(string username)
        {
            User user = null!;

            try
            {
                Log.Info("Finding user");
                SQLiteConnection con = _dbUtils.GetConnection();

                using (var command = new SQLiteCommand(
                           "SELECT id, password FROM users where username = @username", con
                       ))
                {
                    command.Parameters.AddWithValue("@username", username);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            long id = Convert.ToInt64(reader["id"]);
                            string password = reader["password"].ToString();

                            user = new User(username, password);
                            user.Id = id;
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Log.Error(ex);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

            return user;
        }
    }
}