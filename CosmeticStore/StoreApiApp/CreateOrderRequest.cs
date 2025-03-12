namespace StoreApiApp.Controllers
{
    public partial class OrdersController
    {
        public class CreateOrderRequest
        {
            public List<string> ProductIds { get; set; }
        }
    }
}
