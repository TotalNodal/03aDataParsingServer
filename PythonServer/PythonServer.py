from flask import Flask, request, jsonify

app = Flask(__name__)

@app.route('/receive', methods=['POST'])
def receive_json():
    try:
        # Receive JSON data from the sender
        json_data = request.get_json()

        # Decode the JSON data to a Python dictionary
        decoded_data = json_data

        # Print the decoded data
        print("Received JSON data:")
        print(decoded_data)

        # You can now use the decoded_data as needed

        return jsonify({"status": "success"})
    except Exception as e:
        return jsonify({"status": "error", "message": str(e)})

if __name__ == '__main__':
    # Run the Flask application on port 5001
    app.run(port=5001)