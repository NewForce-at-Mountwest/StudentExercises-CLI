using StudentExercisesDemo.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace StudentExercisesDemo.Data
{
    class Repository
    {

        public SqlConnection Connection
        {
            get
            {
                // This is "address" of the database
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=StudentExercises;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }

        public List<Exercise> GetAllExercises()
        {

            using (SqlConnection conn = Connection)
            {

                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = "SELECT Id, Name, Language FROM Exercise";
                    SqlDataReader reader = cmd.ExecuteReader();
                    List<Exercise> exercises = new List<Exercise>();

                    while (reader.Read())
                    {

                        Exercise currentExercise = new Exercise
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Language = reader.GetString(reader.GetOrdinal("Language"))

                        };

                        exercises.Add(currentExercise);
                    }

                    reader.Close();
                    return exercises;
                }
            }
        }

        // Returns a list of students with their nested exercises 
        public List<Student> GetStudentsWithExercises()
        {

            using (SqlConnection conn = Connection)
            {

                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"SELECT Student.FirstName, Student.LastName, Cohort.Name AS 'Cohort Name', Exercise.Name AS 'Exercise Name', Exercise.Language 
FROM Student
JOIN Cohort ON Student.CohortId = Cohort.Id
JOIN StudentExercise ON Student.Id = StudentExercise.StudentId
JOIN Exercise ON StudentExercise.ExerciseId = Exercise.Id";
                    SqlDataReader reader = cmd.ExecuteReader();

                    // Make a big empty list of students that we'll add to
                    List<Student> studentsWithExercises = new List<Student>();

                    while (reader.Read())
                    {
                        // Get the student's first name-- we'll use it first to check and see if the student has already been added to the list
                        string studentFirstName = reader.GetString(reader.GetOrdinal("FirstName"));

                        // Check and see if the student is already in the list
                        if (studentsWithExercises.FirstOrDefault(student => student.FirstName == studentFirstName) == null)
                        {

                            // If not, create the student
                            Student currentStudent = new Student
                            {
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName"))

                            };

                            // And the cohort
                            Cohort currentCohort = new Cohort
                            {
                                CohortName = reader.GetString(reader.GetOrdinal("Cohort Name"))
                            };

                            // And the exercise
                            Exercise currentExercise = new Exercise
                            {
                                Name = reader.GetString(reader.GetOrdinal("Exercise Name")),
                                Language = reader.GetString(reader.GetOrdinal("Language"))
                            };

                            // Assign the cohort to the student
                            currentStudent.Cohort = currentCohort;

                            // Assign the exercise to the student
                            currentStudent.assignedExercises.Add(currentExercise);

                            // Add the student (with the cohort! and the exercises! because we already attached them!) to the big list
                            studentsWithExercises.Add(currentStudent);

                        } else
                        {
                            // If the student IS already in the list, we don't have to create it again
                            // All we need to do is create a new instance of exercise
                            Exercise currentExercise = new Exercise
                            {
                                Name = reader.GetString(reader.GetOrdinal("Exercise Name")),
                                Language = reader.GetString(reader.GetOrdinal("Language"))
                            };

                            // We still need to add the exercise to the right student.  Since it's already in the list, we have to dig into the list and find the right student 
                            Student studentToAssignTo = studentsWithExercises.FirstOrDefault(student => student.FirstName == studentFirstName);

                            studentToAssignTo.assignedExercises.Add(currentExercise);

                        }

                    
                    }

                    reader.Close();
                    return studentsWithExercises;
                }
            }
        }
    }
}
