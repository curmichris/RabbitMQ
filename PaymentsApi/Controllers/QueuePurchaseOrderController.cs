using Microsoft.AspNetCore.Mvc;
using PaymentsApi.Models;

namespace PaymentsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueuePurchaseOrderController : ControllerBase
    {
        [HttpPost]
        public IActionResult MakePayment([FromBody] PurchaseOrder purchaseOrder)
        {
            return Ok(purchaseOrder);
        }
    }
}