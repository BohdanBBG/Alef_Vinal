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

        public Task<CodeEntity> GetOne(string id);

        public Task<IList<CodeEntity>> GetAll();

        public Task Add(NewCodeEntity value);

        public Task<bool> Update(CodeEntity value);

        public Task<bool> Delete(string entityId);

    }
}
