using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ADODemo.DataAccess;
using ADODemo.Models;
using System;

namespace ADODemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentDA _studentDA;

        public StudentsController(IConfiguration configuration)
        {
            _studentDA = new StudentDA(configuration.GetConnectionString("DevConnection"));
        }

        [HttpGet]
        [Route("GetAllStudents")]
        public IActionResult GetAllStudents()
        {
            try
            {
                var students = _studentDA.GetAllStudents();
                return Ok(students);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [Route("AddStudent")]
        public IActionResult AddStudent(Student obj)
        {
            try
            {
                // Validate input
                if (string.IsNullOrEmpty(obj.Name))
                {
                    return BadRequest("Please provide a valid name.");
                }

                // Call data access layer method to add student
                string message = _studentDA.AddStudent(obj);
                return Ok(message);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        [Route("UpdateStudent")]
        public IActionResult UpdateStudent(Student obj)
        {
            try
            {
                // Validate input
                if (string.IsNullOrEmpty(obj.Name))
                {
                    return BadRequest("Please provide a valid name.");
                }

                // Call data access layer method to update student
                string message = _studentDA.UpdateStudent(obj);
                return Ok(message);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpDelete]
        [Route("DeleteStudent")]
        public IActionResult DeleteStudent(Student obj)
        {
            try
            {
                // Call data access layer method to delete student
                string message = _studentDA.DeleteStudent(obj);
                return Ok(message);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}




































//using ADODemo.Models;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Data.SqlClient;
//using System.Data;

//namespace ADODemo.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class StudentController : ControllerBase
//    {
//        private readonly IConfiguration _configuration;

//        public StudentController (IConfiguration configuration) 
//        {
//            _configuration = configuration;
//        }

//        [HttpGet]
//        [Route("GetAllStudent")]
//        public List<Student> GetStudents() 
//        { 
//            List<Student> lst = new List<Student>();
//            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DevConnection"));
//            SqlCommand  cmd = new SqlCommand("Select * from Students", con);
//            SqlDataAdapter da =  new SqlDataAdapter(cmd);
//            DataTable dt =new DataTable();
//            da.Fill(dt);

//            for(int i =0; i < dt.Rows.Count; i++) 
//            { 
//                Student obj = new Student();
//                obj.Name = dt.Rows[i]["name"].ToString();
//                obj.ContactNumber = int.Parse(dt.Rows[i]["ContactNumber"].ToString());
//                obj.Age = int.Parse(dt.Rows[i]["Age"].ToString()); 
//                lst.Add(obj);
//            }
//            return lst;
//        }
//        [HttpPost]
//        [Route("AddStudent")]
//        public string AddStudent(Student obj) 
//        {
//            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DevConnection"));

//            SqlCommand cmd = new SqlCommand("insert into Students Values  ('','','')", con);
//            con.Open();
//            cmd.ExecuteNonQuery();
//            con.Close();
//            return "Record Save Succcesfully ";


//        }
//    }
//}
