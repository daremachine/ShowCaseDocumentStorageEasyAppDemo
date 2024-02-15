using DocumentStorageEasyAppDemo.Core;
using DocumentStorageEasyAppDemo.DocumentStorageModule;
using DocumentStorageEasyAppDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace DocumentStorageEasyAppDemo.Routes
{
    public static class Routes
    {
        public static RouteGroupBuilder DocumentStorageApi(this RouteGroupBuilder group)
        {
            group.MapPost("/", async ([FromBody] DocumentModel model,
                HttpResponse httpResponse,
                IDocumentStorageService service)
                =>
            {
                try
                {
                    httpResponse.StatusCode = StatusCodes.Status201Created;
                    await service.DocumentAdd(new Document(model.Id, model.Tags, model.Data?.ToString()));
                }
                catch (DocumentAlreadyExistWithIdException)
                {
                    httpResponse.StatusCode = StatusCodes.Status409Conflict;
                }
            });
            group.MapPut("/", async ([FromBody] DocumentModel model,
                HttpResponse httpResponse,
                IDocumentStorageService service)
                =>
            {
                try
                {
                    httpResponse.StatusCode = StatusCodes.Status204NoContent;
                    await service.DocumentUpdate(model.Id, model.Tags, model.Data?.ToString());
                }
                catch (DocumentNotFoundException)
                {
                    httpResponse.StatusCode = StatusCodes.Status404NotFound;
                }
            });
            group.MapGet("/{id}", async (string id,
                [FromHeader(Name = "ContentType")] string? contentType,
                HttpResponse httpResponse,
                IDocumentStorageService service)
                =>
            {
                if (string.IsNullOrEmpty(contentType)) contentType = "application/json";
                httpResponse.ContentType = contentType;
                try
                {
                    var stream = await service.Document(id, contentType);

                    if (contentType == "plain/text") id += ".txt";

                    return Results.File(stream, contentType, id);
                }
                catch (DocumentNotFoundException)
                {
                    httpResponse.StatusCode = StatusCodes.Status404NotFound;
                    return Results.File(Array.Empty<byte>(), contentType, "");
                }
            });

            return group;
        }
    }
}
