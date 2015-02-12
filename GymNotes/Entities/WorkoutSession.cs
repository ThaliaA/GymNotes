using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GymNotes.Entities
{
    public class WorkoutSession
    {
        public DateTime Date { get; set; }
        public Dictionary<Exercise, List<Set>> Exercise { get; set; }
    }
}