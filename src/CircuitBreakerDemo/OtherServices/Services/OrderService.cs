namespace OtherServices.Services
{
    using System.Net.Http;
    using System.Threading.Tasks;
    
    public interface IOrderService
    {
        Task<string> GetOrderDetailsAsync(string orderId);
    }

    public class OrderService : IOrderService
    {
        public async Task<string> GetOrderDetailsAsync(string orderId)
        {
            using (HttpClient client = new HttpClient())
            {
                return await client.GetStringAsync($"http://localhost:9999/api/values/{orderId}");
            }
        }
    }
}
