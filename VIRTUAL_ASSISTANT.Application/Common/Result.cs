using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VIRTUAL_ASSISTANT.Application.Common
{
    public class Result<T, TError>
    {
        private const string MESSAGE_OK = "Operation Successful.";
        private const string MESSAGE_ACCEPTED = "Request Accepted.";
        private const string MESSAGE_BAD_REQUEST = "Bad Request.";
        private const string MESSAGE_NOT_UNAUTHORIZED = "Unauthorized Access.";
        private const string MESSAGE_NOT_FOUND = "Not Found";
        private const string MESSAGE_INTERNAL_SERVER_ERROR = "Internal Server Error.";
        private const string MESSAGE_TIME_OUT = "Gateway Timeout.";

        public int StatusCode { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? MessageCode { get; set; }
        public T? Data { get; set; }
        public TError? Errors { get; set; }

        // Sucesso (200 OK)
        public static Result<T, TError> SuccessResult(T data, string message = MESSAGE_OK, string messageCode = "")
        {
            return new Result<T, TError>
            {
                StatusCode = 200,
                Success = true,
                Message = message,
                MessageCode = messageCode,
                Data = data,
                Errors = default
            };
        }

        // Aceito (202 Accepted)
        public static Result<T, TError> AcceptedResult(T data, string message = MESSAGE_ACCEPTED, string messageCode = "")
        {
            return new Result<T, TError>
            {
                StatusCode = 202,
                Success = true,
                Message = message,
                MessageCode = messageCode,
                Data = data,
                Errors = default
            };
        }

        // Erro de Validação (400 Bad Request)
        public static Result<T, TError> BadRequestResult(string message = MESSAGE_BAD_REQUEST, string messageCode = "", TError errors = default!)
        {
            return new Result<T, TError>
            {
                StatusCode = 400,
                Success = false,
                Message = message,
                MessageCode = messageCode,
                Data = default(T),
                Errors = errors
            };
        }

        // Não Autorizado (401 Unauthorized)
        public static Result<T, TError> UnauthorizedResult(string message = MESSAGE_NOT_UNAUTHORIZED, string messageCode = "", TError errors = default!)
        {
            return new Result<T, TError>
            {
                StatusCode = 401,
                Success = false,
                Message = message,
                MessageCode = messageCode,
                Data = default(T),
                Errors = errors
            };
        }

        // Proibido (403 Forbidden)
        public static Result<T, TError> ForbiddenResult(string message = "Access is forbidden", string messageCode = "", TError errors = default!)
        {
            return new Result<T, TError>
            {
                StatusCode = 403,
                Success = false,
                Message = message,
                MessageCode = messageCode,
                Data = default(T),
                Errors = errors
            };
        }

        // Não encontrado (404 Not Found)
        public static Result<T, TError> NotFoundResult(string message = MESSAGE_NOT_FOUND, string messageCode = "", TError errors = default!)
        {
            return new Result<T, TError>
            {
                StatusCode = 404,
                Success = false,
                Message = message,
                MessageCode = messageCode,
                Data = default(T),
                Errors = errors
            };
        }

        // Erro Interno do Servidor (500 Internal Server Error)
        public static Result<T, TError> InternalServerErrorResult(string message = MESSAGE_INTERNAL_SERVER_ERROR, string messageCode = "", TError errors = default!)
        {
            return new Result<T, TError>
            {
                StatusCode = 500,
                Success = false,
                Message = message,
                MessageCode = messageCode,
                Data = default(T),
                Errors = errors
            };
        }

        // Gateway Timeout (504 Gateway Timeout)
        public static Result<T, TError> GatewayTimeoutResult(string message = MESSAGE_TIME_OUT, string messageCode = "", TError errors = default!)
        {
            return new Result<T, TError>
            {
                StatusCode = 504,
                Success = false,
                Message = message,
                MessageCode = messageCode,
                Data = default(T),
                Errors = errors
            };
        }
    }
}
