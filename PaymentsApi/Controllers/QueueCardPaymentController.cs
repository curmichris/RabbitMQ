using Common;
using Microsoft.AspNetCore.Mvc;
using PaymentsApi.Models;
using PaymentsApi.RabbitMQ;
using System;

namespace PaymentsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueCardPaymentController : ControllerBase
    {
        [HttpPost]
        public IActionResult MakePayment([FromBody] CardPayment payment)
        {
            try
            {
                RabbitMQClient client = new RabbitMQClient();
                client.SendPayment(payment);
                client.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return Ok(payment);
        }
    }
}