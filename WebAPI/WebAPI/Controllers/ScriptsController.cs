using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Models;
using System.IO;
using WebAPI.Data;
using Microsoft.EntityFrameworkCore;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScriptsController : ControllerBase
    {


        private readonly ApplicationContext _context;

        public ScriptsController(ApplicationContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        {
            return await _context.Employee.Select(e => new Employee
            {
                EmployeeId = e.EmployeeId,
                EmployeeName = e.EmployeeName,
                DateOfBirth = e.DateOfBirth,
                DateOfEmployment = e.DateOfEmployment,
                Salary = e.Salary,
                PhotoFileName = e.PhotoFileName
            }).ToListAsync();
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetSalaryMore10000()
        {
            return await _context.Employee.Where(e => e.Salary >= 10000).Select(e =>  new Employee
            {
                EmployeeId = e.EmployeeId,
                EmployeeName = e.EmployeeName,
                DateOfBirth = e.DateOfBirth,
                DateOfEmployment = e.DateOfEmployment,
                Salary = e.Salary,
                PhotoFileName = e.PhotoFileName
            }).ToListAsync();
        }


        [Route("UpdateSalary")]
        [HttpPut]
        public async Task<IActionResult> UpdateSalary()
        {
            var employees = _context.Employee.Where(e => e.Salary <= 10000).ToList();
            foreach (Employee employee in employees)
            {
                employee.Salary = 15000;
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }

            return NoContent();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteOldEmployees()
        {
            _context.Employee.RemoveRange(_context.Employee.Where(e => DateTime.Compare(e.DateOfBirth, DateTime.Now) >= 70 ));
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
