using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TutorialRestApi.Models;

namespace TutorialRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;
        public EmployeeController(IConfiguration configuration,IWebHostEnvironment webHostEnvironment)
        {
            _configuration = configuration;
            _env = webHostEnvironment;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select EmployeeID,EmoployeeName,Department,
            convert(varchar(10),DateOfJoining,120) as DateOfJoining,
            PhotoFileName from dbo.Employee";
            DataTable _table = new DataTable();
            string _source = _configuration.GetConnectionString("Default");
            SqlDataReader _reader;
            using (SqlConnection _myCon = new SqlConnection(_source))
            {
                _myCon.Open();
                using (SqlCommand _command = new SqlCommand(query, _myCon))
                {
                    _reader = _command.ExecuteReader();
                    _table.Load(_reader);

                    _reader.Close();
                    _myCon.Close();
                }
            }
            return new JsonResult(_table);
        }

        [HttpPost]
        public JsonResult Post(Employee employee)
        {
            string query = @"insert into dbo.Employee values
             (
                '" + employee.EmoployeeName + @"',
                '" + employee.Department + @"',
                '" + employee.DateOfJoining + @"',
                '" + employee.PhotoFileName + @"'
             )";
            DataTable _table = new DataTable();
            string _source = _configuration.GetConnectionString("Default");
            SqlDataReader _reader;
            using (SqlConnection _myCon = new SqlConnection(_source))
            {
                _myCon.Open();
                using (SqlCommand _command = new SqlCommand(query, _myCon))
                {
                    _reader = _command.ExecuteReader();
                    _table.Load(_reader);

                    _reader.Close();
                    _myCon.Close();
                }
            }
            return new JsonResult("Success");
        }

        [HttpPut]
        public JsonResult Put(Employee employee)
        {
            string query = @"update dbo.Employee set
             EmoployeeName = '" + employee.EmoployeeName + @"',
             Department = '" + employee.Department + @"',
             DateOfJoining = '" + employee.DateOfJoining + @"',
             PhotoFileName = '" + employee.PhotoFileName + @"'
             where  EmployeeID = " + employee.EmployeeID + @" ";

            DataTable _table = new DataTable();
            string _source = _configuration.GetConnectionString("Default");
            SqlDataReader _reader;
            using (SqlConnection _myCon = new SqlConnection(_source))
            {
                _myCon.Open();
                using (SqlCommand _command = new SqlCommand(query, _myCon))
                {
                    _reader = _command.ExecuteReader();
                    _table.Load(_reader);

                    _reader.Close();
                    _myCon.Close();
                }
            }
            return new JsonResult("Success");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"delete from dbo.Employee 
            where  EmployeeID = " + id + @" ";
            DataTable _table = new DataTable();
            string _source = _configuration.GetConnectionString("Default");
            SqlDataReader _reader;
            using (SqlConnection _myCon = new SqlConnection(_source))
            {
                _myCon.Open();
                using (SqlCommand _command = new SqlCommand(query, _myCon))
                {
                    _reader = _command.ExecuteReader();
                    _table.Load(_reader);

                    _reader.Close();
                    _myCon.Close();
                }
            }
            return new JsonResult("Success");
        }

        [Route("SaveFile")]
        [HttpPost]

        public JsonResult SaveFile()
        {
            try
            {
                var _httprequest = Request.Form;
                var _postedFile = _httprequest.Files[0];
                string filename = _postedFile.FileName;
                var physicalpath = _env.ContentRootPath + "/Photos" + filename;
                using (var stream = new FileStream(physicalpath, FileMode.Create))
                {
                    _postedFile.CopyTo(stream);
                }
                return new JsonResult(filename);
            }
            catch (Exception ex)
            {
                return new JsonResult("annonyms.png");
            }
        }

        [Route("GetAllDepartmentNames")]
        public JsonResult GetAllDepartmentNames()
        {
            string query = @"select DepartmentName from dbo.Department";
            DataTable _table = new DataTable();
            string _source = _configuration.GetConnectionString("Default");
            SqlDataReader _reader;
            using (SqlConnection _myCon = new SqlConnection(_source))
            {
                _myCon.Open();
                using (SqlCommand _command = new SqlCommand(query, _myCon))
                {
                    _reader = _command.ExecuteReader();
                    _table.Load(_reader);

                    _reader.Close();
                    _myCon.Close();
                }
            }
            return new JsonResult(_table);
        }
    }
}
