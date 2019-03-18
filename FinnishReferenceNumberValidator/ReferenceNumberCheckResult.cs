using System;
using System.Collections.Generic;
using System.Text;

namespace FinnishReferenceNumberValidator
{
    public class ReferenceNumberCheckResult
    {
        public bool IsValid { get; private set; }
        public string ValidationMessage { get; private set; }

        public ReferenceNumberCheckResult()
        {

        }

        public ReferenceNumberCheckResult(bool result, string validationMessage)
        {
            IsValid = result;
            ValidationMessage = validationMessage;
        }
    }
}
