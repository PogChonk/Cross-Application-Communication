--// ModuleScript in ServerStorage

local api = {}

local HTTPS = game:GetService("HttpService")

local BASE = "https://testcommunications.glitch.me"
local ENDPOINT = "/Messages"

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

function api.POST(data)
	local success, result = pcall(function()
		return HTTPS:RequestAsync({
			Url = BASE..ENDPOINT,
			Method = "POST",
			Headers = {
				["Content-Type"] = "application/json"
			},
			Body = HTTPS:JSONEncode(data)
		})
	end)

	if success then
		return result
	else
		local errorMessage = "\nSuccess: "..tostring(result.Success).."\nStatus: "..result.StatusCode.."\nMessage: "..result.StatusMessage.."\n"
		error(errorMessage, 2)
	end
end

return api
