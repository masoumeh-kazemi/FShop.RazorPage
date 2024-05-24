using FShop.RazorPage.Infrastructure;
using FShop.RazorPage.Services.Auth;
using FShop.RazorPage.Services.Banners;
using FShop.RazorPage.Services.Categories;
using FShop.RazorPage.Services.Comments;
using FShop.RazorPage.Services.Orders;
using FShop.RazorPage.Services.Products;
using FShop.RazorPage.Services.Roles;
using FShop.RazorPage.Services.Sellers;
using FShop.RazorPage.Services.Sliders;
using FShop.RazorPage.Services.UserAddress;
using FShop.RazorPage.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterApiServices();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("Account", configPolicy =>
    {
        configPolicy.RequireAuthenticatedUser();
    });
    option.AddPolicy("Seller", configPolicy =>
    {
        configPolicy.RequireAuthenticatedUser();
        configPolicy.RequireAssertion(f => f.User.Claims.Any
            (c => c.Type == ClaimTypes.Role && c.Value.Contains("Seller")));
    });
});

builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizeFolder("/Profile", "Account");
        options.Conventions.AuthorizeFolder("/SellerPanel", "Seller");
    });

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidAudience = builder.Configuration["JwtConfig:Audience"],
        ValidIssuer = builder.Configuration["JwtConfig:Issuer"],
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:SignInKey"])),
        ValidateLifetime = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true
    };
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    var token = context.Request.Cookies["token"]?.ToString();

    if (string.IsNullOrWhiteSpace(token) == false)
    {
        context.Request.Headers.Append("Authorization", $"Bearer {token}");
    }
    await next();
});


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.Use(async (context, next) =>
{
    await next();
    var status = context.Response.StatusCode;
    if (status == 401)
    {
        var path = context.Request.Path;
        context.Response.Redirect($"/auth/login?redirectTo={path}");
    }
});
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.Run();
