using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using desktop_AmzOpsApi;

var builder = WebApplication.CreateBuilder(args);

// Add configuration support
builder.Services.AddControllers();
builder.Services.AddSingleton<ITestDataProvider, TestDataProvider>();

// Enable CORS for Angular frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        policy => policy.WithOrigins("http://localhost:4200")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

// Register repos with DI using connection string from config
builder.Services.AddScoped<desktop_AmzOpsApi.DAL.AmazonAccountRepo>(provider =>
    new desktop_AmzOpsApi.DAL.AmazonAccountRepo(
        builder.Configuration["ConnectionStrings:DefaultConnection"] ?? ""
    ));
builder.Services.AddScoped<desktop_AmzOpsApi.DAL.AmazonSiteRepo>(provider =>
    new desktop_AmzOpsApi.DAL.AmazonSiteRepo(
        builder.Configuration["ConnectionStrings:DefaultConnection"] ?? ""
    ));
builder.Services.AddScoped<desktop_AmzOpsApi.DAL.AmazonTeamMemberRepo>(provider =>
    new desktop_AmzOpsApi.DAL.AmazonTeamMemberRepo(
        builder.Configuration["ConnectionStrings:DefaultConnection"] ?? ""
    ));

// Register BLL/services with DI
builder.Services.AddScoped<desktop_AmzOpsApi.BLL.AmazonAccountService>();
builder.Services.AddScoped<desktop_AmzOpsApi.BLL.AmazonSiteService>();
builder.Services.AddScoped<desktop_AmzOpsApi.BLL.AmazonTeamMemberService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Enable CORS before controllers
app.UseCors("AllowAngularDev");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();