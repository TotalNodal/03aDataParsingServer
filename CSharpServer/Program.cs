using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

class Program
{
    [Serializable]
    public class Customer
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
    }
    

    static void Main()
    {
    

        TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 8000);
        server.Start();

        Console.WriteLine("C# Server is waiting for connections...");
        

        while (true)
        {
            TcpClient client = server.AcceptTcpClient();
            NetworkStream stream = client.GetStream();

            byte[] data = new byte[1024];
            int bytesRead = stream.Read(data, 0, data.Length);

            // Decode the received data using base64
            string base64EncodedData = Encoding.ASCII.GetString(data, 0, bytesRead);
            byte[] decodedData = Convert.FromBase64String(base64EncodedData);
            string jsonData = Encoding.UTF8.GetString(decodedData);
            

            // Deserialize the JSON data to a Customer object
            Customer? customer = JsonConvert.DeserializeObject<Customer>(jsonData);

            // Perform logic with the customer object
            ProcessCustomer(customer);

            // Close the connection
            client.Close();
        }
        
    }




    static void ProcessCustomer(Customer? customer)
{
    if (customer != null)
    {
        // Add your logic here
        Console.WriteLine($"Processing customer: Name - {customer.Name}, Address - {customer.Address}");

        // Example: Modify the customer object
        customer.Name = "Updated Name";
        customer.Address = "Updated Address";

        // Serialize and send the updated customer object back to Python server
        SendCustomerToPythonServer(customer);
    }
    else
    {
        // Handle the case where 'customer' is null
        Console.WriteLine("Received null customer object.");
    }



    static void SendCustomerToPythonServer(Customer customer)
    {
        TcpClient client = new TcpClient("127.0.0.1", 8000);
        NetworkStream stream = client.GetStream();

        // Serialize the updated customer object to JSON
        string jsonUpdatedCustomer = JsonConvert.SerializeObject(customer);

        // Encode the JSON data using base64
        byte[] encodedUpdatedData = Convert.ToBase64String(Encoding.UTF8.GetBytes(jsonUpdatedCustomer)).SelectMany(c => Encoding.ASCII.GetBytes(c.ToString())).ToArray();

        // Print the encoded message to the console before sending
        Console.WriteLine($"Encoded Message Before Sending: {Encoding.UTF8.GetString(encodedUpdatedData)}");

        stream.Write(encodedUpdatedData, 0, encodedUpdatedData.Length);

        // Close the connection
        client.Close();
    }
}
}