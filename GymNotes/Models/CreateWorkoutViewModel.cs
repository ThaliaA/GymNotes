using GymNotes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymNotes.Models
{
    public class CreateWorkoutViewModel
    {
        public WorkoutDefinition WorkoutDefinition { get; set; }
        public List<ExerciseDefinition> ExerciseDefinitions { get; set; }
        public ExerciseDefinition ExerciseDefinition { get; set; }
    }
}