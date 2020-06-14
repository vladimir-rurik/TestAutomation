using System;
using System.Collections.Generic;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TestAutomation.Model;

namespace TestAutomation.Steps
{
    [Binding]
    public class Conversions
    {
        [StepArgumentTransformation]
        public IEnumerable<BankAccountDTO> BankAccountsTransformation(Table table)
        {
            return table.CreateSet<BankAccountDTO>();
        }
    }
}
