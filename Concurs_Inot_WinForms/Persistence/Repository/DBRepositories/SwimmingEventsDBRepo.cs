using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using Model.Domain;
using Persistence.Repository.Interfaces;
using log4net;

namespace Persistence.Repository.DBRepositories
{
    public class SwimmingEventsDbRepo(string url) : SwimmingEventsRepository
    {
        private readonly DBUtils _dbUtils = new(url);
        private static readonly ILog Log = LogManager.GetLogger(typeof(DBUtils));
        public SwimmingEvent FindOne(long id)
        {
            SwimmingEvent swimmingEvent = null!;

            try
            {
                Log.Info("Finding swimming event");
                SQLiteConnection con = _dbUtils.GetConnection();

                using (var command = new SQLiteCommand(
                           "SELECT style, distance FROM swimming_Events where id = @id", con
                       ))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string style = reader["style"].ToString();
                            int distance = Convert.ToInt32(reader["distance"]);

                            swimmingEvent = new SwimmingEvent(distance, style);
                            swimmingEvent.Id = id;
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

            return swimmingEvent;
        }

        public IEnumerable<SwimmingEvent> FindAll()
        {
            List<SwimmingEvent> swimmingEvents = new List<SwimmingEvent>();

            try
            {
                Log.Info("Finding swimming events");
                SQLiteConnection con = _dbUtils.GetConnection();
                string sqlQuery = "SELECT\n" +
                    "    se.id AS event_id,\n" +
                    "    se.distance,\n" +
                    "    se.style,\n" +
                    "    COUNT(r.id_participant) AS num_participants\n" +
                    "FROM swimming_events se\n" +
                    "         LEFT JOIN records r ON se.id = r.id_event\n" +
                    "GROUP BY se.id, se.distance, se.style;";

                using (var command = new SQLiteCommand(sqlQuery, con
                       ))
                {

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            long id = Convert.ToInt64(reader["event_id"]);
                            string style = reader["style"].ToString();
                            int distance = Convert.ToInt32(reader["distance"]);
                            int nrParticipants = Convert.ToInt32(reader["num_participants"]);
                            SwimmingEvent swimmingEvent = new SwimmingEvent(distance, style);
                            swimmingEvent.Id = id;
                            swimmingEvent.nrParticipants = nrParticipants;

                            swimmingEvents.Add(swimmingEvent);
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

            return swimmingEvents;
        }

        public SwimmingEvent Save(SwimmingEvent entity)
        {
            try
            {
                Log.Info("Saving swimming event");
                SQLiteConnection con = _dbUtils.GetConnection();

                using (var command = new SQLiteCommand(
                           "INSERT INTO swimming_Events(distance, style) VALUES (@distance, @style)", con
                       ))
                {
                    command.Parameters.AddWithValue("@distance", entity.Distance);
                    command.Parameters.AddWithValue("@style", entity.Style);
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
                Log.Info("Deleting swimming event");
                SQLiteConnection con = _dbUtils.GetConnection();

                using (var command = new SQLiteCommand(
                           "DELETE FROM swimming_Events WHERE id = @id", con
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
    }
}