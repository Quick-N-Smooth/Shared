## Http test concept for testing NET Core Middlewares

The following sample application gives a solution for http testing **NET Core middlewares**.

*Problem descrption:*
Using Http request tests is a well defined solution for testing the whole http API application. It works by 
creating http request in the test application and send it to the API.
XUnit gives a control over the list of services in the target API application making it possible to use dubbed
services or mockes during the http process.

The problem with Testing Middlewares is that it is not a part of the Core services thus they are not in the service list.
It is therefore impossible to have a direct control during the test.
However DI also work in the case of Middlewares. So as a possible solition is to create a service component for the active
part of the Middleware and add it to the service list. That particular service will be automatically injected into the
Middleware. During the test XUnit gives control over the service thus gives an indirect controll over the middleware.

The sameple application uses an exception handling middleware for handling any non-handled exception.
There are 2 types of custom global exception handling solution.
- Exception filter attribute which works in controller methods
- Exception handling middleware which catches unhandled exceptions during the whole http core chain.

So, the testing creates and runs all the possible scenarios for handling an exception in filter attribute and exception
handler middleware.



