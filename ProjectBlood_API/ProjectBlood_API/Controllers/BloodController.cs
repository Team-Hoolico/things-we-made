using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Npgsql;
using System.Xml.Linq;

namespace ProjectBlood_API.Controllers {
    [Route("Blood/")]
    [ApiController]
    public class BloodController : ControllerBase {
        private readonly ILogger<BloodController> _logger;
        public BloodController(ILogger<BloodController> logger) {
            _logger = logger;
        }

        [HttpGet]
        [Route("Users/BloodType")]
        public async Task<IActionResult> UsersWithBloodType(string bloodtype) {
            string[] key = new[] { "name", "surname", "cities", "phonenumber", "email" };
            try {
                using (NpgsqlCommand command = new NpgsqlCommand($"SELECT Users.name, Users.surname, Contacts.cities, Contacts.phonenumber, Contacts.email FROM Users INNER JOIN Contacts ON Contacts.customid = Users.customid WHERE bloodtype='{bloodtype}'",
                    SQLDatabase.Connection)) {
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync()) {
                        JArray Result = new JArray();
                        while (reader.Read()) {
                            Result.Add(ResponseJSON.Create(key, reader));
                        }
                        return StatusCode(200, Result.ToString());
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return StatusCode(404, ResponseJSON.ErrorValue.ToString());
            }
        }

        [HttpGet]
        [Route("Users/BloodType/From")]
        public async Task<IActionResult> UsersWithBloodTypeFrom(string bloodtype, string cities) {
            string[] key = new[] { "name", "surname", "phonenumber", "email" };
            try {
                using (NpgsqlCommand command = new NpgsqlCommand($"SELECT Users.name, Users.surname, Contacts.phonenumber, Contacts.email FROM Users INNER JOIN Contacts ON Contacts.customid = Users.customid WHERE bloodtype='{bloodtype}' AND Contacts.cities LIKE '%{cities}%'",
                    SQLDatabase.Connection)) {
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync()) {
                        JArray Result = new JArray();
                        while (reader.Read()) {
                            Result.Add(ResponseJSON.Create(key, reader));
                        }
                        return StatusCode(200, Result.ToString());
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return StatusCode(404, ResponseJSON.ErrorValue.ToString());
            }
        }
    }
}
