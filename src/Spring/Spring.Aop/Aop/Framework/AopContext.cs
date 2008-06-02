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

using System.Collections;
using Spring.Threading;

#endregion

namespace Spring.Aop.Framework
{
	/// <summary>
	/// This class contains various <see langword="static"/> methods used to
	/// obtain information about the current AOP invocation.
	/// </summary>
	/// <remarks>
	/// <p>
	/// The <see langword="static"/>
	/// <see cref="Spring.Aop.Framework.AopContext.CurrentProxy"/> property is
	/// usable if the AOP framework is configured to expose the current proxy
	/// (not the default)... it returns the AOP proxy in use. Target objects or
	/// advice can use this to make advised calls. They can also use it to find
	/// advice configuration.
	/// </p>
	/// <note>
	/// The AOP framework does not expose proxies by default, as there is a
	/// performance cost in doing so.
	/// </note>
	/// <p>
	/// The functionality in this class might be used by a target object that
	/// needed access to resources on the invocation. However, this approach
	/// should not be used when there is a reasonable alternative, as it makes
	/// application code dependent on usage under AOP and the Spring.NET AOP
	/// framework.
	/// </p>
	/// </remarks>
	/// <author>Rod Johnson</author>
	/// <author>Aleksandar Seovic (.NET)</author>
	public sealed class AopContext
	{
        private const string CURRENTPROXY_SLOTNAME = "AopContext.CurrentProxySlotName";

		/// <summary>
		/// The AOP proxy associated with this thread.
		/// </summary>
		/// <remarks>
		/// <p>
		/// Will be <cref lang="null"/> unless the
		/// <see cref="Spring.Aop.Framework.ProxyConfig.ExposeProxy"/> property
		/// on the controlling proxy has been set to <see langword="true"/>.
		/// </p>
		/// <p>
		/// The default value for the 
		/// <see cref="Spring.Aop.Framework.ProxyConfig.ExposeProxy"/> property
		/// is <see langword="false"/>, for performance reasons.
		/// </p>
		/// </remarks>
		private static Stack ProxyStack
        {
            get 
            {
                Stack proxyStack = LogicalThreadContext.GetData(CURRENTPROXY_SLOTNAME) as Stack;
                if (proxyStack == null)
                {
                    proxyStack = new Stack();
                    LogicalThreadContext.SetData(CURRENTPROXY_SLOTNAME, proxyStack);
                }
                return proxyStack;
            }
        }

		/// <summary>
		/// Gets the current AOP proxy.
		/// </summary>
		/// <exception cref="AopConfigException">
		/// If the proxy stack is empty.
		/// </exception>
		public static object CurrentProxy
		{
			get
			{
                if (ProxyStack.Count == 0)
				{
					throw new AopConfigException(
						"Cannot find proxy: Set the 'ExposeProxy' property " +
						"to 'true' on IAdvised to make it available.");
				}
                return ProxyStack.Peek();
			}
		}

		/// <summary>
		/// Sets the current proxy by pushing it to the proxy stack. 
		/// </summary>
		/// <remarks>
		/// <p>
		/// This method is for internal use only, and should never be called by
		/// client code.
		/// </p>
		/// </remarks>
		/// <param name="proxy">
		/// The proxy to put on top of the proxy stack.
		/// </param>
		public static void PushProxy(object proxy)
		{
            ProxyStack.Push(proxy);
		}

		/// <summary>
		/// Removes the current proxy from the proxy stack, making the previous
		/// proxy (if any) the current proxy.
		/// </summary>
		/// <remarks>
		/// <p>
		/// This method is for internal use only, and should never be called by
		/// client code.
		/// </p>
		/// </remarks>
		/// <exception cref="AopConfigException">
		/// If the proxy stack is empty.
		/// </exception>
		public static void PopProxy()
		{
            if (ProxyStack.Count == 0)
			{
				throw new AopConfigException(
					"Proxy stack empty. Always call 'PushProxy' before 'PopProxy'.");
			}
            ProxyStack.Pop();
		}

		#region Constructor (s) / Destructor

		// CLOVER:OFF

		/// <summary>
		/// Creates a new instance of the
		/// <see cref="Spring.Aop.Framework.AopContext"/> class.
		/// </summary>
		/// <remarks>
		/// <p>
		/// This is a utility class, and as such exposes no public constructors.
		/// </p>
		/// </remarks>
		private AopContext()
		{
		}

		// CLOVER:ON

		#endregion
	}
}