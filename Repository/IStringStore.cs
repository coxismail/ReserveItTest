using System.Collections.Generic;

namespace ReserveItTest.Repository
{
    public interface IStringStore
    {
        List<string> GetList();

        void StoreData(List<string> data);
    }
}
