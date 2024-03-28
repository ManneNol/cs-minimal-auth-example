namespace Server;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

public class Auth
{
    public static async Task<IResult> Login(HttpContext ctx)
    {
        // here you should query the database about the info sent from the client
        // and return early if its incorrect
        // return TypedResults.Problem("Wrong email or password")

        await ctx.SignInAsync("opa23.teachers.foodcourt", new ClaimsPrincipal(
            new ClaimsIdentity(
                new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "my_name"),
                    new Claim(ClaimTypes.Role, "admin"),
                },
                "opa23.teachers.foodcourt"
            )
        ));
        return TypedResults.Ok("Signed in");
    }
}