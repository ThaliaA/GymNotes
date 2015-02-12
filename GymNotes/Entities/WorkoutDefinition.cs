using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymNotes.Entities
{
    public class WorkoutDefinition
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Exercise> Exercises { get; set; }
    }
}