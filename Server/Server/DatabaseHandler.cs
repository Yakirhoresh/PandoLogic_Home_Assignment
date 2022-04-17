using System.IO;
using System.Data.SQLite;
using System.Collections.Generic;

namespace Server
{
    public static class DatabaseHandler
    {
        // Private Constants:
        static string DBPath = $"{Directory.GetCurrentDirectory()}\\DB\\JobsDB.db";
        static string ConnectionString = $"URI=file:{DBPath}";
        static string JobTitelsTableName = "Test_JobTitles";
        static string JobsTableName = "Test_Jobs";

        // Private Fields:
        static SQLiteConnection _connection;

        // Private Properties:
        static SQLiteConnection Connection { get => _connection == null ? OpenDatabaseConnection() : _connection; }

        // Private Methods:
        static SQLiteConnection OpenDatabaseConnection()
        {
            _connection = new SQLiteConnection(ConnectionString);
            _connection.Open();
            return _connection;
        }

        // Public Methods:
        public static List<JobTitle> ReadJobTitels()
        {
            string query = $"SELECT * FROM {JobTitelsTableName}";
            using SQLiteCommand command = new SQLiteCommand(query, Connection);
            using SQLiteDataReader reader = command.ExecuteReader();

            List<JobTitle> jobsTitles = new List<JobTitle>();
            while (reader.Read())
                jobsTitles.Add(new JobTitle(reader.GetValues()));

            return jobsTitles;
        }
        public static List<Job> ReadJobs(JobTitle jobTitle)
        {
            string query = $"SELECT DISTINCT City, State FROM {JobsTableName} WHERE JobTitleId ='{jobTitle.ID}'";
            using SQLiteCommand command = new SQLiteCommand(query, Connection);
            using SQLiteDataReader reader = command.ExecuteReader();

            List<Job> jobsTitles = new List<Job>();
            while (reader.Read())
                jobsTitles.Add(new Job(reader.GetValues()));

            return jobsTitles;
        }
    }
}
