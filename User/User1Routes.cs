using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreApi2.Data;
using StoreApi2.Token;

namespace StoreApi2.User
{
    public static class User1Routes
    {
        public static void AddRoutesUser1(this WebApplication app) 
        {
            var routesUser1 = app.MapGroup("User1");

            routesUser1.MapPost("connect/token", async ([FromBody]LoginUser1Request request, AppDbContext context, IConfiguration configuration, CancellationToken ct) =>
            {
                var verificacao = await context.User1s.AnyAsync(use1 => use1.Name == request.Name || use1.Password == request.Password, ct);

                if (verificacao)

                    return Token.Token.Create(configuration, "Admin");

                return "usuario não encontrado";
                
                
            });
            
            routesUser1.MapPost("", async ([FromBody]AddUser1Request request, AppDbContext context, CancellationToken ct) =>
            {
                var verificacao = await context.User1s.AnyAsync(use1 => use1.Name == request.Name || use1.Email == request.Email || use1.Cpf == request.Cpf, ct);

                if (verificacao) return Results.Conflict("Usuario já existe");

                var newUser1 = new User1(request.Name, request.Email, request.Password, request.Cpf);
                await context.User1s.AddAsync(newUser1, ct);
                await context.SaveChangesAsync(ct);

                var UserReturn = new User1Dto(newUser1.Id, newUser1.Name, newUser1.Email, newUser1.Password, newUser1.Cpf );

                return Results.Ok(UserReturn);
            });

            routesUser1.MapGet("", async (AppDbContext context, CancellationToken ct) =>
            {
                var user = await context.User1s                            
                                .Where(user1 => user1.Active)
                                .Select(user => new User1Dto(user.Id, user.Name, user.Email, user.Password, user.Cpf))
                                .ToListAsync(ct);

                return user;


            });

            routesUser1.MapGet("login", async ([FromBody]LoginUser1Request request, AppDbContext context, CancellationToken ct) =>
            {
                var verificacao = await context.User1s.AnyAsync(use1 => use1.Name == request.Name || use1.Password == request.Password, ct);

                if (verificacao)

                    return Results.Ok("Login efetuado com sucesso");

                return Results.Conflict();






            });
        }
    }
}
