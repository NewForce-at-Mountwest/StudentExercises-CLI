using StudentExercisesDemo.Data;
using StudentExercisesDemo.Models;
using System;
using System.Collections.Generic;

namespace StudentExercisesDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Repository repo = new Repository();
            Console.WriteLine("Exercise Report:");
            repo.GetAllExercises().ForEach(exercise => Console.WriteLine($@"
Exercise Name: {exercise.Name}
Exercise Language: {exercise.Language}

"));
            Console.WriteLine("Students with Exercises:");
            List<Student> studentsWithExercises = repo.GetStudentsWithExercises();
            foreach(Student currentStudent in studentsWithExercises)
            {
                Console.WriteLine();
                Console.WriteLine($"Name: {currentStudent.FirstName} {currentStudent.LastName}");
                Console.WriteLine($"Cohort: {currentStudent.Cohort.CohortName}");
                Console.WriteLine("Assigned Exercises:");
                foreach(Exercise currentExercise in currentStudent.assignedExercises)
                {
                    Console.WriteLine($"{currentExercise.Name} -- {currentExercise.Language}");
                }
                Console.WriteLine("-----------------------------------------");

            }


        }
    }
}
