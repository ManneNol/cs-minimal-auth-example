using MySql.Data.MySqlClient;

namespace Server;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

public class Auth
{
    public record LoginData(string Email, string Password);

    public static async Task<IResult> Login(LoginData user, State state, HttpContext ctx)
    {
        MySqlCommand cmd = new("select id, role from users where email = @Email and password = @Password", state.db);
        
        cmd.Parameters.AddWithValue("@Email", user.Email);
        cmd.Parameters.AddWithValue("@Password", user.Password);
        
        using var reader = cmd.ExecuteReader();
        
        // Check if any are found, if none it returns false, if 1 or more, it gives access to them
        bool bIsFound = reader.Read();
        
        if (!bIsFound)
        {
            return TypedResults.Problem("No Such User Exists");
        }
        
        string id = reader.GetInt32("id").ToString();
        string role = reader.GetString("role");
        
        Console.WriteLine(id + ", " + role);
        await ctx.SignInAsync("opa23.teachers.foodcourt", new ClaimsPrincipal(
            new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id),
                    new Claim(ClaimTypes.Role, role),
                },
                "opa23.teachers.foodcourt"
            )
        ));
        return TypedResults.Ok("Signed in");
    }
}