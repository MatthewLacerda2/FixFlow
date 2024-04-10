**Technical aspects**

Everything is CRUDed via an ASP.NET Core JSON WebApi. The webapp uses Token Authentication and Authorization
The Users database is in MySQL, and the Appointments are saved in MongoDB

On the Front-End, we are using a React with Typescript project. Everything there is done via http requests. Glad to be using a Token on this

---

**How to run:**

You'll need .NET 8.0 SDK https://dotnet.microsoft.com/en-us/download/dotnet/8.0
And Docker Desktop https://www.docker.com/products/docker-desktop/

Once you have it, download the 'mysql' docker image:

`docker pull mysql`

Then, get the MySQL container online

`docker run -d -p 3306:3306 --name mysql_container -e MYSQL_ROOT_PASSWORD=xpvista7810 -e MYSQL_DATABASE=fixflow -e MYSQL_USER=lendacerda -e MYSQL_PASSWORD=xpvista7810 mysql`

And the MongoDB

`docker run --name mongo_db -d -p 27017:27017 mongo`

Than, get into the Server folder

`cd .\FixFlow.Server`

And run

`dotnet ef migrations add InitialCreate`
`dotnet ef database update`

Now you can run the project with `dotnet watch run`
P.S.: i recommend you do this from a separate 'Terminal' window, as you'll have the BackEnd and FrontEnd both running with Hot-Reload in separate tabs
