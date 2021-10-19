using FluentValidation;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.Extensions.DependencyInjection;

using Xunit;

namespace PipelineRD.Validation.Tests
{
    public class PipelineRDExtensionsTests
    {
        private readonly IServiceProvider _serviceProvider;

        public PipelineRDExtensionsTests(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [Fact]
        public async Task Should_Pipeline_Validate_Request()
        {
            var request = new SampleRequest() { ValidModel = false };
            var pipeline = _serviceProvider.GetService<IPipeline<ContextSample>>();
            pipeline.AddNext<IFirstSampleStep>();
            pipeline.AddNext<ISecondSampleStep>();
            pipeline.AddNext<IThirdSampleStep>();

            var result = await pipeline.ExecuteWithValidation(request);

            Assert.Equal(400, result.StatusCode);
            Assert.Single(result.Errors);
        }

        [Fact]
        public async Task Should_Pipeline_Validate_Request_Using_Validator_Implementation()
        {
            var request = new SampleRequest() { ValidModel = false };
            var pipeline = _serviceProvider.GetService<IPipeline<ContextSample>>();
            var validator = new SampleRequestValidator();
            pipeline.AddNext<IFirstSampleStep>();
            pipeline.AddNext<ISecondSampleStep>();
            pipeline.AddNext<IThirdSampleStep>();

            var result = await pipeline.ExecuteWithValidation(request, validator);

            Assert.Equal(400, result.StatusCode);
            Assert.Single(result.Errors);
        }
    }

    public class SampleRequest
    {
        public Guid Guid { get; set; } = Guid.NewGuid();

        public bool ValidFirst { get; set; } = true;
        public bool ValidSecond { get; set; } = true;

        public bool ValidModel { get; set; }
    }

    public class ContextSample : BaseContext
    {
        public bool ValidFirst { get; set; } = true;

        public ContextSample()
        {
        }
    }

    public class SampleRequestValidator : AbstractValidator<SampleRequest>
    {
        public SampleRequestValidator()
        {
            RuleFor(x => x.ValidModel)
                .Equal(true);
        }
    }

    public class FirstSampleStep : RequestStep<ContextSample>, IFirstSampleStep
    {
        public override RequestStepResult HandleRequest()
        {
            return this.Next();
        }
    }

    public interface IFirstSampleStep : IRequestStep<ContextSample>
    { }

    public class SecondSampleStep : RequestStep<ContextSample>, ISecondSampleStep
    {
        public override RequestStepResult HandleRequest()
        {
            return this.Next();
        }
    }

    public interface ISecondSampleStep : IRequestStep<ContextSample>
    { }

    public class ThirdSampleStep : RequestStep<ContextSample>, IThirdSampleStep
    {
        public override RequestStepResult HandleRequest()
        {
            return this.Finish(200);
        }
    }

    public interface IThirdSampleStep : IRequestStep<ContextSample>
    { }
}
