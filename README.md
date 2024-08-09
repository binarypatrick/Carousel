Carousel is a simple app that stands up a webpage that rotates through multiple images. The intent is to host locally and provide a single point where photo frames or kiosks can point to to load from.

To stand up the service you'll need to configure the `.env` file. Copy the example and rename it, and specify the photo location as `PHOTOS_DIRECTORY`.

Once this is configured, run the following:

```bash
docker compose up -d --build
```

This will build the dockerfile and stand up the webapp. You should be able to access it in any web browser at `http://localhost:8080`

Photos are rotated through randomly, and the `date taken` EXIF field is used to display the datestamp.