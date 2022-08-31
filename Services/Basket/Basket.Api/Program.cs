#region Using
using Basket.Api.Repositories;
#endregion

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

#region PrimordialServices
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region OriginalServices
builder.Services.AddStackExchangeRedisCache(x =>
{
    x.Configuration = configuration.GetValue<string>("CacheSettings:ConnectionString");
});
#endregion

#region ManulalServices
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
#endregion

var app = builder.Build();

#region Middelware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();
app.MapControllers();
app.Run();
#endregion