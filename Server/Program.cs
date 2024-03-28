using MySql.Data.MySqlClient;
using Server;

State state = new State(new("server=localhost;uid=root;pwd=password;database=cs_demo;port=3307"));
try
{
    state.db.Open();
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication().AddCookie("opa23.teachers.foodcourt");
builder.Services.AddAuthorizationBuilder().AddPolicy("admin_route", policy => policy.RequireRole("admin"));
builder.Services.AddSingleton(state);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/login", Auth.Login);
app.MapGet("/admin", () => "Hello, Admin!").RequireAuthorization("admin_route");

app.Run("http://localhost:3001");

public record State(MySqlConnection db);