using System.Net;
using WebApi.Services;

namespace UnitTests
{
    public class ExceptionHandlingServiceTests
    {
        private readonly ExceptionHandlingService target;

        public ExceptionHandlingServiceTests()
        {
            target = new ExceptionHandlingService();
        }

        // use full namespace for exception types, it is easier then to interpret the test
        [Theory]
        [InlineData(typeof(System.ArgumentNullException), HttpStatusCode.BadRequest, "ArgumentNullException", "middleware")]
        [InlineData(typeof(Microsoft.AspNetCore.Connections.ConnectionResetException), HttpStatusCode.RequestTimeout, "ConnectionResetException handled in middleware")]
        public void HandleKnownExceptions(Type exceptionType, HttpStatusCode expectedStatusCode, params string[]? expectedWordsInDescription)
        {
            var exception = InstanciateExceptionOfType(exceptionType);
            if (exception is null)
            {
                exception = InstanciateManually(exceptionType);
            }

            if (exception is null)
            {
                throw new ArgumentNullException("Exception type is impossible to create.");
            }

            ExceptionResponse response = target.CreateExceptionResponse(exception);

            Assert.True(response.StatusCode.Equals(expectedStatusCode), $"The expected status code do not match. Expected:{(int)expectedStatusCode}; Found:{response.StatusCode}");

            if (expectedWordsInDescription is not null)
            {
                foreach (var expectedWord in expectedWordsInDescription)
                {
                    Assert.True(response.Description.Contains(expectedWord), $"The expected word in the error description in not found '{expectedWord}'");
                }
            }
        }

        private static dynamic? InstanciateManually(Type exceptionType)
        {
            var fullName = exceptionType.FullName;
            switch (fullName)
            {
                case "Microsoft.AspNetCore.Connections.ConnectionResetException":
                    return new Microsoft.AspNetCore.Connections.ConnectionResetException("ConnectionResetException");
                default:
                    return null;
            }
        }

        private static dynamic? InstanciateExceptionOfType(Type exceptionType)
        {
            object? @object;
            try
            {
                @object = Activator.CreateInstance(exceptionType);
                dynamic? typedException = Convert.ChangeType(@object, exceptionType);
                if (typedException is null)
                {
                    return null;
                }

                return typedException;
            }
            catch
            {
                return null;
            }
        }
    }
}