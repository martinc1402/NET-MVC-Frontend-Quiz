using FrontEndQuiz.Data;
using FrontEndQuiz.Migrations;
using FrontEndQuiz.Models;
using FrontEndQuiz.Models.SpecModels;
using FrontEndQuiz.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndQuiz.Controllers
{
  [ApiController]
    [Route("[controller]")]
    public class QuizController : Controller
    {
        private readonly DataContext _db;

        public QuizController(DataContext db)
        {
            _db = db;
        }



        // Create Question

        [HttpGet("CreateQuestion")]
        public ActionResult CreateQuestion()
        {
        
         List<Topic> TopicList = _db.Topics.ToList();
        
          QuizViewModel QVM = new QuizViewModel()
          {
             _question = new Question(),
             _answer = new List<Answer>()
             { new Answer() {},
               new Answer() {},
               new Answer() {},
               new Answer() {}},
               _topics = TopicList,
               SelectedTopicId = 0
          };
          
          return View(QVM);
        }

        [HttpPost("CreateQuestion")]
        public IActionResult CreateQuestion([FromForm] QuizViewModel quizViewModel)
        {

          if (!ModelState.IsValid)
          {
            // In case of validation errors, return the form with the validation errors.
            quizViewModel._topics = _db.Topics.ToList();
            return View(quizViewModel);
          }

          var question = new Question
          {
            Title = quizViewModel._question.Title,
            QuestionString = quizViewModel._question.QuestionString,
            TopicId = quizViewModel.SelectedTopicId,
            Answers = quizViewModel._answer
          };


          _db.Questions.Add(question);
          _db.SaveChanges();

          return RedirectToAction("Index");
           
        }

        // Actions relating to editing questions
        [HttpGet("EditQuestion")]
        public async Task<IActionResult> EditQuestion(int id)
        {

          var TopicList = _db.Topics.ToList();
          var question = await _db.Questions.Include(q => q.Answers).SingleOrDefaultAsync(x => x.QuestionId == id);
          
          QuizViewModel QVM = new QuizViewModel()
          {
             _question = question,
             _answer = question.Answers,
              _topics = TopicList,
              SelectedTopicId = question.TopicId
          };
          

            return View(QVM);
        }

        [HttpPost("EditQuestion")]

        public async Task<IActionResult> EditQuestion([FromForm] QuizViewModel quizViewModel)
        {

          if (!ModelState.IsValid)
          {
            // In case of validation errors, return the form with the validation errors.
            quizViewModel._topics = _db.Topics.ToList();
            return View(quizViewModel);
          }

          var question = await _db.Questions.Include(q => q.Answers).SingleOrDefaultAsync(x => x.QuestionId == quizViewModel._question.QuestionId);
          question.Title = quizViewModel._question.Title;
          question.QuestionString = quizViewModel._question.QuestionString;
          question.Answers = quizViewModel._question.Answers;
          question.TopicId = quizViewModel.SelectedTopicId;

          _db.SaveChanges();

           return RedirectToAction("Index");
        }

        // Delete a Question
        [HttpPost("DeleteQuestion")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
          var questionToDelete = await _db.Questions.Include(q => q.Answers).FirstOrDefaultAsync(x => x.QuestionId == id);
          _db.Questions.Remove(questionToDelete);
          _db.SaveChanges();

          return RedirectToAction("Index");
        }

        // Edit an individual answer
        [HttpGet("EditAnswer")]
        public async Task<IActionResult> EditAnswer(int id)
        {
            var answer = await _db.Answers.SingleOrDefaultAsync(a => a.AnswerId == id);

            return View(answer);
        }

        [HttpPost("EditAnswer")]
        public async Task<IActionResult> EditAnswer([FromForm] Answer answer)
        {
          if (!ModelState.IsValid)
          {
            // In case of validation errors, return the form with the validation errors.
            return View(answer);
          }

        var answerToUpdate = await _db.Answers.SingleOrDefaultAsync(a => a.AnswerId == answer.AnswerId);
        answerToUpdate.AnswerText = answer.AnswerText;
        answerToUpdate.IsCorrect = answer.IsCorrect;
        _db.SaveChanges();
        return RedirectToAction("Index");
        }

        // Add an Answer
        [HttpGet("CreateAnswer")]
        public IActionResult CreateAnswer(int id)
        {
          var answer = new Answer();
          answer.QuestionId = id;

          return View(answer);
        }

        [HttpPost("CreateAnswer")]
        public IActionResult CreateAnswer([FromForm] Answer answer)
        {
          if (!ModelState.IsValid)
          {
            // In case of validation errors, return the form with the validation errors.
            return View(answer);
          }

         _db.Answers.Add(answer);
          _db.SaveChanges();

          return RedirectToAction("Index");
        }


        // List of questions and answers (landing page)
   
       [HttpGet("")]
        public async Task<IActionResult> Index()
        {

            var questions = await _db.Questions.Include(q => q.Answers).Include(t => t.Topic).ToListAsync();
            return View(questions);
        }

    }
}