using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FrontEndQuiz.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using FrontEndQuiz.Models;

namespace FrontEndQuiz.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly DataContext _db;

        public HomeController(DataContext db)
        {
            _db = db;
        }
        
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {

            var topics = await _db.Topics.ToListAsync();
            return View(topics);
        }

        [HttpGet("StartQuiz/{id:int}")]
        public async Task<IActionResult> TopicQuiz(int id)
        {
             var questions = await _db.Questions.Include(q => q.Answers).Where(x => x.TopicId == id).ToListAsync();

             // Shuffle the list of questions.
             Random rng = new Random();
             var shuffledQuestions = questions.OrderBy(q => rng.Next()).ToList();

             return View(shuffledQuestions);

    }

    // New method to get all questions in Json with a 200 Ok status
    [HttpGet("GetAllQuestions")]
    public IActionResult GetAllQuestions()
    {
        List<Question> questionList = _db.Questions.ToList();
        return Ok(questionList);
    }

// New method to get all answers in Json with a 200 Ok status
    [HttpGet("GetAllAnswers")]
    public IActionResult GetAllAnswers()
    {
        List<Answer> answerList = _db.Answers.ToList();
        return Ok(answerList);
    }

  // Edit an individual answer
        [HttpGet("GetAnswer/{id}")]
        public async Task<IActionResult> GetAnswer(int id)
        {
            var answer = await _db.Answers.SingleOrDefaultAsync(a => a.AnswerId == id);

            return Ok(answer);
        }
}
}