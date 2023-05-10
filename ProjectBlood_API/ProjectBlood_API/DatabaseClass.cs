using System.Data.Common;
using Newtonsoft.Json.Linq;
using Npgsql;

namespace ProjectBlood_API {
    public static class SQLDatabase {
        public static string login = "Host=localhost;Username=postgres;Password=EpicDatabaseTime;Database=ProjectBlood";
        public static NpgsqlConnection Connection = new NpgsqlConnection(login);
    }

    public static class ResponseJSON {
        public static JObject ErrorValue = new JObject() {{"Error", "Not Found"}};
        public static JObject Create(string[] key,NpgsqlDataReader value) {
            JObject result = new JObject();
            for (int i=0; i<key.Length; i++) {
                result.Add(key[i], 
                    value[key[i]] switch {
                    (long) => (long)value[key[i]],
                    (_) => (string)value[key[i]]
                });
            }

            return result;
        }
    }
}
