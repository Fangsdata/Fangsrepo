# Run the website locally and it will connect to the remote API on the internet:
### In the directory /Fangsrepo/Aflinn/FangsdataWebSite/web-app
### Type in the terminal: 1. npm install 2. npm start

# Run the API locally (password required in appsettings.json):
### In the directory /Fangsrepo/Aflinn/OffloadWebApi
### Type 1. dotnet build 2. dotnet run


# Fangsrepo

## OffloadWebApi

### Boats controller



#### Request

`GET api/boats/radio/{radioSignal}

    curl -i https://fangsdata-api.herokuapp.com/api/boats/radio/LEQS

#### Response
```
HTTP/1.1 200 OK
Connection: keep-alive
Date: Thu, 07 May 2020 11:06:31 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked
Via: 1.1 vegur
```

```json
{
  "id": 0,
  "registrationId": "F 0185NK",
  "radioSignalId": "LEQS",
  "name": "VALDIMAR H",
  "state": "Troms og Finnmark",
  "town": "NORDKAPP",
  "nationality": "NORGE",
  "length": 38,
  "weight": 359,
  "builtYear": 1968,
  "enginePower": 700,
  "fishingGear": "Autoline",
  "image": "https://upload.wikimedia.org/wikipedia/commons/f/f1/Flag_of_Norway.png",
  "mapData": [
    {
      "latitude": 70.45927,
      "longitude": 17.21144,
      "time": null
    }
  ]
}
```

#### Request

`GET api/boats/registration/{rregistrationId}

    curl -i https://fangsdata-api.herokuapp.com/api/boats/registration/F%200185NK

#### Response
```
HTTP/1.1 200 OK
Connection: keep-alive
Date: Thu, 07 May 2020 11:17:27 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked
Via: 1.1 vegur
```

```json
{
  "id": 0,
  "registrationId": "F 0185NK",
  "radioSignalId": "LEQS",
  "name": "VALDIMAR H",
  "state": "Troms og Finnmark",
  "town": "NORDKAPP",
  "nationality": "NORGE",
  "length": 38,
  "weight": 359,
  "builtYear": 1968,
  "enginePower": 700,
  "fishingGear": "Autoline",
  "image": "https://upload.wikimedia.org/wikipedia/commons/f/f1/Flag_of_Norway.png",
  "mapData": [
    {
      "latitude": 70.46013,
      "longitude": 17.21165,
      "time": null
    }
  ]
}
```

### Maps Controller


#### Request

`GET api/maps/boats/radio/{RadioSignal}

    curl -i https://fangsdata-api.herokuapp.com/api/maps/boats/radio/LEQS

#### Response
```
HTTP/1.1 200 OK
Connection: keep-alive
Date: Thu, 07 May 2020 11:17:27 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked
Via: 1.1 vegur
```

```json
[
  {
    "latitude": 70.46013,
    "longitude": 17.21165,
    "time": null
  }
]
```



### Offloads Controller


#### Request

`GET api/Offloads
```
Query Parameters
 - count : number
 - fishingGear : [string, string, ...]
 - boatLength : [number, number] || enum : 'under 11m', '11m - 14,99m', '15m - 20,99m', '21m - 27,99m', '28m og over'
 - landingTown : [string, string, ...]
 - landingState : [string, string, ...]
 - month : [number/string, number/string, ...]
 - year : [string, string, ...]
 - fishType : [string, string, ...]
```

    curl -i https://fangsdata-api.herokuapp.com/api/offloads?fishingGear=Teiner&Count=1

#### Response
```
HTTP/1.1 200 OK
Connection: keep-alive
Date: Thu, 07 May 2020 11:17:27 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked
Via: 1.1 vegur
```

```json

[
    {
        "id": -1,
        "town": "",
        "state": "",
        "landingDate": null,
        "totalWeight": 19077469.631271362,
        "fish": null,
        "boatRegistrationId": "RCK576",
        "boatRadioSignalId": "",
        "boatName": "",
        "boatFishingGear": "Teiner",
        "boatLength": 0,
        "boatLandingTown": "",
        "boatLandingState": "",
        "boatNationality": "",
        "avrage": 0,
        "largestLanding": 0,
        "smallest": 0,
        "trips": 0
    }
]
```



#### Request

`GET api/Offloads/details/{offloadId}

    curl -i https://fangsdata-api.herokuapp.com/api/Offloads/details/7000011127066

#### Response
```
HTTP/1.1 200 OK
Connection: keep-alive
Date: Thu, 07 May 2020 11:17:27 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked
Via: 1.1 vegur
```

```json

