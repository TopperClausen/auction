using System.Text;
using backend;
using backend.Models;
using backend.Models.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

DotEnv.SetEnv();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddCors(options => {
    options.AddPolicy(name: "_myAllowSpecificOrigins", policy => {
        policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
    });
});

builder.Services.AddDbContext<Context>();
builder.Services.AddAutoMapper(cfg =>
{
    cfg.CreateMap<Car, CarDTO>()
        .ForMember(dest => dest.User, opt => opt.Ignore());
}, AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"))),
                    ValidIssuer = "SecretMe",
                    ValidAudience = "SecretYou", // Replace with your actual audience
                };
            });

        // Add authorization policy to exclude /sessions and /users
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("ExcludeSessionsAndUsers", policy =>
                policy.RequireAssertion(context =>
                    !context.Resource.ToString().StartsWith("/sessions", StringComparison.OrdinalIgnoreCase) &&
                    !context.Resource.ToString().StartsWith("/users", StringComparison.OrdinalIgnoreCase)));
        });

var app = builder.Build();

app.UseCors("_myAllowSpecificOrigins");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
