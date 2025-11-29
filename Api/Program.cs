using Application.Common.Handlers.Companies;
using Application.Common.Handlers.Users;
using Infrastructure.Repositories.Companies;
using Infrastructure.Repositories.Users;
using Domain.Persistence.Companies;
using Domain.Persistence.Users;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ICompanyUnitOfWork, CompanyUnitOfWork>();
builder.Services.AddScoped<IUserUnitOfWork, UserUnitOfWork>();


builder.Services.AddScoped<UpdateCompanyRequestHandler>();
builder.Services.AddScoped<DeleteCompanyRequestHandler>();
builder.Services.AddScoped<GetCompanyRequestHandler>();

builder.Services.AddScoped<GetAllUsersRequestHandler>();
builder.Services.AddScoped<UpdateUserRequestHandler>();
builder.Services.AddScoped<DeleteUserRequestHandler>();

builder.Services.AddScoped<IExternalUserApiClient, ExternalUserApiClient>();
builder.Services.AddScoped<ICacheService, MemoryCacheService>();
builder.Services.AddMemoryCache(); 

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
