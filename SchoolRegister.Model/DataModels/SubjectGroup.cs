using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolRegister.Model.DataModels
{
    public class SubjectGroup
    {
        public virtual Subject Subject { get; set; }
        [Key]
        public int SubjectId { get; set; }
        public virtual Group Group { get; set; }

        [Key]
        public int GroupId { get; set; }
        public SubjectGroup() { }
    }
}