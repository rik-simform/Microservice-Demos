using Microsoft.AspNetCore.Mvc;
using Polly;
using Polly.Retry;

namespace UserService.Controllers
{
    public class UsersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private Dictionary<int, string> _users = null;
        public UsersController()
        {
            if (_users == null)
            {
                _users = new Dictionary<int, string>();
                _users.Add(1, "Nilkesh kamath");
                _users.Add(2, "Suresh kalmadi");
                _users.Add(3, "Sanjay Ranchod");
                _users.Add(4, "Mukesh Hari");
            }
        }
        [HttpGet]
        [Route("GetCustomerName/{customerCode}")]
        public ActionResult<string> GetCustomerName(int customerCode)
        {
            if (_users != null && _users.ContainsKey(customerCode))
            {
                return _users[customerCode];
            }
            return "Customer Not Found";
        }

        [HttpGet]
        [Route("GetCustomerNameWithTempFailure/{customerCode}")]
        public ActionResult<string> GetCustomerNameWithTempFailure(int customerCode)
        {
            try
            {
                var retryPolicy = RetryPolicy.Handle<Exception>()
                                    .WaitAndRetry(new[]
                                    {
                                        TimeSpan.FromSeconds(1),
                                        TimeSpan.FromSeconds(2),
                                        TimeSpan.FromSeconds(3)
                                    });

                return retryPolicy.Execute(() =>
                {
                    Random rnd = new Random();
                    int randomError = rnd.Next(1, 11);  // creates a number between 1 and 10

                    if (randomError % 2 == 0)
                        throw new Exception();

                    if (_users != null && _users.ContainsKey(customerCode))
                    {
                        return _users[customerCode];
                    }
                    return "Customer Not Found";
                });
                
            }
            catch
            {
                //Log Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
