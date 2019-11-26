using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentExercisesDemo.Models
{
    public class Cohort {
       
        public string CohortName {get; set;}
        public List<Student> StudentList {get; set;} = new List<Student>();
        public List<Instructor> InstructorList {get; set;} = new List<Instructor>();

    }
}