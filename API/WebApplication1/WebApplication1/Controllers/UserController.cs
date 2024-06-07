using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApplication.Models;
using Dapper;
using System.Data.SqlClient; // Assuming you've installed Dapper

namespace WebApplication1.Controllers
{
    [Route("usuario")]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            string query = @"select * from usuario";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("BudgetIt")))
            {
                try
                {
                    // Assuming connection string is correct in appsettings.json
                    var transactions = connection.Query<Usuario>(query);
                    return Json(transactions);
                }
                catch (Exception ex)
                {
                    // Handle the exception, log the error, return an appropriate error response
                    return StatusCode(500, ex.Message);
                }
            }
        }

        //[HttpPost]
        //public async Task<IActionResult> Post(Usuario user)
        //{
        //    string query = @"insert into usuario (nome, saldo) values (@nome, @saldo)";

        //    using (var connection = new SqlConnection(_configuration.GetConnectionString("BudgetIt")))
        //    {
        //        try
        //        {
        //            await connection.ExecuteAsync(query, new { nome = user.Nome, saldo = user.Saldo});
        //            return StatusCode(200,  user.Nome); // Or NoContent() if no data needs to be returned
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle the exception, log the error, return an appropriate error response
        //            return StatusCode(500, ex.Message);
        //        }
        //    }
        //}
        [HttpPut]
        public async Task<IActionResult> Put(Usuario user)
        {
            string query = @"
                update usuario 
                set nome = @nome
                where id_usuario = 1";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("BudgetIt")))
            {
                try
                {
                    await connection.ExecuteAsync(query, new { nome = user.Nome });
                    return StatusCode(200, "User updated"); // Or NoContent() if no data needs to be returned
                }
                catch (Exception ex)
                {
                    // Handle the exception, log the error, return an appropriate error response
                    return StatusCode(500, ex.Message);
                }
            }
        }

    }
}
