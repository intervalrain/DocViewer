using DocViewer.Application.Common.Interfaces;

namespace DocViewer.Infrastructure.Common.Services;

public class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.Now;
}

