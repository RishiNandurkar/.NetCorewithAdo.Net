using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using ADODemo.Models;
using System;
using Newtonsoft.Json.Linq;

namespace ADODemo.DataAccess
{
    public class StudentDA
    {
        private string _connectionString;

        public StudentDA(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Student> GetAllStudents()
        {
            List<Student> lst = new List<Student>();
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("GetStudentsSP", con); // Execute the stored procedure
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                // Parse JSON and populate Student objects
                                string jsonResult = reader.GetString(0);
                                JArray studentsArray = JArray.Parse(jsonResult);
                                foreach (JObject studentObject in studentsArray)
                                {
                                    Student obj = new Student();
                                    obj.Name = (string)studentObject["Name"];
                                    obj.ContactNumber = (int)studentObject["ContactNumber"];
                                    obj.Age = (int)studentObject["Age"];
                                    lst.Add(obj);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return new List<Student>(); // Return an empty list in case of exception
            }
            return lst;
        }

        public string AddStudent(Student obj)
        {
            string message = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("InsertStudentSP", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input parameters
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@ContactNumber", obj.ContactNumber);
                    cmd.Parameters.AddWithValue("@Age", obj.Age);
                    // Output parameter
                    SqlParameter messageParameter = new SqlParameter("@Message", SqlDbType.NVarChar, 200);
                    messageParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(messageParameter);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    // Get the value of the output parameter after executing the command
                    message = Convert.ToString(cmd.Parameters["@Message"].Value);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                message = "Something went wrong."; // Return a generic message in case of exception
            }
            return message; // Return the message
        }

        public string UpdateStudent(Student obj)
        {
            string message = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("UpdateStudentSP", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input parameters
                    cmd.Parameters.AddWithValue("@Id", obj.Id);
                    cmd.Parameters.AddWithValue("@Name", obj.Name);
                    cmd.Parameters.AddWithValue("@ContactNumber", obj.ContactNumber);
                    cmd.Parameters.AddWithValue("@Age", obj.Age);
                    // Output parameter
                    SqlParameter messageParameter = new SqlParameter("@Message", SqlDbType.NVarChar, 200);
                    messageParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(messageParameter);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    // Get the value of the output parameter after executing the command
                    message = Convert.ToString(cmd.Parameters["@Message"].Value);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                message = "Something went wrong."; // Return a generic message in case of exception
            }
            return message; // Return the message
        }

        public string DeleteStudent(Student obj)
        {
            string message = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    SqlCommand cmd = new SqlCommand("DeleteStudentsSP", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input parameters
                    cmd.Parameters.AddWithValue("@Id", obj.Id);
                    // Output parameter
                    SqlParameter messageParameter = new SqlParameter("@Message", SqlDbType.NVarChar, 200);
                    messageParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(messageParameter);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    // Get the value of the output parameter after executing the command
                    message = Convert.ToString(cmd.Parameters["@Message"].Value);
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                message = "Something went wrong."; // Return a generic message in case of exception
            }
            return message; // Return the message
        }
    }
}




//using Microsoft.Data.SqlClient;
//using System.Collections.Generic;
//using System.Data;
//using ADODemo.Models;
//using System;
//using Newtonsoft.Json.Linq;

//namespace ADODemo.DataAccess
//{
//    public class StudentDA
//    {
//        private string _connectionString;

//        public StudentDA(string connectionString)
//        {
//            _connectionString = connectionString;
//        }


//        public List<Student> GetAllStudents()
//        {
//            List<Student> lst = new List<Student>();
//            using (SqlConnection con = new SqlConnection(_connectionString))
//            {
//                SqlCommand cmd = new SqlCommand("GetStudentsSP", con); // Execute the stored procedure
//                con.Open();
//                using (SqlDataReader reader = cmd.ExecuteReader())
//                {
//                    if (reader.HasRows)
//                    {
//                        while (reader.Read())
//                        {
//                            // Parse JSON and populate Student objects
//                            string jsonResult = reader.GetString(0);
//                            JArray studentsArray = JArray.Parse(jsonResult);
//                            foreach (JObject studentObject in studentsArray)
//                            {
//                                Student obj = new Student();
//                                obj.Name = (string)studentObject["Name"];
//                                obj.ContactNumber = (int)studentObject["ContactNumber"];
//                                obj.Age = (int)studentObject["Age"];
//                                lst.Add(obj);
//                            }
//                        }
//                    }
//                }
//            }
//            return lst;
//        }





//        public string AddStudent(Student obj)
//        {
//            string message = string.Empty;
//            using (SqlConnection con = new SqlConnection(_connectionString))
//            {
//                SqlCommand cmd = new SqlCommand("InsertStudentSP", con);
//                cmd.CommandType = CommandType.StoredProcedure;

//                // Input parameters
//                cmd.Parameters.AddWithValue("@Name", obj.Name);
//                cmd.Parameters.AddWithValue("@ContactNumber", obj.ContactNumber);
//                cmd.Parameters.AddWithValue("@Age", obj.Age);
//                // Output parameter
//                SqlParameter messageParameter = new SqlParameter("@Message", SqlDbType.NVarChar, 200);
//                messageParameter.Direction = ParameterDirection.Output;
//                cmd.Parameters.Add(messageParameter);
//                con.Open();
//                cmd.ExecuteNonQuery();

//                // Get the value of the output parameter after executing the command
//                message = Convert.ToString(cmd.Parameters["@Message"].Value);
//            }
//            return message; // Return the message
//        }
//        public string UpdateStudent(Student obj)
//        {
//            string message = string.Empty;
//            using (SqlConnection con = new SqlConnection(_connectionString))
//            {
//                SqlCommand cmd = new SqlCommand("UpdateStudentSP", con);
//                cmd.CommandType = CommandType.StoredProcedure;

//                // Input parameters
//                cmd.Parameters.AddWithValue("@Id", obj.Id);
//                cmd.Parameters.AddWithValue("@Name", obj.Name);
//                cmd.Parameters.AddWithValue("@ContactNumber", obj.ContactNumber);
//                cmd.Parameters.AddWithValue("@Age", obj.Age);
//                // Output parameter
//                SqlParameter messageParameter = new SqlParameter("@Message", SqlDbType.NVarChar, 200);
//                messageParameter.Direction = ParameterDirection.Output;
//                cmd.Parameters.Add(messageParameter);
//                con.Open();
//                cmd.ExecuteNonQuery();

//                // Get the value of the output parameter after executing the command
//                message = Convert.ToString(cmd.Parameters["@Message"].Value);
//            }
//            return message; // Return the message
//        }
//        public string DeleteStudent(Student obj)
//        {
//            string message = string.Empty;
//            using (SqlConnection con = new SqlConnection(_connectionString))
//            {
//                SqlCommand cmd = new SqlCommand("DeleteStudentsSP", con);
//                cmd.CommandType = CommandType.StoredProcedure;

//                // Input parameters
//                cmd.Parameters.AddWithValue("@Id", obj.Id);
//                // Output parameter
//                SqlParameter messageParameter = new SqlParameter("@Message", SqlDbType.NVarChar, 200);
//                messageParameter.Direction = ParameterDirection.Output;
//                cmd.Parameters.Add(messageParameter);
//                con.Open();
//                cmd.ExecuteNonQuery();

//                // Get the value of the output parameter after executing the command
//                message = Convert.ToString(cmd.Parameters["@Message"].Value);
//            }
//            return message; // Return the message
//        }


//    }
//}



//public List<Student> GetAllStudents()
//{
//    List<Student> lst = new List<Student>();
//    using (SqlConnection con = new SqlConnection(_connectionString))
//    {
//        SqlCommand cmd = new SqlCommand("Select * from Students", con);
//        SqlDataAdapter da = new SqlDataAdapter(cmd);
//        DataTable dt = new DataTable();
//        da.Fill(dt);

//        foreach (DataRow row in dt.Rows)
//        {
//            Student obj = new Student();
//            obj.Name = row["Name"].ToString();
//            obj.ContactNumber = int.Parse(row["ContactNumber"].ToString());
//            obj.Age = int.Parse(row["Age"].ToString());
//            lst.Add(obj);
//        }
//    }
//    return lst;
//}

//public void AddStudent(Student obj)
//{
//    using (SqlConnection con = new SqlConnection(_connectionString))
//    {
//        SqlCommand cmd = new SqlCommand("InsertStudentSP", con);
//        cmd.CommandType=CommandType.StoredProcedure;

//        cmd.Parameters.AddWithValue("@Name", obj.Name);
//        cmd.Parameters.AddWithValue("@ContactNumber", obj.ContactNumber);
//        cmd.Parameters.AddWithValue("@Age", obj.Age);
//        con.Open();
//        cmd.ExecuteNonQuery();
//    }
//}