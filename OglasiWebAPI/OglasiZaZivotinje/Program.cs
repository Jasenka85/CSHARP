using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

    // SECURITY

    sgo.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Autorizacija radi tako da se prvo na ruti /api/v1/Autorizacija/token.  
                      autorizirate i dobijete token (kopirajte ga bez navodnika). 
                      U donji prozor?i? upi�ite: Bearer [razmak] [token]",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    sgo.AddSecurityRequirement(new OpenApiSecurityRequirement()
      {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
        });

    // END SECURITY


    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    sgo.IncludeXmlComments(xmlPath);
});

//los nacin
builder.Services.AddCors(opcije =>
{
    opcije.AddPolicy("CorsPolicy",	
        builder =>
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

//dodavaje baze podataka
builder.Services.AddDbContext<OglasiContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString(name: "OglasiContext")));


// SECURITY

builder.Services.AddAuthentication(x => {
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MojKljucKojijeJakoMisteriozaniDugacak")),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = false
    };
});

builder.Services.AddAuthorization();


// END SECURITY



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

// SECURITY
app.UseAuthentication();
app.UseAuthorization();
// ENDSECURITY

app.UseStaticFiles();

app.MapControllers();

app.UseCors("CorsPolicy");

app.UseDefaultFiles();

app.UseDeveloperExceptionPage();

app.MapFallbackToFile("index.html");

app.Run();
