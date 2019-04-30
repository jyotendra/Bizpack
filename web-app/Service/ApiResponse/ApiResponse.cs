

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;

namespace Bizpack.Service.ApiResponse {
    public class ApiResponseBase {
        public int Status { get; }

        // [JsonProperty (NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; } = null;

        public object Result { get; set; } = null;

        public IEnumerable<string> Errors { get; set; } = null;

        public ApiResponseBase (int statusCode, string message = null) {
            Status = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode (statusCode);
        }

        private static string GetDefaultMessageForStatusCode (int statusCode) {
            switch (statusCode) {
                case 200:
                    return "Success";
                case 400:
                    return "Bad Request";
                case 404:
                    return "Resource not found";
                case 500:
                    return "An unhandled error occurred";
                default:
                    return null;
            }
        }
    }

    public class ApiBadRequestResponse : ApiResponseBase {

        public ApiBadRequestResponse (ModelStateDictionary modelState) : base (400) {
            if (modelState.IsValid) {
                throw new ArgumentException ("ModelState must be invalid", nameof (modelState));
            }

            Errors = modelState.SelectMany (x => x.Value.Errors)
                .Select (x => x.ErrorMessage).ToArray ();
        }

        public ApiBadRequestResponse (int statusCode, IEnumerable<string> errors) : base (statusCode) {
            Errors = errors ?? Errors ;
        }
    }

    public class ApiOkResponse : ApiResponseBase {

        public ApiOkResponse (object result = null, string message = null) : base (200) {
            Result = result;
            Message = message ?? Message;
        }
    }

}