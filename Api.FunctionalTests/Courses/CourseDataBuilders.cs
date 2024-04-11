using BbgEducation.Api.Common.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.FunctionalTests.Courses;
internal static class CourseDataBuilders
{
    public static CreateCourseRequest BuildTestRequest() {
        return new CreateCourseRequest("test course 1", "test description", false);
    }
}
