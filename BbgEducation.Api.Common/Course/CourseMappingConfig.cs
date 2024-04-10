using BbgEducation.Api.Common.Course;
using BbgEducation.Application.Courses.Create;
using Mapster;

namespace BbgEducation.Api.Common.BbgProgram
{
    public class CourseMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config) {

            config.NewConfig<CreateCourseRequest, CourseCreateCommand>()
                .Map(dest => dest, src => src);
         
        }

    }
}
