
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Ixxtension
{
	public static class ComUtils
	{
		private static List<Type> VSInterfaces = GetVSInterfaces();

		public static List<Type> GetSupportedInterfaces(object comObject)
		{
			var result = new List<Type>();

			if (comObject != null)
			{
				IntPtr iUnknown = Marshal.GetIUnknownForObject(comObject);

				foreach (var interfaceType in VSInterfaces)
				{
					Guid guid = interfaceType.GUID;
					IntPtr ptr;
					Marshal.QueryInterface(iUnknown, ref guid, out ptr);
					if (ptr != IntPtr.Zero)
					{
						result.Add(interfaceType);
					}
				}
			}

			return result;
		}

		public static object GetPropertyValue(EnvDTE.Property property)
		{
			try
			{
				return property.Value;
			}
			catch
			{
				return null;
			}
		}

		public struct PropertyDesc
		{
			public string Name;
			public Type Type;
			public object Value;
			public List<Type> Interfaces;

			public override string ToString()
			{
				var InterfaceCount = Interfaces == null ? 0 : Interfaces.Count;
				return string.Format("{0}, {1}, {2}", Name, Type.Name, InterfaceCount);
			}
		}

		public static List<PropertyDesc> ListProperties(EnvDTE.Properties properties)
		{
			var result = new List<PropertyDesc>();

			for (int i = 1; i <= properties.Count; i++)
			{
				var desc = new PropertyDesc();

				var property = properties.Item(i);
				desc.Name = property.Name;
				desc.Value = GetPropertyValue(property);
				desc.Type = desc.Value != null ? desc.Value.GetType() : typeof(System.Object);
				if (desc.Type.Name == "__ComObject")
				{
					desc.Interfaces = GetSupportedInterfaces(desc.Value);
					if (desc.Interfaces.Count > 0)
					{
						desc.Type = desc.Interfaces.Last();
					}
				}

				result.Add( desc );
			}

			return result;
		}

		private static Assembly GetVSAssembly()
		{
			return Assembly.GetAssembly(typeof(EnvDTE.ProjectItem));
		}

		private static List<Type> GetVSInterfaces()
		{
			var interfaces = new List<Type>();
			foreach (var type in GetVSAssembly().GetTypes())
			{
				if (type.IsInterface && type.GUID != Guid.Empty)
				{
					interfaces.Add(type);
				}
			}
			return interfaces;
		}
	}
}