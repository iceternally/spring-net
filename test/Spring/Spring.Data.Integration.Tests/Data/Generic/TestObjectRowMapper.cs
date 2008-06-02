#region License

/*
 * Copyright � 2002-2007 the original author or authors.
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


#if NET_2_0

using System.Data;
using Spring.Objects;
using Spring.Data.Generic;

namespace Spring.Data.Generic
{
    /// <summary>
    /// This is 
    /// </summary>
    /// <remarks>
    ///
    /// </remarks>
    /// <author>Mark Pollack</author>
    public class TestObjectRowMapper<T> : IRowMapper<T> where T : TestObject, new()
    {
        public T MapRow(IDataReader reader, int rowNum) 
        {
            if (reader == null) return new T();
            T to = new T();
            to.ObjectNumber = reader.GetInt32(0);
            to.Age = reader.GetInt32(1);
            to.Name = reader.GetString(2);
            return to;
        }
    }
}

#endif