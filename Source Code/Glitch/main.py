from flask import Flask, request
from flask_restful import Api, Resource

app = Flask(__name__)
api = Api(app)

messages = []
  
@app.route("/Messages", methods = ["GET", "POST"])
def handleRequests():
  if request.method == "GET":
    return {"Server": "HTTP 200: OK", "Data": messages}
  elif request.method == "POST":
    words = request.get_json()
    messages.append(words)
    return {"Server": "HTTP 201: Created"}

if __name__ == "__main__":
  app.run(debug=False)
