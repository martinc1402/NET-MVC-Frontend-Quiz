using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndQuiz.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect{ get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }

    }
}