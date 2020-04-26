using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace ClothingStore.Mapper
{
	internal sealed class Mapper<TOut> : IMapper<TOut>
	{
		private readonly Dictionary<string, PropertyInfo> _dbModelProperties;
		private readonly Func<TOut> _activator;

		public Mapper()
		{
			var outType = typeof(TOut);

			_dbModelProperties = outType.GetProperties().ToDictionary(p => p.Name);

			var emptyConstructor = outType.GetConstructor(Array.Empty<Type>());
			_activator = emptyConstructor == null
				? throw new KeyNotFoundException($"Default constructor for '{outType}' not found")
				: Expression.Lambda<Func<TOut>>(Expression.New(emptyConstructor)).Compile();
		}

		public TOut Map(object source)
		{
			var outInstance = _activator();

			var dtoProperties = source.GetType().GetProperties();

			foreach(var property in dtoProperties)
			{
				if (_dbModelProperties.TryGetValue(property.Name, out var outProperty))
				{
					var sourceValue = property.GetValue(source);
					outProperty.SetValue(outInstance, sourceValue);
				}
			}

			return outInstance;
		}

		public IEnumerable<TOut> Map(IEnumerable<object> source)
		{
			List<TOut> outMappedList = new List<TOut>();

			foreach(var obj in source)
			{
				outMappedList.Add(Map(obj));
			}

			return outMappedList;
		}
	}
}
