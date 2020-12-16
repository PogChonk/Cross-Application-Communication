# Cross-Application Communication

###### Ever wondered how to send a message from a 3rd party application to Roblox, like a *Remote Connection*? Well look no farther! This Github will explain all 3 steps of how to achieve this!

#

## Requirements

###### Before we continue, there are some prerequisites.
* VS(C) - Visual Studio (Community) (IDE where you can create a plentiful amount of different types of applications) [VSC Download](https://visualstudio.microsoft.com/downloads/)
  * IDE where we'll be creating our application.
* Python - Preferrably the latest version (Python3.9) [Python3 Download](https://www.python.org/downloads/)
  * Used for our REST API which is how we'll be communicating between applications.
  
And then of course, we'll need Roblox Studio if you don't have that downloaded already.

#

## Step 1: Understanding our REST API

##### What is REST API?

REST API (sometimes referred to as RESTful API) is a form of architectural style that sites use for communication between another OS (Operating System) or another Application.

![RESTful Web Services Architecture](https://miro.medium.com/max/500/1*EbBD6IXvf3o-YegUvRB_IA.jpeg)

##### How are we going to use this?

Simple! We send a request from our Application to our REST API hosted on a website, possibly on [Glitch.com](https://glitch.com/), and then the message we sent will be manipulated or parsed and stored as data in a table of some sorts. Now, at that point there is data stored in the API, but how are we going to access it? Roblox has **HTTPService** which has the **HTTPS:RequestAsync()** method that allows Http Requests to be made. REST API provide the methods of **GET**, **POST**, and **DELETE**. Though, it provides more, these are the only ones that we'll be using here. Feel free to do some more research and expand your knowledge!

## Step 2: Getting Flask and Gunicorn

By this point, Python3 would be installed. Now, there's a REST API library in Python called **Flask API**. Now, we'll need 2 libraries in total. One being **Flask** the REST API library we're going to be using. The other one being **Gunicorn**.

Since we're going to be hosting this REST API on Glitch, create an account if you don't have one, and create a new project.

![Location](https://i.gyazo.com/1769004f52cb5e73e4223722ae19fa92.png)

Start off by deleting everything in your environment.

You will need a file called **requirements.txt**. In there will be all the libraries to be installed if they're not already. You will only need **Flask** and **Gunicorn**.

```
Flask
Gunicorn
```

You're **requirements.txt** should look like this. This is just a simple text file that stores the names of the library and will be installed whenever our **glitch.json** file is ran. It will install it by running: **pip3 install --user -r requirements.txt**. You don't need to do this since the **glitch.json** file will do it automatically.

Now, Glitch allows Python to be run. But, we need to setup our **glitch.json** that indicates how to start our **main.py**. Add another file and name it **glitch.json**. This is how Glitch knows what to run and how to start our **main.py**.

```json
{
  "install": "pip3 install --user -r requirements.txt",
  "start": "PYTHONUNBUFFERED=true gunicorn -w 7 main:app",
  "watch": {
    "ignore": [
      "\\.pyc$"
    ],
    "install": {
      "include": [
        "^requirements\\.txt$"
      ]
    },
    "restart": {
      "include": [
        "\\.py$",
        "^start\\.sh"
      ]
    },
    "throttle": 1000
  }
}
```

The first line is what installs the libraries from the **requirements.txt** file. The second line is what runs **Gunicorn** and sets up our **WSGI**. It's recommended to run as many works as there are cores with 1 extra worker. So, the equation would be `(2 x numberOfCores) + 1`. In this case, **Glitch** has **3** Cores, so it'd be `(2 x 3) + 1` which is **7**.

##### If you're running this on your computer, and you're on Windows and you don't know how many cores you have: Ctrl + Shift + Esc (Opens Task Manager), Click **More Details** at the bottom left of the window. Then head on over to performance and under the graph you'll see how many cores you have.

![Cores](https://i.gyazo.com/f43f71bae1393a1976da7e39db8f8df7.png)

##### If you're running this on your computer, and you're on Linux and you don't know how many cores you have, open a terminal and type in the command: `nproc`. It'll output the number of cores your machine has.

![Linux Cores](https://i.gyazo.com/c3791a44834d4181e37398878f4090b9.png)

##### Flask alone isn't meant to be used for production. When you run Flask it'll warn you to use a proper WSGI (Web Server Gateway Interface). A pretty common one is being Gunicorn as mentioned above.

## Step 3: Understanding Flask and Gunicorn

##### What is Flask?

[Flask](https://www.flaskapi.org/) is a Micro Web Framework library for Python. Why is a *Micro* Web Framework? Well, because it does not require particular tools or libraries. It also has no database abstraction layer, form validation, or any other components where pre-existing third-party libraries provide common functions.

##### How do I setup flask?

Use the `from` and `import` keywords in python to import Flask.

```py
from flask import Flask, request
from flask_restful import Api, Resource
```

We'll need `Flask`, `Api`, `Resource`, and `Request` from `Flask` and `Flask_RESTful`. These are installed automatically from our **requirements.txt**.

Now, with Flask we setup an app by simply doing `app = Flask(__name__)`, and then we also need the API from that App, we can use what we imported: `api = Api(app)`.

```py
app = Flask(__name__)
api = Api(app)
```

Now, we want to run our application, we can do that by using `app.run(debug=False)`. Using `debug=False` indicates that we're not attempting to debug the app, so no extra output will be given. Though, if your application isn't working and there's errors and you want to debug it, simply change `False` to `True`: `debug=True`.

```py
if __name__ == "__main__":
    app.run(debug=False)
```

The app is going to execute some code only if the file was run directly, and not imported.

Now that we have it all setup your environment should look like this:

![Environment](https://i.gyazo.com/36f2ae75c27978e62eb824c56112aa6f.png)

And your **main.py** should look like this:

```py
from flask import Flask, request
from flask_restful import Api, Resource

app = Flask(__name__)
api = Api(app)

if __name__ == "__main__":
    app.run(debug=False)
```

## Step 4: Setting up our methods and resources for Flask

So, to setup our endpoints, we'll need classes and add that as a resource to our api with defining a link endpoint.

So, we define a class, and receive the resource that it was executed on.

##### Note: These should be under `api = Api(app)` and above `if __name == "__main__":`

```py
class HelloWorld(Resource):
```

Now, for the methods we use `functions`. These functions can be a range from `get`, `post`, `put`, `delete`, and `patch`. There *are* other methods, but these are the most common used HTTP methods.

So, if we're setting up a `get` request for our class `HelloWorld`, we'll do something along the lines of...

```py
class HelloWorld(Resource):
  def get(self):
    print("A GET Request was made!")
```

Now, for it to *actually* take effect, we need to add it as a resource to our API.

We can do that by doing...

```py
api.add_resource(HelloWorld, "/EndpointName")
```

The arguments for `add_resource` is the name of the class, and then the endpoint that it will be called on. Accessing `/EndpointName` from the base link, which would be the live site's link. So, if you send a `GET` request to `https://yourProjectName.glitch.me/EndpointName` it would print `A GET Request was made!` in the console.

###### To find the link to your `live site`, in your project at the top left you'll see `Share`, click it and it'll show a window. At the bottom of the window you'll see it says `live site`, and there's a paper clip to copy the link, click that and then that's the base link.

![Share](https://i.gyazo.com/31d8e07f5d2c74abb05acb6ecda2714c.png)
![Live Site](https://i.gyazo.com/57a2493ce05706e04460408ccfcba8ee.png)

So now your code should look like this:

```py
from flask import Flask, request
from flask_restful import Api, Resource

app = Flask(__name__)
api = Api(app)

class HelloWorld(Resource):
  def get(self):
    print("A GET Request was made!")

api.add_resource(HelloWorld, "/EndpointName")

if __name__ == "__main__":
    app.run(debug=False)
```

Now, for this example we're going to be sending "commands" to the API. So, we can set it up with a table called "commands".

```py
commands = {}
```

And then we'll need a `POST`, `GET`, and `DELETE` request methods, so we can add commands, get the list of commands, and return

We can then setup classes.

```py
class PostCommand(Resource):


class GetCommands(Resource):


class DeleteCommands(Resource):


```

Now, we need to add our methods corresponding to the classes.

```py
class PostCommand(Resource):
  def post(self):
    

class GetCommands(Resource):
  def get(self):
    

class DeleteCommands(Resource):
  def delete(self):
    

```

With the `GET` request is easy, we just send back the commands dictionary.

```py
class GetCommands(Resource):
  def get(self):
    return {"Server": "HTTP 200: OK", "Data": commands}
```

This returns a dictionary that indicates that the request was successful with `HTTP 201: OK`, and then we also send the commands dictionary itself.

![HTTP Codes](https://i.gyazo.com/c817090782c50efd298dfa171100cef9.png)

We'll be using `200: OK` and `201: Created`. `200: OK` for `DELETE` and `GET`, and `201: Created` for `POST`.

For the `DELETE` request, it's also pretty simple. We just use the `request` object and use `.get_json()` and for loop and delete any keys in the commands that match.

```py
class DeleteCommands(Resource):
  def delete(self):
    for c in request.get_json():
      del commands[c]
    return {"Server": "HTTP 200: OK"}
```

`for c in request.get_json():` is a for loop where `c` is the key. We then use the `del` keyword to remove the key from the commands dictionary by doing `del commands[c]`.

It's also pretty similar to the `POST`, except for deleting, we're adding. So, we'd just do `commands[c] = request.get_json()[c]`

```py
class PostCommand(Resource):
  def post(self):
    for c in request.get_json():
      commands[c] = request.get_json()[c]
    return {"Server": "HTTP 201: Created"}
```

This does the same thing, except it adds a key to the dictionary and sets the value as what it was sent as.



## Step 5: Understanding on how we want to communicate

So, let's think about this. We have an application. It sends an HTTP Request to our REST API. Our REST API then parses the link for the message inside of it.











