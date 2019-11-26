using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentExercisesDemo.Models

{
    public class Student {

        public int Id { get; set; }
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public int CohortId { get; set; }
        public Cohort Cohort {get; set;}
        public List<Exercise> assignedExercises {get; set;} = new List<Exercise>();
    }
}