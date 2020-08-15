using Alef_Vinal.Contexts;
using Alef_Vinal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alef_Vinal.Repositories
{
    public class DataRepository : IDataRepository
    {
        private CodeEntityContext _db { get; set; }

        public DataRepository(CodeEntityContext db)
        {
            _db = db;
        }


        public bool IsEmptyDb()
        {
            return !_db.CodeEntities.Any();
        }

        public async Task<CodeEntity> GetMany()
        {
            throw new NotImplementedException();
        }

        public async Task<CodeEntity> GetOne()
        {
            throw new NotImplementedException();
        }

        public async Task Add(CodeEntity value)
        {
            await _db.CodeEntities.AddAsync(value);

            await _db.SaveChangesAsync();
        }

        public async Task<bool> Delete(string entityId)
        {
            throw new NotImplementedException();
        }

        public async Task Update(CodeEntity value)
        {
            throw new NotImplementedException();
        }
      
    }
}
