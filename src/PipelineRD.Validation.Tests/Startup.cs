using Microsoft.Extensions.DependencyInjection;

using PipelineRD.Extensions;
using PipelineRD.Settings;

namespace PipelineRD.Validation.Tests
{
    class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.UsePipelineRD(x =>
            {
                x.UseCacheInMemory(new MemoryCacheSettings());
                x.AddPipelineServices(x =>
                {
                    x.InjectAll();
                    x.InjectRequestValidators();
                });
            });
        }
    }
}
