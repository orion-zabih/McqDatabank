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
    
    public partial class Test_versions
    {
        public long test_version_id { get; set; }
        public int version_number { get; set; }
        public Nullable<long> test_question_id { get; set; }
        public Nullable<long> test_id { get; set; }
    
        public virtual Test_questions Test_questions { get; set; }
        public virtual User_Tests User_Tests { get; set; }
    }
}
