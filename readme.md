## What's Bizpack ?

A mini-ERP application based on the following stack:

* .NET Core - for building API and client facing, server-rendered web-pages.
* Angular - for building Administrator interface.
* NativeScript - for building cross-platform native mobile application.


## Why Bizpack ?

Bizpack is an endeavour to provide minimal IT backbone required to test a business model. It's typically targeted towards startups so that they can quickly test out their ideas. 

Any IT enabled business, at the very least, requires:

* A web-application to serve API request or server-rendered web pages
* An admin application for system administration tasks, like user-management, product-management etc.
* A mobile application targetting ios/android to reach customers.

Bizpack tries to encompass all of those components in a single coupled project and that too while using scalable and state of the art technologies. Below is description on why some specific technologies were used to make various project components.


### Why .NET Core ?

.NET Core is eight times faster than Node.js, see this [report](https://raygun.com/blog/dotnet-vs-nodejs/). That means you need eight times lesser VMs to handle as much traffic as a Node.js application would handle at a given time. Plus, it has got very integrated libraries like Entity Framework Core, SignalR and Identity Framework - that makes development experience really homely.

.NET Core has been open-sourced by Microsoft and is under active development by them. Once you develop your services the biggest issue is of launching your app economically over cloud while keeping it scalable - with .NET Core you get best of both worlds.


### Why Angular ?

Angular has got very coupled ecosystem, like router, form-module, validators, interceptors all nicely packaged and documented. Over all these years, it's API has been backward compatible and the team pays special attention to introduce lesser breaking changes while moving forward. All the more, it's based on typescript which provides better intellisense and linting support. 
Angular projects are found to be much more maintainable than JS frameworks like React.js.

### Why NativeScript ?

NativeScript comes with promise of producing Native mobile apps while facilitating usage of famous frontend-frameworks like Angular, to build mobile apps. In Bizpack, we are using Angular alongwith NativeScript to create mobile application, in order to re-use angular skills and services wherever possible. Plus, NativeScript comes with an option of cloud build which might be of interest to teams who want to target android/ios platform which might be physically unavailable to them.

### Looking for contribution ?

If you find the stack interesting and want to contribute to it, then you are most welcome. It's still in very nascent stage and there's lots of scope of improvement. You may contribute in following ways :

* Direct the project - tell us what you want to include in this ERP.
* Fork, add features, create PR.
* Register bugs.
* Write unit tests.
* Help with writing CI/CD pipeline for build automation.
* Suggest design for application.
* Manage project / track issues.

This is a learning opportunity for all of us !