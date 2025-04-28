using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using log4net;
using Model.Domain;
using Model.Domain.Validator;
using Persistence.Repository.Interfaces;

namespace Persistence.Repository.DBRepositories
{
    public class ParticipantsDbRepo(string url, IValidator<Participant> validator) : ParticipantsRepository
    {
        private readonly DBUtils _dbUtils = new(url);
        private static readonly ILog Log = LogManager.GetLogger(typeof(DBUtils));
        private readonly IValidator<Participant> _validator = validator;

        public Participant FindOne(long id)
        {
            Participant participant = null!;

            try
            {
                Log.Info("Finding participant");
                SQLiteConnection con = _dbUtils.GetConnection();

                using (var command = new SQLiteCommand(
                           "SELECT first_name, last_name, age FROM participants where id = @id", con
                       ))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string firstName = reader["first_name"].ToString();
                            string lastName = reader["last_name"].ToString();
                            int age = Convert.ToInt32(reader["age"]);

                            participant = new Participant(firstName, lastName, age);
                            participant.Id = id;
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

            return participant;
        }

        public IEnumerable<Participant> FindAll()
        {
            List<Participant> participants = new List<Participant>();

            try
            {
                Log.Info("Finding participants");
                SQLiteConnection con = _dbUtils.GetConnection();

                using (var command = new SQLiteCommand(
                           "SELECT id, first_name, last_name, age FROM participants", con
                       ))
                {

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string firstName = reader["first_name"].ToString();
                            string lastName = reader["last_name"].ToString();
                            int age = Convert.ToInt32(reader["age"]);

                            Participant participant = new Participant(firstName, lastName, age);
                            participant.Id = id;

                            participants.Add(participant);
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

            return participants;
        }

        public Participant Save(Participant entity)
        {
            _validator.Validate(entity);
            try
            {
                Log.Info("Saving participant");
                SQLiteConnection con = _dbUtils.GetConnection();

                using (var command = new SQLiteCommand(
                           "INSERT INTO participants(first_name, last_name, age) VALUES (@firstName, @lastName, @age);"
                           + "SELECT last_insert_rowid();", con
                       ))
                {
                    command.Parameters.AddWithValue("@firstName", entity.FirstName);
                    command.Parameters.AddWithValue("@lastName", entity.LastName);
                    command.Parameters.AddWithValue("@age", entity.Age);
                    var result = command.ExecuteScalar();
                    long insertedId = -1;
                    if (result != null && long.TryParse(result.ToString(), out insertedId))
                    {
                        entity.Id = insertedId;
                    }
                    else
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
                Log.Info("Deleting participant");
                SQLiteConnection con = _dbUtils.GetConnection();

                using (var command = new SQLiteCommand(
                           "DELETE FROM participants WHERE id = @id", con
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

        public IEnumerable<Participant> GetParticipantsForEvent(long eventId)
        {
            List<Participant> participants = new List<Participant>();

            try
            {
                Log.Info("Finding participants");
                SQLiteConnection con = _dbUtils.GetConnection();

                string query = "SELECT\n" +
                    "    p.id,\n" +
                    "    p.first_name,\n" +
                    "    p.last_name,\n" +
                    "    p.age,\n" +
                    "    COUNT(r.id_event) AS num_events\n" +
                    "FROM records AS r\n" +
                    "         INNER JOIN participants AS p ON r.id_participant = p.id\n" +
                    "WHERE p.id IN (SELECT id_participant FROM records WHERE id_event = @idEvent)\n" +
                    "GROUP BY p.id, p.first_name, p.last_name, p.age";


                using (var command = new SQLiteCommand(
                           query, con
                       ))
                {
                    command.Parameters.AddWithValue("@idEvent", eventId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string firstName = reader["first_name"].ToString();
                            string lastName = reader["last_name"].ToString();
                            int age = Convert.ToInt32(reader["age"]);
                            int nrEvents = Convert.ToInt32(reader["num_events"]);

                            Participant participant = new Participant(firstName, lastName, age);
                            participant.Id = id;
                            participant.nrEvents = nrEvents;

                            participants.Add(participant);
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

            return participants;

        }

        public IEnumerable<Participant> GetParticipantsForEvent(long eventId, string fullname)
        {
            List<Participant> participants = new List<Participant>();

            try
            {
                Log.Info("Finding participants");
                SQLiteConnection con = _dbUtils.GetConnection();

                string query = "SELECT\n" +
                    "    p.id,\n" +
                    "    p.first_name,\n" +
                    "    p.last_name,\n" +
                    "    p.age,\n" +
                    "    COUNT(r.id_event) AS num_events\n" +
                    "FROM records AS r\n" +
                    "         INNER JOIN participants AS p ON r.id_participant = p.id\n" +
                    "WHERE p.id IN (SELECT id_participant FROM records WHERE id_event = @idEvent)\n" +
                    "AND((LOWER(p.first_name) || ' ' || LOWER(p.last_name)) LIKE ('%' || LOWER(@fullName) || '%'))\n" +
                    "GROUP BY p.id, p.first_name, p.last_name, p.age;";

                using (var command = new SQLiteCommand(
                           query, con
                       ))
                {
                    command.Parameters.AddWithValue("@idEvent", eventId);
                    command.Parameters.AddWithValue("@fullName", fullname);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id"]);
                            string firstName = reader["first_name"].ToString();
                            string lastName = reader["last_name"].ToString();
                            int age = Convert.ToInt32(reader["age"]);

                            Participant participant = new Participant(firstName, lastName, age);
                            participant.Id = id;

                            participants.Add(participant);
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

            return participants;

        }
    }
}