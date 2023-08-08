using System.Net;

namespace Common
{
    public class OperationResult<TData>
    {
        public string Title { get; set; } = null;
        public bool IsSuccess { get; set; }
        public TData Data { get; set; }
        public MetaData MetaData { get; set; }
        public OperationResultStatus Status { get; set; }

        public static OperationResult<TData> Success(TData data)
        {
            return new OperationResult<TData>()
            {
                IsSuccess = true,
                Status = OperationResultStatus.Success,
                Data = data,
                MetaData = new MetaData()
                {
                    Status = OperationResultStatus.Success,
                    Message = "عملیات با موفقیت انجام شد"
                }
            };
        }
        public static OperationResult<TData> NotFound()
        {
            return new OperationResult<TData>()
            {
                IsSuccess = true,
                Status = OperationResultStatus.NotFound,
                MetaData = new MetaData()
                {
                    Status = OperationResultStatus.NotFound,
                    Message = "اطلاعات یافت نشد"
                }
            };
        }
        public static OperationResult<TData> Error()
        {
            return new OperationResult<TData>()
            {
                IsSuccess = false,
                Data = default(TData),
                MetaData = new MetaData()
                {
                    Status = OperationResultStatus.Error,
                    Message = "عملیات ناموفق"
                }
            };
        }
    }
    public class OperationResult
    {
        public const string SuccessMessage = "عملیات با موفقیت انجام شد";
        public const string ErrorMessage = "عملیات با شکست مواجه شد";
        public const string NotFoundMessage = "اطلاعات یافت نشد";
        public string Message { get; set; }
        public string Title { get; set; } = null;
        public OperationResultStatus Status { get; set; }

        public static OperationResult Error()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Error,
                Message = ErrorMessage,
            };
        }
        public static OperationResult NotFound(string message)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.NotFound,
                Message = message,
            };
        }
        public static OperationResult NotFound()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.NotFound,
                Message = NotFoundMessage,
            };
        }
        public static OperationResult Error(string message)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Error,
                Message = message,
            };
        }
        public static OperationResult Error(string message, OperationResultStatus status)
        {
            return new OperationResult()
            {
                Status = status,
                Message = message,
            };
        }
        public static OperationResult Success()
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Success,
                Message = SuccessMessage,
            };
        }
        public static OperationResult Success(string message)
        {
            return new OperationResult()
            {
                Status = OperationResultStatus.Success,
                Message = message,
            };
        }
    }

    public class MetaData
    {
        public string Message { get; set; }
        public OperationResultStatus Status { get; set; }
    }

    public enum OperationResultStatus
    {
        Error = 10,
        Success = 200,
        NotFound = 404
    }
}