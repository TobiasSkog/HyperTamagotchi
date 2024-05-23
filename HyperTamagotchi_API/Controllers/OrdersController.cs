using HyperTamagotchi_API.Data;
using HyperTamagotchi_API.Filters;
using HyperTamagotchi_API.Models;
using HyperTamagotchi_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HyperTamagotchi_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(ApplicationDbContext context) : ControllerBase
    {
        private readonly ApplicationDbContext _context = context;

        // GET: api/Orders/GetOrdersFromCustomer
        [HttpGet("GetOrdersFromCustomer")]
        [AuthorizeByRole("Customer", "Admin")]
        public async Task<ActionResult<List<Order>>> GetOrdersFromCustomer()
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email).Value;

            var orders = await _context.Orders
            .Where(o => o.Customer.Email == email)
            .Include(o => o.Customer)
            .Include(o => o.Items)
                .ThenInclude(i => i.ShoppingItem)
            .OrderByDescending(o => o.OrderDate)
            .Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                Customer = new CustomerDto
                {
                    Id = o.Customer.Id,
                    FirstName = o.Customer.FirstName,
                    LastName = o.Customer.LastName,
                    FullName = (o.Customer.FirstName + " " + o.Customer.LastName),
                    Email = o.Customer.Email!,
                    AddressId = o.Customer.AddressId,
                    ShoppingCartId = o.Customer.ShoppingCartId
                },
                OrderDate = o.OrderDate,
                ShippingDate = o.ShippingDate,
                ExpectedDate = o.ExpectedDate,
                Items = o.Items.Select(i => new ShoppingItemDto
                {
                    ShoppingItemId = i.ShoppingItem.ShoppingItemId,
                    Name = i.ShoppingItem.Name,
                    Description = i.ShoppingItem.Description,
                    Stock = (byte)i.ShoppingItem.Stock,
                    Price = i.ShoppingItem.Price,
                    CurrencyType = i.ShoppingItem.CurrencyType,
                    Discount = i.ShoppingItem.Discount,
                    ImagePath = i.ShoppingItem.ImagePath,
                    Quantity = i.ShoppingItem.Quantity
                }).ToList()
            })
            .ToListAsync();

            return Ok(orders);

            //List<OrderDto> ordersDto = [];
            //foreach (var order in orders)
            //{
            //    ordersDto.Add(new OrderDto
            //    {
            //        OrderId = order.OrderId,
            //        Customer = new CustomerDto
            //        {
            //            Id = order.Customer.Id,
            //            FirstName = order.Customer.FirstName,
            //            LastName = order.Customer.LastName,
            //            FullName = (order.Customer.FirstName + " " + order.Customer.LastName),
            //            Email = order.Customer.Email!,
            //            AddressId = order.Customer.AddressId,
            //            ShoppingCartId = order.Customer.ShoppingCartId
            //        },
            //        OrderDate = order.OrderDate,
            //        ShippingDate = order.ShippingDate,
            //        ExpectedDate = order.ExpectedDate,
            //        Items = order.Items.Select(i => new ShoppingItemDto
            //        {
            //            ShoppingItemId = i.ShoppingItem.ShoppingItemId,
            //            Name = i.ShoppingItem.Name,
            //            Description = i.ShoppingItem.Description,
            //            Stock = (byte)i.ShoppingItem.Stock,
            //            Price = i.ShoppingItem.Price,
            //            CurrencyType = i.ShoppingItem.CurrencyType,
            //            Discount = i.ShoppingItem.Discount,
            //            ImagePath = i.ShoppingItem.ImagePath,
            //            Quantity = i.ShoppingItem.Quantity
            //        }).ToList()
            //    });
            //}

            //return Ok(ordersDto);
        }

        // GET: api/Orders/5
        [HttpGet("GetOrderById/{id}")]
        [AuthorizeByRole("Admin", "Customer")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _context.Orders
            .Where(o => o.OrderId == id)
            .Include(o => o.Customer)
            .Include(o => o.Items)
                .ThenInclude(o => o.ShoppingItem)
            .OrderByDescending(o => o.OrderDate)
            .Select(o => new OrderDto
            {
                OrderId = o.OrderId,
                Customer = new CustomerDto
                {
                    Id = o.Customer.Id,
                    FirstName = o.Customer.FirstName,
                    LastName = o.Customer.LastName,
                    FullName = (o.Customer.FirstName + " " + o.Customer.LastName),
                    Email = o.Customer.Email!,
                    AddressId = o.Customer.AddressId,
                    ShoppingCartId = o.Customer.ShoppingCartId
                },
                OrderDate = o.OrderDate,
                ShippingDate = o.ShippingDate,
                ExpectedDate = o.ExpectedDate,
                Items = o.Items.Select(i => new ShoppingItemDto
                {
                    ShoppingItemId = i.ShoppingItem.ShoppingItemId,
                    Name = i.ShoppingItem.Name,
                    Description = i.ShoppingItem.Description,
                    Stock = (byte)i.ShoppingItem.Stock,
                    Price = i.ShoppingItem.Price,
                    CurrencyType = i.ShoppingItem.CurrencyType,
                    Discount = i.ShoppingItem.Discount,
                    ImagePath = i.ShoppingItem.ImagePath,
                    Quantity = i.ShoppingItem.Quantity
                }).ToList()
            })
            .FirstOrDefaultAsync();
            return Ok(order);
            //var order = await _context.Orders
            //.Include(o => o.Customer)
            //.Include(o => o.Items)
            //    .ThenInclude(i => i.ShoppingItem)
            //.FirstOrDefaultAsync(o => o.OrderId == id);
            //var orderDto = new OrderDto
            //{
            //    OrderId = order.OrderId,
            //    Customer = new CustomerDto
            //    {
            //        Id = order.Customer.Id,
            //        FirstName = order.Customer.FirstName,
            //        LastName = order.Customer.LastName,
            //        FullName = (order.Customer.FirstName + " " + order.Customer.LastName),
            //        Email = order.Customer.Email!,
            //        AddressId = order.Customer.AddressId,
            //        ShoppingCartId = order.Customer.ShoppingCartId
            //    },
            //    OrderDate = order.OrderDate,
            //    ShippingDate = order.ShippingDate,
            //    ExpectedDate = order.ExpectedDate,
            //    Items = order.Items.Select(i => new ShoppingItem
            //    {
            //        ShoppingItemId = i.ShoppingItem.ShoppingItemId,
            //        Name = i.ShoppingItem.Name,
            //        Description = i.ShoppingItem.Description,
            //        Stock = i.ShoppingItem.Stock,
            //        Price = i.ShoppingItem.Price,
            //        CurrencyType = i.ShoppingItem.CurrencyType,
            //        Discount = i.ShoppingItem.Discount,
            //        ImagePath = i.ShoppingItem.ImagePath,
            //        Quantity = i.ShoppingItem.Quantity
            //    }).ToList()
            //};

            //var jsonResponse = JsonConvert.SerializeObject(order, new JsonSerializerSettings
            //{
            //    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            //});

            //return Ok(jsonResponse);
        }

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [AuthorizeByRole("Customer", "Admin")]
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
        [HttpPost]
        [AuthorizeByRole("Customer", "Admin")]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrder", new { id = order.OrderId }, order);
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }
    }
}
