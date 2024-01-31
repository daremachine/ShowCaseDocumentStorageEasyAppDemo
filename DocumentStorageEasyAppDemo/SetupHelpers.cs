using DocumentStorageEasyAppDemo.Core.DocumentTypes;
using DocumentStorageEasyAppDemo.Core.DocumentTypes.Types;
using DocumentStorageEasyAppDemo.Core.StorageProviders;
using DocumentStorageEasyAppDemo.DocumentStorageModule;

public static class SetupHelpers
{
    public static void RegisterServices(IServiceCollection services)
    {
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddSingleton<IStorageProvider, StorageProviderInMemory>();
        services.AddScoped<IDocumentStorageService, DocumentStorageService>();


        // REGISTER DOCUMENT TYPES
        var typeConverter = new DocumentTypeConverter();
        typeConverter.RegisterDocumentType(typeof(DocumentTypeXML));
        typeConverter.RegisterDocumentType(typeof(DocumentTypeText));
        typeConverter.RegisterDocumentType(typeof(DocumentTypeJson));

        services.AddSingleton<IDocumentTypeConverter, DocumentTypeConverter>(_ => typeConverter);
    }
}