using AutoMapper;
using Microsoft.Extensions.Logging;
using SchoolRegister.DAL.EF;
using SchoolRegister.Model.DataModels;
using SchoolRegister.Services.Interfaces;
using SchoolRegister.ViewModels.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SchoolRegister.Services.ConcreteServices
{
    public class GroupService : BaseService, IGroupService
    {
        public GroupService(ApplicationDbContext dbContext, IMapper mapper, ILogger logger) : base(dbContext, mapper, logger)
        {
        }

        public GroupVm AddOrUpdateGroup(AddOrUpdateGroupVm addOrUpdateGroupVm)
        {
            try
            {
                if (addOrUpdateGroupVm == null)
                    throw new ArgumentNullException($"View model parameter is null");
                var groupEntity = Mapper.Map<Group>(addOrUpdateGroupVm);
                if (!addOrUpdateGroupVm.Id.HasValue || addOrUpdateGroupVm.Id == 0)
                    DbContext.Groups.Add(groupEntity);
                else
                    DbContext.Groups.Update(groupEntity);
                DbContext.SaveChanges();
                var groupVm = Mapper.Map<GroupVm>(groupEntity);
                return groupVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public StudentVm AttachStudentToGroup(AttachDetachStudentToGroupVm attachDetachStudentToGroupVm)
        {
            try
            {
                if (attachDetachStudentToGroupVm == null)
                    throw new ArgumentNullException($"View model parameter is null");

                var student = DbContext.Users.OfType<Student>().FirstOrDefault(x => x.Id == attachDetachStudentToGroupVm.StudentId);
                if (student == null)
                    throw new ArgumentNullException($"There is no Student with this ID");

                var group = DbContext.Groups.FirstOrDefault(x => x.Id == attachDetachStudentToGroupVm.GroupId);
                if (group == null)
                    throw new ArgumentNullException($"There is no Group with this ID");

                group.Students.Add(student);
                DbContext.SaveChanges();
                var sts = group.Students.ToList();
                var studentVm = Mapper.Map<Student, StudentVm>(student);
                return studentVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public GroupVm AttachSubjectToGroup(AttachDetachSubjectGroupVm attachSubjectGroupVm)
        {
            try
            {
                if (attachSubjectGroupVm == null)
                    throw new ArgumentNullException($"View model parameter is null");
                var group = DbContext.Groups.FirstOrDefault(x => x.Id == attachSubjectGroupVm.GroupId);
                if (group == null)
                    throw new ArgumentNullException($"There is no Group with this ID");

                var subject = DbContext.Subjects.FirstOrDefault(x => x.Id == attachSubjectGroupVm.SubjectId);
                if (subject == null)
                    throw new ArgumentNullException($"There is no subject with this ID");
                var subjectGroup = Mapper.Map<SubjectGroup>(attachSubjectGroupVm);
                DbContext.SubjectGroups.Add(subjectGroup);
                DbContext.SaveChanges();
                var groupVM = Mapper.Map<GroupVm>(group);
                return groupVM;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public SubjectVm AttachTeacherToSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
        {
            try
            {
                if (attachDetachSubjectToTeacherVm == null)
                    throw new ArgumentNullException($"View model parameter is null");

                var teacher = DbContext.Users.OfType<Teacher>().FirstOrDefault(x => x.Id == attachDetachSubjectToTeacherVm.TeacherId);
                if (teacher == null)
                    throw new ArgumentNullException($"There is no Group with this ID");

                var subject = DbContext.Subjects.FirstOrDefault(x => x.Id == attachDetachSubjectToTeacherVm.SubjectId);
                if (subject == null)
                    throw new ArgumentNullException($"There is no subject with this ID");

                subject.Teacher = teacher;
                DbContext.SaveChanges();

                var subjectVm = Mapper.Map<SubjectVm>(subject);
                return subjectVm;

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public StudentVm DetachStudentFromGroup(AttachDetachStudentToGroupVm detachStudentToGroupVm)
        {
            try
            {
                if (detachStudentToGroupVm == null)
                    throw new ArgumentNullException($"View model parameter is null");
                var group = DbContext.Groups.FirstOrDefault(x => x.Id == detachStudentToGroupVm.GroupId);
                if (group == null)
                    throw new ArgumentNullException($"There is no Group with this ID");

                var student = DbContext.Users.OfType<Student>().FirstOrDefault(x => x.Id == detachStudentToGroupVm.StudentId);
                if (student == null)
                    throw new ArgumentNullException($"There is no Student with this ID");

                student.GroupId = null;
                student.Group = null;
                DbContext.SaveChanges();
                var studentVm = Mapper.Map<StudentVm>(student);
                return studentVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public GroupVm DetachSubjectFromGroup(AttachDetachSubjectGroupVm detachSubjectVm)
        {
            try
            {
                if (detachSubjectVm == null)
                    throw new ArgumentNullException($"View model parameter is null");
                var group = DbContext.Groups.FirstOrDefault(x => x.Id == detachSubjectVm.GroupId);
                if (group == null)
                    throw new ArgumentNullException($"There is no Group with this ID");

                var subject = group.SubjectGroups.FirstOrDefault(x => x.SubjectId == detachSubjectVm.SubjectId);
                if (subject == null)
                    throw new ArgumentNullException($"There is no subject with this ID in this group");

                group.SubjectGroups.Remove(subject);
                DbContext.SaveChanges();
                var groupVm = Mapper.Map<GroupVm>(group);
                return groupVm;

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public SubjectVm DetachTeacherFromSubject(AttachDetachSubjectToTeacherVm attachDetachSubjectToTeacherVm)
        {
            try
            {
                if (attachDetachSubjectToTeacherVm == null)
                    throw new ArgumentNullException($"View model parameter is null");
                var subject = DbContext.Subjects.FirstOrDefault(x => x.Id == attachDetachSubjectToTeacherVm.SubjectId);
                if (subject == null)
                    throw new ArgumentNullException($"There is no subject with this ID");


                if (subject.TeacherId == attachDetachSubjectToTeacherVm.TeacherId)
                    subject.TeacherId = null;
                subject.Teacher = null;

                DbContext.SaveChanges();
                var subjectVm = Mapper.Map<SubjectVm>(subject);
                return subjectVm;

            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public GroupVm GetGroup(Expression<Func<Group, bool>> filterPredicate)
        {
            try
            {
                if (filterPredicate == null)
                    throw new ArgumentNullException($" FilterExpression is null");
                var groupEntity = DbContext.Groups.FirstOrDefault(filterPredicate);
                var groupVm = Mapper.Map<GroupVm>(groupEntity);
                return groupVm;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }

        public IEnumerable<GroupVm> GetGroups(Expression<Func<Group, bool>> filterPredicate = null)
        {
            try
            {
                var groupEntities = DbContext.Groups.AsQueryable();
                if (filterPredicate != null)
                    groupEntities = groupEntities.Where(filterPredicate);
                var groupsVms = Mapper.Map<IEnumerable<GroupVm>>(groupEntities);
                return groupsVms;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, ex.Message);
                throw;
            }
        }
    }
}