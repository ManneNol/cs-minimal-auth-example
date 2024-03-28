using Server;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication().AddCookie("opa23.teachers.foodcourt");
builder.Services.AddAuthorizationBuilder().AddPolicy("admin_route", policy => policy.RequireRole("admin"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/login", Auth.Login);
app.MapGet("/admin", () => "Hello, Admin!").RequireAuthorization("admin_route");

app.Run("http://localhost:3001");