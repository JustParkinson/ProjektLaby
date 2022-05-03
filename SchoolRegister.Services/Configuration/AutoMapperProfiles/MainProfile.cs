using AutoMapper;
using SchoolRegister.Model.DataModels;
using SchoolRegister.ViewModels.VM;
using System.Linq;

namespace SchoolRegister.Services.Configuration.AutoMapperProfiles
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            //AutoMapper maps
            CreateMap<Subject, SubjectVm>() // map from Subject(src) to SubjectVm(dst)
                                            // custom mapping: FirstName and LastName concat string to TeacherName
                .ForMember(dest => dest.TeacherName, x => x.MapFrom(src => $"{src.Teacher.FirstName} {src.Teacher.LastName}"))
                // custom mapping: IList<Group> to IList<GroupVm>
                .ForMember(dest => dest.Groups, x => x.MapFrom(src => src.SubjectGroups.Select(y => y.Group)));

            CreateMap<AddOrUpdateSubjectVm, Subject>();
            
            CreateMap<SubjectVm, AddOrUpdateSubjectVm>();

            CreateMap<Group, GroupVm>()
            .ForMember(dest => dest.Students, x => x.MapFrom(src => src.Students))
            .ForMember(dest => dest.Subjects, x => x.MapFrom(src => src.SubjectGroups.Select(s => s.Subject)));

            CreateMap<AddOrUpdateGroupVm, Group>();

            CreateMap<Teacher, TeacherVm>()
                .ForMember(dest => dest.Subjects, x => x.MapFrom(src => src.Subjects));
            CreateMap<Student, StudentVm>()
                .ForMember(dest => dest.Grades, x=> x.MapFrom(x => x.Grades));            CreateMap<Grade, GradeVm>();

            CreateMap<Grade, GradeVm>();

            CreateMap<GetGradesReportVm, Grade>();
            CreateMap<AttachDetachSubjectGroupVm, SubjectGroup>();


            CreateMap<AddGradeToStudentVm, Grade>();

            CreateMap<Grade, AddGradeToStudentVm>();

            CreateMap<Parent,ParentVm>()
                .ForMember(dest => dest.Students, x=> x.MapFrom(src => src.Students));
        }    
    }
}