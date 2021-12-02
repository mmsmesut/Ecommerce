using Ecommerce.BLL.Abstract;
using Ecommerce.BLL.Concrate;
using Ecommerce.Data;
using Ecommerce.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;


namespace Ecommerce.Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            //Order Servis'inin bir örneği alınır 
            var serviceProvider = new ServiceCollection()
               .AddDbContext<EcommerceDbContext>(
                options => options.UseSqlServer(@"Server=.;Database=ECOMMERCE;Trusted_Connection=True;")
             )
             .AddScoped(typeof(IRepositoryBase<Order>), typeof(RepositoryBase<Order>))
             .BuildServiceProvider();
             
             var orderService = serviceProvider.GetService<IRepositoryBase<Order>>();

            try
            {               
                    var factory = new ConnectionFactory() { HostName = "localhost" };
                    using (IConnection connection = factory.CreateConnection())
                    {
                        using (IModel channel = connection.CreateModel())
                        {

                            channel.QueueDeclare(queue: "EcommerceOrder_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
                            var consumer = new EventingBasicConsumer(channel);
                            consumer.Received += (model, ea) => //Bu metod sayesinde sürekli bir dinleme yapılır 
                            {
                                var body = ea.Body.ToArray(); //byte olarak mesaj alınır 
                                var message = Encoding.UTF8.GetString(body); //string veriye çevrilir
                                Order incominOrder = JsonConvert.DeserializeObject<Order>(message); //Deserilize edilerek incominOrder nesnesine dönüştürülür 

                                Console.WriteLine($"Alınan Sipariş: Ürün Adı :{incominOrder.Product.ProductName} , Adet : {incominOrder.Count}," +
                                      $"UniqueId : {incominOrder.UId}");

                                incominOrder.Product.Categori = null;
                                incominOrder.Product = null;
                                if (orderService.Add(incominOrder))
                                {

                                    Console.WriteLine("Kayıt  Başarılı");
                                }
                            };

                            channel.BasicConsume(queue: "EcommerceOrder_queue", autoAck: true, consumer: consumer); // Kuyruktan mesajlar çekilir ,"autoAck: true" mesaj alındaıktan sonra kuyruktan silinsin 
                            Console.WriteLine("Mesaj Alındı");
                            Console.ReadLine();
                        }
                    }
            }
            catch (Exception ex)
            {
                //Log tutulacak 
                throw;
            }
        }
    }
}
