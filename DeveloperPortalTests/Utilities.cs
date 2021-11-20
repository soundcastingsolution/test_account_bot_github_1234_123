using System;
using System.Collections;
using System.Reflection;

namespace DeveloperPortal.Tests
{
    public static class Utilities
    {
        public static bool AreObjectsEquivalent<T>(T actual, T expected)
        {
            var properties = typeof(T).GetProperties();
            foreach (var property in properties)
            {
                bool isEnumerable = typeof(ICollection).IsAssignableFrom(property.PropertyType)
                    || (typeof(IEnumerable).IsAssignableFrom(property.PropertyType)
                    && !(typeof(string).IsAssignableFrom(property.PropertyType)));

                if (!isEnumerable)
                {
                    var actualValue = property.GetValue(actual);
                    var expectedValue = property.GetValue(expected);

                    /// If we have dates, ignore the time portion. There's enough lag in testing
                    /// to cause false-negatives.
                    /// 
                    if (IsDateTime(property))
                    {
                        var tempActualValue = property.GetValue(actual) as DateTime?;
                        var tempExpectedValue = property.GetValue(expected) as DateTime?;

                        if (tempActualValue.HasValue)
                            actualValue = tempActualValue.Value.Date;
                        if (tempExpectedValue.HasValue)
                            expectedValue = tempExpectedValue.Value.Date;
                    }

                    if (OnlyOneValueIsNull(actualValue, expectedValue))
                        return false;

                    if (BothValuesAreNull(actualValue, expectedValue))
                        continue;

                    if (!actualValue.Equals(expectedValue))
                        return false;
                }
            }

            return true;
        }

        private static bool IsDateTime(PropertyInfo property)
        {
            return typeof(DateTime).IsAssignableFrom(property.PropertyType) ||
                   typeof(DateTime?).IsAssignableFrom(property.PropertyType);
        }

        private static bool OnlyOneValueIsNull(object actualValue, object expectedValue)
        {
            if (null == actualValue && null != expectedValue)
                return true;

            if (null != actualValue && null == expectedValue)
                return true;

            return false;
        }

        private static bool BothValuesAreNull(object actualValue, object expectedValue)
        {
            return null == actualValue && null == expectedValue;
        }
    }
}
