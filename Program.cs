using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace NedarvedeKlasser
{
    class Program
    {
        public const string connectionString = "Data Source =.; Initial Catalog = persons; Integrated Security = True";

        static void Main(string[] args)
        {
            Program program = new Program();

            List<Student> studentList = new List<Student>();
            List<Teacher> teacherList = new List<Teacher>();
            List<CourseModel> courseList = new List<CourseModel>();
            List<StudentGrade> studentGradesList = new List<StudentGrade>();
            List<Student> studentListUpload = new List<Student>();
            List<Teacher> teacherListUpload = new List<Teacher>();
            List<CourseModel> courseListUpload = new List<CourseModel>();
            List<StudentGrade> studentGradesListUpload = new List<StudentGrade>();

            //InitInput(studentListUpload, teacherListUpload, courseListUpload, studentGradesListUpload);
            //UploadSQL(studentListUpload, teacherListUpload, courseListUpload, studentGradesListUpload, program);


            //LoadDB(program, studentList, teacherList, courseList, studentGradesList);
            //WriteToScreen(studentList, teacherList, courseList, studentGradesList);

            //InputStudent(studentListUpload, 3, "Athea", 1);
            //InputTeacher(teacherListUpload, 2, "Søren", false, 29000);
            //InputCourse(courseListUpload, 231021083, CourseEnum.ComputerTechnology, "Computer Technology 23102108", 2);
            //InputStudentGrade(studentGradesListUpload, 231021083, 3, 10);
            //InputStudentGrade(studentGradesListUpload, 231021083, 2);
            UploadSQL(studentListUpload, teacherListUpload, courseListUpload, studentGradesListUpload, program);

            LoadDB(program, studentList, teacherList, courseList, studentGradesList);
            WriteToScreen(studentList, teacherList, courseList, studentGradesList);

        }


        private static void WriteToScreen(List<Student> studentList, List<Teacher> teacherList, List<CourseModel> CourseList, List<StudentGrade> studentGradesList)
        {

            Console.WriteLine("********************************************************************");
            Console.WriteLine("\t-- Elever --\n----------------------------------------------------");
            foreach (Student s in studentList)
            {
                Console.WriteLine("\n" +
                    s.Id + ", " +
                    s.Name + ", " +
                    s.Title + ", " +
                    s.Warnings);
                Console.WriteLine("\tFag:");
                foreach (StudentGrade SG in studentGradesList.Where(id => s.Id == id.FK_StudentId))
                {
                    foreach (CourseModel C in CourseList.Where(w => w.Id == SG.FK_CourseId))
                    { 
                        if (SG.Grade != null)
                        Console.WriteLine("\t" + C.Id + ", " + C.Name + ", " + C.Type + ", " + C.TeacherId + ", " + SG.Grade); 
                        else
                            Console.WriteLine("\t" + C.Id + ", " + C.Name + ", " + C.Type + ", " + C.TeacherId + ", " + "Ingen karakter");
                    }
                }
            }
            Console.WriteLine("\n\t-- Lærere --\n----------------------------------------------------");

            foreach (Teacher t in teacherList)
            {
                Console.WriteLine("\n" +
                    t.Id + ", " +
                    t.Name + ", " +
                    t.Title + ", " +
                    t.CoffeeClub + ", " +
                    t.Wages);
                Console.WriteLine("\tFag:");
                foreach (CourseModel C in CourseList.Where(w => w.TeacherId == t.Id))
                { Console.WriteLine("\t" + C.Id + ", " + C.Name + ", " + C.Type); }
            }
            Console.WriteLine("\n********************************************************************");
        }

        private static void LoadDB(Program program, List<Student> studentList, List<Teacher> teacherList, List<CourseModel> courseList, List<StudentGrade> studentGradesList)
        {
            studentList.Clear();
            teacherList.Clear();
            courseList.Clear();
            studentGradesList.Clear();
            
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string sql2 = "SELECT * FROM student";
                SqlCommand sqlCommand2 = new SqlCommand(sql2, sqlConnection);
                SqlDataReader reader = sqlCommand2.ExecuteReader();
                while (reader.Read())
                {
                    Student student = new Student();

                    student.Id = reader.GetInt32(0);
                    student.Name = reader.GetString(1);
                    TitleEnum titleEnum = new TitleEnum();
                    Enum.TryParse(reader.GetString(2), out titleEnum);
                    student.Title = titleEnum;
                    student.Warnings = reader.GetInt32(3);

                    studentList.Add(student);
                }
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string sql2 = "SELECT * FROM teacher";
                SqlCommand sqlCommand2 = new SqlCommand(sql2, sqlConnection);
                SqlDataReader reader = sqlCommand2.ExecuteReader();
                while (reader.Read())
                {
                    Teacher teacher = new Teacher();

                    teacher.Id = reader.GetInt32(0);
                    teacher.Name = reader.GetString(1);
                    TitleEnum titleEnum = new TitleEnum();
                    Enum.TryParse(reader.GetString(2), out titleEnum);
                    teacher.Title = titleEnum;
                    teacher.CoffeeClub = reader.GetBoolean(3);
                    teacher.Wages = reader.GetInt32(4);

                    teacherList.Add(teacher);
                }
            }
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string sql2 = "SELECT * FROM course";
                SqlCommand sqlCommand2 = new SqlCommand(sql2, sqlConnection);
                SqlDataReader reader = sqlCommand2.ExecuteReader();
                while (reader.Read())
                {
                    CourseModel course = new CourseModel();

                    course.Id = reader.GetInt32(0);
                    CourseEnum courseEnum = new CourseEnum();
                    Enum.TryParse(reader.GetString(1), out courseEnum);
                    course.Type = courseEnum;
                    course.Name = reader.GetString(2);
                    course.TeacherId = reader.GetInt32(3);

                    courseList.Add(course);
                }
            }

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                string sql2 = "SELECT * FROM studentCourse";
                SqlCommand sqlCommand2 = new SqlCommand(sql2, sqlConnection);
                SqlDataReader reader = sqlCommand2.ExecuteReader();
                while (reader.Read())
                {
                    StudentGrade SG = new StudentGrade();

                    SG.FK_StudentId = reader.GetInt32(0);
                    SG.FK_CourseId = reader.GetInt32(1);
                    if (!reader.IsDBNull(2))
                    {
                        SG.Grade = reader.GetInt32(2);
                    }
                    studentGradesList.Add(SG);
                }
            }
        }

        private static void UploadSQL(List<Student> studentListUpload, List<Teacher> teacherListUpload, List<CourseModel> courseListUpload, List<StudentGrade> studentGradesListUpload, Program program)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    foreach (Student s in studentListUpload)
                    {
                        string sql = "INSERT INTO student (Id, [name], title, warnings) VALUES(@Id, @name, @title, @warnings) ";
                        using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                        {
                            sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = s.Id;
                            sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = s.Name;
                            sqlCommand.Parameters.Add("@title", SqlDbType.NVarChar).Value = s.Title;
                            sqlCommand.Parameters.Add("@warnings", SqlDbType.Int).Value = s.Warnings;

                            sqlCommand.ExecuteNonQuery();
                            //Console.WriteLine(test.ToString());
                            //Console.ReadKey(true);
                        }
                    }
                    studentListUpload.Clear();

                    foreach (Teacher t in teacherListUpload)
                    {
                        string sql = "INSERT INTO teacher (Id, name, title, coffeeclub, wages) VALUES(@Id, @name, @title, @coffeeclub, @wages) ";
                        using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                        {
                            sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = t.Id;
                            sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = t.Name;
                            sqlCommand.Parameters.Add("@title", SqlDbType.NVarChar).Value = t.Title;
                            sqlCommand.Parameters.Add("@coffeeclub", SqlDbType.Bit).Value = t.CoffeeClub;
                            sqlCommand.Parameters.Add("@wages", SqlDbType.Int).Value = t.Wages;

                            sqlCommand.ExecuteNonQuery();
                            //Console.WriteLine(test.ToString());
                            //Console.ReadKey(true);
                        }
                    }
                    teacherListUpload.Clear();

                    foreach (CourseModel c in courseListUpload)
                    {
                        string sql = "INSERT INTO course (Id, type, name, teacherId) VALUES(@Id, @type, @name, @teacherId) ";
                        using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                        {
                            sqlCommand.Parameters.Add("@Id", SqlDbType.Int).Value = c.Id;
                            sqlCommand.Parameters.Add("@type", SqlDbType.NVarChar).Value = c.Type;
                            sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar).Value = c.Name;
                            sqlCommand.Parameters.Add("@teacherId", SqlDbType.Int).Value = c.TeacherId;

                            sqlCommand.ExecuteNonQuery();
                            //Console.WriteLine(test.ToString());
                            //Console.ReadKey(true);
                        }
                    }
                    courseListUpload.Clear();

                    foreach (StudentGrade s in studentGradesListUpload)
                    {
                        if (s.Grade != null)
                        {
                            string sql = "INSERT INTO studentCourse (FK_StudentId, FK_CourseId, grade) VALUES(@FK_StudentId, @FK_CourseId, @grade) ";
                            using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                            {
                                sqlCommand.Parameters.Add("@FK_StudentId", SqlDbType.Int).Value = s.FK_StudentId;
                                sqlCommand.Parameters.Add("@FK_CourseId", SqlDbType.Int).Value = s.FK_CourseId;

                                sqlCommand.Parameters.Add("@grade", SqlDbType.Int).Value = s.Grade;


                                sqlCommand.ExecuteNonQuery();
                                //Console.WriteLine(test.ToString());
                                //Console.ReadKey(true);
                            }
                        }
                        else
                        {
                            string sql = "INSERT INTO studentCourse (FK_StudentId, FK_CourseId) VALUES(@FK_StudentId, @FK_CourseId) ";
                            using (SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection))
                            {
                                sqlCommand.Parameters.Add("@FK_StudentId", SqlDbType.Int).Value = s.FK_StudentId;
                                sqlCommand.Parameters.Add("@FK_CourseId", SqlDbType.Int).Value = s.FK_CourseId;

                                sqlCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    studentGradesListUpload.Clear();

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Fail" + ex.ToString());
                }
            }
        }

        private static void InitInput(List<Student> studentListUpload, List<Teacher> teacherListUpload, List<CourseModel> courseListUpload, List<StudentGrade> studentGradesListUpload)
        {
            CourseModel course = new CourseModel
            {
                Id = 231021082,
                Type = CourseEnum.Network,
                Name = "Network 23102108",
                TeacherId = 1
            };
            courseListUpload.Add(course);

            CourseModel course2 = new CourseModel
            {
                Id = 231021080,
                Type = CourseEnum.Databases,
                Name = "Database 23102108",
                TeacherId = 1
            };
            courseListUpload.Add(course2);

            Teacher teachers = new Teacher
            {
                Id = 1,
                Name = "Henrik",
                Title = TitleEnum.Teacher,
                CoffeeClub = true,
                Wages = 30000,
                Courses = new List<CourseModel> { course, course2 }
            };
            teacherListUpload.Add(teachers);

            Student student = new Student
            {
                Id = 1,
                Name = "Anders",
                Title = TitleEnum.Student,
                Warnings = 0
            };
            studentListUpload.Add(student);

            Student student2 = new Student
            {
                Id = 2,
                Name = "Ditte",
                Title = TitleEnum.Student,
                Warnings = 0
            };
            studentListUpload.Add(student2);

            StudentGrade studentGrade = new StudentGrade
            {
                FK_CourseId = 231021080,
                FK_StudentId = 1,
                Grade = 12
            };
            studentGradesListUpload.Add(studentGrade);

            StudentGrade studentGrade2 = new StudentGrade
            {
                FK_CourseId = 231021082,
                FK_StudentId = 2,
                Grade = 10
            };
            studentGradesListUpload.Add(studentGrade2);

            StudentGrade studentGrade3 = new StudentGrade
            {
                FK_CourseId = 231021082,
                FK_StudentId = 1,
                Grade = 10
            };
            studentGradesListUpload.Add(studentGrade3);
        }

        private static void InputStudent(List<Student> studentListUpload, int id, string name, int warnings)
        {
            Student student = new Student
            {
                Id = id,
                Name = name,
                Title = TitleEnum.Student,
                Warnings = warnings
            };
            studentListUpload.Add(student);

        }

        private static void InputTeacher(List<Teacher> teacherListUpload, int id, string name, bool coffeeclub, int wages)
        {
            Teacher teacher = new Teacher
            {
                Id = id,
                Name = name,
                Title = TitleEnum.Teacher,
                CoffeeClub = coffeeclub,
                Wages = wages
            };
            teacherListUpload.Add(teacher);

        }

        private static void InputCourse(List<CourseModel> courseListUpload, int id, CourseEnum type, string name, int teacherId)
        {
            CourseModel course = new CourseModel
            {
                Id = id,
                Type = type,
                Name = name,
                TeacherId = teacherId
            };
            courseListUpload.Add(course);
        }

        private static void InputStudentGrade(List<StudentGrade> studentGradesListUpload, int courseId, int studentId)
        {
            StudentGrade studentGrade = new StudentGrade
            {
                FK_CourseId = courseId,
                FK_StudentId = studentId
            };
            studentGradesListUpload.Add(studentGrade);
        }
        
        private static void InputStudentGrade(List<StudentGrade> studentGradesListUpload, int courseId, int studentId, int grade)
        {
            StudentGrade studentGrade = new StudentGrade
            {
                FK_CourseId = courseId,
                FK_StudentId = studentId,
                Grade = grade
            };
            studentGradesListUpload.Add(studentGrade);
        }
    }
}
