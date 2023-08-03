using FluentValidation;
using Microsoft.EntityFrameworkCore;
using EmpresaGenericaAPI.CQRS.Commands;
using EmpresaGenericaAPI.CQRS.Commands.EmpleadoDelete;
using EmpresaGenericaAPI.CQRS.Commands.EmpleadoPost;
using EmpresaGenericaAPI.CQRS.Commands.EmpleadoPut;
using EmpresaGenericaAPI.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(config =>
{
    config.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddScoped<IValidator<PostEmpleadoCommand>, PostEmpleadoCommandValidator>();
builder.Services.AddScoped<IValidator<PutEmpleadoCommand>, PutEmpleadoCommandValidator>();
builder.Services.AddScoped<IValidator<DeleteEmpleadoCommand>, DeleteEmpleadoCommandValidator>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EfcodeFirstContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
