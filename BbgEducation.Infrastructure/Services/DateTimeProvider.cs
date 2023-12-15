using BbgEducation.Application.Common.Interfaces.Services;

namespace BbgEducation.Infrastructure.Services;
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
