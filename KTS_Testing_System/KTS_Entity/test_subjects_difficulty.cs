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
    
    public partial class test_subjects_difficulty
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public test_subjects_difficulty()
        {
            this.subject_difficulty_importance = new HashSet<subject_difficulty_importance>();
        }
    
        public long test_subject_difficulty_id { get; set; }
        public Nullable<long> user_test_subject_id { get; set; }
        public string difficulty_code { get; set; }
        public int no_of_questions { get; set; }
    
        public virtual question_difficulty question_difficulty { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<subject_difficulty_importance> subject_difficulty_importance { get; set; }
        public virtual user_test_subjects user_test_subjects { get; set; }
    }
}
