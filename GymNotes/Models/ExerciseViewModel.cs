using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymNotes.Models
{
    public class ExerciseModel
    {
        public ExerciseDefinition Exercise { get; set; }
        
    }

    public class ExerciseDefinition
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string TargetMuscle { get; set; }
    }
}