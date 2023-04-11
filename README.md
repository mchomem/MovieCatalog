# MovieCatalog

MovieCatalog is an application using asp.net mvc core, based on the tutorial [Asp.net mvc core](https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-7.0&tabs=visual-studio).
Some refinements, such as separating the classes of services and repositories.
An application just to test this technology.

## Getting Started: How to use

Open the project using `Visual Studio 2022`, in the `appsettings.json` file, change the database connection string in the `MovieCatalogContext` key for your database connection, after selecting the project and using the `Package Manager Console` run the `update-database` command to build the database.
Build the application using `Visual Studio 2022`. After successful construction, the binaries can be published to an application server such as IIS, or alternatively just run the application in Visual Studio 2022 itself;
Ask your system administrator to provide the URL to access the MovieCatalog application.

Note: when the application is run for the first time, a few records load will be applied to the database.

## Development Team

-   [Misael C. Homem](https://www.linkedin.com/in/misael-da-costa-homem-8b07a158/) (Developer, Analyst, Architect)

## Features

-   Data maintenance for film catalog;
-   Dashboard to display some information about movies;

## Technologies Used

-   C#. net core
-   ASP.NET core mvc
-   Entity framework core

## How to Contribute

1.  Fork the project
2.  Clone the fork to your local machine
3.  Create a branch for your changes: `git checkout -b my-branch`
4.  Make changes and commit: `git commit -am "My changes"`
5.  Push your branch to your fork: `git push origin my-branch`
6.  Open a Pull Request in the original repository
7.  Wait for your Pull Request to be reviewed and merged
