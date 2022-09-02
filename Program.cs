using ObjectToArrayContruct2.Helpers;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();



app.MapGet("/", () => Ok(playerDataJSONDictionary));

app.Run();

