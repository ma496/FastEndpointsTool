using FastEndpointsTool.Parsing;

namespace FastEndpointsTool;


public class ArgumentInfo
{
    public ArgumentType Type { get; init; }
    public string Name { get; init; } = null!;
    public string ShortName { get; init; } = null!;
    public string Description { get; init; } = null!;
    public IList<ArgumentOption> Options { get; init; } = null!;

    public static IList<ArgumentInfo> Arguments()
    {
        return new List<ArgumentInfo>
        {
            new ArgumentInfo
            {
                Type = ArgumentType.CreateProject,
                Name = "createproject",
                ShortName = "cp",
                Description = "Create a new project.",
                Options = new List<ArgumentOption>
                {
                    new ArgumentOption
                    {
                        Name = "--name",
                        ShortName = "-n",
                        Description = "Name of project.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--output",
                        ShortName = "-o",
                        Description = "Path of project.",
                        Required = false,
                    },
                }
            },
            new ArgumentInfo
            {
                Type = ArgumentType.Init,
                Name = "initialize",
                ShortName = "init",
                Description = "create fetool.json file.",
                Options = new List<ArgumentOption>
                {
                    new ArgumentOption
                    {
                        Name = "--directory",
                        ShortName = "-d",
                        Description = "Directory of project.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--projectName",
                        ShortName = "-pn",
                        Description = "Name of project.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--rootNamespace",
                        ShortName = "-rn",
                        Description = "Root namespace of project.",
                        Required = false,
                    },
                }
            },
            new ArgumentInfo
            {
                Type = ArgumentType.Endpoint,
                Name = "endpoint",
                ShortName = "ep",
                Description = "Creaete endpoint with request, response, validator and mapper.",
                Options = new List<ArgumentOption>
                {
                    new ArgumentOption
                    {
                        Name = "--name",
                        ShortName = "-n",
                        Description = "Name of endpoint class.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--method",
                        ShortName = "-m",
                        Description = "Http method.",
                        Required= true,
                    },
                    new ArgumentOption
                    {
                        Name = "--url",
                        ShortName = "-u",
                        Description = "Url of endpoint.",
                        Required= true,
                    },
                    new ArgumentOption
                    {
                        Name = "--entity",
                        ShortName = "-e",
                        Description = "Name of entity class.",
                        Required= true,
                    },
                    new ArgumentOption
                    {
                        Name = "--output",
                        ShortName = "-o",
                        Description = "Path of endpoint.",
                        Required = false,
                    },
                    new ArgumentOption
                    {
                        Name = "--group",
                        ShortName = "-g",
                        Description = "Endpoint group class name.",
                        Required = false,
                        NormalizeMethod = Helpers.GroupName,
                    },
                    new ArgumentOption
                    {
                        Name = "--authorization",
                        ShortName = "-auth",
                        Description = "Permission based authorization.",
                        Required = false,
                        Default = "false",
                    },
                    new ArgumentOption
                    {
                        Name = "--permission",
                        ShortName = "-per",
                        Description = "Permission name.",
                        Required = false,
                    },
                }
            },
            new ArgumentInfo
            {
                Type = ArgumentType.EndpointWithoutMapper,
                Name = "endpointwithoutmapper",
                ShortName = "epwm",
                Description = "Creaete endpoint with request, response and validator.",
                Options = new List<ArgumentOption>
                {
                    new ArgumentOption
                    {
                        Name = "--name",
                        ShortName = "-n",
                        Description = "Name of endpoint class.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--method",
                        ShortName = "-m",
                        Description = "Http method.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--url",
                        ShortName = "-u",
                        Description = "Url of endpoint.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--output",
                        ShortName = "-o",
                        Description = "Path of endpoint.",
                        Required = false,
                    },
                    new ArgumentOption
                    {
                        Name = "--group",
                        ShortName = "-g",
                        Description = "Endpoint group class name.",
                        Required = false,
                        NormalizeMethod = Helpers.GroupName,
                    },
                    new ArgumentOption
                    {
                        Name = "--authorization",
                        ShortName = "-auth",
                        Description = "Permission based authorization.",
                        Required = false,
                        Default = "false",
                    },
                    new ArgumentOption
                    {
                        Name = "--permission",
                        ShortName = "-per",
                        Description = "Permission name.",
                        Required = false,
                    }
                }
            },
            new ArgumentInfo
            {
                Type = ArgumentType.EndpointWithoutResponse,
                Name = "endpointwithoutresponse",
                ShortName = "epwr",
                Description = "Creaete endpoint with request and validator.",
                Options = new List<ArgumentOption>
                {
                    new ArgumentOption
                    {
                        Name = "--name",
                        ShortName = "-n",
                        Description = "Name of endpoint class.",
                        Required= true,
                    },
                    new ArgumentOption
                    {
                        Name = "--method",
                        ShortName = "-m",
                        Description = "Http method.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--url",
                        ShortName = "-u",
                        Description = "Url of endpoint.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--output",
                        ShortName = "-o",
                        Description = "Path of endpoint.",
                        Required = false,
                    },
                    new ArgumentOption
                    {
                        Name = "--group",
                        ShortName = "-g",
                        Description = "Endpoint group class name.",
                        Required = false,
                        NormalizeMethod = Helpers.GroupName,
                    },
                    new ArgumentOption
                    {
                        Name = "--authorization",
                        ShortName = "-auth",
                        Description = "Permission based authorization.",
                        Required = false,
                        Default = "false",
                    },
                    new ArgumentOption
                    {
                        Name = "--permission",
                        ShortName = "-per",
                        Description = "Permission name.",
                        Required = false,
                    }
                }
            },
            new ArgumentInfo
            {
                Type = ArgumentType.EndpointWithoutRequest,
                Name = "endpointwithoutrequest",
                ShortName = "epwreq",
                Description = "Creaete endpoint with response.",
                Options = new List<ArgumentOption>
                {
                    new ArgumentOption
                    {
                        Name = "--name",
                        ShortName = "-n",
                        Description = "Name of endpoint class.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--method",
                        ShortName = "-m",
                        Description = "Http method.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--url",
                        ShortName = "-u",
                        Description = "Url of endpoint.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--output",
                        ShortName = "-o",
                        Description = "Path of endpoint.",
                        Required = false,
                    },
                    new ArgumentOption
                    {
                        Name = "--group",
                        ShortName = "-g",
                        Description = "Endpoint group class name.",
                        Required = false,
                        NormalizeMethod = Helpers.GroupName,
                    },
                    new ArgumentOption
                    {
                        Name = "--authorization",
                        ShortName = "-auth",
                        Description = "Permission based authorization.",
                        Required = false,
                        Default = "false",
                    },
                    new ArgumentOption
                    {
                        Name = "--permission",
                        ShortName = "-per",
                        Description = "Permission name.",
                        Required = false,
                    }
                }
            },
            new ArgumentInfo
            {
                Type = ArgumentType.EndpointWithoutResponseAndRequest,
                Name = "endpointwithoutresponseandrequest",
                ShortName = "epwrreq",
                Description = "Creaete endpoint.",
                Options = new List<ArgumentOption>
                {
                    new ArgumentOption
                    {
                        Name = "--name",
                        ShortName = "-n",
                        Description = "Name of endpoint class.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--method",
                        ShortName = "-m",
                        Description = "Http method.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--url",
                        ShortName = "-u",
                        Description = "Url of endpoint.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--output",
                        ShortName = "-o",
                        Description = "Path of endpoint.",
                        Required = false,
                    },
                    new ArgumentOption
                    {
                        Name = "--group",
                        ShortName = "-g",
                        Description = "Endpoint group class name.",
                        Required = false,
                        NormalizeMethod = Helpers.GroupName,
                    },
                    new ArgumentOption
                    {
                        Name = "--authorization",
                        ShortName = "-auth",
                        Description = "Permission based authorization.",
                        Required = false,
                        Default = "false",
                    },
                    new ArgumentOption
                    {
                        Name = "--permission",
                        ShortName = "-per",
                        Description = "Permission name.",
                        Required = false,
                    }
                },
            },
            new ArgumentInfo
            {
                Type = ArgumentType.CreateEndpoint,
                Name = "createendpoint",
                ShortName = "cep",
                Description = "Creaete endpoint with request, response, validator and mapper.",
                Options = new List<ArgumentOption>
                {
                    new ArgumentOption
                    {
                        Name = "--name",
                        ShortName = "-n",
                        Description = "Name of endpoint class.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--pluralName",
                        ShortName = "-pn",
                        Description = "Plural name of the entity for CRUD operations.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--method",
                        ShortName = "-m",
                        Description = "Http method.",
                        Required = true,
                        Default = "post",
                        IsInternal = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--url",
                        ShortName = "-u",
                        Description = "Url of endpoint.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--entity",
                        ShortName = "-e",
                        Description = "Name of entity class.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--output",
                        ShortName = "-o",
                        Description = "Path of endpoint.",
                        Required = false,
                    },
                    new ArgumentOption
                    {
                        Name = "--group",
                        ShortName = "-g",
                        Description = "Endpoint group class name.",
                        Required = false,
                        NormalizeMethod = Helpers.GroupName,
                    },
                    new ArgumentOption
                    {
                        Name = "--dataContext",
                        ShortName = "-dc",
                        Description = "DataContext class name.",
                        Required = false
                    },
                    new ArgumentOption
                    {
                        Name = "--authorization",
                        ShortName = "-auth",
                        Description = "Permission based authorization.",
                        Required = false,
                        Default = "false",
                    },
                    new ArgumentOption
                    {
                        Name = "--permission",
                        ShortName = "-per",
                        Description = "Permission name.",
                        Required = false,
                    }
                }
            },
            new ArgumentInfo
            {
                Type = ArgumentType.UpdateEndpoint,
                Name = "updateendpoint",
                ShortName = "uep",
                Description = "Update endpoint with request, response, validator and mapper.",
                Options = new List<ArgumentOption>
                {
                    new ArgumentOption
                    {
                        Name = "--name",
                        ShortName = "-n",
                        Description = "Name of endpoint class.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--pluralName",
                        ShortName = "-pn",
                        Description = "Plural name of the entity for CRUD operations.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--method",
                        ShortName = "-m",
                        Description = "Http method.",
                        Required = true,
                        Default = "put",
                        IsInternal = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--url",
                        ShortName = "-u",
                        Description = "Url of endpoint.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--entity",
                        ShortName = "-e",
                        Description = "Name of entity class.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--output",
                        ShortName = "-o",
                        Description = "Path of endpoint.",
                        Required = false,
                    },
                    new ArgumentOption
                    {
                        Name = "--group",
                        ShortName = "-g",
                        Description = "Endpoint group class name.",
                        Required = false,
                        NormalizeMethod = Helpers.GroupName,
                    },
                    new ArgumentOption
                    {
                        Name = "--dataContext",
                        ShortName = "-dc",
                        Description = "DataContext class name.",
                        Required = false
                    },
                    new ArgumentOption
                    {
                        Name = "--authorization",
                        ShortName = "-auth",
                        Description = "Permission based authorization.",
                        Required = false,
                        Default = "false",
                    },
                    new ArgumentOption
                    {
                        Name = "--permission",
                        ShortName = "-per",
                        Description = "Permission name.",
                        Required = false,
                    }
                }
            },
            new ArgumentInfo
            {
                Type = ArgumentType.GetEndpoint,
                Name = "getendpoint",
                ShortName = "gep",
                Description = "Get endpoint with request, response, validator and mapper.",
                Options = new List<ArgumentOption>
                {
                    new ArgumentOption
                    {
                        Name = "--name",
                        ShortName = "-n",
                        Description = "Name of endpoint class.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--pluralName",
                        ShortName = "-pn",
                        Description = "Plural name of the entity for CRUD operations.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--method",
                        ShortName = "-m",
                        Description = "Http method.",
                        Required = true,
                        Default = "get",
                        IsInternal = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--url",
                        ShortName = "-u",
                        Description = "Url of endpoint.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--entity",
                        ShortName = "-e",
                        Description = "Name of entity class.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--output",
                        ShortName = "-o",
                        Description = "Path of endpoint.",
                        Required = false,
                    },
                    new ArgumentOption
                    {
                        Name = "--group",
                        ShortName = "-g",
                        Description = "Endpoint group class name.",
                        Required = false,
                        NormalizeMethod = Helpers.GroupName,
                    },
                    new ArgumentOption
                    {
                        Name = "--dataContext",
                        ShortName = "-dc",
                        Description = "DataContext class name.",
                        Required = false
                    },
                    new ArgumentOption
                    {
                        Name = "--baseProperties",
                        ShortName = "-bp",
                        Description = "Base properties.",
                        IsInternal = true,
                        Required = true,
                        Default = "true"
                    },
                    new ArgumentOption
                    {
                        Name = "--authorization",
                        ShortName = "-auth",
                        Description = "Permission based authorization.",
                        Required = false,
                        Default = "false",
                    },
                    new ArgumentOption
                    {
                        Name = "--permission",
                        ShortName = "-per",
                        Description = "Permission name.",
                        Required = false,
                    }
                }
            },
            new ArgumentInfo
            {
                Type = ArgumentType.ListEndpoint,
                Name = "listendpoint",
                ShortName = "lep",
                Description = "Create list endpoint with request, response, validator and mapper.",
                Options = new List<ArgumentOption>
                {
                    new ArgumentOption
                    {
                        Name = "--name",
                        ShortName = "-n",
                        Description = "Name of endpoint class.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--pluralName",
                        ShortName = "-pn",
                        Description = "Plural name of the entity for CRUD operations.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--method",
                        ShortName = "-m",
                        Description = "Http method.",
                        Required = true,
                        Default = "get",
                        IsInternal = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--url",
                        ShortName = "-u",
                        Description = "Url of endpoint.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--entity",
                        ShortName = "-e",
                        Description = "Name of entity class.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--output",
                        ShortName = "-o",
                        Description = "Path of endpoint.",
                        Required = false,
                    },
                    new ArgumentOption
                    {
                        Name = "--group",
                        ShortName = "-g",
                        Description = "Endpoint group class name.",
                        Required = false,
                        NormalizeMethod = Helpers.GroupName,
                    },
                    new ArgumentOption
                    {
                        Name = "--dataContext",
                        ShortName = "-dc",
                        Description = "DataContext class name.",
                        Required = false
                    },
                    new ArgumentOption
                    {
                        Name = "--baseProperties",
                        ShortName = "-bp",
                        Description = "Base properties.",
                        IsInternal = true,
                        Required = true,
                        Default = "true"
                    },
                    new ArgumentOption
                    {
                        Name = "--authorization",
                        ShortName = "-auth",
                        Description = "Permission based authorization.",
                        Required = false,
                        Default = "false",
                    },
                    new ArgumentOption
                    {
                        Name = "--permission",
                        ShortName = "-per",
                        Description = "Permission name.",
                        Required = false,
                    }
                }
            },
            new ArgumentInfo
            {
                Type = ArgumentType.DeleteEndpoint,
                Name = "deleteendpoint",
                ShortName = "dep",
                Description = "Create delete endpoint with request, response, and validator.",
                Options = new List<ArgumentOption>
                {
                    new ArgumentOption
                    {
                        Name = "--name",
                        ShortName = "-n",
                        Description = "Name of endpoint class.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--pluralName",
                        ShortName = "-pn",
                        Description = "Plural name of the entity for CRUD operations.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--method",
                        ShortName = "-m",
                        Description = "Http method.",
                        Required = true,
                        Default = "delete",
                        IsInternal = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--url",
                        ShortName = "-u",
                        Description = "Url of endpoint.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--entity",
                        ShortName = "-e",
                        Description = "Name of entity class.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--output",
                        ShortName = "-o",
                        Description = "Path of endpoint.",
                        Required = false,
                    },
                    new ArgumentOption
                    {
                        Name = "--group",
                        ShortName = "-g",
                        Description = "Endpoint group class name.",
                        Required = false,
                        NormalizeMethod = Helpers.GroupName,
                    },
                    new ArgumentOption
                    {
                        Name = "--dataContext",
                        ShortName = "-dc",
                        Description = "DataContext class name.",
                        Required = false
                    },
                    new ArgumentOption
                    {
                        Name = "--authorization",
                        ShortName = "-auth",
                        Description = "Permission based authorization.",
                        Required = false,
                        Default = "false",
                    },
                    new ArgumentOption
                    {
                        Name = "--permission",
                        ShortName = "-per",
                        Description = "Permission name.",
                        Required = false,
                    }
                }
            },
            new ArgumentInfo
            {
                Type = ArgumentType.CrudEndpoint,
                Name = "crudendpoint",
                ShortName = "crud",
                Description = "Create CRUD endpoints (Create, Read, Update, Delete, List) for an entity.",
                Options = new List<ArgumentOption>
                {
                    new ArgumentOption
                    {
                        Name = "--name",
                        ShortName = "-n",
                        Description = "Name of the class for CRUD operations.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--pluralName",
                        ShortName = "-pn",
                        Description = "Plural name of the entity for CRUD operations.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--entity",
                        ShortName = "-e",
                        Description = "Name of entity class.",
                        Required = true,
                    },
                    new ArgumentOption
                    {
                        Name = "--output",
                        ShortName = "-o",
                        Description = "Path of endpoints.",
                        Required = false,
                    },
                    new ArgumentOption
                    {
                        Name = "--dataContext",
                        ShortName = "-dc",
                        Description = "DataContext class name.",
                        Required = false
                    },
                    new ArgumentOption
                    {
                        Name = "--authorization",
                        ShortName = "-auth",
                        Description = "Permission based authorization.",
                        Required = false,
                        Default = "false",
                    }
                }
            }
        };
    }
}

public class ArgumentOption
{
    public string Name { get; init; } = null!;
    public string ShortName { get; init; } = null!;
    public string Description { get; init; } = null!;
    public bool Required { get; init; }
    public string Default { get; set; } = null!;
    public bool IsInternal { get; set; }
    public Func<string, string>? NormalizeMethod { get; set; } = null!;
}
