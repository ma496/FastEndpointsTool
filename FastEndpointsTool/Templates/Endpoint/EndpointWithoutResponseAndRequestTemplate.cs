using FastEndpointsTool.Extensions;
using FastEndpointsTool.Parsing.Endpoint;

namespace FastEndpointsTool.Templates.Endpoint;

public class EndpointWithoutResponseAndRequestTemplate : TemplateBase<EndpointArgument>
{
    public override string Template(EndpointArgument arg)
    {
        var template = $@"
sealed class {arg.Name}Endpoint : EndpointWithoutRequest
{{
    public override void Configure()
    {{
        {(!string.IsNullOrWhiteSpace(arg.Group) ? $"Group<{arg.Group}>();" : string.Empty)}
        {arg.Method.ToPascalCase()}(""{arg.Url}"");
    }}

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {{
    }}
}}
";

        if (string.IsNullOrWhiteSpace(arg.Group))
            template = DeleteLine(template, 5);
        return template;
    }
}
