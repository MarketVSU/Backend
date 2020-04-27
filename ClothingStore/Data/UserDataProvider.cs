using ClothingStore.DTOs;
using ClothingStore.Mapper;
using ClothingStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Data
{
	public class UserDataProvider: DataProvider
	{
		public UserDataProvider(ApplicationContext db) : base(db)
		{

		}

		public async Task<UserParamsDTO> GetUserById(int id)
		{
			var mapper = new Mapper<UserParamsDTO>();

			var dbSet = _db.Set<User>();

			return mapper.Map(await dbSet.FirstOrDefaultAsync(x => x.Id == id));
		}
	}
}
