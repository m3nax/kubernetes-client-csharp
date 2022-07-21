using KubernetesClient.Extensions.Hosting.Alpha;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddKubernetesClient((builder) =>
{
    builder.FromConfigFile();
    builder.UseContext("contextName");
    builder.WithMasterUrl("192.168.0.10");
});

// Load kubernetes configuration
//var kubernetesClientConfig = KubernetesClientConfiguration.BuildDefaultConfig();

// Register Kubernetes client interface as sigleton
//builder.Services.AddSingleton<IKubernetes>(new Kubernetes(kubernetesClientConfig));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

// Start the service
app.Run();


// Swagger ui can be accesse at: http://localhost:<yourBindingPort>/swagger