{
  "id": "7000011127066",
  "town": "NORDKAPP",
  "state": "Troms og Finnmark",
  "landingDate": "2020-05-04T00:00:00",
  "totalWeight": 143074.3,
  "fish": [
    {
      "id": 202001,
      "type": "Hyse",
      "condition": "Sløyd med hode",
      "preservation": "Fersk/ukonservert",
      "packaging": "Uspesifisert",
      "quality": "Superior",
      "application": "Fersk (konsum)",
      "weight": 91738.08
    },
    {
      "id": 201001,
      "type": "Torsk",
      "condition": "Sløyd uten hode, run",
      "preservation": "Fersk/ukonservert",
      "packaging": "Uspesifisert",
      "quality": "A",
      "application": "Fersk (konsum)",
      "weight": 27756
    },
     ...
  ],
  "boat": {
    "id": -1,
    "registration_id": "F 0185NK",
    "radioSignalId": "LEQS",
    "name": "VALDIMAR H",
    "state": "",
    "nationality": "",
    "town": "",
    "length": 0,
    "fishingGear": "Autoline",
    "image": ""
  },
  "mapData": [
    {
      "latitude": 71.5,
      "longitude": 14,
      "time": null
    }
  ]
}
```

#### Request

`GET api/Offloads/{boatRegistrationId}/{count}/{pageNO}

    curl -i https://fangsdata-api.herokuapp.com/api/Offloads/F%200185NK/1/1

#### Response
```
HTTP/1.1 200 OK
Connection: keep-alive
Date: Thu, 07 May 2020 11:17:27 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked
Via: 1.1 vegur
```

```json

[
  {
    "id": "7000011127066",
    "town": "NORDKAPP",
    "state": "Troms og Finnmark",
    "landingDate": "2020-05-04T00:00:00",
    "totalWeight": 143074.3,
    "fish": null,
    "boat": null,
    "mapData": [
      {
        "latitude": 14,
        "longitude": 71.5,
        "time": null
      }
    ]
  }
]
```


#### Request

`GET api/Offloads/{boatRegistrationId}/{count}

    curl -i https://fangsdata-api.herokuapp.com/api/Offloads/F%200185NK/1

#### Response
```
HTTP/1.1 200 OK
Connection: keep-alive
Date: Thu, 07 May 2020 11:17:27 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked
Via: 1.1 vegur
```

```json

[
  {
    "id": "7000011127066",
    "town": "NORDKAPP",
    "state": "Troms og Finnmark",
    "landingDate": "2020-05-04T00:00:00",
    "totalWeight": 143074.3,
    "fish": null,
    "boat": null,
    "mapData": [
      {
        "latitude": 14,
        "longitude": 71.5,
        "time": null
      }
    ]
  }
]
```

### Search Controller

#### Request

`GET api/Search/boats/{boatSearchTerm}"
```
  Serchterm can be : BoatName, RegistrationId, RadioSignal
```

    curl -i https://fangsdata-api.herokuapp.com/api/Search/boats/F%200185NK/

#### Response
```
HTTP/1.1 200 OK
Connection: keep-alive
Date: Thu, 07 May 2020 11:17:27 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked
Via: 1.1 vegur
```

```json

[
  {
    "id": -1,
    "registration_id": "F 0185NK",
    "radioSignalId": "LEQS",
    "name": "VALDIMAR H",
    "state": "Troms og Finnmark",
    "nationality": "NORGE",
    "town": "NORDKAPP",
    "length": 38,
    "fishingGear": "Autoline",
    "image": "https://upload.wikimedia.org/wikipedia/commons/f/f1/Flag_of_Norway.png"
  }
]
```


#### Request

`GET api/Search/boats/{boatSearchTerm}/{count}/{pageNo}"
```
  Serchterm can be : BoatName, RegistrationId, RadioSignal
```

    curl -i https://fangsdata-api.herokuapp.com/api/Search/boats/F%200185NK/1/1

#### Response
```
HTTP/1.1 200 OK
Connection: keep-alive
Date: Thu, 07 May 2020 11:17:27 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Transfer-Encoding: chunked
Via: 1.1 vegur
```

```json

[
  {
    "id": -1,
    "registration_id": "F 0185NK",
    "radioSignalId": "LEQS",
    "name": "VALDIMAR H",
    "state": "Troms og Finnmark",
    "nationality": "NORGE",
    "town": "NORDKAPP",
    "length": 38,
    "fishingGear": "Autoline",
    "image": "https://upload.wikimedia.org/wikipedia/commons/f/f1/Flag_of_Norway.png"
  }
]
```

