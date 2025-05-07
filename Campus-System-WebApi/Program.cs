using Campus_System_WebApi.Controllers.Middlewares;
using Campus_System_WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//di
builder.Services.Add_Dependency_Injection(builder.Configuration);

//swagger
builder.Services.AddSwaggerOptions(builder.Configuration);

//mongodb setting
builder.Services.Add_MongoDb_Context(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/General/swagger.json", "General");
    options.SwaggerEndpoint("/swagger/Developer/swagger.json", "Developer");

    //���\���v�i�H�b�P�@���s������ܤ��O�s
    options.EnablePersistAuthorization();
});

//������~�B�z
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
