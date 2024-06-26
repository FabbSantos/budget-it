﻿using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApplication.Models;
using Dapper;
using System.Data.SqlClient; // Assuming you've installed Dapper

namespace WebApplication1.Controllers
{
    [Route("transacao")]
    public class TransacaoController : Controller
    {
        private readonly IConfiguration _configuration;

        public TransacaoController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Get()
        {
            string query = @"select * from transacao";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("BudgetIt")))
            {
                try
                {
                    // Assuming connection string is correct in appsettings.json
                    var transactions = connection.Query<Transacao>(query);
                    return Json(transactions);
                }
                catch (Exception ex)
                {
                    // Handle the exception, log the error, return an appropriate error response
                    return StatusCode(500, ex.Message);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Transacao transacao)
        {
            string query = @"insert into transacao (data, valor, tipo) values ( @data, @valor, @tipo)";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("BudgetIt")))
            {
                try
                {
                    await connection.ExecuteAsync(query, new
                    {
                        data = transacao.Data ?? DateTime.Now, // Use nullish coalescing for optional data
                        valor = transacao.Valor,
                        tipo = transacao.Tipo
                    });

                    return StatusCode(200, transacao); // Or NoContent() if no data needs to be returned
                }
                catch (Exception ex)
                {
                    // Handle the exception, log the error, return an appropriate error response
                    return StatusCode(500, ex.Message);
                }
            }
        }




        [HttpPut]
        public async Task<IActionResult> Put(Transacao transacao)
        {
            string query = @"
                update transacao
                set valor = @valor,
                    tipo = @tipo
                where id_transacao = @id_transacao";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("BudgetIt")))
            {
                try
                {
                    await connection.ExecuteAsync(query, new { valor = transacao.Valor, tipo = transacao.Tipo, id_transacao = transacao.Id_transacao });
                    return StatusCode(200, "Transaction updated"); // Or NoContent() if no data needs to be returned
                }
                catch (Exception ex)
                {
                    // Handle the exception, log the error, return an appropriate error response
                    return StatusCode(500, ex.Message);
                }
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            string query = @"
                delete from transacao
                where id_transacao = @Id";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("BudgetIt")))
            {
                try
                {
                    await connection.ExecuteAsync(query, new { Id });
                    return StatusCode(200, "Transaction deleted "); // Or NoContent() if no data needs to be returned
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
