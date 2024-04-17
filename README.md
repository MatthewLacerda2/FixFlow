**Technical aspects**

The BackEnd is entirely a ASP.NET Core WebApi which handles HTTP CRUD requests to a MySQL database
Authentication, Authorization, Validation and Rate Limitation middlewares are implemented
We are using Tokens for this one

On the Front-End, we are using a React with Typescript project using Vite

---

**How to run:**

You'll need .NET 8.0 SDK https://dotnet.microsoft.com/en-us/download/dotnet/8.0
And Docker Desktop https://www.docker.com/products/docker-desktop/

Once you have it, download the 'mysql' docker image:

`docker pull mysql`

Then, get the MySQL container online

`docker run -d -p 3306:3306 --name fixflow -e MYSQL_ROOT_PASSWORD=xpvista7810 -e MYSQL_DATABASE=fixflow -e MYSQL_USER=lendacerda -e MYSQL_PASSWORD=xpvista7810 mysql`

At the MySQL terminal, you'll need to Grant \* PRIVILEGES to the 'lendacerda' User

`GRANT ALL PRIVILEGES ON mysql.* TO 'lendacerda'@'%';`
`FLUSH PRIVILEGES;`

Than, get into the Server folder

`cd .\FixFlow.Server`

And run

`dotnet ef migrations add InitialCreate`
`dotnet ef database update`

Now you can run the project with `dotnet watch run`
P.S.: i recommend you do this from a separate 'Terminal' window, as you'll have the BackEnd and FrontEnd both running with Hot-Reload in separate tabs
