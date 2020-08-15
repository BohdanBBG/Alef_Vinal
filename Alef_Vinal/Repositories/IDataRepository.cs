using Alef_Vinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alef_Vinal.Repositories
{
    public interface IDataRepository
    {
        public bool IsEmptyDb();

        public Task<CodeEntity> GetOne();

        public Task<CodeEntity> GetMany();

        public Task Add(CodeEntity value);

        public Task Update(CodeEntity value);

        public Task<bool> Delete(string entityId);

    }
}
