using EmplyeeWebAPI.DatabaseConnection;
using EmplyeeWebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EmplyeeWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly DbConnectionContext _context;
        public EmployeeController(DbConnectionContext context)
        {
           _context = context;
        }

        //update employee details
        [HttpPut("Id:int")]
        public async Task<IActionResult> UpdateEmployeInfo(int Id, EmployeeDetails emp)
        {
            var data = await _context.EmployeeDetailsTable.FindAsync(Id); //find if data is available in database
            var data_empcode = await _context.EmployeeDetailsTable.Where(e=>e.employeeCode == emp.employeeCode).ToListAsync(); //find if employeeCode is available or not
            if (data == null)
            {
                //if data is null
                return NotFound("Data Is Not Available");
            }
            else if (data_empcode.Count == 0) //check if employeeCode is not exit
            {
                //if employeeCode does not exit then try to update data
                try
                {
                    // as per instruction update emp name and emp code
                    data.employeeName = emp.employeeName;
                    data.employeeCode = emp.employeeCode;
                    //data.employeeSalary = emp.employeeSalary;
                    //data.supervisorId = emp.supervisorId;                    

                    _context.Entry(data).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return Ok(data);
                }catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                if(data.employeeId == data_empcode[0].employeeId) // if employee code is available and match with the data get by id, then we can update employee value
                {
                    try
                    {
                        data.employeeName = emp.employeeName;
                        data.employeeCode = emp.employeeCode;
                        data.employeeSalary = emp.employeeSalary;
                        data.supervisorId = emp.supervisorId;

                        _context.Entry(data).State = EntityState.Modified;
                        await _context.SaveChangesAsync();
                        return Ok(data);
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }                  
                }
                else // if employeCode is already available and doesn't match with the data then its a badrequest
                {
                    return BadRequest(emp.employeeCode + "is Already Available");
                }
            }
            
        }

        //get third highest salary holder employee
        [HttpGet]
        [Route("/ThirdHighestSalary")]
        public async Task<ActionResult> Get()
        {
            var data = await _context.EmployeeDetailsTable.OrderBy(e=>e.employeeSalary).Reverse().Skip(2).Take(1).ToListAsync(); // get third highest salary, becaues they order by desending order.
            try
            {
                var re = new
                {
                    third_Highest_Salary = data
                };
                return Ok(re);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //get minToMax and maxToMin salary of employee who hav not been absent for a day
        [HttpGet]
        [Route("/MinMaxSalary")]
        public async Task<IActionResult> MaxMinSalary()
        {
            var emp = await _context.EmployeeDetailsTable.ToListAsync(); // get employee table data
            var attendences = await _context.EmployeeAttendenceTable.ToListAsync(); // get attendence table data

            var attendence = attendences.GroupBy(e => e.employeeId).Select(gr => new
            {
                empid = gr.Key,
                isabsent = gr.Sum(e => e.isAbsent)
            }); // groupby employee and find all the absent record

            var MinToMaxSalary = from at in attendence
                                 join e in emp on at.empid equals e.employeeId
                                 where at.isabsent == 0
                                 orderby e.employeeSalary
                                 select new
                                 {
                                     id = at.empid,
                                     name = e.employeeName,
                                     empcode = e.employeeCode,
                                     salary = e.employeeSalary,
                                 };

            var re = new
            {
                ForNotAbsent_Salary_MaxToMin = MinToMaxSalary.Reverse(),
                ForNotAbsent_Salary_MinToMax = MinToMaxSalary,
            };
            return Ok(re);
        }

        //get monthely attendence report
        [HttpGet]
        [Route("/MonthelyReport")]
        public async Task<IActionResult> MonthelyReport()
        {
            var attendences = from at in await _context.EmployeeAttendenceTable.ToListAsync()
                             select new
                             {
                                 empid = at.employeeId,
                                 isPresent = at.isPresent,
                                 isAbsent = at.isAbsent,
                                 isOffday = at.isOffday,
                                 year = Convert.ToInt32(at.attendenceDate.Split("-")[0]),
                                 Month = Convert.ToInt32(at.attendenceDate.Split("-")[1])
                             };

            string[] monthname = new string[] { "January","February","March","April","May","June","July","Auguest","September","October","Novenber","Decenber" };

            var attendence = attendences.GroupBy(e => new { e.empid, e.year, e.Month }).Select(gr => new
            {
                empid = gr.Key.empid,
                Month = monthname[gr.Key.Month-1],
                present = gr.Sum(e=>e.isPresent),
                absent = gr.Sum(e=>e.isAbsent),
                offDay = gr.Sum(e=>e.isOffday),

            });

            var report = from at in attendence
                         join emp in await _context.EmployeeDetailsTable.ToListAsync()
                         on at.empid equals emp.employeeId
                         orderby emp.employeeId
                         select new
                         {
                            employeeName = emp.employeeName,                            
                            monthName = at.Month,
                            employeeSalary = emp.employeeSalary,
                            totalPresent = at.present,
                            totalAbsent = at.absent,
                            totalOffDay = at.offDay,
                         };
            return Ok(report);
        }

        //get hierarychy of employee and supervisor
        [HttpGet]
        [Route("/heierchy")]
        public async Task<IActionResult> GetHierarychy( int Id)
        {
            bool flag = true;
            int supid = 0;
            var data = await _context.EmployeeDetailsTable.FindAsync(Id);  // find 1st value          
            HashSet<string> names = new HashSet<string>();
            
            if (data != null)
            {
                names.Add(data.employeeName); // add name of employee
                supid = data.supervisorId;
                while (flag)
                {
                    var ss = await _context.EmployeeDetailsTable.FindAsync(supid);// search based on supervisorId
                    if (ss != null) // if not null then proceed
                    {
                        if (names.Contains(ss.employeeName)) // if name is already exits then just skip the process by making flag false 
                        {
                            flag = false;
                        }
                        else
                        {
                            names.Add(ss.employeeName); // add name
                            supid = ss.supervisorId; // update supervisorId
                        }
                    }
                    else
                    {
                        flag = false;
                    }
                }
                return Ok(string.Join(" -> ",names.ToArray())); // join string to show the desier output
            }
            else
            {
                return BadRequest("Data Not Available");
            }
        }
    }
}
