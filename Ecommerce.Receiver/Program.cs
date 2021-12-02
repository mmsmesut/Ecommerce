using Ecommerce.BLL.Abstract;
using Ecommerce.BLL.Concrate;
using Ecommerce.Data;
using Ecommerce.Entity;
using Ecommerce.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using System;
using System.IO;
using System.Text;


namespace Ecommerce.Receiver
{
    class Program
    {

        public static IConfiguration configuration { get; } = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();

        static void Main(string[] args)
        {
            var elasticsearchUrl = configuration["Elastic"];

            Serilog.Log.Logger = new LoggerConfiguration()
             .Enrich.FromLogContext()
             .MinimumLevel.Information()
             .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
             .MinimumLevel.Override("System", LogEventLevel.Debug)
             .WriteTo.Debug()
             .WriteTo.Console()
             .WriteTo.File(@"c:\log\log.txt", rollingInterval: RollingInterval.Day)
             .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticsearchUrl))
             {
                AutoRegisterTemplate = true,
                AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                IndexFormat = $"serverlog-{DateTime.Now:yyyy.MM.dd}"
             })
             .CreateLogger();

           
            #region DB
            var serviceProvider = new ServiceCollection()
               .AddDbContext<EcommerceDbContext>(
                options => options.UseSqlServer(@"Server=.;Database=ECOMMERCE;Trusted_Connection=True;")
             )
             .AddScoped(typeof(IRepositoryBase<Order>), typeof(RepositoryBase<Order>))
             .BuildServiceProvider();

            var orderService = serviceProvider.GetService<IRepositoryBase<Order>>(); 
            #endregion

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
                                string orderedProductName = incominOrder.Product.ProductName;
                                int orderedProductCount = incominOrder.Count;


                                Console.WriteLine($"Alınan Sipariş: Ürün Adı :{incominOrder.Product.ProductName} , Adet : {incominOrder.Count}," +
                                      $"UniqueId : {incominOrder.UId}");

                                incominOrder.Product.Categori = null;
                                incominOrder.Product = null;
                                if (orderService.Add(incominOrder))
                                {
                                    Console.WriteLine("Kayıt  Başarılı");
                                    Serilog.Log.Logger.Information($"Ürün Adı : {orderedProductName} olan {orderedProductCount} Adet Ürün, {incominOrder.UId} ' Referans Kodu ile Sipariş olarak sisteme kaydedilmiştir");
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

                Serilog.Log.Logger.Error("Sipariş Kaydı Başarısız");
            }

            CreateHostBuilder(args).Build().Run();
        }


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                .UseSerilog()
                .UseStartup<Startup>();
             });
    }
}
