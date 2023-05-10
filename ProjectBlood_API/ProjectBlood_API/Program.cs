using Npgsql;
using ProjectBlood_API;

var builder = WebApplication.CreateBuilder(args);
await SQLDatabase.Connection.OpenAsync();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

//Database init
try {
    string CreateTable = File.ReadAllText("InitTables.sql",System.Text.Encoding.UTF8);
    await using (NpgsqlCommand command = new NpgsqlCommand(CreateTable, SQLDatabase.Connection)) {
        await command.ExecuteNonQueryAsync();
        Console.WriteLine("Created Tables if not existed");
    }
} catch(Exception ex) {
    Console.WriteLine(ex.Message);
    throw;
}


app.Run();
