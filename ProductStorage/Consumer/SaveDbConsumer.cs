using Contracts;
using MassTransit;

namespace ProductStorage.Consumer
{
    public class SaveDbConsumer : IConsumer<ProductRequest>
    {
        private readonly List<ProductRequest> _products = new List<ProductRequest>();
        public async Task Consume(ConsumeContext<ProductRequest> context)
        {
            //await Task.Delay(5000).ContinueWith(task =>
            //   _products.Add(new Product
            //   {
            //       Id = context.Message.Id,
            //       Amount = context.Message.Amount,
            //       DateTime = context.Message.DateTime,
            //       ProductName = context.Message.ProductName,
            //   })
            //);
            _products.Add(new ProductRequest
            {
                Id = context.Message.Id,
                Amount = context.Message.Amount,
                DateTime = context.Message.DateTime,
                ProductName = context.Message.ProductName,
            });

            Console.WriteLine($"insert db {context.Message.Id}");

            await context.RespondAsync<ResponseMessage>(new { MessageName = "Successed" });

          
            //   return Task.CompletedTask;
        }
    }
}
