using FastEndpointsTool.Parsing.Endpoint;
using FastEndpointsTool.Templates;
using System.Reflection;
using System.Text;

namespace FastEndpointsTool.Generator;

public class EndpointGenerator : CodeGeneratorBase<EndpointArgument>
{
    public override async Task Generate(EndpointArgument argument)
    {
        var directory = Directory.GetCurrentDirectory();
        var (setting, projectDir) = await Helpers.GetSetting(directory);
            
        if (argument.Type == EndpointType.CrudEndpoint)
        {
            argument.Output = Path.Combine(argument.Output ?? string.Empty, argument.PluralName);
            var endpointDir = GetEndpointDir(projectDir, setting.Project.EndpointPath, argument.Output);
            if (!Directory.Exists(endpointDir))
                Directory.CreateDirectory(endpointDir);

            await GenerateCrudGroup(argument, setting, endpointDir);
            argument.Group = $"{argument.PluralName}Group";

            var entityNamespace = GetClassNamespace(projectDir, setting.Project.Name, argument.Entity);
            var groupNamespace = string.Empty;
            var dataContextNamespace = GetClassNamespace(projectDir, setting.Project.Name, argument.DataContext);   

            // Create endpoint
            var createEndpointArgument = (EndpointArgument) argument.Clone();
            createEndpointArgument.Type = EndpointType.CreateEndpoint;
            createEndpointArgument.Method = "post";
            await GenerateEndpoint(createEndpointArgument, setting, endpointDir, entityNamespace, groupNamespace, dataContextNamespace);

            // Create update endpoint
            var updateEndpointArgument = (EndpointArgument) argument.Clone();
            updateEndpointArgument.Type = EndpointType.UpdateEndpoint;
            updateEndpointArgument.Method = "put";
            await GenerateEndpoint(updateEndpointArgument, setting, endpointDir, entityNamespace, groupNamespace, dataContextNamespace);

            // Create list endpoint
            var listEndpointArgument = (EndpointArgument) argument.Clone();
            listEndpointArgument.Type = EndpointType.ListEndpoint;
            listEndpointArgument.Method = "get";
            listEndpointArgument.Url = "list";
            await GenerateEndpoint(listEndpointArgument, setting, endpointDir, entityNamespace, groupNamespace, dataContextNamespace);

            // Create get endpoint
            var getEndpointArgument = (EndpointArgument) argument.Clone();
            getEndpointArgument.Type = EndpointType.GetEndpoint;
            getEndpointArgument.Method = "get";
            await GenerateEndpoint(getEndpointArgument, setting, endpointDir, entityNamespace, groupNamespace, dataContextNamespace);

            // Create delete endpoint
            var deleteEndpointArgument = (EndpointArgument) argument.Clone();
            deleteEndpointArgument.Type = EndpointType.DeleteEndpoint;
            deleteEndpointArgument.Method = "delete";
            await GenerateEndpoint(deleteEndpointArgument, setting, endpointDir, entityNamespace, groupNamespace, dataContextNamespace);
        }
        else
        {   
            var endpointDir = GetEndpointDir(projectDir, setting.Project.EndpointPath, argument.Output);
            if (!Directory.Exists(endpointDir))
                Directory.CreateDirectory(endpointDir);

            var entityNamespace = GetClassNamespace(projectDir, setting.Project.Name, argument.Entity);
            var groupNamespace = GetClassNamespace(projectDir, setting.Project.Name, argument.Group);
            var dataContextNamespace = GetClassNamespace(projectDir, setting.Project.Name, argument.DataContext);
                
            await GenerateEndpoint(argument, setting, endpointDir, entityNamespace, groupNamespace, dataContextNamespace);
        }
    }

    #region Helpers

    private async Task GenerateEndpoint(EndpointArgument argument, FeToolSetting setting, string endpointDir, string entityNamespace,
        string groupNamespace, string dataContextNamespace)
    {
        var fileName = $"{Helpers.EndpointName(argument.Name, argument.Type)}Endpoint.cs";
        var filePath = Path.Combine(endpointDir, fileName);

        var template = GenerateEndpointCode(argument, setting, entityNamespace, groupNamespace, dataContextNamespace);

        await File.WriteAllTextAsync(filePath, template);

        Console.WriteLine($"{fileName} file created under {endpointDir}");
    }

