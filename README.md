# DanskeBank API

DanskeBank Rest API Assignment using .Net Core 5.0

![Azure deployment](https://github.com/sonderangebot10/DanskeBank-Assignment/actions/workflows/main_danskebank-assignment.yml/badge.svg) - [LINK TO HOSTED WEB APP](https://danskebank-assignment.azurewebsites.net/)

*NOTE: Cold start might take some time to initialize for the initial request.*

## Table of Contents

- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [cURLs](#cURLs)
- [Remarks](#remarks)

### Prerequisites

1. Required:
    * [.NET Core 5.0](https://dotnet.microsoft.com/download/dotnet-core/5.0) - Hosting boundle runtime for hosting the backend service
	* [Docker](https://www.docker.com/) - Docker is a software platform that simplifies the process of building, running, managing and distributing applications
    
2. Optional:
    * [IIS Express](https://docs.microsoft.com/en-us/iis/extensions/introduction-to-iis-express/iis-express-overview) - Lightweight, self-contained version of IIS optimized for developers
    * Development IDE
        * [Visual Studio](https://visualstudio.microsoft.com/downloads)
        * [Visual Studio Code](https://code.visualstudio.com/)

## Installation 

To run the solution locally, simply have Docker running on your computer, then navigate to the root directory and run the following command in your command prompt:

`docker build -t danskeassignment .`

After the container build is done, run the following command to start the server:

`docker run -d -p 8080:80 --name danskeapp danskeassignment`

To access the API navigate to http://localhost/
    
## cURLs

*Create a new company*

`curl -X POST "https://danskebank-assignment.azurewebsites.net/api/Companies" -H  "accept: application/json" -H  "Content-Type: application/json" -d "{\"name\":\"string\",\"country\":\"string\",\"phoneNumber\":\"string\"}"`

*Get a list of all companies*

`curl -X GET "https://danskebank-assignment.azurewebsites.net/api/Companies/GetAll" -H  "accept: application/json"`

*Get details about a company*

`curl -X GET "https://danskebank-assignment.azurewebsites.net/api/Companies/{companyId}" -H  "accept: application/json"`

*Update a company*

`curl -X PATCH "https://danskebank-assignment.azurewebsites.net/api/Companies/{companyId}" -H  "accept: application/json" -H  "Content-Type: application/json" -d "{\"name\":\"string\",\"country\":\"string\",\"phoneNumber\":\"string\"}"`

*Add an owner of the company*

`curl -X POST "https://danskebank-assignment.azurewebsites.net/api/Companies/{companyId}/AddOwner" -H  "accept: application/json" -H  "Content-Type: application/json" -d "{\"name\":\"string\",\"ssn\":\"string\"}"`

*Check of social security number*

`curl -X GET "https://danskebank-assignment.azurewebsites.net/api/Ssn/123-45-6789/Validate" -H  "accept: application/json" -H  "Authorization: admin"`

## Remarks 

What I would do if there was more time and resources for further development of this solution:
- more tests;
- more detailed error handling;
- better CI/CD: for example, tests integration, etc.;
- implement a real ssn validator;
- implementation of authentication and authorization: I think in this and most cases the most advanced and versatile solution is to use OpenID protocol. It is a very widely used framework, so in terms of security, features and performance it is one of the best solutions. It would allow to fulfill the concept of access groups and would be simple to implement;

Service architecture is loosely based on [Manga clean architecture template](https://github.com/ivanpaulovich/clean-architecture-manga)
