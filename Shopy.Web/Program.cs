

Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
{
    webBuilder.UseStartup<Startup>();
}).Build().Run();

// using Shopy.Web.Models;
// Cart cart = new();
// cart.RemoveFromCart(20);