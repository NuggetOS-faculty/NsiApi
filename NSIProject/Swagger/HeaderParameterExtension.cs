using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace NSIProject.Swagger;

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

public class HeaderParameterExtension : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null)
            operation.Parameters = new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "x-nsi-username",
            In = ParameterLocation.Header,
            Required = false // set to true if this is a required parameter
        });

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "x-nsi-password",
            In = ParameterLocation.Header,
            Required = false // set to true if this is a required parameter
        });
    }
}