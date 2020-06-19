using System;
namespace com.spectrum.UserLog.Core
{
    public class ModelResult<TModel> where TModel : BaseModel
    {
        public TModel Model { get; set; }

        public ModelAction Action { get; set; }
    }
}
