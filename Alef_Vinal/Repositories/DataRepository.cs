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

        public async Task<CodeEntity> GetOne( string id)
        {
            return await _db.CodeEntities.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<CodeEntity>> GetAll()
        {
           return await _db.CodeEntities.ToListAsync();
        }
        

        public async Task Add(CodeEntity newCodeEntity)
        {
            await _db.CodeEntities.AddAsync(newCodeEntity);

            await _db.SaveChangesAsync();
        }

        public async Task<bool> Delete(string entityId)
        {
            var toDelete = await _db.CodeEntities.FirstOrDefaultAsync(x => x.Id == entityId);

            if (toDelete != null)
            {
                _db.CodeEntities.Remove(toDelete);

                await _db.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> Update(CodeEntity newCodeEntity)
        {
            var toUpdate = await _db.CodeEntities.FirstOrDefaultAsync(x => x.Id == newCodeEntity.Id);

            if (toUpdate != null)
            {
                toUpdate.Name = newCodeEntity.Name;


                if (newCodeEntity.Value.Length == 1)
                {
                    newCodeEntity.Value = "00" + newCodeEntity.Value;
                }

                if (newCodeEntity.Value.Length == 2)
                {
                    newCodeEntity.Value = "0" + newCodeEntity.Value.ToString();
                }


                toUpdate.Value = newCodeEntity.Value;

                await _db.SaveChangesAsync();

                return true;
            }

            return false;
        }
      
    }
}
