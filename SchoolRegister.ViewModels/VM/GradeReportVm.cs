using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SchoolRegister.Model.DataModels;

namespace SchoolRegister.ViewModels.VM
{
    public class GradeReportVm
    {
        public int StudentId { get; set; }
        public List<GradeVm> Grades { get; set; }
    }
}