using FastEndpointsTool.Extensions;
using FastEndpointsTool.Parsing;

namespace FastEndpointsTool.Templates.Endpoint.Crud;

public class DeleteEndpointTemplate : TemplateBase<EndpointArgument>
{
    public override string Template(EndpointArgument arg)
    {
        var (setting, projectDir) = Helpers.GetSetting(Directory.GetCurrentDirectory()).Result;
        var assembly = Helpers.GetProjectAssembly(projectDir, setting.Project.Name);
        var constructorParams = new string[] { !string.IsNullOrWhiteSpace(arg.DataContext) ? $"{arg.DataContext} context" : string.Empty };

        var template = $@"
sealed class {arg.Name}Endpoint : Endpoint<{arg.Name}Request, {arg.Name}Response>
{{
    {(!string.IsNullOrWhiteSpace(arg.DataContext) ? $"private readonly {arg.DataContext} _dbContext;" : RemoveLine(3, 4))}

    public {arg.Name}Endpoint({string.Join(", ", constructorParams)})
    {{
        {(!string.IsNullOrWhiteSpace(arg.DataContext) ? $"_dbContext = context;" : RemoveLine(7))}
    }}

    public override void Configure()
    {{
        Delete(""{Helpers.JoinUrl(arg.Url ?? string.Empty, $"{{{GetIdProperty(assembly, arg.Entity, arg.EntityFullName).Name.ToLower()}}}")}"");
        {(!string.IsNullOrWhiteSpace(arg.Group) ? $"Group<{arg.Group}>();" : RemoveLine(13))}
        {(!string.IsNullOrWhiteSpace(arg.Permission) ? $"Permissions(Allow.{arg.Permission});" : "AllowAnonymous();")}
    }}

    public override async Task HandleAsync({arg.Name}Request request, CancellationToken cancellationToken)
    {{
        // get entity from db
        var entity = {(!string.IsNullOrWhiteSpace(arg.DataContext) ? $"await _dbContext.{arg.PluralName}.FindAsync([request.{GetIdProperty(assembly, arg.Entity, arg.EntityFullName).Name}], cancellationToken: cancellationToken);" : $"new {arg.Entity}()")}; 
        if (entity == null)
        {{
            await SendNotFoundAsync(cancellationToken);
            return;
        }}

        // Delete the entity from the db
        {(!string.IsNullOrWhiteSpace(arg.DataContext) ? $"_dbContext.{arg.PluralName}.Remove(entity);" : RemoveLine(28))}
        {(!string.IsNullOrWhiteSpace(arg.DataContext) ? $"await _dbContext.SaveChangesAsync(cancellationToken);" : RemoveLine(29))}
        await SendAsync(new {arg.Name}Response {{ Success = true }}, cancellation: cancellationToken);
    }}
}}

sealed class {arg.Name}Request
{{
    public {ConvertToAlias(GetIdProperty(assembly, arg.Entity, arg.EntityFullName).PropertyType.Name)} {GetIdProperty(assembly, arg.Entity, arg.EntityFullName).Name} {{ get; set; }}
}}

sealed class {arg.Name}Validator : Validator<{arg.Name}Request>
{{
    public {arg.Name}Validator()
    {{
        RuleFor(x => x.{GetIdProperty(assembly, arg.Entity, arg.EntityFullName).Name}).NotEmpty();
    }}
}}

sealed class {arg.Name}Response
{{
    public bool Success {{ get; set; }}
    public string Message {{ get; set; }} = null!;
}}
";

        template = DeleteLines(template);
        template = Merge(arg.UsingNamespaces, arg.Namespace, template);
        return template;
    }
}