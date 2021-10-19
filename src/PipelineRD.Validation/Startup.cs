using FluentValidation;

using PipelineRD.Validation;

using System;

namespace PipelineRD.Test
{
    public class Startup
    {
        public async void Test()
        {
            IServiceProvider serviceProvider = null;
            IPipeline<TestContext> pipeline = new Pipeline<TestContext>(serviceProvider);
            var request = new TestRequest();
            await pipeline
                .ExecuteWithValidation(request);
        }
    }

    public class TestContext : BaseContext
    {

    }

    public class TestRequest
    {

    }
}
