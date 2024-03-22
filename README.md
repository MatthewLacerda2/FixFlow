You can run this by running

`docker-compose up`

But once it's up, you'll need to run the Initial Migration

`dotnet ef database migration`

The port for the website is 5173 and for the webapi is 5105. You can look at the API documentation at localhost:5105/swagger

Have fun