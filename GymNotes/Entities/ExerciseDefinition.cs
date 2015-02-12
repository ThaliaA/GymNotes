using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymNotes.Entities
{
    public class ExerciseDefinition
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string TargetMuscle { get; set; }
    }
}