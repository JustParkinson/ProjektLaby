using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;

namespace SchoolRegister.Services.ConcreteServices
{
    public class TeacherService : BaseService, ITeacherService
    {
        UserManager<User> _userManager;

        public TeacherService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger) 
        : base(dbContext, mapper, logger) { }

        public TeacherVm GetTeacher(Expression<Func<Teacher, bool>> filterPredicate)
        {
            try
            {
                if (filterPredicate == null)
                    throw new ArgumentNullException($" FilterExpression is null");
                var teacherEntity = DbContext.Users.OfType<Teacher>().FirstOrDefault(filterPredicate);
                var teacherVm = Mapper.Map<TeacherVm>(teacherEntity);
                return teacherVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }        
        }

        public IEnumerable<TeacherVm> GetTeachers(Expression<Func<Teacher, bool>> filterPredicate = null)
        {
            try
            {
                var teacherEntities = DbContext.Users.OfType<Teacher>().AsQueryable();
                if (filterPredicate != null)
                    teacherEntities = teacherEntities.Where(filterPredicate);
                var teacherVms = Mapper.Map<IEnumerable<TeacherVm>>(teacherEntities);
                return teacherVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }        
        }

        public IEnumerable<GroupVm> GetTeachersGroups(TeachersGroupsVm getTeachersGroups)
        {
            try
            {
                var teachersGroups = DbContext.Subjects
                        .Where(x => x.TeacherId == getTeachersGroups.TeacherId)
                        .SelectMany(x=>x.SubjectGroups).Select(x=>x.Group);
                        
                var teacherGroupsVm = Mapper.Map<IEnumerable<GroupVm>>(teachersGroups);
                return teacherGroupsVm;

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }          }
    }
}
