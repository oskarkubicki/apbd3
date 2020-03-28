﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apbd3.DAL;
using apbd3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace apbd3.Controllers


{

    [ApiController]
    [Route("api/students")]

    public class StudentsController : ControllerBase
    {
        private readonly IDbService _dbservice;

        List<Student> lista;


        public StudentsController(IDbService dbService)
        
        {

            _dbservice = dbService;
        }
        
        [HttpGet]
        public IActionResult GetStudents()
        {

            using (var client = new SqlConnection("Data Source=db-mssql.pjwstk.edu.pl;Initial Catalog=2019SBD;Integrated Security=True")) 
                using(var com =new SqlCommand())
            
            {

                com.Connection = client;
                com.CommandText = "select FirstName,LastName,BirthDate,Name,Semester from Student,Studies,Enrollment where Student.IdEnrollment=Enrollment.IdEnrollment and Enrollment.IdStudy=Studies.IdStudy";

                client.Open();
                var dr = com.ExecuteReader();


                lista = new List<Student>();


                while (dr.Read())
                {

                    var st = new Student();

                    st.Firstname = dr["FirstName"].ToString();
                    st.Lastname = dr["LastName"].ToString();
                    st.BirthDate = dr["BirthDate"].ToString();
                    st.Studies = dr["Name"].ToString();
                    st.Semester = dr["Semester"].ToString();


                    lista.Add(st);



                }

            }
            return Ok(lista);
        }


        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {

            if (id == 1)
            {

                return Ok("Kowalski");

            }
            else if (id == 2)
            {

                return Ok("Malewski");

            }
            else {

                return NotFound("The element was not found");
            }
        }


        [HttpPost]


        public IActionResult createStudent(Student student)
        {
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }



        [HttpPut("{Id}")]


        public IActionResult putStudent(int Id)


        {

            Student student = new Student();
            student.IdStudent = Id;
            student.IndexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok("Update complete");
        }



        [HttpDelete("{Id}")]


        public IActionResult deleteStudent(int Id)


        {


           
            return Ok("Delete complete");
        }

    }
}