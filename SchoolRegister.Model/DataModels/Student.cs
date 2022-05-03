using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SchoolRegister.Model.DataModels
{
    public class Student : User
    {
        public virtual Group Group { get; set; }
        [ForeignKey("Group")]
        public int? GroupId { get; set; }
        public virtual IList<Grade> Grades { get; set; }
        public virtual Parent Parent { get; set; }
        [ForeignKey("Parent")]
        public int? ParentId { get; set; }

        [NotMapped]
        public double AveragedGrade => Grades == null || Grades.Count == 0 ? 0.0d :
                                                            Math.Round(Grades.Average(g => (int)g.GradeValue), 1);

        [NotMapped]
        public IDictionary<string, double> AverageGradePerSubject => Grades
            .GroupBy(z => z.Subject.Name)
            .Select(s => s)
            .ToDictionary(x => x.Key, x => x.Average(x => (int)x.GradeValue));

        [NotMapped]
        public IDictionary<string, List<GradeScale>> GradesPerSubject => Grades
            .GroupBy(z => z.Subject.Name)
            .Select(s => s)
            .ToDictionary(x => x.Key, x => x.Select(x => x.GradeValue).ToList());

        public Student() { }

    }


}
