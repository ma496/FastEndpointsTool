using FastEndpointsTool.Extensions;
using FastEndpointsTool.Parsing;

namespace FastEndpointsTool.Templates.Endpoint;

public class EndpointWithoutRequestTemplate : TemplateBase<EndpointArgument>
{
    public override string Template(EndpointArgument arg)
    {
        var template = $@"
sealed class {arg.Name}Endpoint : EndpointWithoutRequest<{arg.Name}Response>
{{
    public override void Configure()
    {{
        {arg.Method.ToPascalCase()}(""{Helpers.JoinUrl(arg.Url)}"");
        {(!string.IsNullOrWhiteSpace(arg.Group) ? $"Group<{arg.Group}>();" : RemoveLine(6))}
        {(!string.IsNullOrWhiteSpace(arg.Permission) ? $"Permissions(Allow.{arg.Permission});" : "AllowAnonymous();")}
    }}

    public override async Task HandleAsync(CancellationToken cancellationToken)
    {{
        await SendAsync(new {arg.Name}Response
        {{
            // Add your response properties here
        }}, cancellation: cancellationToken);
    }}
}}

sealed class {arg.Name}Response
{{
    // Define response properties here
}}
";

        template = DeleteLines(template);
        template = Merge(arg.UsingNamespaces, arg.Namespace, template);
        return template;
    }
}
