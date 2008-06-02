#region License

/*
 * Copyright � 2002-2005 the original author or authors.
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

#region Imports

using System;
using Common.Logging;
using NUnit.Framework;

#endregion

namespace Spring.Objects.Factory.Config
{
	/// <summary>
	/// Unit tests for the LogFactoryObject class.
	/// </summary>
	/// <author>Rick Evans (.NET)</author>
	[TestFixture]
	public sealed class LogFactoryObjectTests
	{
		[Test]
		[ExpectedException(typeof (ArgumentException))]
		public void CheckThatLogNamePropertyMustBeSet()
		{
			LogFactoryObject fac = new LogFactoryObject();
			fac.GetObject();
		}

		[Test]
		[ExpectedException(typeof (ArgumentNullException))]
		public void CheckLogNamePropertyWithNullName()
		{
			LogFactoryObject fac = new LogFactoryObject();
			fac.LogName = null;
		}

		[Test]
		[ExpectedException(typeof (ArgumentNullException))]
		public void CheckLogNamePropertyWithEmptyName()
		{
			LogFactoryObject fac = new LogFactoryObject();
			fac.LogName = string.Empty;
		}

		[Test]
		[ExpectedException(typeof (ArgumentNullException))]
		public void CheckInstantiationWithNullName()
		{
			new LogFactoryObject(null);
		}

		[Test]
		[ExpectedException(typeof (ArgumentNullException))]
		public void CheckInstantiationWithEmptyName()
		{
			new LogFactoryObject(string.Empty);
		}

		[Test]
		public void CheckLogNamePropertyStripsRedundantWhitespaceFromName()
		{
			LogFactoryObject fac = new LogFactoryObject();
			fac.LogName = "   Foo  ";
			Assert.AreEqual("Foo", fac.LogName);
		}

		[Test]
		public void CheckGetObjectWorksWithValidLogName()
		{
			LogFactoryObject fac = new LogFactoryObject();
			fac.LogName = "Foo";
			ILog log = fac.GetObject() as ILog;
			Assert.IsNotNull(log, "Mmm... pulled a null ILog instance from a properly configured LogFactoryObject instance.");
		}

		[Test]
		public void CheckGetObjectReturnsSharedInstance()
		{
			LogFactoryObject fac = new LogFactoryObject();
			fac.LogName = "Foo";
			ILog log = fac.GetObject() as ILog;
			ILog anotherLogInstance = fac.GetObject() as ILog;
			Assert.IsTrue(log == anotherLogInstance,
			              "Okay, the LogFactoryObject ain't returning shared instances (it should).");
		}

		[Test]
		[ExpectedException(typeof (ArgumentException), "The 'LogName' property has not been set.")]
		public void CheckAfterPropertiesSetBlowsUpIfNotCorrectlyConfigured()
		{
			LogFactoryObject fac = new LogFactoryObject();
			fac.AfterPropertiesSet();
		}

		[Test]
		public void CheckAfterPropertiesSetPassesIfCorrectlyConfigured()
		{
			LogFactoryObject fac = new LogFactoryObject("Bing!");
			fac.AfterPropertiesSet();
		}

		[Test]
		public void ItsDefinitelyASingletonYeah()
		{
			LogFactoryObject fac = new LogFactoryObject("Bing!");
			Assert.IsTrue(fac.IsSingleton,
			              "The LogFactoryObject class must be configured to return shared instances (it currently ain't).");
		}

		[Test]
		public void ObjectTypePropertyYieldsTheCorrectType()
		{
			LogFactoryObject fac = new LogFactoryObject("Bing!");
			Assert.AreEqual(typeof (ILog), fac.ObjectType,
			                "Mmm... the LogFactoryObject class ain't giving back ILog types (it must).");
		}
	}
}