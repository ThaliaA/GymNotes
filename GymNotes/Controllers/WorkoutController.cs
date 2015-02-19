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
using GymNotes.Entities; 

namespace GymNotes.Controllers
{
    public class WorkoutController : Controller
    {
        // GET: Workout

        private static async Task<Database> GetDb(DocumentClient client) {
           
            Database database = client.CreateDatabaseQuery().Where(x => x.Id == "GymNotes").AsEnumerable().FirstOrDefault();

            return database;
        }
        private static async Task<DocumentCollection> GetCollection(string collection)
        {
            var client = new DocumentClient(new Uri("https://gymnotes.documents.azure.com:443"), "ocCyErceT/NWyuLFwOXTi1KIsT59oC1aiboEgx56R1DoOAmegoeYhIolEZK/ZB8UirfBhn/7ZmfqP5bk/5oNmg==");
            var database = await GetDb(client);
            // Create a document collection.
            var workoutCollection = client.CreateDocumentCollectionQuery(database.CollectionsLink).Where(x => x.Id == collection).AsEnumerable().FirstOrDefault();
            if (workoutCollection == null)
            {
                DocumentCollection documentCollection = await client.CreateDocumentCollectionAsync(database.CollectionsLink,
                    new DocumentCollection
                    {
                        Id = collection
                    });
                return documentCollection;
            }
            return workoutCollection;
        }
        private static async Task<WorkoutDefinition> AddWorkout(WorkoutDefinition model)
        {
            var client = new DocumentClient(new Uri("https://gymnotes.documents.azure.com:443"), "ocCyErceT/NWyuLFwOXTi1KIsT59oC1aiboEgx56R1DoOAmegoeYhIolEZK/ZB8UirfBhn/7ZmfqP5bk/5oNmg==");
            var database = await GetDb(client);
            var collection = await GetCollection("Workouts");

            await client.CreateDocumentAsync(collection.SelfLink, model);

            return model;
        }
        private static async Task<WorkoutDefinition> AddExercise(WorkoutDefinition model)
        {
            var client = new DocumentClient(new Uri("https://gymnotes.documents.azure.com:443"), "ocCyErceT/NWyuLFwOXTi1KIsT59oC1aiboEgx56R1DoOAmegoeYhIolEZK/ZB8UirfBhn/7ZmfqP5bk/5oNmg==");
            var database = await GetDb(client);
            var collection = await GetCollection("Workouts");

            await client.CreateDocumentAsync(collection.SelfLink, model);

            return model;
        }
        private static async Task<List<ExerciseDefinition>> AddExerciseDefinition(ExerciseDefinition model)
        {
            var client = new DocumentClient(new Uri("https://gymnotes.documents.azure.com:443"), "ocCyErceT/NWyuLFwOXTi1KIsT59oC1aiboEgx56R1DoOAmegoeYhIolEZK/ZB8UirfBhn/7ZmfqP5bk/5oNmg==");
            var database = await GetDb(client);
            var collection = await GetCollection("Exercises");

            await client.CreateDocumentAsync(collection.SelfLink, model);


            return await GetExerciseDefinitions();
        }
        private static async Task<List<ExerciseDefinition>> GetExerciseDefinitions( )
        {
            var client = new DocumentClient(new Uri("https://gymnotes.documents.azure.com:443"), "ocCyErceT/NWyuLFwOXTi1KIsT59oC1aiboEgx56R1DoOAmegoeYhIolEZK/ZB8UirfBhn/7ZmfqP5bk/5oNmg==");
            var database = await GetDb(client);
            var collection = await GetCollection("Exercises");

            // Query the documents using LINQ lambdas for the Andersen family.
            //var exercises = client.CreateDocumentQuery(collection.DocumentsLink,
            //"SELECT * FROM Exercises");
            var docs = client.CreateDocumentQuery<ExerciseDefinition>(collection.DocumentsLink).Select(f => f);                
            return docs.ToList<ExerciseDefinition>();
        }
        public ActionResult Index()
        {
            var workoutCollection = GetCollection("Workouts");
            var viewmodel = new WorkoutDefinition();

            return View(viewmodel);
        }

        public ActionResult Create()
        {
            var viewmodel = new CreateWorkoutViewModel();
            return View(viewmodel);
        }
        [HttpPost]
        public ActionResult Create(CreateWorkoutViewModel viewmodel)
        {
            
            var workoutmodel = AddWorkout(viewmodel.WorkoutDefinition);
            var listOfExerciseDefinitions = GetExerciseDefinitions();
            viewmodel.ExerciseDefinitions = listOfExerciseDefinitions.Result;
            Session["CreateWorkout"] = viewmodel;

            return View("AddExercises", viewmodel);

        }
        [HttpPost]
        public ActionResult AddExercises(FormCollection formCollection)
        {
            var createWorkoutViewModel = new CreateWorkoutViewModel();
            if (TryUpdateModel(createWorkoutViewModel, formCollection))
            {
                var workoutmodel = AddExerciseDefinition(createWorkoutViewModel.ExerciseDefinition);
                var sessionModel = Session["CreateWorkout"] as CreateWorkoutViewModel;
                sessionModel.ExerciseDefinitions = workoutmodel.Result;
                if (sessionModel != null)
                {
                    var exercise = new Exercise();
                    exercise.ExerciseDefinition = createWorkoutViewModel.ExerciseDefinition;
                    if (sessionModel.WorkoutDefinition.Exercises != null) {
                        
                        sessionModel.WorkoutDefinition.Exercises.Add(exercise);
                    }
                    else
                    {
                        sessionModel.WorkoutDefinition.Exercises = new List<Exercise>();
                        sessionModel.WorkoutDefinition.Exercises.Add(exercise);
                    }
                    Session["CreateWorkout"] = sessionModel;
                    return View("AddExercises", sessionModel);
                }

            }
            return RedirectToAction("Index", "Home");

        }
        public ActionResult Start()
        {
            var viewmodel = new WorkoutDefinition();
            
            return View(viewmodel);
        }
    }
}