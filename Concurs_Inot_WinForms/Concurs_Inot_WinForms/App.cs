using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Concurs_Inot_WinForms.Domain;
using Concurs_Inot_WinForms.Domain.Validator;
using Concurs_Inot_WinForms.Repository.DBRepositories;
using Concurs_Inot_WinForms.Repository.Interfaces;
using Concurs_Inot_WinForms.Service;
using Concurs_Inot_WinForms.Service.Interfaces;
using Concurs_Inot_WinForms.UI;
using log4net;
using log4net.Config;

namespace Concurs_Inot_WinForms;

static class App
{
    private static readonly ILog log = LogManager.GetLogger(typeof(App));
    /// <summary>
    ///  The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
        // Configurează log4net folosind fișierul XML
        XmlConfigurator.Configure(new FileInfo("log4net.config"));


        log.Info("Starting application...");

        String connectionString = ConfigurationManager.ConnectionStrings["tasksDB"].ConnectionString;

        //System.Console.WriteLine(connectionString);
        //TestRepoParticipants(connectionString);
        //testRepoSwimmingEvents(connectionString);
        //testRepoRecords(connectionString);
        //testRepoUsers(connectionString);


        IMainService mainService = new MainService(
            new ParticipantsDbRepo(connectionString, new ParticipantValidator()),
            new SwimmingEventsDbRepo(connectionString),
            new RecordsDBRepo(connectionString)
        );
        IAuthService authService = new AuthService(new UsersDbRepo(connectionString));


        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

#pragma warning disable CA1416 // Validate platform compatibility
        Application.Run(new LoginForm(authService, mainService));
#pragma warning restore CA1416 // Validate platform compatibility


        log.Info("Closing application...");
    }

    private static void TestRepoParticipants(String connectionString)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Testing participants...");
        Console.ResetColor();
        
        //Test
        ParticipantsDbRepo repo = new ParticipantsDbRepo(connectionString, new ParticipantValidator());
        
        Participant p1 = repo.FindOne(1);
        
        Console.WriteLine("Participant id 1: " + p1.FirstName + " " + p1.LastName + " " + p1.Age);
        
        Participant p2 = new Participant("test first name", "test last name", 99);
        repo.Save(p2);
        
        IEnumerable<Participant> participants = repo.FindAll();
        
        Console.WriteLine("Found {0} participants", participants.Count());
        foreach (Participant p in participants)
        {
            Console.WriteLine("Participant id "+ p.Id + ": " + p.FirstName + " " + p.LastName + " " + p.Age);
        }
        
        repo.Delete(participants.Last().Id);
        
        participants = repo.FindAll();
        
        Console.WriteLine("Found {0} participants after delete", participants.Count());
        foreach (Participant p in participants)
        {
            Console.WriteLine("Participant id "+ p.Id + ": " + p.FirstName + " " + p.LastName + " " + p.Age);
        }
        
        //----

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("End test participants...");
        Console.ResetColor();
    }
    
    private static void testRepoSwimmingEvents(String connectionString)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Testing swimming events...");
        Console.ResetColor();
        
        //Test
        SwimmingEventsDbRepo repo = new SwimmingEventsDbRepo(connectionString);
        
        SwimmingEvent p1 = repo.FindOne(1);
        
        Console.WriteLine("SwimmingEvent id 1: " + p1.Distance + " " + p1.Style );
        
        SwimmingEvent p2 = new SwimmingEvent(99, "test style");
        repo.Save(p2);
        
        IEnumerable<SwimmingEvent> events = repo.FindAll();
        
        Console.WriteLine("Found {0} events", events.Count());
        foreach (SwimmingEvent p in events)
        {
            Console.WriteLine("SwimmingEvent id "+ p.Id + ": " + p.Distance + " " + p.Style );
        }
        
        repo.Delete(events.Last().Id);
        
        events = repo.FindAll();
        
        Console.WriteLine("Found {0} events after delete", events.Count());
        foreach (SwimmingEvent p in events)
        {
            Console.WriteLine("SwimmingEvent id "+ p.Id + ": " + p.Distance + " " + p.Style );
        }

        //----

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("End test swimming events...");
        Console.ResetColor();
    }
    
    private static void testRepoRecords(String connectionString)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Testing records...");
        Console.ResetColor();
        
        //Test
        RecordsDBRepo repo = new RecordsDBRepo(connectionString);
        
        Record r1 = repo.FindOne(1);
        Participant p1 = r1.participant;
        SwimmingEvent s1 = r1.swimmingEvent;
        Console.WriteLine("Record id 1: " + p1.FirstName + " " + p1.LastName + " " + p1.Age + "\n" + s1.Distance + " " + s1.Style);

        s1.Id = 5;
        Record p2 = new Record(p1,s1);
        repo.Save(p2);
        
        IEnumerable<Record> records = repo.FindAll();
        
        Console.WriteLine("Found {0} records", records.Count());
        foreach (Record p in records)
        {
            Console.WriteLine("Record id "+ p.Id + ": " + p.participant.Id + " " + p.swimmingEvent.Id);
        }
        
        repo.Delete(records.Last().Id);
        
        records = repo.FindAll();
        
        Console.WriteLine("Found {0} records after delete", records.Count());
        foreach (Record p in records)
        {
            Console.WriteLine("Record id "+ p.Id + ": " + p.participant.Id + " " + p.swimmingEvent.Id);
        }

        //----
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("End test records...");
        Console.ResetColor();
    }
    
    
    private static void testRepoUsers(String connectionString)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Testing users...");
        Console.ResetColor();
        
        //Test
        UsersRepository repo = new UsersDbRepo(connectionString);
        
        User p1 = repo.FindOne(1);
        
        Console.WriteLine("User id 1: " + p1.username + " " + p1.password);
        
        User p2 = new User("test username", "test password");
        repo.Save(p2);
        
        IEnumerable<User> users = repo.FindAll();
        
        Console.WriteLine("Found {0} users", users.Count());
        foreach (User p in users)
        {
            Console.WriteLine("User id "+ p.Id + ": " + p.username + " " + p.password);
        }
        
        repo.Delete(users.Last().Id);
        
        users = repo.FindAll();
        
        Console.WriteLine("Found {0} users after delete", users.Count());
        foreach (User p in users)
        {
            Console.WriteLine("User id "+ p.Id + ": " + p.username + " " + p.password);
        }
        
        //----

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("End test users...");
        Console.ResetColor();
    }
}