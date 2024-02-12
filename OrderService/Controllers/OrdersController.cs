using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    public class OrdersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly ILogger<OrdersController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private HttpClient _httpClient;
        private string apiurl = @"http://localhost:23833/";
        private OrderDetails _orderDetails = null;
        public OrdersController(ILogger<OrdersController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            if (_orderDetails == null)
            {
                _orderDetails = new OrderDetails
                {
                    Id = 7261,
                    SetupDate = DateTime.Now.AddDays(-10),
                    Items = new List<Item>()
                };
                _orderDetails.Items.Add(new Item
                {
                    Id = 6514,
                    Name = ".NET Core Book"
                });
            }
        }
        [HttpGet]
        [Route("GetOrderByCustomer/{customerCode}")]
        public OrderDetails GetOrderByCustomer(int customerCode)
        {
            _httpClient = _httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(apiurl);
            var uri = "/api/Customer/GetCustomerName/" + customerCode;
            var result = _httpClient.GetStringAsync(uri).Result;
            _orderDetails.CustomerName = result;
            return _orderDetails;
        }
    }
}
