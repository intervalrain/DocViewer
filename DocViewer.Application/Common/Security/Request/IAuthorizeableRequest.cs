using MediatR;

namespace DocViewer.Application.Common.Security.Request;

public interface IAuthorizeableRequest<T> : IRequest<T>
{
    string UserId { get; }
}

