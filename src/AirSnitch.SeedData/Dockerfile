FROM mongo

COPY apiUser.json /apiUser.json

ARG MongoDbConnectionString
ARG MongoDbName

CMD mongoimport -h mongo:27017 -u root -p "example" -d AirQ -c apiUser --drop --file apiUser.json
