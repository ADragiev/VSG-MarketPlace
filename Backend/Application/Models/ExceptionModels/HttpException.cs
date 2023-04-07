using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ExceptionModels
{
    public class HttpException : Exception
    {
        public HttpException(string Message, HttpStatusCode httpStatusCode) : base(Message)
        {
            HttpStatusCode = httpStatusCode;
        }

        public HttpStatusCode HttpStatusCode { get; }
    }
}
