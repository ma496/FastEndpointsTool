﻿namespace Backend.Features.SayHello;

sealed class Endpoint : Endpoint<Request, Response>
{
    public override void Configure()
    {
        Post("/api/hello");
        // AllowAnonymous();
        Roles("Admin");
    }

    public override async Task HandleAsync(Request r, CancellationToken c)
    {
        await SendAsync(new()
        {
            Message = $"Hello {r.FirstName} {r.LastName}..."
        });
    }
}