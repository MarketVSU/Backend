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
		private protected DbContext _db;

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

		public async Task CreateMapped<TOut>(object sender)
			where TOut: class
		{
			var mapper = new Mapper<TOut>();

			var item = mapper.Map(sender);

			var dbSet = _db.Set<TOut>();

			try
			{
				await dbSet.AddAsync(item);

				await _db.SaveChangesAsync();
			}
			catch
			{
				throw new Exception($"Cant put item in {typeof(TOut).Name} dbSet");
			}
		}

		public async Task UpdateMapped<TOut>(object sender)
			where TOut : class
		{
			var mapper = new Mapper<TOut>();

			var item = mapper.Map(sender);

			var dbSet = _db.Set<TOut>();

			try
			{
				dbSet.Update(item);

				await _db.SaveChangesAsync();
			}
			catch
			{
				throw new Exception($"Cant update item in {typeof(TOut).Name} dbSet");
			}
		}

		public async Task DeleteMapped<TOut>(object sender)
			where TOut : class
		{
			var mapper = new Mapper<TOut>();

			var item = mapper.Map(sender);

			var dbSet = _db.Set<TOut>();

			try
			{
				dbSet.Remove(item);

				await _db.SaveChangesAsync();
			}
			catch
			{
				throw new Exception($"Cant delete item in {typeof(TOut).Name} dbSet");
			}
		}
	}
}
