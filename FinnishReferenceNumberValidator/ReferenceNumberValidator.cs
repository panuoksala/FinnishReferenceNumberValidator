using System;
using System.Globalization;

namespace FinnishReferenceNumberValidator
{
    /// <summary>
    /// Validate Finnish reference number according to Finanssiala specification.
    /// 
    /// </summary>
    public static class ReferenceNumberValidator
    {
        /// <summary>
        /// Built according to this specification:
        /// http://www.finanssiala.fi/maksujenvalitys/dokumentit/kotimaisen_viitteen_rakenneohje.pdf
        /// </summary>
        /// <param name="referenceNumber"></param>
        /// <returns></returns>
        public static ReferenceNumberCheckResult IsValidFinnishReference(string referenceNumber)
        {
            if (referenceNumber.Length > 20)
                return new ReferenceNumberCheckResult(false, "Reference number max length is 20 characters");

            if (referenceNumber.Length < 4)
                return new ReferenceNumberCheckResult(false, "Reference number minimum length is 4 characters");

            // Check that reference number contains only digits and also strip leading zeroes.
            if (!long.TryParse(referenceNumber, NumberStyles.Integer, CultureInfo.InvariantCulture, out long refereceNumberWithOutLeadingZeroes))
            {
                return new ReferenceNumberCheckResult(false, "Reference number can contain only numeric values");
            }

            if(refereceNumberWithOutLeadingZeroes <0)
            {
                return new ReferenceNumberCheckResult(false, "Reference number cannot be negative value");
            }

            var calcSum = 0;
            var counter = 0;
            int[] weights = { 7, 3, 1 };
            var strippedReferenceNumber = refereceNumberWithOutLeadingZeroes.ToString(CultureInfo.InvariantCulture);
            // Skip check digit, so start from position -2
            for (var i = strippedReferenceNumber.Length - 2; i >= 0; i--)
            {
                var weightIndex = counter % 3;
                var curWeight = weights[weightIndex];
                var strDigit = strippedReferenceNumber[i].ToString();
                var digit = Convert.ToInt32(strDigit);
                calcSum += digit * curWeight;

                counter++;
            }

            var checkDigit = ((10 - (calcSum % 10)) % 10).ToString();
            if (!CompareCheckDigit(strippedReferenceNumber, checkDigit))
            {
                return new ReferenceNumberCheckResult(false, "Invalid checksum");
            }

            return new ReferenceNumberCheckResult(true, "Valid reference number");
        }

        private static bool CompareCheckDigit(string strippedReferenceNumber, string checkDigit)
        {
            return Equals(checkDigit, strippedReferenceNumber[strippedReferenceNumber.Length - 1].ToString());
        }
    }
}
