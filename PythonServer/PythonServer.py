import socket
import json
import base64

server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.bind(('127.0.0.1', 8000))
server.listen()

print("Python Server is waiting for connections...")

while True:
    client, address = server.accept()
    print(f"Connection from {address}")

    # Simulate a customer object
    customer_object = {
        'Name': 'John Doe',
        'Address': '123 Main Street'
    }

    # Serialize the customer object to JSON
    json_data = json.dumps(customer_object)

    # Encode the JSON data using base64
    encoded_data = base64.b64encode(json_data.encode('utf-8'))
    
    # Print the encoded message to the console
    print(f"Encoded Message: {encoded_data.decode('utf-8')}")

    # Send the encoded data to the C# server
    client.send(encoded_data)

    # Close the connection
    client.close()