## How it works

In the API in the AuthorController Get method, there is a check for the "culture"
http request header. It the header is missing the method throws an ```ArgumentNullException```.

There is also a ```CheckRequestCultureMiddleware.cs``` Middlerware which also checks
for the same request header and also thows a exception.

On the exception handling side, there is a ```ApiExceptionFilterAttribute.cs``` exception
filter that handles the exception and converts it into a BadRequest response.
There is also a ```ExceptionHandlingMiddleware``` that does the same.

Now the test...

There are several options, 
- When both the exceptionfilter AND the middleware ARE activated.
- When only the exception filter is activated
- When only the exception handling middleware is activated


*MORE EXPLANATINON IS IN THE TEST CASES*
