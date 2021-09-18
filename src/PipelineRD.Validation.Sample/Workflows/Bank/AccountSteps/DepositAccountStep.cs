using System;

namespace PipelineRD.Validation.Sample.Workflows.Bank.AccountSteps
{
    public class DepositAccountStep : RequestStep<BankContext>, IDepositAccountStep
    {
        public override RequestStepResult HandleRequest()
        {
            Console.WriteLine("DepositAccountStep");
            
            return this.Next();
        }
    }

    public interface IDepositAccountStep : IRequestStep<BankContext>
    {
    }
}