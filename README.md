# MediatrSamples
Samples from my recent presentation on vertical slice architecture and Mediatr

## SuperSimple

Basic demonstration of the capabilities of Mediatr including Requests, Notifications, and Behaviors

## SuperFake.NoMediatr

Reference application built n-tier style that I use for comparison purposes. Be sure to run 
`Update-Database` or the CLI equivalent before running the sample

## SuperFake.Mediatr

Reference application is refactored to take advantage of Mediatr by streamlining the domain logic and moving the queries to a web specific home.  Again, Be sure to run `Update-Database` or the CLI equivalent before running the sample

## SuperFake.MediatorSlices

Reference application is refactored even further to completely separate the domains (a la microservices).  This sample incorporates Mediatr Notifications to sync data updates across the domain datastores allowing them to work completely independently.  While it does not have the independent deployment model of a typical microservice architecture it does have the a distinct messaging layer and none of that pesky eventual consistency ux complication.  Be sure to run the following Powershell commands in VS or the CLI equivalent before the sample

- `Update-Database -Context SuperFake.Customers.Data.SuperFakeCustomersDbContext`
- `Update-Database -Context SuperFake.Products.Data.SuperFakeProductsDbContext`
- `Update-Database -Context SuperFake.Orders.Data.SuperFakeOrdersDbContext`
- `Update-Database -Context SuperFake.Web.Data.SuperFakeWebDbContext`
