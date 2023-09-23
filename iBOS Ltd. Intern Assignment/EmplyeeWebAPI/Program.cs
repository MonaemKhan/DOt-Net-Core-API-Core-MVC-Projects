using EmplyeeWebAPI.DatabaseConnection;
using EmplyeeWebAPI.Model;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//useing InMemoryDatabase
builder.Services.AddDbContext<DbConnectionContext>(option=>option.UseInMemoryDatabase("EmployeeDatabase")); // add database connection

var app = builder.Build();

// add database table value
using(var scope = app.Services.CreateScope()) // use this add data into InMemoryDatabase
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DbConnectionContext>();
    if (!dbContext.EmployeeDetailsTable.Any()) // add value to employee details table
    {
        dbContext.EmployeeDetailsTable.AddRange(
                 new EmployeeDetails
                 {
                     employeeId = 502030,
                     employeeName = "Mehidi Hasan",
                     employeeCode = "EMP320",
                     employeeSalary = 50000,
                     supervisorId = 502036,
                 },
                new EmployeeDetails
                {
                    employeeId = 502031,
                    employeeName = "Ashikur Rahman",
                    employeeCode = "EMP321",
                    employeeSalary = 45000,
                    supervisorId = 502036,
                },
                new EmployeeDetails
                {
                    employeeId = 502032,
                    employeeName = "Rakibul Islam",
                    employeeCode = "EMP322",
                    employeeSalary = 52000,
                    supervisorId = 502030,
                },
                new EmployeeDetails
                {
                    employeeId = 502033,
                    employeeName = "Hasan Abdullah",
                    employeeCode = "EMP323",
                    employeeSalary = 46000,
                    supervisorId = 502031,
                },
                new EmployeeDetails
                {
                    employeeId = 502034,
                    employeeName = "Akib Khan",
                    employeeCode = "EMP324",
                    employeeSalary = 66000,
                    supervisorId = 502032,
                },
                new EmployeeDetails
                {
                    employeeId = 502035,
                    employeeName = "Rasel Shikder",
                    employeeCode = "EMP325",
                    employeeSalary = 53500,
                    supervisorId = 502033,
                },
                new EmployeeDetails
                {
                    employeeId = 502036,
                    employeeName = "Selim Reja",
                    employeeCode = "EMP326",
                    employeeSalary = 59000,
                    supervisorId = 502035,
                }
        );
        dbContext.SaveChanges();
    }
    if (!dbContext.EmployeeAttendenceTable.Any()) // add value to employee attendence table
    {
        dbContext.EmployeeAttendenceTable.AddRange(
            new EmployeePresent
            {
                //new add
                employeeId = 502030,
                attendenceDate = "2023-06-23", // 4 record
                isPresent = 1,
                isAbsent = 0,
                isOffday = 0, 
            },
            new EmployeePresent
            {
                employeeId = 502030,
                attendenceDate = "2023-06-24",
                isPresent = 1,
                isAbsent = 0,
                isOffday = 0,
            },
            new EmployeePresent
            {
                //new add
                 employeeId = 502030,
                 attendenceDate = "2023-06-25",
                 isPresent = 0,
                 isAbsent = 1,
                 isOffday = 0,
            },
            new EmployeePresent
            {
                //new add
                employeeId = 502030,
                attendenceDate = "2023-06-26",
                isPresent = 0,
                isAbsent = 0,
                isOffday = 1,
            },
            new EmployeePresent
            {
                    employeeId = 502031,
                    attendenceDate = "2023-06-25", // 1 record
                    isPresent = 1,
                    isAbsent = 0,
                    isOffday = 0,
            },
            new EmployeePresent
            {
                //new add
                employeeId = 502034,
                attendenceDate = "2023-06-25", // 1 record
                isPresent = 1,
                isAbsent = 0,
                isOffday = 0,
            },
            new EmployeePresent
            {
                //new add
                employeeId = 502036,
                attendenceDate = "2023-06-25", // 1 record
                isPresent = 1,
                isAbsent = 0,
                isOffday = 0,
            }
        );
        dbContext.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
