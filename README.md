# Yal.BookStore

## About this solution

This is a layered startup solution based on [Domain Driven Design (DDD)](https://abp.io/docs/latest/framework/architecture/domain-driven-design) practises. All the fundamental ABP modules are already installed. 

### Pre-requirements

* [.NET 9.0+ SDK](https://dotnet.microsoft.com/download/dotnet)
* [Node v20.11+](https://nodejs.org/en)

### Configurations

The solution comes with a default configuration that works out of the box. However, you may consider to change the following configuration before running your solution:

* Check the `ConnectionStrings` in `appsettings.json` files under the `Yal.BookStore.Web` and `Yal.BookStore.DbMigrator` projects and change it if you need.

### Before running the application

#### Generating a Signing Certificate

In the production environment, you need to use a production signing certificate. ABP Framework sets up signing and encryption certificates in your application and expects an `openiddict.pfx` file in your application.

This certificate is already generated by ABP CLI, so most of the time you don't need to generate it yourself. However, if you need to generate a certificate, you can use the following command:

```bash
dotnet dev-certs https -v -ep openiddict.pfx -p 5a91a259-e2e9-45b6-826f-b21b3ebc5aa4
```

> `5a91a259-e2e9-45b6-826f-b21b3ebc5aa4` is the password of the certificate, you can change it to any password you want.

It is recommended to use **two** RSA certificates, distinct from the certificate(s) used for HTTPS: one for encryption, one for signing.

For more information, please refer to: https://documentation.openiddict.com/configuration/encryption-and-signing-credentials.html#registering-a-certificate-recommended-for-production-ready-scenarios

> Also, see the [Configuring OpenIddict](https://abp.io/docs/latest/deployment/configuring-openiddict#production-environment) documentation for more information.

#### Install Client-Side Libraries

Run the following command in the directory of your final application:

```bash
abp install-libs
```

> This command installs all NPM packages for MVC/Razor Pages and Blazor Server UIs and this command is already run by the ABP CLI, so most of the time you don't need to run this command manually.

#### Create the Database

Run `Yal.BookStore.DbMigrator` to create the initial database. This should be done in the first run. It is also needed if a new database migration is added to the solution later.

### Solution structure

This is a layered monolith application that consists of the following applications:

* `Yal.BookStore.DbMigrator`: A console application which applies the migrations and also seeds the initial data. It is useful on development as well as on production environment.
* `Yal.BookStore.Web`: ASP.NET Core MVC / Razor Pages application that is the essential web application of the solution.

### Deploying the application

Deploying an ABP application is not different than deploying any .NET or ASP.NET Core application. However, there are some topics that you should care about when you are deploying your applications. You can check ABP's [Deployment documentation](https://abp.io/docs/latest/deployment) before deploying your application.

### Additional resources

You can see the following resources to learn more about your solution and the ABP Framework:

* [Web Application Development Tutorial](https://abp.io/docs/latest/tutorials/book-store/part-01?UI=Blazor&DB=EF)
* [Application Startup Template Structure](https://abp.io/docs/latest/solution-templates/layered-web-application)
* [LeptonX Lite MVC UI](https://abp.io/docs/latest/ui-themes/lepton-x-lite/asp-net-core)
