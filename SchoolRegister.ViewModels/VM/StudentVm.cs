using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace SchoolRegister.ViewModels.VM
{
    public class StudentVm
    {
        public string StudentName { get; set; }
        public GroupVm Group { get; set; }
        public int? GroupId { get; set; }
        public IList<GradeVm> Grades { get; set; }
        public ParentVm Parent { get; set; }
        public int? ParentId { get; set; }
    }
}