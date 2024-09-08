using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrontEndQuiz.Models;
using FrontEndQuiz.Models.SpecModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;


namespace FrontEndQuiz.Data
{
    public class DataContext: DbContext
    {


    public DataContext(DbContextOptions options): base(options)
    {
    }

    public DbSet<Question> Questions { get; set; }

    public DbSet<Answer> Answers { get; set; }

    public DbSet<Topic> Topics { get; set; }

}
}