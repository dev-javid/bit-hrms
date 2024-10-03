namespace Application.Common.MediatR;

public interface IUpdateCommand : IRequest<bool>
{
}

public interface IUpdateCommandHandler<TRequest> : IRequestHandler<TRequest, bool> where TRequest : IUpdateCommand
{
}

public interface IAddCommand<out TResponse> : IRequest<TResponse>
{
}

public interface IAddCommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse> where TRequest : IAddCommand<TResponse>
{
}

public interface IMediatRCommand : IRequest<MediatRResponse>
{
}

public interface IMediatRCommandHandler<TRequest> : IRequestHandler<TRequest, MediatRResponse> where TRequest : IMediatRCommand
{
}

public interface IMediatRCommand<TResponse> : IRequest<MediatRResponse<TResponse>> where TResponse : struct
{
}

public interface IMediatRCommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, MediatRResponse<TResponse>> where TRequest : IMediatRCommand<TResponse> where TResponse : struct
{
}