    private async Task GenerateCrudGroup(EndpointArgument argument, FeToolSetting setting, string endpointDir)
    {
        var fileName = $"{argument.PluralName}Group.cs";
        var filePath = Path.Combine(endpointDir, fileName);

        var template = GenerateCrudGroupCode(argument, setting);

        await File.WriteAllTextAsync(filePath, template);

        Console.WriteLine($"{fileName} file created under {endpointDir}");
    }

    private string GenerateEndpointCode(EndpointArgument argument, FeToolSetting setting, string entityNamespace,
        string groupNamespace, string dataContextNamespace)
    {
        var templateBuilder = new StringBuilder();

        argument.EntityFullName = $"{entityNamespace}{(!string.IsNullOrWhiteSpace(entityNamespace) ? "." : string.Empty)}{argument.Entity}";
        argument.GroupFullName = $"{groupNamespace}{(!string.IsNullOrWhiteSpace(groupNamespace) ? "." : string.Empty)}{argument.Group}";
        argument.DataContextFullName = $"{dataContextNamespace}{(!string.IsNullOrWhiteSpace(dataContextNamespace) ? "." : string.Empty)}{argument.DataContext}";

        var namespaces = new List<string?>
        {
            "FastEndpoints",
            "FluentValidation",
            entityNamespace,
            groupNamespace,
            dataContextNamespace
        };
        var endpointNamespace = GetEndpointNamespace(setting.Project.RootNamespace, setting.Project.EndpointPath, argument.Output);
        var uniqueNamespaces = namespaces
            .Where(x => !string.IsNullOrWhiteSpace(x) && x != endpointNamespace)
            .Select(x => x!)
            .Distinct()
            .ToList();

        argument.UsingNamespaces = uniqueNamespaces;
        argument.Namespace = endpointNamespace;

        var templateType = GetTypesImplementingInterface(typeof(ITemplate<>))
            .Where(t => t.Name == $"{argument.Type}Template")
            .SingleOrDefault();
        if (templateType == null)
            throw new Exception($"{argument.Type}Template class not found.");
        var templateObj = Activator.CreateInstance(templateType);
        var templateMethod = templateType.GetMethod("Template");
        if (templateMethod == null)
            throw new Exception($"{templateType.FullName} not implement Template method.");
        var templateResult = templateMethod.Invoke(templateObj, [argument]);
        var templateText = templateResult as string;
        if (string.IsNullOrWhiteSpace(templateText))
            throw new Exception($"{templateType.FullName}.{templateMethod.Name} return empty template.");
        templateBuilder.AppendLine(templateText);

        return templateBuilder.ToString();
    }

    private string GenerateCrudGroupCode(EndpointArgument argument, FeToolSetting setting)
    {
        var templateBuilder = new StringBuilder();

        var endpointNamespace = GetEndpointNamespace(setting.Project.RootNamespace, setting.Project.EndpointPath, argument.Output);

        argument.UsingNamespaces = new List<string> { "FastEndpoints" };
        argument.Namespace = endpointNamespace;

        var templateType = GetTypesImplementingInterface(typeof(ITemplate<>))
            .Where(t => t.Name == "CrudGroupTemplate")
            .SingleOrDefault();
        if (templateType == null)
            throw new Exception("CrudGroupTemplate class not found.");
        var templateObj = Activator.CreateInstance(templateType);
        var templateMethod = templateType.GetMethod("Template");
        if (templateMethod == null)
            throw new Exception($"{templateType.FullName} not implement Template method.");
        var templateResult = templateMethod.Invoke(templateObj, [argument]);
        var templateText = templateResult as string;
        if (string.IsNullOrWhiteSpace(templateText))
            throw new Exception($"{templateType.FullName}.{templateMethod.Name} return empty template.");
        templateBuilder.AppendLine(templateText);

        return templateBuilder.ToString();
    }


    private IEnumerable<Type> GetTypesImplementingInterface(Type genericInterfaceType)
    {
        return Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetInterfaces()
                         .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericInterfaceType));
    }

    #endregion
}