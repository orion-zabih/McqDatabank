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
    
    public partial class question_images
    {
        public long question_id { get; set; }
        public byte[] image_data { get; set; }
    
        public virtual question question { get; set; }
    }
}
