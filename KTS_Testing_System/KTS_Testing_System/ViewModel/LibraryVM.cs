using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KTS_Entity;
namespace KTS_Testing_System.ViewModel
{
	public class LibraryVM:ResponseMessage
	{
        public Department department { get; set; }
        public question_levels questionLevels { get; set; }
        public subject subject { get; set; }
    }
    public class DepartmentIndexVM : ResponseMessage
    {
        public IEnumerable<Department> DepartmentsList { get; set; }

        public DepartmentIndexVM()
        {
            DepartmentsList = null;
        }
    }
    public class QuestionLevelsIndexVM : ResponseMessage
    {
        public IEnumerable<question_levels> QuestionLevelsList { get; set; }

        public QuestionLevelsIndexVM()
        {
            QuestionLevelsList = null;
        }
        
    }
    public class SubjectsIndexVM : ResponseMessage
    {
        public IEnumerable<subject> SubjectsList { get; set; }

        public SubjectsIndexVM()
        {
            SubjectsList = null;
        }

    }
    public class FilteredDepartments
    {
        public int serial_no { get; set; }
        public long department_id { get; set; }
        public string description { get; set; }
        public long? user_Id { get; set; }
        public string user { get; set; }
        public string insertion_timestamp { get; set; }
        public FilteredDepartments()
        {

        }
    }
    public class FilteredQuestionLevels
    {
        public int serial_no { get; set; }
        public int level_id { get; set; }
        public string description { get; set; }
        public FilteredQuestionLevels()
        {

        }
    }
    public class FilteredSubjects
    {
        public int serial_no { get; set; }
        public long subject_id { get; set; }
        public string description { get; set; }
        public FilteredSubjects()
        {

        }
    }
}