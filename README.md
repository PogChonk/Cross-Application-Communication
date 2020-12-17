# Cross-Application Communication

#### Ever wondered how to send a message from a 3rd party application to Roblox, like a *Remote Connection*? Well look no farther! This Github will explain all 3 steps of how to achieve this!

#

## Requirements

#### Before we continue, there are some prerequisites.
* VS(C) - Visual Studio (Community) (IDE where you can create a plentiful amount of different types of applications) [VSC Download](https://visualstudio.microsoft.com/downloads/)
  * IDE where we'll be creating our application.
* Python - Preferrably the latest version (Python3.9) [Python3 Download](https://www.python.org/downloads/)
  * Used for our REST API which is how we'll be communicating between applications.
* Glich - You'll need a [Glitch](https://glitch.com/) account
  * This is where we'll be hosting our REST API
  
And then of course, we'll need Roblox Studio if you don't have that downloaded already.

#

## Basic Knowledge

* #### What is a REST API?

A **REST API** is simply a web server architectural style for handling HTTP Requests, it's also sometimes called **RESTful API**.

**REST** stands for Representational (RE) State (S) Transfer (T) **->** Representational State Transfer (REST).

**API** stands for Application (A) Programming (P) Interface (I) **->** Application Programming Interface (API).

* #### What will we use for our REST API?

Simple, we'll be using a library written in Python for Python. It's called **Flask**, which is a Micro Web Framework. Why is it Micro? Because it does not require particular tools or libraries. It has no database abstraction layer, form validation, or any other components where pre-existing third-party libraries provide common functions.

Since **Flask** itself isn't meant to be used as a production server, they recommend a proper WSGI. A pretty common WSGI Server that's used with Flask is **Gunicorn**. **Gunicorn** is a Python Web Server Gateway Interface HTTP server.

* #### What languages do we need to know?

We will be creating our REST API in Python. **Python**

We will be creating our Application in Visual Studio Community, and C# is a common language that's used in that program. **C#**

We will be polling for our REST API in Roblox, which uses a revised version of Lua, called Luau. **Lua/u**

Though you don't need extensive knowledge as this is a step-by-step guide. You just need to know the basics of Python, C#, and Luau. If not, that's fine! This will still work for you, just won't make as much sense. Feel free to do research before/after this to get a better understanding.

#

## Step 1: Glitch

### Step 1A: Setting up our environent

Head over to [glitch.com](https://glitch.com/) and sign in if you have an account, or create one if you don't have an account.

When you create your account, at the top right you'll see a button that says **New Project**, click it and then choose **hello-webpage**.

![Image](https://cdn.discordapp.com/attachments/784546024664334360/788903245992689664/unknown.png)

##### Feel free to change the name of your project at the top left.

Alright, now your workspace should look like this.

![Image](https://i.gyazo.com/36d002bfacdfef591b7bba57eafe1fc1.png)

Go ahead and *delete* **everything**.

![Image](https://i.gyazo.com/d4b4fc63a29565ac6daecc1362a6a3a2.png)

Now add 3 files.
* glitch.json
* main.py
* requirements.txt

![Image](https://i.gyazo.com/5555054f2bacb8a47d4f8a92587e9b93.png)

Why do we need these files? Well...
* We need **glitch.json** for our **main.py** script to run using a proper WSGI Server (Web Server Gateway Interface)
* We also need **main.py** which is where our **REST API** will be running.
* And **requirements.txt** is how we'll be installing our 2 requirements, **Flask** (REST API) and **Gunicorn** (WSGI Server)

### Step 1B: Setting up our JSON file

This file is how we'll tell Glitch to run **main.py** via our WSGI Server, Gunicorn.

So, since it's a **json** file, we'll be using the format of...

```json
{
 "Key": "Value"
}
```

There are only a few key lines you'll want to understand.

The first line consists of:

```json
"install": "pip3 install --user -r requirements.txt"
```

Which will install the libraries that are specified in our **requirements.txt** (Don't worry, we're not there yet; you didn't miss a step).

The next line is:

```json
"start": "PYTHONUNBUFFERED=True gunicorn -w 1 main:app"
```

But what does this mean? This is indicating the command on how to run our **main.py**.

`PYTHONUNBUFFERED=True` means that the output of our **main.py** will be outputted straight to the logs.

`gunicorn -w 1 main:app` indicates that we're running our **main.py**'s **app** which is how we create our REST API through Flask, more about that later on. But what does the `1` mean? It means how many workers will be initiated to "work for our API".

You will change this number depending on how many **cores** you have on your machine. Since we're using **Glitch**, it only has **3** core's. The equation to find out the number is `(2 x numberOfCores) + 1`. Glitch has 3 cores, so `(2 x 3) + 1` is equal to `7`. Though, since we're working with data that can be accessed by all workers, we can use a single worker and it'll do the same thing, except give better results than with 7 workers. Since they all have different data according to each of them, as in they each get their own copy of the script. So everytime there was a POST request sent it'd be 1/7 workers that have that data, so it's the same chance of receiving the same data.

**LINUX**: If you're wondering how to find out how many core's is on your machine: You can use `nproc` in the terminal and it'll output the amount of cores.

![Image](https://i.gyazo.com/b155227e62967c21f5d9125020c2a02f.png)

**WINDOWS**: If you're wondering how to find out how many core's is on your machine: You can press `Ctrl + Shift + Esc` (Opens Task Manager), press **More Details** at the bottom left of the window. Now, at the top you'll see a window called **Performance**; click that and then under the graph you'll see how many core's you have.

![Image](https://i.gyazo.com/63f59fd4afb601bc78a37659e2360442.png)

For me, I have **4 Cores**, so I would have **9 Workers**. `(2 x 4) + 1` is equal to `9`.

The other lines aren't *as* important, but the only 2 other ones that are semi-important is

```json
"install": {
  "include": ["^requirements\\.txt$"]
},
"restart": {
  "include": ["\\.py$", "^start\\.sh"]
},
```

This will specify which file to install `requirements.txt`, and which file to execute when restarting `????.py`

Alright, now the rest aren't really important. After all this, your **glitch.json** should look something like this. (Don't worry, there's extra stuff added here too)

```json
{
  "install": "pip3 install --user -r requirements.txt",
  "start": "PYTHONUNBUFFERED=True gunicorn -w 1 main:app",
  "watch": {
    "ignore": ["\\.pyc$"],
    "install": {
      "include": ["^requirements\\.txt$"]
    },
    "restart": {
      "include": ["\\.py$", "^start\\.sh"]
    },
    "throttle": 1000
  }
}
```

That's our **glitch.json** file done!

### Step 1C: Setting up our Requirements file

There are only 3 libraries we'll need. Those being: `Flask`, `Gunicorn`, `Flask_RESTful`.

So, in your `requirements.txt` file, all you need is those 3 libraries on different lines...

```
Flask
Flask_RESTful
Gunicorn
```

And that's the `requirements.txt` setup! Now's the tricky part, our REST API!

### Step 1D: Setting up our REST API (main.py) file

Let's start off by importing everything we need.

* What we'll need
  * from flask
    * import Flask
    * import request
  * from flask_restful
    * import Api

`flask` and `flask_restful` are the libraries installed from `Flask` from the `requirements.txt` file.

`Flask` and `request` are from `flask` and `Api` is from `flask_restful`.

So, your code should look something like...

```py
from flask import Flask, request
from flask_restful import Api
```

Now we need to setup our app, so we can setup our api.

We can do that by creating a new `app` object from `Flask`.

`app = Flask(__name__)`

Now we have our app, now we need our `api` object from `Api`.

`api = Api(app)`

Now you should have

```py
app = Flask(__name__)
api = Api(app)
```

Now, our API won't be running. We have to call `app.run()` *under everything*.

We'll be using

```py
if __name__ == "__main__":
  app.run(debug=False)
```

So we only run the code if's being directly called and not imported.

So after everything, your code should look like.

```py
from flask import Flask, request
from flask_restful import Api

app = Flask(__name__)
api = Api(app)

if __name__ == "__main__":
  app.run(debug=False)
```

In your `logs` you should see something along the lines of this...

![Image](https://i.gyazo.com/2048a6c266b180259efe41bc8d6998ae.png)

##### Note, if you see this warning in the logs, feel then free to update your pip.

![Image](https://i.gyazo.com/810ae6248b87d45d0f33fa3d553b7173.png)

##### You can do this by opening the terminal and typing in the command: `/usr/bin/python3 -m pip install --upgrade pip` - Exactly like that.

Now that we have our basic layout, we can set up our first route.

We can do that with `@app.route(route, methods)`.

An `endpoint` will have a `/` and then any word after it. So, for example an endpoint can be `/HelloWorld`. With methods, it's an array of valid methods that can be performed on this endpoint.

```py
@app.route("/HelloWorld", methods = ["GET"])
```
This specifies the the endpoint will be the `BASE` url + the `ENDPOINT` url.

To find your base url, at the top left you'll see a button called `SHARE`; click it and a new window will show up. On the new window at the bottom you'll see a link, and next to it, it'll say `Live Site` with a paper clip next to it so you can copy it.

![Image](https://i.gyazo.com/a5030511cfc1e772ba76a93b0296315b.png)

So the live site url will be your base url. Mine for example is, `https://testcommunication.glitch.me`. I then specified my route as `/HelloWorld`. So `BASE` + `Endpoint`, `https://testcommunication.glitch.me/HelloWorld`.

When you do `@app.route()`, you need to define a function right below it. This function will be the function that is executed when a request was sent to the url.

```py
@app.route("/HelloWorld", methods = ["GET"])
def doSomething():
  return {"Server": "Hi"}
```

When you access this URL you will get whatever was returned, in this case `{"Server": "Hi"}`. Do note though, anything that's being returned ***has*** to be JSON Serializable. Meaning, it can be converted from the current type to a JSON string.

Now, your code should look like...

```py
from flask import Flask, request
from flask_restful import Api

app = Flask(__name__)
api = Api(app)

@app.route("/HelloWorld",methods=["GET"])
def doSomething():
  return {"Server": "Hi"}

if __name__ == "__main__":
  app.run(debug=False)
```

`@app.route("/HelloWorld",methods=["GET"])` Is specifying the `endpoint` is the `BASE` URL + `Endpoint`. In this case the endpoint is `/HelloWorld`, so the function underneath will only run when the URL is sent a `GET` request, which is what `methods=["GET"]` specifies. You can add multiple methods or just 1, and you can change that one too.

To test this, you can manually send a `GET` request to the BASE URL + Endpoint, or you can just do it online and test it.

You can use [REQBIN](https://reqbin.com/).

![Image](https://i.gyazo.com/3e955eaf8e1a0dfe8b0ca1615e12ae4b.png)

You can also specify an argument *required* in the URL by doing `<TYPE:NAME>`. So, for example if I wanted a number from the string, I can specify that by doing `<int:num>`. Though, when you specify an argument in the URL and you want to receive it as an argument in the function, you'll receive it as the name you specified it as. So, for example.

```py
@app.route("/HelloWorld/<int:num>",methods=["GET"])
def doSomething(num):
  return {"Server": num * 3}
```

Whatever number was passed through in the URL will be received as an argument in the function. So by doing `https://testcommunication.glitch.me/HelloWorld/3`, the function will receive `3` and that would be `num` since it was specified, and it'd return `3 * 3` since I specified the first argument and set the second argument.

![Image](https://i.gyazo.com/893e6e7fda571f19c803e13a8b6b0259.png)

Now that the basics are explained, we can start learning how to setup the communication stage.

We'll need 2 Methods and 1 Endpoint. 1 Common Endpoint, 1 Method for `GET` requests, and 1 Method for `POST` requests.

We can easily set that up by specifying the endpoint and the methods that are allowed.

Since we imported `request` we can check which method, and get the data they're sending by using `request.method == ""` and `request.get_json()`.

The `GET` request is simple. We just return the `messages` table.

The `POST` request is also pretty simple. We just use the `get_json()` method and for loop through all the keys and add it to the `messages` and index the result of `request.get_json()` with the iterator variable.

```py
messages = {}

@app.route("/Messages", methods = ["GET", "POST"])

def handleRequests():
  if request.method == "GET":
    return {"Server": "HTTP 200: OK", "Data": message}
  elif request.method == "POST":
    words = request.get_json()
    for word in words:
      messages[word] = words[word]
    return {"Server": "HTTP 201: Created"}
```

```py
messages = {}
```
is the dictionary that'll hold all the messages to and fro applications.

```py
@app.route("/Messages", methods = ["GET", "POST"])
```

sets the endpoint as `BASE` url + `/Messages`. This also only allows `GET` and `POST` requests on this endpoint, all the other requests are invalid.

```py
def handleRequests():
```

just defines the function to execute when a `POST` or `GET` request is sent to the endpoint.

```py
if request.method == "GET":
    return {"Server": "HTTP 200: OK", "Data": message}
```

Is what executes if the method was a `GET` request. It'll return a dictionary that says that the request was valid and returns the messages themselves.

```py
elif request.method == "POST":
    words = request.get_json()
    for word in words:
      messages[word] = words[word]
    return {"Server": "HTTP 201: Created"}
```

This code will execute if the method was a `POST` request. It'll get the JSON part of the request, or the body, and loop through it and add it to the `messages` dictionary.

Doing `dictionary[key] = value` adds an element to the dictionary where the key is set to a value. So, in this case `messages[word]` will set the key as `word` and doing `words[word]` will retrieve the word it was originally equal to from `words` and set that as the value to the key.

For example, if I sent a request of...

```json
{
  "Hello": "World"
}
```

`Hello` would be `word` and `World` would be `words[word]`. In the `messages` dictionary it would look like...

```py
messages = {
  "Hello": "World"
}
```

![Image](https://i.gyazo.com/12194cf41eabdcf0e95438586fc8feb4.png)

##### `GET` Request

![Image](https://i.gyazo.com/88dcbaf0d0030776899279c6f53dd06e.png)

##### `POST` Request

![Image](https://i.gyazo.com/5490611f5552aa99c2c905d523d73e51.png)

##### `GET` Request - As you can see, we get the same data!

### Step 1DA: Another way of setting up our Endpoints

The method above (`@app.route("", methods=[])`) works good for setting up endpoints. But, maybe you want to set it up a different way. Well, you can create classes and add those as resources. There will be functions in those classes defining which methods are valid.

Since we're needing resources, we'll have to import them from `flask_restful`.

```py
from flask import Flask, request
from flask_restful import Api, Resource
```

Alright, now we need to setup our class that we'll be adding.

```py
class HelloWorld(Resource):
```

Now we need to define which methods are valid. We can do this by creating functions in these classes with the name of the methods, receiving `self`.

```py
class HelloWorld(Resource):
  def get(self):
```

This indicates a Resource of `HelloWorld` will only accept `GET` requests.

Now we just have to return something, and add it as a resource.

```py
class HelloWorld(Resource):
  def get(self):
    return {"Server": "HTTP 200: OK"}
```

All that's left is to add it as a resource to our api, and define our endpoint where it can be reached.

```py
api.add_resource(HelloWorld, "/HelloWorld")
```

So your code should look like this.

```py
class HelloWorld(Resource):
  def get(self):
    return {"Server": "Class Method"}
    
api.add_resource(HelloWorld, "/HelloWorld")
```

![Image](https://i.gyazo.com/9ced26f06c7b140bca4b5dd0da800251.png)

You can add as many functions/methods that are allowed, and you even add specifiers in the URL if you want to, just like we did above.

```py
class HelloWorld(Resource):
  def get(self, num):
    return {"Server": "Class Method: " + str(num)}

api.add_resource(HelloWorld, "/HelloWorld/<int:num>")
```

![Image](https://i.gyazo.com/7fa4ac982f5b466810cc6a235c04e8d6.png)

Either method works! I'll be doing the `@app.route("", methods = [])` method.

Alright! That's the REST API setup! Let's move onto Roblox HTTP Requests!

## Step 2: Roblox HTTP Requests

Head on over to Roblox Studio. To start off, we're going to need to enable **HTTP Requests**. To do that, on the tabs at the top press **Home**. On the window below it, you'll see a button that says **Game Settings** with a blue gear. Press that and on the side bar press **Security** and make sure **Allow HTTP Requests** is enabled (it should be green).

![Image](https://i.gyazo.com/95e29d6ad237393b904aa1c3eb3cc60d.png)

It should look something like this. After that, go ahead and create a **ModuleScript** located anywhere. I'll be creating mine in **ServerStorage** and naming it **RESTAPI**. I will then also be adding a **ServerScript** in **ServerScriptService** and naming it **Main**.

![Image](https://i.gyazo.com/73ac76dc98e4a2346ae592158a3fdb4d.png)

This is what my **SSS** and **SS** look like (ServerScriptService and ServerStorage).

Alright, let's start off by configuring our **ModuleScript**.

```lua
local api = {}



return api
```

Is what we'll go with for setting up our module. Now, we need to add our **HTTPService** with our **GET** and **POST** requests to our **REST API**.

If you have your link, go ahead and paste it with a **local** variable.

You can also add your **POST** and **GET** request endpoint's if they're different, or just one if it's the same.

```lua
local HTTPS = game:GetService("HttpService")

local BASE = "https://testcommunications.glitch.me"
local ENDPOINT = "/Messages"
```

Alright, now our setup should look something like this.

```lua
local api = {}

local HTTPS = game:GetService("HttpService")

local BASE = "https://testcommunications.glitch.me"
local ENDPOINT = "/Messages"

return api
```

Alright, now we can create our `GET` function and our `POST` function. In our `POST` function we'll only need arguments for stuff to send to our REST API.

```
function api.GET()

end
```

So, let's think about this. We'll be using [RequestAsync()](https://developer.roblox.com/en-us/api-reference/function/HttpService/RequestAsync).

`RequestAsync` takes a dictionary, and gives back a dictionary. We'll also be using `pcall` like you would with any HTTP Request. We'll be handling the error, and the response.

```lua
function api.GET()
	local success, result = pcall(function()
		return HTTPS:RequestAsync({
			Url = BASE..ENDPOINT,
			Method = "GET",
		})
	end)

	if success then
		return result
	else
		local errorMessage = "\nSuccess: "..tostring(result.Success).."\nStatus: "..result.StatusCode.."\nMessage: "..result.StatusMessage.."\n"
		error(errorMessage, 2)
	end
end
```

Alright, let's talk about this. `local success, result`. `Success` is a `boolean`, meaning it's either `true` or `false`. `Result` is either the error message generated in the pcall if `success` is `false`. If `success` is `true`, then `result` will be whatever is returned from the pcall. If nothing is returned, it is then declared `nil`. `Pcall` just stands for `protected-call`, which means error's generated in this function don't error out in the console and doesn't stop the main thread from running.

With the `HTTPS:RequestAsync()` we gave it a dictionary where we specified the `Url`, which is our `BASE` url + `ENDPOINT` url. We then specified the method of `GET` since the function is supposed to be a `GET` request, we set the method as `GET`.

We then checked if it was a successful, if it was then we return the JSON String version of the Body so they have options of what they want to do with it.

If it *wasn't* successful, then we generate an error message that tells us which status code and which message was generated from our API. We then throw an error with `error()` and yield the calling thread. We also have to use `tostring()` on `boolean`'s to concatenate them, or they'd error.

Alright, now for our `POST` function. It's pretty similar to our `GET` function, so I'll only be explaining the new stuff.

```lua
function api.POST(Data)
	local success, result = pcall(function()
		return HTTPS:RequestAsync({
			Url = BASE..ENDPOINT,
			Method = "POST",
			Headers = {
				["Content-Type"] = "application/json"
			},
			Body = HTTPS:JSONEncode(Data)
		})
	end)

	if success then
		return result
	else
		local errorMessage = "\nSuccess: "..tostring(result.Success).."\nStatus: "..result.StatusCode.."\nMessage: "..result.StatusMessage.."\n"
		error(errorMessage, 2)
	end
end
```

Alright, the only new thing here is that we changed the `Method` from `GET` to `POST`, so we can *send* data to our REST API. We also added `Headers` which is needed, because we're going to be sending `JSON`, so we're specifying that the content we're sending is `JSON`. We also then added the `Body` which is what our API will receive as the `request.get_json()`. We're making sure the data we're sending is valid JSON by using `JSONEncode` on the Body received.

Now, that's our **ModuleScript** setup, now we need to have our **ServerScript** do `POST` and `GET` so we can make sure it works.

We can do that by requiring the module and calling the functions.

```lua
local serverStorage = game:GetService("ServerStorage")
local api = require(serverStorage:WaitForChild("RESTAPI"))
```

Make sure you change `RESTAPI` if you change the name of your Module!

Now, we just call our functions!

```lua
print(api.GET())

local dictionary = {
  ["Hello"] = "World"
}

print(api.POST(dictionary))

print(api.GET())
```

![Image](https://i.gyazo.com/1caf12ca14b94abfc42826f1e8c28ccb.png)

As you can see, this method works!

You can go the extra mile and create your own parser for the result, and you have plenty of options of what you want to customize!

But, that's it for Roblox! We'll now need to work on our application in **C#** in *Visual Studio Community*.

## Step 3: C# Application (VSC)



















































































































