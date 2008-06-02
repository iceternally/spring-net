#region License

/*
 * Copyright 2002-2004 the original author or authors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#endregion

using System;

using NUnit.Framework;

namespace Spring.Globalization.Formatters
{
	/// <summary>
	/// Unit tests for CurrencyFormatter class.
	/// </summary>
    /// <author>Aleksandar Seovic</author>
    [TestFixture]
    public class CurrencyFormatterTests
	{
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FormatNullValue()
        {
            CurrencyFormatter fmt = new CurrencyFormatter();
            fmt.Format(null);
        }

        [Test]
        public void ParseNullOrEmptyValue()
        {
            CurrencyFormatter fmt = new CurrencyFormatter();
            Assert.AreEqual(0, fmt.Parse(null));
            Assert.IsTrue(fmt.Parse("") is double);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void FormatNonNumber()
        {
            CurrencyFormatter fmt = new CurrencyFormatter();
            fmt.Format("not a number");
        }

        [Test]
        public void FormatUsingDefaults()
        {
            CurrencyFormatter fmt = new CurrencyFormatter("en-US");
            Assert.AreEqual("$1,234.00", fmt.Format(1234));
            Assert.AreEqual("$1,234.56", fmt.Format(1234.56));
            Assert.AreEqual("($1,234.00)", fmt.Format(-1234));
            Assert.AreEqual("($1,234.56)", fmt.Format(-1234.56));

            fmt = new CurrencyFormatter(CultureInfoUtils.SerbianLatinCultureName);
            Assert.AreEqual("1.234,00 Din.", fmt.Format(1234));
            Assert.AreEqual("1.234,56 Din.", fmt.Format(1234.56));
            Assert.AreEqual("-1.234,00 Din.", fmt.Format(-1234));
            Assert.AreEqual("-1.234,56 Din.", fmt.Format(-1234.56));

            fmt = new CurrencyFormatter(CultureInfoUtils.SerbianCyrillicCultureName);
            Assert.AreEqual("1.234,00 Дин.", fmt.Format(1234));
            Assert.AreEqual("1.234,56 Дин.", fmt.Format(1234.56));
            Assert.AreEqual("-1.234,00 Дин.", fmt.Format(-1234));
            Assert.AreEqual("-1.234,56 Дин.", fmt.Format(-1234.56));
        }

        [Test]
        public void ParseUsingDefaults()
        {
            CurrencyFormatter fmt = new CurrencyFormatter("en-US");
            Assert.AreEqual(1234, fmt.Parse("$1,234.00"));
            Assert.AreEqual(1234.56, fmt.Parse("$1,234.56"));
            Assert.AreEqual(-1234, fmt.Parse("($1,234.00)"));
            Assert.AreEqual(-1234.56, fmt.Parse("($1,234.56)"));

            fmt = new CurrencyFormatter(CultureInfoUtils.SerbianLatinCultureName);
            Assert.AreEqual(1234, fmt.Parse("1.234,00 Din."));
            Assert.AreEqual(1234.56, fmt.Parse("1.234,56 Din."));
            Assert.AreEqual(-1234, fmt.Parse("-1.234,00 Din."));
            Assert.AreEqual(-1234.56, fmt.Parse("-1.234,56 Din."));

            fmt = new CurrencyFormatter(CultureInfoUtils.SerbianCyrillicCultureName);
            Assert.AreEqual(1234, fmt.Parse("1.234,00 Дин."));
            Assert.AreEqual(1234.56, fmt.Parse("1.234,56 Дин."));
            Assert.AreEqual(-1234, fmt.Parse("-1.234,00 Дин."));
            Assert.AreEqual(-1234.56, fmt.Parse("-1.234,56 Дин."));
        }

        [Test]
        public void FormatUsingCustomSettings()
        {
            CurrencyFormatter fmt = new CurrencyFormatter("en-US");
            fmt.DecimalDigits = 0;
            fmt.NegativePattern = 1;
            Assert.AreEqual("$1,234", fmt.Format(1234));
            Assert.AreEqual("$1,235", fmt.Format(1234.56));
            Assert.AreEqual("-$1,234", fmt.Format(-1234));
            Assert.AreEqual("-$1,235", fmt.Format(-1234.56));

            fmt = new CurrencyFormatter(CultureInfoUtils.SerbianLatinCultureName);
            fmt.PositivePattern = 1;
            fmt.CurrencySymbol = "din";
            Assert.AreEqual("1.234,00din", fmt.Format(1234));
            Assert.AreEqual("1.234,56din", fmt.Format(1234.56));
            Assert.AreEqual("-1.234,00 din", fmt.Format(-1234));
            Assert.AreEqual("-1.234,56 din", fmt.Format(-1234.56));

            fmt = new CurrencyFormatter(CultureInfoUtils.SerbianCyrillicCultureName);
            fmt.GroupSizes = new int[] {1, 2};
            fmt.GroupSeparator = "'";
            Assert.AreEqual("1'23'4,00 Дин.", fmt.Format(1234));
            Assert.AreEqual("1'23'4,56 Дин.", fmt.Format(1234.56));
            Assert.AreEqual("-1'23'4,00 Дин.", fmt.Format(-1234));
            Assert.AreEqual("-1'23'4,56 Дин.", fmt.Format(-1234.56));
        }

        [Test]
        public void ParseUsingCustomSettings()
        {
            CurrencyFormatter fmt = new CurrencyFormatter("en-US");
            fmt.DecimalDigits = 0;
            fmt.NegativePattern = 1;
            Assert.AreEqual(1234, fmt.Parse("$1,234"));
            Assert.AreEqual(1234.56, fmt.Parse("$1,234.56"));
            Assert.AreEqual(-1234, fmt.Parse("-$1,234"));
            Assert.AreEqual(-1234.56, fmt.Parse("-$1,234.56"));

            fmt = new CurrencyFormatter("sr-SP-Latn");
            fmt.PositivePattern = 1;
            fmt.CurrencySymbol = "din";
            Assert.AreEqual(1234, fmt.Parse("1.234,00din"));
            Assert.AreEqual(1234.56, fmt.Parse("1.234,56din"));
            Assert.AreEqual(-1234, fmt.Parse("-1.234,00 din"));
            Assert.AreEqual(-1234.56, fmt.Parse("-1.234,56 din"));

            fmt = new CurrencyFormatter(CultureInfoUtils.SerbianCyrillicCultureName);
            fmt.GroupSizes = new int[] {1, 2};
            fmt.GroupSeparator = "'";
            Assert.AreEqual(1234, fmt.Parse("1'23'4,00 Дин."));
            Assert.AreEqual(1234.56, fmt.Parse("1'23'4,56 Дин."));
            Assert.AreEqual(-1234, fmt.Parse("-1'23'4,00 Дин."));
            Assert.AreEqual(-1234.56, fmt.Parse("-1'23'4,56 Дин."));
        }

    }
}
