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

using NUnit.Framework;

#endregion

namespace Spring.Objects.Factory.Support
{
	/// <summary>
	/// Unit tests for the ChildObjectDefinition class.
    /// </summary>
    /// <author>Rick Evans</author>
	[TestFixture]
    public sealed class ChildObjectDefinitionTests
    {
        [Test]
        [ExpectedException(typeof(ObjectDefinitionValidationException))]
        public void ChokesIfParentNamePropertyIsNullAtValidationTime()
        {
            new ChildObjectDefinition(null, null).Validate();
        }

        [Test]
        [ExpectedException(typeof(ObjectDefinitionValidationException))]
        public void ChokesIfParentNamePropertyIsEmptyAtValidationTime()
        {
            new ChildObjectDefinition(String.Empty, null).Validate();
        }

        [Test]
        [ExpectedException(typeof(ObjectDefinitionValidationException))]
        public void ChokesIfParentNamePropertyIsJustWhitespaceAtValidationTime()
        {
            new ChildObjectDefinition("    ", null).Validate();
        }

	    [Test]
	    public void Ctor_ParentNameOnly()
	    {
	        const string expectedParentName = "foo";
	        ChildObjectDefinition def = new ChildObjectDefinition(expectedParentName);
            Assert.AreEqual(expectedParentName, def.ParentName,
                "ParentName property not initialized correctly by ctor.");
            Assert.IsNotNull(def.PropertyValues,
                "PropertyValues must be init'd to a non-null collection if not explicitly supplied.");
            Assert.AreEqual(0, def.PropertyValues.PropertyValues.Length, 
                "PropertyValues must be init'd to an empty collection if not explicitly supplied.");
	    }
	}
}
