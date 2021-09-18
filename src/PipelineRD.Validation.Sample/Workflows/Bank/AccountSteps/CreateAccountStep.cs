using PipelineRD.Validation.Sample.Models;

using System;

namespace PipelineRD.Validation.Sample.Workflows.Bank.AccountSteps
{
    public class CreateAccountStep : RequestStep<BankContext>, ICreateAccountStep
    {
        public override RequestStepResult HandleRequest()
        {
            var request = this.Request<CreateAccountModel>();
            if (request.Cidade == "SP") return this.Rollback(400);
            Console.WriteLine("CreateAccountStep");
            
            return this.Next();
        }
    }

    public interface ICreateAccountStep : IRequestStep<BankContext>
    {
    }
}