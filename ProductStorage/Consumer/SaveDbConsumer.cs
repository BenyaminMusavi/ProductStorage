using Contracts;
using MassTransit;

namespace ProductStorage.Consumer
{
    public class SaveDbConsumer : IConsumer<Product>
    {
        private readonly List<Product> _products = new List<Product>();
        public async Task Consume(ConsumeContext<Product> context)
        {
            await Task.Delay(5000).ContinueWith(task =>
               _products.Add(new Product
               {
                   Id = context.Message.Id,
                   Amount = context.Message.Amount,
                   DateTime = context.Message.DateTime,
                   ProductName = context.Message.ProductName,
               })
            );

            Console.WriteLine($"insert db {context.Message.Id}");

            await context.RespondAsync(new ResponseMessage { MessageName = "Successed" });

            //if (_products.Count == 50)
            //{
            //    var message = _products;

            //}
            //   return Task.CompletedTask;
        }
    }
}
