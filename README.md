**What the fuck is this?**

This is an app focused on Customer retention. The app keeps track of your customer's last appointment, and reminds you to contact them at a later date to schedule a new one, plus it has all the quality-of-life you might expect to come along with it like a Calendar, Client listing, and rules for creating Schedules and Appointments

**Technical aspects**

BackEnd: ASP.NET Core WebApi, which handles HTTP CRUD requests to a PostGre DB
            Authentication, Authorization, Validation and Rate Limitation middlewares are implemented
            We are using Tokens for this one

Front-End: Flutter

---

**How to run:**

You'll need .NET 8.0 SDK https://dotnet.microsoft.com/en-us/download/dotnet/8.0
Flutter SDK https://docs.flutter.dev/get-started/install/windows/mobile#install-the-flutter-sdk
And a PostGre DB. I suggest a dockerized one

`docker pull postgres:latest`
`docker run --name fixflow-postgres -e POSTGRES_DB=fixflow -e POSTGRES_USER=lendacerda -e POSTGRES_PASSWORD=xpvista7810 -p 5432:5432 -d postgres`

Than, from .\FixFlow.Server, run:
`dotnet restore` to load the packages
`dotnet build`
`dotnet ef migrations add InitialMigration`
`dotnet ef database update` to create the database
`dotnet watch run` to run the project ("watch" turns on 'hot reload')

From the .\client_sdk, run `dart pub get` to get it's dependencies

And from .\flow_app, run:
`flutter pub get`
`flutter run`
