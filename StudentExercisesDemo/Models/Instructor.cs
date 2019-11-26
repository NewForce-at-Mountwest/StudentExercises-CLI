using System;

namespace StudentExercisesDemo.Models
{
    public class Instructor{
        public int Id {get;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public int CohortId { get; set; }
        public Cohort Cohort {get; set;}

    }
}