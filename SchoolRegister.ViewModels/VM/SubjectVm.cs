using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace SchoolRegister.ViewModels.VM
{
    public class SubjectVm
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IList<GroupVm> Groups { get; set; }
        public string TeacherName { get; set; }
        public int? TeacherId { get; set; }
    }
}