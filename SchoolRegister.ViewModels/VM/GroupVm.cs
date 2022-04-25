using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolRegister.ViewModels.VM
{
    public class GroupVm
    {
        public IList<SubjectVm> Subjects { get; set; }
        public IList<StudentVm> Students { get; set; }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}