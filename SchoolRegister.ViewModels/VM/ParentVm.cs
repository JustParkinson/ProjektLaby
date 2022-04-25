using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace SchoolRegister.ViewModels.VM
{
    public class ParentVm
    {
        public string ParentName { get; set; }
        public virtual IList<StudentVm> Students { get; set; }
    }
}