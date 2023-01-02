//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KTS_Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public question()
        {
            this.answers = new HashSet<answer>();
            this.Test_questions = new HashSet<Test_questions>();
        }
    
        public long question_id { get; set; }
        public string description { get; set; }
        public long subject_id { get; set; }
        public string difficulty_code { get; set; }
        public string importance_code { get; set; }
        public int marks { get; set; }
        public Nullable<int> question_level_id { get; set; }
        public long user_Id { get; set; }
        public System.DateTime insertion_timestamp { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<answer> answers { get; set; }
        public virtual question_difficulty question_difficulty { get; set; }
        public virtual question_images question_images { get; set; }
        public virtual question_importance question_importance { get; set; }
        public virtual question_levels question_levels { get; set; }
        public virtual subject subject { get; set; }
        public virtual User User { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Test_questions> Test_questions { get; set; }
    }
}