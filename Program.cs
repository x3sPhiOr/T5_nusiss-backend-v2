using BookStoreApi.Models;
using BookStoreApi.ReservationApp.Models;
using BookStoreApi.Services;
using BookStoreApi.Views;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<BookStoreDatabaseSettings>(
    builder.Configuration.GetSection("BookStoreDatabase"));

builder.Services.AddSingleton<BooksService>();
//builder.Services.AddScoped<AuthService>();

builder.Services.AddRazorPages();

builder.Services.AddHttpClient<ReservationModel>();

// LOCAL
//builder.Services.AddDbContext<ReservationContext>(options =>
//    options.UseSqlServer(@"Server=(LocalDb)\LocalDB;Database=ReservationDb_v2;Trusted_Connection=True;"));

// AWS
builder.Services.AddDbContext<ReservationContext>(options =>
    options.UseSqlServer(@"Data Source=nusiss.cpku8mwo022g.ap-southeast-1.rds.amazonaws.com;Initial Catalog=NUSISS;User ID=admin;Password=eUbHxTz67UQjN3Rd3liD;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"));

builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

// Configure Stripe
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
Stripe.StripeConfiguration.ApiKey = builder.Configuration["Stripe:SecretKey"];
builder.Services.AddSingleton<StripePaymentService>();
builder.Services.AddScoped<StripePaymentService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 443; // Default HTTPS port
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://localhost:8081", "exp://192.168.1.17:8081") // Replace with your frontend origin
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

// Configure Authentication
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.Authority = "https://dev-apu414kr0tjtv2uv.us.auth0.com/";
//    options.Audience = "https://localhost:7248/api/Books";
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true
//    };
//});

//builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use CORS policy
app.UseCors("AllowSpecificOrigin");

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapRazorPages();
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
