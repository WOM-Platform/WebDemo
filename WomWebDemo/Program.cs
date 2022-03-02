var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddScoped<WomPlatform.Connector.Client>(provider =>
{
    string domain = Environment.GetEnvironmentVariable("WOM_DOMAIN") ?? "wom.domain";

    return new WomPlatform.Connector.Client(domain, provider.GetRequiredService<ILoggerFactory>());
});
builder.Services.AddScoped<WomPlatform.Connector.Instrument>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var confDemo = configuration.GetRequiredSection("DemoCredentials");
    var confSource = confDemo.GetRequiredSection("Source");
    string keyPath = confSource["KeyPath"];

    Console.WriteLine("Path {0} in directory {1} exists {2}", keyPath, Directory.GetParent(keyPath), Directory.Exists(Directory.GetParent(keyPath)));
    Console.WriteLine("Directory {0} contains {1}", Directory.GetParent(keyPath), string.Join(", ", Directory.GetFiles(Directory.GetParent(keyPath))));

    using var keyStream = new FileStream(, FileMode.Open);

    var client = provider.GetRequiredService<WomPlatform.Connector.Client>();
    return client.CreateInstrument(confSource["Id"], keyStream);
});
builder.Services.AddScoped<WomPlatform.Connector.PointOfSale>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    var confDemo = configuration.GetRequiredSection("DemoCredentials");
    var confPos = confDemo.GetRequiredSection("Pos");
    string keyPath = confSource["KeyPath"];

    Console.WriteLine("Path {0} in directory {1} exists {2}", keyPath, Directory.GetParent(keyPath), Directory.Exists(Directory.GetParent(keyPath)));
    Console.WriteLine("Directory {0} contains {1}", Directory.GetParent(keyPath), string.Join(", ", Directory.GetFiles(Directory.GetParent(keyPath))));

    using var keyStream = new FileStream(keyPath, FileMode.Open);

    var client = provider.GetRequiredService<WomPlatform.Connector.Client>();
    return client.CreatePos(confPos["Id"], keyStream);
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
