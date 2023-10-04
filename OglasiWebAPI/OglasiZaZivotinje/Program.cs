using System.Reflection;
using Microsoft.EntityFrameworkCore;
using OglasiZaZivotinje.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(sgo => {

    var o = new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "Oglasi za zivotinje",
        Version = "v1",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact()
        {
            Email = "jaugustinovic85@gmail.com",
            Name = "Jasenka Augustinovic"
        },
        Description = "Ovo je dokumentacija za oglase",
        License = new Microsoft.OpenApi.Models.OpenApiLicense()
        {
            Name = "Edukacijska licenca"
        }
    };
    sgo.SwaggerDoc("v1", o);
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    sgo.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(opcije =>
{
    opcije.AddPolicy("CorsPolicy",	
        builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});


builder.Services.AddDbContext<OglasiContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString(name: "OglasiContext")));


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger(opcije =>
    {
        opcije.SerializeAsV2 = true;

    });
    app.UseSwaggerUI(opcije =>
    {
        opcije.ConfigObject.AdditionalItems.Add("requestSnippetsEnabled", true);
    });
//}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.MapControllers();

app.UseCors("CorsPolicy");

app.UseDefaultFiles();

app.UseDeveloperExceptionPage();

app.MapFallbackToFile("index.html");

app.Run();
