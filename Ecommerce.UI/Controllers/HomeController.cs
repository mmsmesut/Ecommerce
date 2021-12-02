using Ecommerce.BLL.Abstract;
using Ecommerce.BLL.Concrate;
using Ecommerce.Entity;
using Ecommerce.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.UI.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository=null;
        private readonly ICategoryRepository _categoryRepository = null;
        private readonly IOrderRepository _orderRepository = null;

        public HomeController(IProductRepository productRepository, ICategoryRepository categoryRepository,IOrderRepository orderRepository)
        {
        //    _logger = logger;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _orderRepository = orderRepository;
        }

        public IActionResult Index()
        {
            List<Product> products =  _productRepository.All().ToList();
            return View(products);
        }


        public IActionResult NewOrder(int id)
        {
            var orderedProduct = _productRepository.Where(x => x.ProductId == id).FirstOrDefault();
            if (orderedProduct != null)
            {

                Order myOrder = new Order() { ProductId = id, Count = 1, Product = orderedProduct, UId = Guid.NewGuid() };
                AddDataToQueue(myOrder);
            }

            return RedirectToAction("Index");
        }


        public IActionResult Order()
        {
            List<Order> orders = _orderRepository.All().ToList();
            return View(orders);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void AddDataToQueue(Order myOrder)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };  //RabbitMQ Hostuna bağlanmayı sağlar 
            using (IConnection connection = factory.CreateConnection()) //Hosta Bağlanma 
            {
                using (IModel channel = connection.CreateModel()) //Yeni bir channel oluşturulur - Yeni bir Session oluşturulur ,Oluşturulan Queue bu session üzerinden gönderilir
                {
                    channel.QueueDeclare
                    (
                             queue: "EcommerceOrder_queue",
                             durable: false, //in memory mi fiizksel mi tutulacak 
                             exclusive: false, //Diğer connectionlarla kullanılma izni
                             autoDelete: false,
                             arguments: null //Excahnge ile aralaklı parametrelerdir
                    );

                    string message = JsonConvert.SerializeObject(myOrder); //Serialize  edilir
                    var body = Encoding.UTF8.GetBytes(message); //Byte çevrilir

                    channel.BasicPublish(exchange: "", routingKey: "EcommerceOrder_queue", basicProperties: null, body: body); //Mesajı alıp bir veya daha fazla kuyruğa sokar
                    //Console.WriteLine($"Gönderilen Sipariş: Ürün Adı :{myOrder.Product.ProductName} , Kategorisi:{myOrder.Product.Categori.CategoryName}, Adet : {myOrder.Count}," +
                    // $"UniqueId : {myOrder.UId}");

                    //logger.Info($"{myOrder.UId}  Referans kodlu sipariş Kuyruğa gönderildi");

                }
                //Console.WriteLine("İlgili data gönderildi");
                //Console.ReadLine();
            }

        }
    }
}
