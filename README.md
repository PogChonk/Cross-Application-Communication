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
"start": "PYTHONUNBUFFERED=True gunicorn -w 7 main:app"
```

But what does this mean? This is indicating the command on how to run our **main.py**.

`PYTHONUNBUFFERED=True` means that the output of our **main.py** will be outputted straight to the logs.

`gunicorn -w 7 main:app` indicates that we're running our **main.py**'s **app** which is how we create our REST API through Flask, more about that later on. But what does the `7` mean? It means how many workers will be initiated to "work for our API".

You will change this number depending on how many **cores** you have on your machine. Since we're using **Glitch**, it only has **3** core's. The equation to find out the number is `(2 x numberOfCores) + 1`. Glitch has 3 cores, so `(2 x 3) + 1` is equal to `7`.

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
  "start": "",
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

There are only 2 libraries we'll need. Those being: `Flask` and `Gunicorn`.

So, in your `requirements.txt` file, all you need is those 2 libraries on different lines...

```
Flask
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

`Flask` and `request` are from `flask` and `Api` is from `flask_restful` which came with `Flask`.

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
















