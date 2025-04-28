using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SQLite;
using log4net;
using Model.Domain;
using Persistence.Repository.Interfaces;

namespace Persistence.Repository.DBRepositories
{
    public class RecordsDBRepo(string url) : RecordsRepository
    {
        private readonly DBUtils _dbUtils = new(url);
        private static readonly ILog Log = LogManager.GetLogger(typeof(DBUtils));
        public Record FindOne(long id)
        {
            Record record = null!;

            try
            {
                Log.Info("Finding record");
                SQLiteConnection con = _dbUtils.GetConnection();

                string query = @"SELECT 
                               records.id AS id_record, 
                               participants.id AS id_participant, 
                               participants.first_name, 
                               participants.last_name, 
                               participants.age, 
                               swimming_events.id AS id_swimming_event, 
                               swimming_events.distance, 
                               swimming_events.style 
                               FROM records 
                               JOIN participants ON records.id_participant = participants.id 
                               JOIN swimming_events ON records.id_event = swimming_events.id
                               WHERE records.id = @id;";


                using (var command = new SQLiteCommand(query, con))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            //Participant
                            long idParticipant = Convert.ToInt64(reader["id_participant"]);
                            string firstName = reader["first_name"].ToString();
                            string lastName = reader["last_name"].ToString();
                            int age = Convert.ToInt32(reader["age"]);

                            Participant participant = new Participant(firstName, lastName, age);
                            participant.Id = idParticipant;

                            //Event
                            long idSwimming = Convert.ToInt64(reader["id_swimming_event"]);
                            int distance = Convert.ToInt32(reader["distance"]);
                            string style = reader["style"].ToString();

                            SwimmingEvent swimmingEvent = new SwimmingEvent(distance, style);
                            swimmingEvent.Id = idSwimming;




                            record = new Record(participant, swimmingEvent);
                            record.Id = id;
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

            return record;
        }

        public IEnumerable<Record> FindAll()
        {
            List<Record> records = new List<Record>();

            string query = @"SELECT 
                               records.id AS id_record, 
                               participants.id AS id_participant, 
                               participants.first_name, 
                               participants.last_name, 
                               participants.age, 
                               swimming_events.id AS id_swimming_event, 
                               swimming_events.distance, 
                               swimming_events.style 
                               FROM records 
                               JOIN participants ON records.id_participant = participants.id 
                               JOIN swimming_events ON records.id_event = swimming_events.id";

            try
            {
                Log.Info("Finding records");
                SQLiteConnection con = _dbUtils.GetConnection();

                using (var command = new SQLiteCommand(query, con))
                {

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //Participant
                            long idParticipant = Convert.ToInt64(reader["id_participant"]);
                            string firstName = reader["first_name"].ToString();
                            string lastName = reader["last_name"].ToString();
                            int age = Convert.ToInt32(reader["age"]);

                            Participant participant = new Participant(firstName, lastName, age);
                            participant.Id = idParticipant;

                            //Event
                            long idSwimming = Convert.ToInt64(reader["id_swimming_event"]);
                            int distance = Convert.ToInt32(reader["distance"]);
                            string style = reader["style"].ToString();

                            SwimmingEvent swimmingEvent = new SwimmingEvent(distance, style);
                            swimmingEvent.Id = idSwimming;

                            long idRecord = Convert.ToInt64(reader["id_record"]);
                            Record record = new Record(participant, swimmingEvent);
                            record.Id = idRecord;

                            records.Add(record);
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

            return records;
        }

        public Record Save(Record entity)
        {
            try
            {
                Log.Info("Saving record");
                SQLiteConnection con = _dbUtils.GetConnection();

                using (var command = new SQLiteCommand(
                           "INSERT INTO records(id_participant, id_event) VALUES (@idParticipant, @idEvent)", con
                       ))
                {
                    command.Parameters.AddWithValue("@idParticipant", entity.participant.Id);
                    command.Parameters.AddWithValue("@idEvent", entity.swimmingEvent.Id);
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
                Log.Info("Deleting record");
                SQLiteConnection con = _dbUtils.GetConnection();

                using (var command = new SQLiteCommand(
                           "DELETE FROM records WHERE id = @id", con
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

        public void SaveRecordsForParticipant(long participantId, IEnumerable<long> events)
        {

            try
            {
                Log.Info("Saving record");
                SQLiteConnection con = _dbUtils.GetConnection();

                foreach (var eventId in events)
                {
                    using (var command = new SQLiteCommand(
                               "INSERT INTO records(id_participant, id_event) VALUES (@idParticipant, @idEvent)", con
                           ))
                    {
                        command.Parameters.AddWithValue("@idParticipant", participantId);
                        command.Parameters.AddWithValue("@idEvent", eventId);
                        int affectedRows = command.ExecuteNonQuery();

                        if (affectedRows == 1)
                        {
                            Log.Info($"Record added: Participant {participantId}, Event {eventId}");
                        }
                        else
                        {
                            Log.Warn($"No record added for Participant {participantId}, Event {eventId}");
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

        }

    }
}