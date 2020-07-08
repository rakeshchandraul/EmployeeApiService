using EmployeeManageMentWebApi.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace EmployeeManageMentWebApi.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class EmployeeController : ApiController
    {
        // GET: api/Employee
        public IEnumerable<Employee> Get()
        {
            using (var context = new TestDbEntities())
            {
                var data = context.Employees.ToList<Employee>();
                return data;
            }
        }

        // GET: api/Employee/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Employee
        public string Post(JArray value)
        {
            using (var context = new TestDbEntities())
            {
                foreach (var entityToInsert in value)
                {
                    var empRecord = entityToInsert.ToObject<Employee>();
                    var existingStudent = context.Employees.Where(s => s.ID == empRecord.ID).FirstOrDefault<Employee>();
                    if (existingStudent != null)
                    {
                        existingStudent.Name = empRecord.Name;
                        existingStudent.Contact = empRecord.Contact;
                        context.SaveChanges();
                    }
                    else
                    {
                        context.Employees.Add(empRecord);
                        context.SaveChanges();
                    }
                }
            }
            return "success";
        }

        // PUT: api/Employee/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Employee/5
        public void Delete(int id)
        {
        }
    }
}
