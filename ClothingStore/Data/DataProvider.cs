using ClothingStore.Mapper;
using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Data
{
	public class DataProvider
	{
		private DbContext _db;

		public DataProvider(ApplicationContext db)
		{
			_db = db;
		}
		
		public async Task<IEnumerable<TOut>> GetIEnumerableMapped<TIn,TOut>() 
			 where TIn : class
		{
			var mapper = new Mapper<TOut>();

			var dbset = _db.Set<TIn>();

			return mapper.Map(await dbset.ToListAsync());
		}

		public async Task<IEnumerable<T>> GetIEnumerable<T>()
			 where T : class
		{
			var dbset = _db.Set<T>();

			return await dbset.ToListAsync();
		}
	}
}
