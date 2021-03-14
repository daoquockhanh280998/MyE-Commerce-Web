using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerWebApplication.Middleware
{
    /// <summary>
    /// Class ApiBadRequestResponse.
    /// Implements the <see cref="CS.Middleware.ApiResponse" />
    /// </summary>
    /// <seealso cref="CS.Middleware.ApiResponse" />
    /// <seealso cref="ApiResponse" />
    public class ApiBadRequestResponse : ApiResponse
    {
        /// <summary>
        /// Gets the errors.
        /// </summary>
        /// <value>The errors.</value>
        public IEnumerable<string> Errors { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiBadRequestResponse" /> class.
        /// </summary>
        /// <param name="modelState">State of the model.</param>
        /// <exception cref="ArgumentException">ModelState must be invalid - modelState</exception>
        public ApiBadRequestResponse(ModelStateDictionary modelState)
            : base(false, 400)
        {
            if (modelState.IsValid)
            {
                throw new ArgumentException("ModelState must be invalid", nameof(modelState));
            }

            Errors = modelState.SelectMany(x => x.Value.Errors)
                .Select(x => x.ErrorMessage).ToArray();
        }
    }
}
