using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Npgsql;

namespace ProjectBlood_API.Controllers {
    [Route("User/")]
    [ApiController]
    public class UserController : ControllerBase {
        private readonly ILogger<UserController> _logger;
        public UserController(ILogger<UserController> logger) {
            _logger = logger;
        }

        [HttpGet]
        [Route("Details")]
        public async Task<IActionResult> GetDetails(long customid) {
            string[] key = new[] { "name", "surname", "bloodtype" };
            try {
                using (NpgsqlCommand command = new NpgsqlCommand($"SELECT name,surname,bloodtype FROM Users WHERE customid={customid}", SQLDatabase.Connection)) {
                    using (NpgsqlDataReader reader = await command.ExecuteReaderAsync()) {
                        JArray Result = new JArray();
                        while (reader.Read()) {
                            Result.Add(ResponseJSON.Create(key, reader));
                        }
                        return StatusCode(200, Result.ToString());
                    }
                }
            }catch(Exception ex) {
                Console.WriteLine(ex.Message);
                return StatusCode(404, ResponseJSON.ErrorValue);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateUser(long customid, string name, string surname, string bloodtype) {
            try{
                using (NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO Users (customid,bloodtype,name,surname) VALUES ({customid},'{bloodtype}','{name}','{surname}')", SQLDatabase.Connection)) {
                    await command.ExecuteNonQueryAsync();
                    return StatusCode(200,true);
                }
            }catch(Exception ex) {
                Console.WriteLine(ex.Message);
                return StatusCode(404, false);
            }
        }

        [HttpPost]
        [Route("Add/Contacts")]
        public async Task<IActionResult> AddContacts(long customid, string cities, string phonenumber, string email) {
            // Cities are seperated by , ex: 'Girne, Magusa'
            // Cities must be added in this order 'Girne, Lefkosa, Magusa, Guzelyurt, Iskele'
            try {
                using (NpgsqlCommand command = new NpgsqlCommand($"INSERT INTO Contacts (customid,cities,phonenumber,email) VALUES ({customid},'{cities}','{phonenumber}','{email}')", SQLDatabase.Connection)) {
                    await command.ExecuteNonQueryAsync();
                    return StatusCode(200, true);
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return StatusCode(404, false);
            }
        }

        [HttpPatch]
        [Route("Change/Name")]
        public async Task<IActionResult> PatchChangeUserName(long customid, string name) {
            try {
                using (NpgsqlCommand command = new NpgsqlCommand($"UPDATE Users SET name='{name}' WHERE customid={customid}", SQLDatabase.Connection)) {
                    await command.ExecuteNonQueryAsync();
                    return StatusCode(200, true);
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return StatusCode(404, true);
            }
        }

        [HttpPatch]
        [Route("Change/Surname")]
        public async Task<IActionResult> PatchChangeUserSurname(long customid, string surname) {
            try {
                using (NpgsqlCommand command = new NpgsqlCommand($"UPDATE Users SET surname='{surname}' WHERE customid={customid}", SQLDatabase.Connection)) {
                    await command.ExecuteNonQueryAsync();
                    return StatusCode(200, true);
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return StatusCode(404, true);
            }
        }


        [HttpPatch]
        [Route("Change/BloodType")]
        public async Task<IActionResult> PatchChangeUserBloodType(long customid, string bloodtype) {
            try {
                using (NpgsqlCommand command = new NpgsqlCommand($"UPDATE Users SET bloodtype='{bloodtype}' WHERE customid={customid}", SQLDatabase.Connection)) {
                    await command.ExecuteNonQueryAsync();
                    return StatusCode(200, true);
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
                return StatusCode(404, true);
            }
        } 
    }
}
