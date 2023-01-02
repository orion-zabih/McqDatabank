using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KTS_Entity;
namespace KTS_Testing_System.ViewModel
{
	public class QuestionsVM : ResponseMessage
    {
        public question Question { get; set; }
        public List<AnswersForQuestion> AnswersForQuestion_DIV { get; set; }
        public QuestionsVM()
        {

            AnswersForQuestion_DIV = new List<AnswersForQuestion>();
        }
    }
    public class QuestionsIndexVM : ResponseMessage
    {
        public IEnumerable<question> QuestionsList { get; set; }
        public QuestionsIndexVM()
        {
            QuestionsList = null;
        }
    }
    public class FilteredQuestions
    {
        public int serial_no { get; set; }
        public long question_id { get; set; }
        public string description { get; set; }
        public string subject_name { get; set; }
        public string difficulty_code { get; set; }
        public string importance_code { get; set; }
        public int marks { get; set; }
        public string question_level { get; set; }
        public long user_Id { get; set; }
        public string user { get; set; }
        public string insertion_timestamp { get; set; }
        public FilteredQuestions()
        {

        }
    }
    public class AnswersForQuestion
    {
        public long answer_id { get; set; }
        public long question_id { get; set; }
        public string description { get; set; }
        public string correct_p { get; set; }
        public AnswersForQuestion()
        {
            correct_p = "false";
        }
    }
}