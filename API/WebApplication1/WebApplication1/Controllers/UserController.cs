using Microsoft.AspNetCore.Mvc;
using System.Data;
using Npgsql;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration? _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get() 
        {
            string query = @"
                select id_usuario as ""id_usuario"",
                        name as ""user_name"",
                        valor as ""valor""
                from usuario";  

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("BudgetIt");
            NpgsqlDataReader myReader;

            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader=myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            return new JsonResult(table);
        }

    }
}
