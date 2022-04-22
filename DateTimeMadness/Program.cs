using ClientContext;
using DateTimeMadness.DataAccessLayer;
using TinyHelpers.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new UtcDateTimeConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddClientContextOperationFilter();
});

builder.Services.AddClientContextAccessor();

builder.Services.AddSqlServer<DataContext>(builder.Configuration.GetConnectionString("SqlConnection"));

var app = builder.Build();
app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseClientContext();

app.UseAuthorization();

app.MapControllers();

app.Run();
