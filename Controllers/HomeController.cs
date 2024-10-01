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
using System.Dynamic;
using FrontEndQuiz.Models.SpecModels;

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
        
        // Original working home page
        // [HttpGet("")]
        // public async Task<IActionResult> Index()
        // {

        //     var topics = await _db.Topics.ToListAsync();
        //     return View(topics);
        // }

      // Attempt at new home page controller with either dynamic model or a view model with both Topics and Questions
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {

            List<Question> questionList = await _db.Questions.ToListAsync();
            var topicIDs = new List<int>();
            // Add all topic IDs from questions to a list
            foreach (Question question in questionList)
            {
                topicIDs.Add(question.TopicId);
            }

            topicIDs = topicIDs.Distinct().ToList();

            List<Topic> relevantTopicList = new List<Topic>();

             List<Topic> allTopics = await _db.Topics.ToListAsync();
             Random rng = new Random();
            var shuffledTopics = allTopics.OrderBy(q => rng.Next()).ToList();

             for (int i = 0; i < shuffledTopics.Count; i++) {
            
            foreach (int topicId in topicIDs) {
                if (shuffledTopics[i].TopicId == topicId) {
                     relevantTopicList.Add(shuffledTopics[i]);
                }
            }
             }

             
            

            dynamic myModel = new ExpandoObject();
            myModel.Topics = relevantTopicList;
            myModel.Questions = await _db.Questions.ToListAsync();
            return View(myModel);
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

    // Get All Questions of a particular topic
    [HttpGet("GetParticularQuestions/{topicId}")]
    public IActionResult GetParticularQuestions(int topicId)
    {
        List<Question> particularQuestionList = _db.Questions.Where(q => q.TopicId == topicId).ToList();
        Random rng = new Random();
        var shuffledQuestions = particularQuestionList.OrderBy(q => rng.Next()).ToList();
        return Ok(shuffledQuestions);
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