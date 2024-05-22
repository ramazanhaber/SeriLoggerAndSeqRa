using Microsoft.AspNetCore.Mvc;

namespace SeriLoggerAndSeqRa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("GetOgrenciById")]
        public ActionResult GetOgrenciById(int id)
        {
            try
            {
                //aa
                _logger.LogInformation("request baþladý " + DateTime.UtcNow);

                string deger = "0";
                int sonuc = 5 / (Convert.ToInt32(deger));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return Ok("baþarýlý");
        }
    }
}
