using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TutorialRestApi.Models;

namespace TutorialRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select DepartmentId,DepartmentName from dbo.Department";
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
        public JsonResult Post(Department department)
        {
            string query = @"insert into dbo.Department values
             ('" + department.DepartmentName + @"')";
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
            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Department department)
        {
            string query = @"update dbo.Department set
            DepartmentName = '" + department.DepartmentName + @"' where  DepartmentId = " + department.DepartmentId + @" ";
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
            string query = @"delete from dbo.Department 
            where  DepartmentId = " + id + @" ";
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

        [HttpGet]
        [Route("getAllDepartmentNames")]
        public JsonResult getAllDepartmentNames()
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
