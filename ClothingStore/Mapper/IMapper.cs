using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Mapper
{
	public interface IMapper<out TOut>
	{
		TOut Map(object source);
	}
}
