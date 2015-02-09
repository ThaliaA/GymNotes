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
        
        private static async Task<DateTime> CreateDb()
        {
           
            var client = new DocumentClient(new Uri("https://gymnotes.documents.azure.com:443"), "ocCyErceT/NWyuLFwOXTi1KIsT59oC1aiboEgx56R1DoOAmegoeYhIolEZK/ZB8UirfBhn/7ZmfqP5bk/5oNmg==");
            Database database = client.CreateDatabaseQuery().Where(x => x.Id == "GymNotes").AsEnumerable().FirstOrDefault();  
                
            // Create a document collection.
            DocumentCollection documentCollection = await client.CreateDocumentCollectionAsync(database.CollectionsLink,
                new DocumentCollection
                {
                    Id = "Workouts"
                });
                return DateTime.Now;
        }
        public ActionResult Index()
        {
            var dt = CreateDb();
            var viewmodel = new WorkoutViewModel();

            return View(viewmodel);
        }

        public ActionResult Create()
        {
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