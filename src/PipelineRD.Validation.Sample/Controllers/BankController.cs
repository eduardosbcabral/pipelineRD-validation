using Microsoft.AspNetCore.Mvc;

using PipelineRD.Validation.Sample.Models;
using PipelineRD.Validation.Sample.Workflows.Bank;

using System.Threading.Tasks;

namespace PipelineRD.Validation.Sample.Controllers
{
    [Route("bank")]
    public class BankController : ControllerBase
    {
        private readonly IBankPipelineBuilder _bankPipelineBuilder;

        public BankController(IBankPipelineBuilder bankPipelineBuilder, BankContext context)
        {
            _bankPipelineBuilder = bankPipelineBuilder;
        }

        public async Task<IActionResult> Get()
        {
            var request = new CreateAccountModel()
            {
                Cidade = "SP"
            };
            var result = await _bankPipelineBuilder.CreateAccount(request);
            return Ok(result);
        }
    }
}
