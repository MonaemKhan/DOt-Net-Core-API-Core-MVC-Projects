using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizApplicationAPI.DbConn;
using QuizApplicationAPI.Model;

namespace QuizApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizQuestionController : Controller
    {
        private readonly DbConnectionContext _context;
        public QuizQuestionController(DbConnectionContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> ViewAllData()
        {
            var data = await _context.QuestionTable.ToListAsync();
            return Ok(data);
        }

        [HttpPost]
        public async Task<ActionResult> Createdata([FromBody] QuizQuestion task)
        {
            await _context.QuestionTable.AddAsync(task);
            await _context.SaveChangesAsync();
            return Ok(task);
        }
    }
}
