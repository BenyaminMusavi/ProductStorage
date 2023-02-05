using Contracts;
using MassTransit;
using ProductStorage.Consumer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
Console.WriteLine("Product Storage");


builder.Services.AddMassTransit(x =>
{

    x.AddConsumer<SaveDbConsumer>();
    x.SetKebabCaseEndpointNameFormatter();
    x.UsingRabbitMq((context, cfg) =>
    {
        //cfg.ReceiveEndpoint(e =>
        //{
        //    e.Consumer<SaveDbConsumer>("",cc =>
        //    {
        //        cc.UseConcurrencyLimit(1);
        //    });
        //});
        //cfg.ReceiveEndpoint(c =>
        //{
        //    c.ConcurrentMessageLimit = 1;
        //});
        cfg.UseConcurrencyLimit(1);
        //cfg.Host(builder.Configuration.GetValue<string>("RabbitConnection"));
        cfg.Host(new Uri("rabbitmq://localhost/"), hst =>
        {
            hst.Username(Constants.UserName);
            hst.Password(Constants.Password);
        });

        cfg.ConfigureEndpoints(context);
        cfg.PrefetchCount = 16;
        //cfg.ReceiveEndpoint(Constants.SendServiceQueue, e =>
        //{
        //    e.Consumer<SaveDbConsumer>();
        //    e.PrefetchCount = 16;
        //});
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
