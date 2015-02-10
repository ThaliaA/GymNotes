using GymNotes.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq; 

namespace GymNotes.Controllers
{
    public class WorkoutController : Controller
    {
        // GET: Workout

        private static async Task<Database> GetDb(DocumentClient client) {
           
            Database database = client.CreateDatabaseQuery().Where(x => x.Id == "GymNotes").AsEnumerable().FirstOrDefault();

            return database;
        }
        private static async Task<DocumentCollection> GetCollection()
        {
            var client = new DocumentClient(new Uri("https://gymnotes.documents.azure.com:443"), "ocCyErceT/NWyuLFwOXTi1KIsT59oC1aiboEgx56R1DoOAmegoeYhIolEZK/ZB8UirfBhn/7ZmfqP5bk/5oNmg==");
            var database = await GetDb(client);
            // Create a document collection.
            var workoutCollection = client.CreateDocumentCollectionQuery(database.CollectionsLink).Where(x => x.Id == "Workouts").AsEnumerable().FirstOrDefault();
            if (workoutCollection == null)
            {
                DocumentCollection documentCollection = await client.CreateDocumentCollectionAsync(database.CollectionsLink,
                    new DocumentCollection
                    {
                        Id = "Workouts"
                    });
                return documentCollection;
            }
            return workoutCollection;
        }
        private static async Task<WorkoutViewModel> AddWorkout()
        {
            var client = new DocumentClient(new Uri("https://gymnotes.documents.azure.com:443"), "ocCyErceT/NWyuLFwOXTi1KIsT59oC1aiboEgx56R1DoOAmegoeYhIolEZK/ZB8UirfBhn/7ZmfqP5bk/5oNmg==");
            var database = await GetDb(client);
            var collection = await GetCollection();

            WorkoutViewModel workout = new WorkoutViewModel() { Name = "Allen Roger", Description = "Beskrivning", Exercises = new List<ExerciseModel>() };
            await client.CreateDocumentAsync(collection.SelfLink, workout);

            return workout;
        }
        public ActionResult Index()
        {
            var workoutCollection = GetCollection();
            var viewmodel = new WorkoutViewModel();

            return View(viewmodel);
        }

        public ActionResult Create()
        {
            var viewmodel = new WorkoutViewModel();
            
            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            var b = AddWorkout();
            var viewmodel = new WorkoutViewModel();

            return View(viewmodel);
        }
        public ActionResult Start()
        {
            var viewmodel = new WorkoutViewModel();
            
            return View(viewmodel);
        }
    }
}