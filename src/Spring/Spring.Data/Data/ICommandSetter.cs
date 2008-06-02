#region License

/*
 * Copyright � 2002-2006 the original author or authors.
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

using System.Data;

#endregion

namespace Spring.Data
{
    
	/// <summary>
	/// Called by the AdoTemplate class to allow implementations
	/// to set any necessary parameters on the command object.
	/// The CommandType and CommandText will have already been supplied.
	/// </summary>
	/// <author>Mark Pollack (.NET)</author>
	public interface ICommandSetter 
	{
	    
	    
        void SetValues(IDbCommand dbCommand);
	}
}
