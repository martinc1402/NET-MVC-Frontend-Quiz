using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndQuiz.Models.ViewModels
{
    public class TopicImageUpdate
    {
        public int TopicId { get; set; } 
        public string TopicName { get; set;}
        public string TopicImageFilePath { get; set; }
    }
}