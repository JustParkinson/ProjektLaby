using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.ViewModels.VM
{
    public class GradeVm
    {
        public GradeScale GradeValue { get; set; }
        public SubjectVm Subject { get; set; }
        public int SubjectId { get; set; }
        public int StudentId { get; set; }
        public StudentVm Student { get; set; }
    }
}