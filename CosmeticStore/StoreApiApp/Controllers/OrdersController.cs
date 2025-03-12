using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CosmeticStoreLib.Data;
using CosmeticStoreLib.Models;

namespace StoreApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class OrdersController : ControllerBase
    {
        private readonly CosmeticStoreContext _context;

        public OrdersController(CosmeticStoreContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet("GetOrders")]
        public IActionResult GetOrders()
        {
            var orders = _context.Orders.Include(o => o.OrderProducts).ThenInclude(op => op.ProductArticleNumberNavigation).ToList();
            return Ok(orders);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("CreateOrder")]
        public IActionResult CreateOrder([FromBody] CreateOrderRequest request)
        {
            // Проверка наличия товаров в запросе
            if (!request?.ProductIds?.Any() ?? true)
            {
                return BadRequest("Ошибка! Список не может быть пустым!");
            }

            var products = _context.Products
                .Where(p => request.ProductIds.Contains(p.ProductArticleNumber))
                .ToList();

            // Проверка наличия всех товаров
            if (products.Count != request.ProductIds.Count)
            {
                return BadRequest("Товары не найдены");
            }

            // Создание заказа
            var order = new Order
            {
                OrderDate = DateTime.Now,
                OrderDeliveryDate = DateTime.Now.AddDays(3),
                OrderPickupCode = new Random().Next(1000, 9999),
                OrderPickupPointId = 1,
                OrderStatus = "Создан!"
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            // Добавление товаров в заказ
            foreach (var product in products)
            {
                _context.OrderProducts.Add(new OrderProduct
                {
                    OrderId = order.OrderId,
                    ProductArticleNumber = product.ProductArticleNumber,
                    ProductAmount = 1
                });
            }

            _context.SaveChanges();
            return Ok();
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }

    }
}
