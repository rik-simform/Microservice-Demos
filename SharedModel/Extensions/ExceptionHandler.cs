using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModel.Extensions
{
    public class ExceptionHandler
    { 
            private readonly ILogger<ExceptionHandler> _logger;

            public ExceptionHandler(ILogger<ExceptionHandler> logger)
            {
                _logger = logger;
            }

            public IActionResult HandleException(Exception exception)
            {
                _logger.LogError(exception, "An unhandled exception occurred");

                var response = new ErrorDetails
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An error occurred while processing the request" + exception.Message
                }.ToString();

                return new ObjectResult(response)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }
        
    }
}
