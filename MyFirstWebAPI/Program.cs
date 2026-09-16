using MyFirstWebAPI.Repository;
using MyFirstWebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Bind to 0.0.0.0 for GitHub Codespaces
builder.WebHost.UseUrls("http://0.0.0.0:5001");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Register Repository and Service
builder.Services.AddSingleton<CustomerRepository>();
builder.Services.AddSingleton<CustomerService>();
builder.Services.AddScoped<EmployeeService>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // Redirect root to Swagger UI
    app.MapGet("/", context =>
    {
        context.Response.Redirect("/swagger/index.html", permanent: false);
        return Task.CompletedTask;
    });
}
else
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowAll");

app.MapControllers();
app.Run();