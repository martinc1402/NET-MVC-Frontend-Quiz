using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using FrontEndQuiz.Models.SpecModels;

namespace FrontEndQuiz.Models
{
    public class Question
    {
        public int QuestionId { get; set; }

        public string Title { get; set; }
  
        public string QuestionString { get; set; }

        // Navigation property to hold answers
        public List<Answer> Answers { get; set; }

        public int TopicId { get; set; }

        public Topic Topic { get; set; }


    }
}