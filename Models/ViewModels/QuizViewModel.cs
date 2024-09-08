using System.ComponentModel.DataAnnotations;
using FrontEndQuiz.Models;
using FrontEndQuiz.Models.SpecModels;

namespace FrontEndQuiz.ViewModels
{
    public class QuizViewModel
    {

      public Question _question { get; set; }
      public List<Answer> _answer { get; set; }

      // Properties for topics
      public List<Topic> _topics { get; set; }
     public int SelectedTopicId { get; set; }

    }
}