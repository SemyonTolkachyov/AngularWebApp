using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebAPI.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using WebAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _env;

        public EmployeeController(ApplicationContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> Get()
        {
            return await _context.Employee.ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult<Employee>> Post(Employee employee)
        {
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return Content("Added Successfully");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.EmployeeId == id);
        }


        [Route("SaveFile")]
        [HttpPost]
        public async Task<string> SaveFile([FromForm] IFormFile file)
        {
            try
            {
                string fName = file.FileName;
                string path = Path.Combine(_env.ContentRootPath, "Photos/" + Path.GetFileName(file.FileName));
                await file.CopyToAsync(new FileStream(path, FileMode.Create));
                return Path.GetFileName(file.FileName);
            }
            catch (Exception)
            {
                return "anonymous.png";
            }
        }


        [Route("GetAllDepartmentNames")]
        public async Task<ActionResult<IEnumerable<String>>> GetAllDepartmentNames()
        {
            return await _context.Department.Select(d => d.DepartmentName).ToListAsync();
        }
    }
}
