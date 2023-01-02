using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KTS_Entity;
namespace KTS_Testing_System.Classes
{
    public class Lookup
    {
        public static IEnumerable<SelectListItem> GetUserStatus()
        {
            IList<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem{Text = "active", Value = "active"},
                new SelectListItem{Text = "banned", Value = "banned"},
                new SelectListItem{Text = "deleted", Value = "deleted"}
            };
            return items;
        }
        public static SelectList GetLevels()
        {
            using (Kts_dataEntities context = new Kts_dataEntities())
            {
                IEnumerable<SelectListItem> resultList = (from obj in context.question_levels select obj).AsEnumerable().Select(obj => new SelectListItem() { Value = obj.level_id.ToString(), Text = Utility.ToTitlecase(obj.description.ToLower()) }).OrderBy(obj => obj.Text);
                return new SelectList(resultList.ToList(), "Value", "Text");
            }
        }
        public static SelectList GetSubjects()
        {
            using (Kts_dataEntities context = new Kts_dataEntities())
            {
                IEnumerable<SelectListItem> resultList = (from obj in context.subjects select obj).AsEnumerable().Select(obj => new SelectListItem() { Value = obj.subject_id.ToString(), Text = Utility.ToTitlecase(obj.description.ToLower()) }).OrderBy(obj => obj.Text);
                return new SelectList(resultList.ToList(), "Value", "Text");
            }
        }
        public static SelectList GetDifficulties()
        {
            using (Kts_dataEntities context = new Kts_dataEntities())
            {
                IEnumerable<SelectListItem> resultList = (from obj in context.question_difficulty select obj).AsEnumerable().Select(obj => new SelectListItem() { Value = obj.difficulty_code, Text = Utility.ToTitlecase(obj.description.ToLower()) }).OrderBy(obj => obj.Text);
                return new SelectList(resultList.ToList(), "Value", "Text");
            }
        }
        public static SelectList GetImportances()
        {
            using (Kts_dataEntities context = new Kts_dataEntities())
            {
                IEnumerable<SelectListItem> resultList = (from obj in context.question_importance select obj).AsEnumerable().Select(obj => new SelectListItem() { Value = obj.importance_code, Text = Utility.ToTitlecase(obj.description.ToLower()) }).OrderBy(obj => obj.Text);
                return new SelectList(resultList.ToList(), "Value", "Text");
            }
        }
    }
}