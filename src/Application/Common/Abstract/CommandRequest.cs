namespace Application.Common.Abstract;

public interface IUpdateCommand : IRequest<bool>
{
}

public interface IUpdateCommandHandler<TRequest> : IRequestHandler<TRequest, bool> where TRequest : IUpdateCommand
{
}

public interface IDeleteCommand : IRequest<bool>
{
}

public interface IDeleteCommandHandler<TRequest> : IRequestHandler<TRequest, bool> where TRequest : IDeleteCommand
{
}

public interface IAddCommand<out TResponse> : IRequest<TResponse>
{
}

public interface IAddCommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IAddCommand<TResponse>
{
}
