How to test AirSnitch.Worker locally?

AirSnitch has a dependecy for AWS SQS service.For local developemnt you coudl use a LocalStack

1.Install LocalStack
2.Install AWS cli
3. Create a target queue in LocalStack SQS:

   aws --endpoint http://localhost:4566 sqs create-queue --queue-name SensorsMeasurements

4. Make sure that queue is created successfully:
   aws --endpoint http://localhost:4566 sqs list-queues

5. Send Message to queue:

   aws --endpoint http://localhost:4566 sqs send-message --queue-url http://localhost:4566/00000000000/SensorsMeasurements --message-body "{\"station\":{\"id\":\"SAVEDNIPRO_136\",\"name\":\"SAVEDNIPRO_136\",\"cityName\":\"Dnipro\",\"countryCode\":\"UA\",\"address\":\"Forest Park of Friendship of Peoples\",\"geoCoordinates\":{\"long\":35.092615,\"lat\":48.5403}},\"measurements\":[{\"value\":27,\"name\":\"PM10\"},{\"value\":14.68,\"name\":\"PM25\"},{\"value\":73.19,\"name\":\"Humidity\"},{\"value\":16.3,\"name\":\"Temperature\"}],\"dateTime\":\"2022-10-29T13:00:00\",\"index\":{\"value\":69,\"type\":\"US_AIQ\"}}"

A list of test stations:

```json

{
  "station": {
    "id": "SAVEDNIPRO_136",
    "name": "SAVEDNIPRO_136",
    "cityName": "Dnipro",
    "countryCode": "UA",
    "address": "Forest Park of Friendship of Peoples",
    "geoCoordinates": { "long": 35.092615, "lat": 48.5403 }
  },
  "measurements": [
    { "value": 27, "name": "PM10" },
    { "value": 14.68, "name": "PM25" },
    { "value": 73.19, "name": "Humidity" },
    { "value": 16.3, "name": "Temperature" }
  ],
  "dateTime": "2022-10-29T13:00:00",
  "index": { "value": 69, "type": "US_AIQ" }
}
```
```json
{
  "station": {
    "id": "SAVEDNIPRO_1275",
    "name": "SAVEDNIPRO_1275",
    "cityName": "Kam\u0027ianske",
    "countryCode": "UA",
    "address": "prospekt Konstytutsii 19",
    "geoCoordinates": { "long": 34.667161, "lat": 48.475955 }
  },
  "measurements": [
    { "value": 17.48, "name": "PM10" },
    { "value": 12.6, "name": "PM25" },
    { "value": 100, "name": "Humidity" },
    { "value": 18.89, "name": "Temperature" }
  ],
  "dateTime": "2022-10-29T13:00:00",
  "index": { "value": 49, "type": "US_AIQ" }
}
```

```json
{
  "station": {
    "id": "SAVEDNIPRO_1581",
    "name": "SAVEDNIPRO_1581",
    "cityName": "Khmilnyk",
    "countryCode": "UA",
    "address": "vulytsia Stoliarchuka, 10",
    "geoCoordinates": { "long": 27.95694, "lat": 49.558073 }
  },
  "measurements": [
    { "value": 15.9, "name": "PM10" },
    { "value": 6.12, "name": "PM25" },
    { "value": 75.73, "name": "Humidity" },
    { "value": 16.65, "name": "Temperature" }
  ],
  "dateTime": "2022-10-29T13:00:00",
  "index": { "value": 43, "type": "US_AIQ" }
}
```

```json
{
  "station": {
    "id": "SAVEDNIPRO_1583",
    "name": "SAVEDNIPRO_1583",
    "cityName": "Sambir",
    "countryCode": "UA",
    "address": "ploshcha Rynok, 1",
    "geoCoordinates": { "long": 23.196529, "lat": 49.518117 }
  },
  "measurements": [
    { "value": 2.12, "name": "PM10" },
    { "value": 1.92, "name": "PM25" },
    { "value": 100, "name": "Humidity" },
    { "value": 27.69, "name": "Temperature" }
  ],
  "dateTime": "2022-10-29T13:00:00",
  "index": { "value": 13, "type": "US_AIQ" }
}
```

To purge queue use:

aws --endpoint http://localhost:4566 sqs purge-queue --queue-url http://localhost:4566/00000000000/SensorsMeasurements