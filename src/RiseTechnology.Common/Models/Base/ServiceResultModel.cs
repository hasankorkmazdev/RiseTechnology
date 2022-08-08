using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RiseTechnology.Common.Models.Base
{
    public class ServiceResultModel
    {
        public ServiceResultModel(object data, HttpStatusCode code = HttpStatusCode.OK, string message="Succesfull")
        {
            this.Code = code;
            this.Data = data;
            this.Message = message;
        }
        public ServiceResultModel(string message = "Succesfull", HttpStatusCode code = HttpStatusCode.OK)
        {
            this.Code = code;
            this.Message = message;
        }
        public HttpStatusCode Code {get;set;}
        public object Data { get; set; }
        public string Message { get; set; }
    }
}
