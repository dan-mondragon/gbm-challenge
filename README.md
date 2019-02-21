# gbm-challenge
Challenge for gbm

## Prerequirements
* Visual Studio 2017
* .NET Core SDK
* SQL Server

## How To Run
* Open solution in Visual Studio 2017
* Set Challenge.Api Web Api project as Startup Project and build the project.
* Open the "Package Manager Console", selecting "Challenge.Data" as default project and run: "update-database" command to generate database and seeders.
* Run the application.

### API Rest

* Enter the following URL in your Browser: http://localhost:55891/swagger. (When Run in Visual Studio, the browser opens up automatically).
* Here you can see all the service endpoints documented and ready to be comsumed and the authentication option.
* You can try the services here or using another tool like POSTMAN.


### Unit Testing
* When you build the test project, the tests appear in Test Explorer. If Test Explorer is not visible, choose Test on the Visual Studio menu, choose Windows, and then choose Test Explorer.

## Built With  
  
*  [Web API](https://docs.microsoft.com/en-us/aspnet/core/web-api/?view=aspnetcore-2.1) - Framework used to make Web API
*  [NuGet](https://www.nuget.org/) - Dependency Management  
*  [Swagger](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) - Used to generate API Documentation

