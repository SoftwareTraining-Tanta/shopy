
Host.CreateDefaultBuilder(args).ConfigureWebHostDefaults(webBuilder =>
{
    webBuilder.UseStartup<Startup>();
}).Build().Run();
// using System.Security.Cryptography;
// using System.Text;

// static string ToSha256(string text)
// {
//     using (HashAlgorithm algorithm = SHA256.Create())
//     {
//         byte[] hashedText = algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));
//         StringBuilder stringBuilder = new();
//         foreach (byte b in hashedText)
//         {
//             stringBuilder.Append(b.ToString("X2"));
//         }
//         return stringBuilder.ToString();
//     }
// }

// Console.WriteLine(ToSha256("password"));