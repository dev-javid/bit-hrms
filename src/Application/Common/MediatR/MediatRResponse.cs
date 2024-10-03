namespace Application.Common.MediatR
{
    public class MediatRResponse
    {
        protected MediatRResponse()
        {
        }

        public string? Error { get; protected set; }

        public static MediatRResponse Success()
        {
            return new MediatRResponse
            {
            };
        }

        public static MediatRResponse Failed(string error)
        {
            return new MediatRResponse
            {
                Error = error
            };
        }
    }

    public class MediatRResponse<T> : MediatRResponse
    {
        private MediatRResponse()
        {
        }

        public T? Value { get; private set; }

        public static MediatRResponse<T> Success(T value)
        {
            return new MediatRResponse<T>
            {
                Value = value
            };
        }

        public new static MediatRResponse<T> Failed(string error)
        {
            return new MediatRResponse<T>
            {
                Error = error
            };
        }
    }
}
