using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KTS_Entity;

namespace KTS_Testing_System.ViewModel
{
    public class TestVM : ResponseMessage
    {
        public User_Tests Tests { get; set; }
        //public string level_id { get; set; }
        //public string subject_id { get; set; }
        //public string difficulty_code { get; set; }
        //public string importance_code { get; set; }
        public List<string> selected_questions { get; set; }
        public int TotalTestQuestions { get; set; }
        public List<SelectedQuestions> listSelectedQuestions { get; set; }
        public List<SubjectFilter> subjectsCollection_SUBDIV { get; set; }
        public TestVM()
        {
            selected_questions = new List<string>();
            listSelectedQuestions = new List<SelectedQuestions>();
            subjectsCollection_SUBDIV = new List<SubjectFilter>();
            TotalTestQuestions = 0;
        }
    }
    public class SubjectFilter
    {
        public long subject_id { get; set; }
        public long? user_test_subject_id { get; set; }
        //public string subject { get; set; }
        public int SubjectNoOFMcqs { get; set; }
        public List<DifficultyFilter> listDifficultyFilter { get; set; }
        public SubjectFilter()
        {
            listDifficultyFilter = new List<DifficultyFilter>();
        }
    }
    public class DifficultyFilter
    {
        public string difficulty_code { get; set; }
        public long? test_subject_difficulty_id { get; set; }
        public int DifficultyNoOFMcqs { get; set; }
        public List<ImportanceFilter> listImportanceFilter { get; set; }
        public DifficultyFilter()
        {
            listImportanceFilter = new List<ImportanceFilter>();
        }
    }
    public class ImportanceFilter
    {
        public string importance_code { get; set; }
        public long? subject_difficulty_importance_id { get; set; }
        public int ImportanceNoOFMcqs { get; set; }
        public ImportanceFilter()
        {

        }
    }
    public class FilteredTests
    {
        public int serial_no { get; set; }      
        public long test_id { get; set; }
        public string description { get; set; }
        public string level { get; set; }
        public int total_questions { get; set; }
        public int total_marks { get; set; }
        public int total_time_minutes { get; set; }
        public string test_creator { get; set; }
        public string status { get; set; }
        public int test_versions { get; set; }
        public long user_Id { get; set; }
        public string users { get; set; }
        public DateTime insertion_timestamp { get; set; }
        public string insertion_timestamp_string { get; set; }
    }
    public class TestIndexVM : ResponseMessage
    {
        public IEnumerable<FilteredTests> FilteredTestsList { get; set; }

        public TestIndexVM()
        {
            FilteredTestsList = null;
        }
    }
    public class FilteredTestVersions
    {
        public int serial_no { get; set; }
       
        public string question { get; set; }
    }
    public class TestVersionsIndexVM : ResponseMessage
    {
        public List<FilteredTestVersions> listFilteredTestVersions{ get; set; }
        public long test_id { get; set; }
        public string test_description { get; set; }
        public int total_questions { get; set; }

        public int test_versions { get; set; }
        public int version_number { get; set; }
        public TestVersionsIndexVM()
        {
            listFilteredTestVersions = new List<FilteredTestVersions>();
        }
    }

    public class FilteredQuestionsPool
    {
        public int serial_no { get; set; }
        public long question_id { get; set; }
        public string level { get; set; }
        public string subject { get; set; }
        public string difficulty { get; set; }
        public string importance { get; set; }
        public string question { get; set; }
        public int marks { get; set; }
        public long user_Id { get; set; }
        public string users { get; set; }
        public DateTime insertion_timestamp { get; set; }
        public string insertion_timestamp_string { get; set; }


        public FilteredQuestionsPool()
        {

        }
    }
    public class SelectedQuestions
    {
        public int serial_no { get; set; }
        public long question_id { get; set; }
        public string level { get; set; }
        public string subject { get; set; }
        public string difficulty { get; set; }
        public string importance { get; set; }
        public string question { get; set; }
        public int marks { get; set; }
        public long user_Id { get; set; }
        public string users { get; set; }
        public DateTime insertion_timestamp { get; set; }
        public string insertion_timestamp_string { get; set; }


        public SelectedQuestions()
        {

        }
    }
    public class TestQuestionVersion
    {
        public int QuestionNo { get; set; }
        public long? question_id { get; set; }
    }
}