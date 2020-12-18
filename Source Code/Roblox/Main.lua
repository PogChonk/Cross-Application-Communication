--// ServerScript in ServerScriptService with HTTP Requests enabled

local serverStorage = game:GetService("ServerStorage")
local api = require(serverStorage:WaitForChild("RESTAPI"))

print(api.GET())
print(api.POST("Anything"))
