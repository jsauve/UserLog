using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace com.spectrum.UserLog.Core
{
    public interface IModelService<TModel> where TModel : BaseModel
    {
        Task<TModel> Create(TModel user);
        Task<IList<TModel>> Read();
        Task<TModel> Read(Guid id);
        Task<TModel> Update(TModel user);
        Task Delete(Guid id);
    }
}
