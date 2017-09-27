# Hidden Messages
Project for UW Software Development Club Fall 2017 hackathon: a twilio service to discover hidden messages based on geolocation.

## What it does

Hidden Messages reveals messages hidden around a geo location.

## How to use Hidden Messages:
### To find hidden messages
Text `find: <address or location name>` to (424) 377-7497

For example, try `find: union south`
### To submit a message
Text `submit: <your message> : <address or location name>` to (424) 377-7497

For example, try `submit: hello world : union south`

## Technologies used

C#, Twilio API, Google Geocoding API, and an Amazon EC2 server.

## To build it yourself

```
cd TwilioSDCProject
nuget restore
msbuild /property:Configuration=Release TwilioSDCProject.csproj
```

This will have the server running on `http://localhost:8080` but unless you have your 8080 port open, the server will not be able to recieve any incoming messages from Twilio, it will however, be able to send messages.  So, if you don't want to expose your ports to the world, you can use cURL or Postman to send 'fake' messages to the server that are formatted like a Twilio POST request.

Here is an example cURL command that will emulate a Twilio request:

```
curl -X POST \
  http://localhost:8080/smslistener \
  -H 'cache-control: no-cache' \
  -H 'content-type: application/x-www-form-urlencoded' \
  -d 'From=4144912551&Body=some%20body%20message%20here&AccountSid=AC1265576a3cee850ecfea3fb83b8ec134'
```
