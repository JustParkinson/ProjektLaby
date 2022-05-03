using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class GradeService : BaseService, IGradeService
    {
        public GradeService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger)
        {
        }

        public GradeVm AddGradeToStudent(AddGradeToStudentVm addGradeToStudentVm)
        {
            try
            {
                if (addGradeToStudentVm == null)
                    throw new ArgumentNullException($"View model parameter is null");
                var gradeEntity = Mapper.Map<Grade>(addGradeToStudentVm);
                var student = DbContext.Users.OfType<Student>().FirstOrDefault(x=>x.Id == addGradeToStudentVm.StudentId);
                if (!(student == null))
                {
                    DbContext.Add(gradeEntity);
                }
                else
                    throw new ArgumentException("There is no student with this ID");
                DbContext.SaveChanges();
                var gradeVM = Mapper.Map<GradeVm>(gradeEntity);
                return gradeVM;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public GradeReportVm GetGradesReportForStudent(GetGradesReportVm getGradeVm)
        {
            try
            {
                if(getGradeVm == null)
                    throw new ArgumentNullException($"View model parameter is null");
                var studentEntity = DbContext.Users.OfType<Student>().FirstOrDefault(x=>x.Id == getGradeVm.StudentId);
                if( studentEntity == null)
                    throw new ArgumentException("There is no student with this ID");
                var gradeEntities = DbContext.Grades.AsQueryable().Where(x=>x.StudentId == getGradeVm.StudentId);
                var gradeEntitiesVm = Mapper.Map<IList<GradeVm>>(gradeEntities);
                var gradeReport = new GradeReportVm()
                {
                    StudentId = getGradeVm.StudentId,
                    Grades = gradeEntitiesVm
                };
                return gradeReport;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }        }
    }
}