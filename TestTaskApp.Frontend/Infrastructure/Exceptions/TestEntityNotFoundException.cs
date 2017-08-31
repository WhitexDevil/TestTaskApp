using System;

namespace TestTaskApp.Frontend.Infrastructure.Exceptions
{
    public class TestEntityNotFoundException:Exception
    {
        public TestEntityNotFoundException()
        {
        }

        public TestEntityNotFoundException(string message) : base(message)
        {
        }
    }
}