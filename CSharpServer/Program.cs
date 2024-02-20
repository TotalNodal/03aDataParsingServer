using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{

    class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
    static async Task Main()
    {
        // Create a sample customer object
        Customer customer = new Customer
        {
            Id = 1,
            Name = "John Doe",
            Email = "john.doe@example.com"
        };

        // Convert the customer object to JSON
        string json = JsonConvert.SerializeObject(customer);

        // Set the URL of the recipient's server
        string url = "http://localhost:5001/receive";

        // Send the JSON data using HTTP POST request
        using (HttpClient client = new HttpClient())
        {
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("JSON data sent successfully");
            }
            else
            {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
    }

    
}