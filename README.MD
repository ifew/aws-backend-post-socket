# Example post mesasage with SigV4 from backend to WebSocket

## Configuration
1. edit appsettings.json and appsettings.Development.json
2. edit json config
```
  "AWS": {
    "Key": "[AWS KEY]",
    "Secret": "[AWS SECRET]",
    "Region": "[AWS REGION]",
    "urlSocketAPI": "https://[SOCKET API GATEWAY ID].execute-api.[AWS REGION].amazonaws.com/[SOCKET API GATEWAY STAGE]/@connections/",
    "urlAPIGw": "https://[SOCKET API GATEWAY ID].execute-api.[AWS REGION].amazonaws.com/dev/hello?name=iFew",
    "Service": "execute-api"
  }
```

## Basic get content from APi Gateway (for test SigV4)
```
GET http://localhost:5001/api/basic
```

## Post content from APi Gateway (for test SigV4)
```
POST http://localhost:5001/api/socket
```

Json Body
```
{
	"connection_id": "XSPlWeCLSQ0AbFw=",
	"message": "ทดสอบ"
}
```