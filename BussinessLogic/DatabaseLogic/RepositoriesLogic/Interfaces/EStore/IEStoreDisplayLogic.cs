using BussinessLogic.DTOs.EStore.EStoreDisplayDto;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.EStore
{
    public interface IEStoreDisplayLogic
    {
        EStoreDisplayIndexDto GetEStoreDisplayIndexDto(int a_eStore, int a_typeItems);
        EStoreDisplayDetailDto GetEStoreDisplayDetailDto(int a_idEStore, int a_typeItems, int product);
    }
}