using iTextSharp.text;
using iTextSharp.text.pdf;
using KTS_Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Telerik.Reporting.Processing;

namespace KTS_Reporting
{
    [System.ComponentModel.DataObject]
    public class ReportEngine
    {
        public static byte[] GetPaperReport(long test_id, int version, ReportExtension reportExtension)
        {
            var report = new Reports.Paper();
            report.ReportParameters["testid"].Value = test_id;
            report.ReportParameters["version"].Value = version;
            return RenderExtension(report, reportExtension);
        }

        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static List<TestVersionIndexVM> GetPaper(decimal test_id, int version)
        {

            try
            {
                List<TestVersionIndexVM> listTestVersionIndexVM = new List<TestVersionIndexVM>();
                using (Kts_dataEntities context = new Kts_dataEntities())
                {
                    User_Tests dbTest = context.User_Tests.Where(s => s.test_id == test_id).FirstOrDefault();
                    if (dbTest == null)
                        return listTestVersionIndexVM;
                    

                    var versionList = dbTest.Test_versions1.Where(v => v.version_number == version).OrderBy(o=>o.Test_questions.question.subject_id).GroupBy(item => item.Test_questions.question.subject_id);
                    var versiongroups = versionList.ToList();
                    int count = 0;
                    int countSubjects = 64;
                    foreach (var item in versiongroups)
                    {
                        countSubjects++;
                        TestVersionIndexVM objTestVersionIndexVM = new TestVersionIndexVM();
                        objTestVersionIndexVM.test_id = dbTest.test_id;
                        objTestVersionIndexVM.test_description = dbTest.description;
                        objTestVersionIndexVM.test_subject = ("PART " + (char)(countSubjects) + " \"" + item.FirstOrDefault().Test_questions.question.subject.description + " \"").ToUpper();
                        objTestVersionIndexVM.test_versions = dbTest.test_versions;
                        objTestVersionIndexVM.total_questions = dbTest.total_questions;
                        objTestVersionIndexVM.total_marks = dbTest.total_marks;
                        objTestVersionIndexVM.total_time_mins = dbTest.total_time_minutes+" Minutes";
                        objTestVersionIndexVM.version_number = version;
                        objTestVersionIndexVM.version_character = (char)(64 + version);
                        foreach (var g in item.OrderBy(o=>o.test_version_id))
                        {
                            count++;
                            Questions objQuestions = new Questions();
                            objQuestions.serial_no = count;
                            objQuestions.question_desc = g.Test_questions != null && g.Test_questions.question != null ? g.Test_questions.question.description : "";

                            if (g.Test_questions != null && g.Test_questions.question != null && g.Test_questions.question.answers != null)
                            {
                                int countAns = 0;
                                foreach (var objAnswer in g.Test_questions.question.answers.OrderBy(a => a.description))
                                {
                                    countAns++;
                                    PropertyInfo property = objQuestions.objAnswers.GetType().GetProperty("option" + countAns);
                                    property.SetValue(objQuestions.objAnswers, objAnswer.description, null);
                                    PropertyInfo propertyalphabet = objQuestions.objAnswers.GetType().GetProperty("option" + countAns + "alphabet");
                                    propertyalphabet.SetValue(objQuestions.objAnswers, ((char)(96 + countAns)).ToString() + ")", null);
                                }
                            }
                            objTestVersionIndexVM.listQuestions.Add(objQuestions);
                        }
                        listTestVersionIndexVM.Add(objTestVersionIndexVM);
                    }
                    /*
     .Select(group => new {
         ID = group.First().test_version_id,
         Counter = group.,
         SrvID = group.Key.SrvID,
         FirstName = group.First().FirstName
     })
     .OrderBy(item => item.ID)*/
                    //dbTest.Test_versions1.Where(v => v.version_number == objTestVersionIndexVM.version_number).OrderBy(s => s.test_question_id).ToList();
                    //int count = 0;
                    //versionList.ForEach(g =>
                    //{
                    //    count++;
                    //    Questions objQuestions = new Questions();
                    //    objQuestions.serial_no = count;
                    //    objQuestions.question_desc = g.Test_questions != null && g.Test_questions.question != null ? g.Test_questions.question.description : "";

                    //    if (g.Test_questions != null && g.Test_questions.question != null && g.Test_questions.question.answers!=null)
                    //    {
                    //        int countAns = 0;
                    //        foreach (var objAnswer in g.Test_questions.question.answers.OrderBy(a=>a.description))
                    //        {
                    //            countAns++;
                    //            PropertyInfo property = objQuestions.objAnswers.GetType().GetProperty("option"+ countAns);
                    //            property.SetValue(objQuestions.objAnswers, objAnswer.description, null);
                    //            PropertyInfo propertyalphabet = objQuestions.objAnswers.GetType().GetProperty("option" + countAns+ "alphabet");
                    //            propertyalphabet.SetValue(objQuestions.objAnswers, ((char)(96 + countAns)).ToString()+")", null);
                    //        }
                    //    }


                    //    listTestVersionIndexVM.listQuestions.Add(objQuestions);
                    //});
                }
                return listTestVersionIndexVM;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {

            }
        }

        public static byte[] GetPaperWithAnswersReport(long test_id, int version, ReportExtension reportExtension)
        {
            var report = new Reports.PaperWithAnswers();
            report.ReportParameters["testid"].Value = test_id;
            report.ReportParameters["version"].Value = version;
            return RenderExtension(report, reportExtension);
        }
        [System.ComponentModel.DataObjectMethod(System.ComponentModel.DataObjectMethodType.Select)]
        public static List<TestVersionIndexVM> GetPaperWithAnswers(decimal test_id, int version)
        {

            try
            {
                List<TestVersionIndexVM> listTestVersionIndexVM = new List<TestVersionIndexVM>();
                using (Kts_dataEntities context = new Kts_dataEntities())
                {
                    User_Tests dbTest = context.User_Tests.Where(s => s.test_id == test_id).FirstOrDefault();
                    if (dbTest == null)
                        return listTestVersionIndexVM;


                    var versionList = dbTest.Test_versions1.Where(v => v.version_number == version).OrderBy(o => o.Test_questions.question.subject_id).GroupBy(item => item.Test_questions.question.subject_id);
                    var versiongroups = versionList.ToList();
                    int count = 0;
                    int countSubjects = 64;
                    foreach (var item in versiongroups)
                    {
                        countSubjects++;
                        TestVersionIndexVM objTestVersionIndexVM = new TestVersionIndexVM();
                        objTestVersionIndexVM.test_id = dbTest.test_id;
                        objTestVersionIndexVM.test_description = dbTest.description;
                        objTestVersionIndexVM.test_subject = ("PART " + (char)(countSubjects) + " \"" + item.FirstOrDefault().Test_questions.question.subject.description + " \"").ToUpper();
                        objTestVersionIndexVM.test_versions = dbTest.test_versions;
                        objTestVersionIndexVM.total_questions = dbTest.total_questions;
                        objTestVersionIndexVM.total_marks = dbTest.total_marks;
                        objTestVersionIndexVM.total_time_mins = dbTest.total_time_minutes + " Minutes";
                        objTestVersionIndexVM.version_number = version;
                        objTestVersionIndexVM.version_character = (char)(64 + version);
                        foreach (var g in item.OrderBy(o => o.test_version_id))
                        {
                            count++;
                            Questions objQuestions = new Questions();
                            objQuestions.serial_no = count;
                            objQuestions.question_desc = g.Test_questions != null && g.Test_questions.question != null ? g.Test_questions.question.description : "";
                            
                            if (g.Test_questions != null && g.Test_questions.question != null && g.Test_questions.question.answers != null)
                            {
                                int countAns = 0;
                                foreach (var objAnswer in g.Test_questions.question.answers.OrderBy(a => a.description))
                                {
                                    countAns++;
                                    if(objAnswer.correct_p)
                                    {
                                        objQuestions.objAnswers.option1 = objAnswer.description;
                                        objQuestions.objAnswers.option1alphabet = "("+ ((char)(96 + countAns)).ToString() + ")";
                                    }
                                    //PropertyInfo property = objQuestions.objAnswers.GetType().GetProperty("option" + countAns);
                                    //property.SetValue(objQuestions.objAnswers, objAnswer.description, null);
                                    //PropertyInfo propertyalphabet = objQuestions.objAnswers.GetType().GetProperty("option" + countAns + "alphabet");
                                    //propertyalphabet.SetValue(objQuestions.objAnswers, ((char)(96 + countAns)).ToString() + ")", null);
                                }
                            }
                            objTestVersionIndexVM.listQuestions.Add(objQuestions);
                        }
                        listTestVersionIndexVM.Add(objTestVersionIndexVM);
                    }
                    /*
     .Select(group => new {
         ID = group.First().test_version_id,
         Counter = group.,
         SrvID = group.Key.SrvID,
         FirstName = group.First().FirstName
     })
     .OrderBy(item => item.ID)*/
                    //dbTest.Test_versions1.Where(v => v.version_number == objTestVersionIndexVM.version_number).OrderBy(s => s.test_question_id).ToList();
                    //int count = 0;
                    //versionList.ForEach(g =>
                    //{
                    //    count++;
                    //    Questions objQuestions = new Questions();
                    //    objQuestions.serial_no = count;
                    //    objQuestions.question_desc = g.Test_questions != null && g.Test_questions.question != null ? g.Test_questions.question.description : "";

                    //    if (g.Test_questions != null && g.Test_questions.question != null && g.Test_questions.question.answers!=null)
                    //    {
                    //        int countAns = 0;
                    //        foreach (var objAnswer in g.Test_questions.question.answers.OrderBy(a=>a.description))
                    //        {
                    //            countAns++;
                    //            PropertyInfo property = objQuestions.objAnswers.GetType().GetProperty("option"+ countAns);
                    //            property.SetValue(objQuestions.objAnswers, objAnswer.description, null);
                    //            PropertyInfo propertyalphabet = objQuestions.objAnswers.GetType().GetProperty("option" + countAns+ "alphabet");
                    //            propertyalphabet.SetValue(objQuestions.objAnswers, ((char)(96 + countAns)).ToString()+")", null);
                    //        }
                    //    }


                    //    listTestVersionIndexVM.listQuestions.Add(objQuestions);
                    //});
                }
                return listTestVersionIndexVM;
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {

            }
        }
        public static List<string> GetQuestionInfo(object sender)
        {
            var dataObject= (Telerik.Reporting.Processing.IDataObject)sender;
            var question = dataObject["listQuestions"] as List<Questions>;
            return question.Select(s=>s.question_desc).ToList();// question.FirstOrDefault().question_desc;
            //var dataObject = (Telerik.Reporting.Processing.IDataObject)sender;
            //var contactInfo = dataObject["listQuestions"] as string;
            //var xDoc = System.Xml.Linq.XDocument.Parse(contactInfo);
            //var telephones = xDoc.Root
            //                     .Elements()
            //                     .Where(elem => elem.Name.LocalName == "question_desc")
            //                     .Select(elem => elem.Value);
            //return string.Format("{0}{1}'s telephones: {2}", dataObject["Title"], dataObject["LastName"], string.Join("; ", telephones));
        }
        public static byte[] ConcatPdfs(IList<byte[]> pdfByteContent)
        {
            using (var ms = new MemoryStream())
            {
                using (var doc = new Document())
                {
                    using (var pdf = new PdfSmartCopy(doc, ms))
                    {
                        doc.Open();
                        //Loop through each byte array
                        foreach (var bytes in pdfByteContent)
                        {
                            //var bytes = p;
                            if (bytes == null || bytes.Length <= 0)
                                continue;
                            //Create a PdfReader bound to that byte array
                            using (var reader = new PdfReader(bytes))
                            {
                                for (int i = 0; i < reader.NumberOfPages; i++)
                                {
                                    var page = pdf.GetImportedPage(reader, i + 1);
                                    pdf.AddPage(page);
                                }
                                pdf.FreeReader(reader);
                                reader.Close();
                            }
                        }
                        doc.Close();
                    }
                }
                //Return just before disposing
                return ms.ToArray();
            }
        }

        public static string ToTitlecase(string name)
        {
            if (name != "")
            {
                CultureInfo cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;

                string title = textInfo.ToTitleCase(name.ToLower());
                return title;
            }
            else
                return name;
        }

        public static byte[] RenderExtension(Telerik.Reporting.Report report, ReportExtension reportExtension)
        {
            try
            {
                ReportProcessor reportProcessor = new ReportProcessor();
                Telerik.Reporting.InstanceReportSource instanceReportSource = new Telerik.Reporting.InstanceReportSource();
                instanceReportSource.ReportDocument = report;
                RenderingResult result = null;
                Object obj = new Object();

                lock (obj)
                {
                    result = reportProcessor.RenderReport(reportExtension.ToString(), instanceReportSource, null);
                }

                if (result == null || result.DocumentBytes == null || result.DocumentBytes.Length <= 0)
                    return null;
                using (MemoryStream ms = new MemoryStream())
                {
                    ms.Write(result.DocumentBytes, 0, result.DocumentBytes.Length);
                    return ms.ToArray();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
    public class TestVersionIndexVM 
    {
        public List<Questions> listQuestions { get; set; }
        public long test_id { get; set; }
        public string test_description { get; set; }
        public string test_subject { get; set; }
        public int total_questions { get; set; }
        public int total_marks { get; set; }
        public string total_time_mins { get; set; }
        public int test_versions { get; set; }
        public int version_number { get; set; }
        public char version_character { get; set; }
        public TestVersionIndexVM()
        {
            listQuestions = new List<Questions>();
        }
    }
    public class Questions
    {
        public int serial_no { get; set; }
        public string question_desc { get; set; }
        

        public Answers objAnswers { get; set; }
        public Questions()
        {
            objAnswers = new Answers();
            //listAnswers = new List<Answers>();
        }
    }
    public class Answers
    {
        public string option1 { get; set; }
        public string option2 { get; set; }
        public string option3 { get; set; }
        public string option4 { get; set; }
        public string option5 { get; set; }
        public string option6 { get; set; }
        public string option1alphabet { get; set; }
        public string option2alphabet { get; set; }
        public string option3alphabet { get; set; }
        public string option4alphabet { get; set; }
        public string option5alphabet { get; set; }
        public string option6alphabet { get; set; }
        //public string answer_desc { get; set; }
        //public bool correct_p {get;set;}
        public Answers()
        {
            //correct_p = false;
        }
    }
    public enum ReportExtension
    {
        PDF,
        XLS,
        CSV,
        RTF,
        XPS,
        DOCX,
        XLSX,
        PPTX,
        MHTML,
        IMAGE,
        HTML5
    }

}
