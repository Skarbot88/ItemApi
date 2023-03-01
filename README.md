# Simple webAPI for .NET core 

## Docker Image Pull

Public image is hosted on docker hub
```
docker run -it --rm -p 8080:5068 -e MongoDbSettings:Host=mongo -e MongoDbSettings:Password={inserPassword} --network=net5tutorial sundharkaru/itemapi:v1
```

This should pull down the public image from docker hub.


