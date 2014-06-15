using BussinessLogic.AuthorizationLogic;
using BussinessLogic.DatabaseLogic.Repositories;
using BussinessLogic.DatabaseLogic.Repositories.Customer;
using BussinessLogic.DatabaseLogic.Repositories.EStore;
using BussinessLogic.DatabaseLogic.Repositories.EStore.EStoreCategory;
using BussinessLogic.DatabaseLogic.Repositories.EStore.EStoreDisplay;
using BussinessLogic.DatabaseLogic.Repositories.EStore.EStoreShipmentType;
using BussinessLogic.DatabaseLogic.Repositories.Employee;
using BussinessLogic.DatabaseLogic.Repositories.FactureRepositories;
using BussinessLogic.DatabaseLogic.Repositories.Safety;
using BussinessLogic.DatabaseLogic.Repositories.Store;
using BussinessLogic.DatabaseLogic.Repositories.WarehouseRepositories;
using BussinessLogic.DatabaseLogic.Repositories.WarehouseRepositories.Currencies;
using BussinessLogic.DatabaseLogic.Repositories.WarehouseRepositories.InventaryRepositories;
using BussinessLogic.DatabaseLogic.Repositories.WarehouseRepositories.SupplyRepositories;
using BussinessLogic.DatabaseLogic.Repositories.WarehouseRepositories.TaxRepositories;
using BussinessLogic.DatabaseLogic.RepositoriesLogic;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Admin;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.EStore;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Employee;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Factures;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Admin;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.EStore;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Employee;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Factures;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Store;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Warehouse;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Warehouse.Supplies;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Store;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Warehouse;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Warehouse.Supplies;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Warehouse.Taxes;
using BussinessLogic.Mappers;
using BussinessLogic.Mappers.Account;
using BussinessLogic.Mappers.Admin;
using BussinessLogic.Mappers.EStore;
using BussinessLogic.Mappers.EStore.EStoreCategory;
using BussinessLogic.Mappers.EStore.EStoreDisplay;
using BussinessLogic.Mappers.EStore.EStoreWarehouse;
using BussinessLogic.Mappers.EStore.EStoryShipmentType;
using BussinessLogic.Mappers.Employee;
using BussinessLogic.Mappers.Factures;
using BussinessLogic.Mappers.Inventary;
using BussinessLogic.Mappers.Safety;
using BussinessLogic.Mappers.Store;
using BussinessLogic.Mappers.Warehouse;
using BussinessLogic.Mappers.Warehouse.Distributions;
using BussinessLogic.Mappers.Warehouse.Product;
using BussinessLogic.Mappers.Warehouse.Supplies;
using BussinessLogic.Mappers.Warehouse.Taxes;
using Database.Core;
using Database.Core.Interfaces;

namespace BussinessLogic.DatabaseLogic
{
    public static class RepositoryLogicFactory
    {
        private static IDataBaseContext Sm_dataBaseContext = new DataBaseContext();

        public static IDataBaseContext DataBaseContext { get { return Sm_dataBaseContext; } set { Sm_dataBaseContext = value; } }

        public static UserManagementLogic GetUserManagementLogic()
        {
            return new UserManagementLogic {
                UserDataBaseRepository = new UserDataBaseRepository{DataBaseContext = Sm_dataBaseContext},
                RoleDataBaseRepository = new RoleDataBaseRepository{DataBaseContext = Sm_dataBaseContext},
                LogOnMapper = new LogOnMapper(),
                RegisterUserDtoMapper = new RegisterUserDtoMapper(),
                DetailsUserToDtoMapper = new DetailsUserToDtoMapper(),
                EditUserToDtoMapper = new EditUserToDtoMapper()
            };
        }

        public static ItemManagementLogic GetItemManagementLogic()
        {
            var itemManagementLogic = new ItemManagementLogic
            {
                ItemRepository = new ItemRepository { DataBaseContext = Sm_dataBaseContext },
                WarehouseRepository = new WarehouseRepository{DataBaseContext = Sm_dataBaseContext},
                ProductWarehouseRepository = new ProductWarehouseRepository{DataBaseContext = Sm_dataBaseContext},
                ServiceWarehouseRepository = new ServiceWarehouseRepository{DataBaseContext = Sm_dataBaseContext},
                ProductTypeRepository = new ProductTypeRepository{DataBaseContext = Sm_dataBaseContext},
                SaleTypeRepository =new SaleTypeRepository{DataBaseContext = Sm_dataBaseContext},
                TaxRepository = new TaxRepository{DataBaseContext = Sm_dataBaseContext},
                AttributeTypeRepository = new AttributeTypeRepository{DataBaseContext = Sm_dataBaseContext},
                AttributeRepository = new AttributeRepository{DataBaseContext = Sm_dataBaseContext},
                SearchSearchItemDtoDtoMapper = new SearchItemDtoMapper(),
                DisplayProductAttributeDtoMapper = new DisplayProductAttributeDtoMapper(),
                DisplayProductDtoMapper = new DisplayProductDtoMapper(),
                ManageProductAttributeDtoMapper = new ManageProductAttributeDtoMapper(),
                ManageProductDtoMapper = new ManageProductDtoMapper(),
                SearchItemByAttributeDtoMapper = new SearchItemByAttributeDtoMapper()
            };

            return itemManagementLogic;
        }

        public static TaxManagementLogic GetTaxManagementLogic()
        {
            var taxManagementLogic = new TaxManagementLogic
            {
                TaxRepository = new TaxRepository { DataBaseContext = Sm_dataBaseContext },
                TaxAddMapper = new TaxAddMapper(),
                TaxEditMapper = new TaxEditMapper(),
                TaxMapper = new TaxMapper()
            };

            return taxManagementLogic;
        }

        public static ISafetyPointsLogic GetSafetyPointLogic()
        {
            return new SafetyPointsLogic
            {
                TypesOfSafetyPointsRepository = new TypesOfSafetyPointsRepository { DataBaseContext = Sm_dataBaseContext },
                WarehouseRepository = new WarehouseRepository {DataBaseContext = Sm_dataBaseContext},
                SafetyPointMapper = new AddNewSafetyPointDtoMapper(),
                SafetyPointRepository = new SafetyPointRepository{DataBaseContext = Sm_dataBaseContext},
                DispalySafetyPointDtoMapper = new DispalySafetyPointDtoMapper(),
                SafetyPointGroupsRepository = new SafetyPointGroupsRepository{DataBaseContext = Sm_dataBaseContext},
                AddSafetyPointGroupMapper = new AddSafetyPointGroupMapper(),
                DispalySafetyPointGroupDtoMapper = new DispalySafetyPointGroupDtoMapper(),
                EditSafetyPointGroupDtoMapper = new EditSafetyPointGroupDtoMapper()
            };
        }

        public static IUserManagementAdminLogic GetUserManagementAdminLogic()
        {
            return new UserManagementAdminLogic
                {
                        UserDataBaseRepository = new UserDataBaseRepository {DataBaseContext = Sm_dataBaseContext},
                        SafetyPointGroupsRepository = new SafetyPointGroupsRepository{DataBaseContext = Sm_dataBaseContext},
                        AdminDisplayUserDtoMapper = new AdminDisplayUserDtoMapper(),
                        SafetyPointGroupForUserManageMapper = new SafetyPointGroupForUserManageMapper(),
                        UserForManageSafetyPointGroupsMapper = new UserForManageSafetyPointGroupsMapper()
                };
        }

        public static WarehouseLogic GetWarehouseLogic()
        {
            return new WarehouseLogic
                {
                    WarehouseRepository = new WarehouseRepository{DataBaseContext = Sm_dataBaseContext},
                    AddWarehouseDtoMapper = new AddWarehouseDtoMapper(),
                    DisplayWarehouseMapper = new DisplayWarehouseMapper(),
                    DisplaySpecyficWarehouseMapper = new DisplaySpecyficWarehouseMapper(),
                    EditWarehouseDtoMapper = new EditWarehouseDtoMapper(),
                    ProductWarehouseRepository = new ProductWarehouseRepository{ DataBaseContext = Sm_dataBaseContext},
                    ServiceWarehouseRepository = new ServiceWarehouseRepository{ DataBaseContext = Sm_dataBaseContext},
                    ManageProductWarehouseMapper =new ManageProductWarehouseMapper(),
                    ProductItemForManageProductWarehouseMapper = new ProductItemForManageProductWarehouseMapper(),
                    ItemRepository = new ItemRepository {DataBaseContext =  Sm_dataBaseContext}
                };
        }
        public static ISupplyLogic GetSupplyLogic()
        {
            return new SupplyLogic
                       {
                           ProductTypesRepository = new ProductTypeRepository { DataBaseContext = Sm_dataBaseContext },
                           SaleTypesRepository = new SaleTypeRepository { DataBaseContext = Sm_dataBaseContext },
                           AttributesRepository = new AttributeRepository { DataBaseContext = Sm_dataBaseContext },
                           WarehouseRepository = new WarehouseRepository{DataBaseContext = Sm_dataBaseContext},
                           SupplyRepository = new SupplyRepository { DataBaseContext = Sm_dataBaseContext },
                           AttributeTypeMapper = new AttributeTypeMapper(),
                           AttributeMapper = new AttributeMapper { AttributeTypeMapper = new AttributeTypeMapper()},
                           ProductAddMapper = new ProductAddMapper(),
                           ProductMapper = new ProductMapper(),
                           SupplyAddMapper = new SupplyAddMapper(),
                           SupplyEditMapper = new SupplyEditMapper(),
                           SupplyViewMapper = new SupplyViewMapper{ProductViewMapper = new ProductViewMapper()},
                           SupplyMapper = new SupplyMapper(),
                           ProductWarehousesMapper = new ProductWarehousesMapper(),
                           WarehousesMapper = new WarehousesMapper(),
                           ItemRepository = new ItemRepository{DataBaseContext = Sm_dataBaseContext},
                       };
        }

        public static IDistributionLogic GetDistributionLogic()
        {
            return new DistributionLogic
                {
                        DistributionRepository = new DistributionRepository{ DataBaseContext =  Sm_dataBaseContext},
                        CustomerRepository =  new CustomerRepository{ DataBaseContext = Sm_dataBaseContext},
                        SearchDistributionDtoMapper = new SearchDistributionDtoMapper(),
                        DisplayDistributionDtoMapper = new DisplayDistributionDtoMapper(),
                        PerformDistributionDtoMapper = new PerformDistributionDtoMapper(),
                        EditDistributionDtoMapper = new EditDistributionDtoMapper(),
                        ItemRepository = new ItemRepository{DataBaseContext = Sm_dataBaseContext},
                        UserDataBaseRepository = new UserDataBaseRepository{DataBaseContext = Sm_dataBaseContext},
                        DisplayItemDtoMapper = new DisplayItemDtoMapper(),
                        SimpleItemDtoMapper = new SimpleItemDtoMapper(),
                        WarehouseRepository = new WarehouseRepository{ DataBaseContext = Sm_dataBaseContext},
                        ProductWarehouseRepository = new ProductWarehouseRepository{DataBaseContext = Sm_dataBaseContext},
                        ServiceWarehouseRepository = new ServiceWarehouseRepository{DataBaseContext = Sm_dataBaseContext}
                };
        }

        public static IEmployeeLogic GetEmployeeLogic()
        {
            return new EmployeeLogic
                {
                        EmployeeRepository = new EmployeeRepository {DataBaseContext = Sm_dataBaseContext},
                        SearchEmployeeDtoMapper = new SearchEmployeeDtoMapper(),
                        DisplayEmployeeDtoMapper = new DisplayEmployeeDtoMapper(),
                        RoleDataBaseRepository = new RoleDataBaseRepository {DataBaseContext = Sm_dataBaseContext},
                        UserDataBaseRepository = new UserDataBaseRepository{DataBaseContext = Sm_dataBaseContext},
                        AddEmployeeDtoMapper = new AddEmployeeDtoMapper(),
                        EditEmployeeDtoMapper = new EditEmployeeDtoMapper()
                };
        }

        public static IFactureLogic GetFactureLogic()
        {
            return new FactureLogic
            {
                FactureRepository = new FactureRepository { DataBaseContext = Sm_dataBaseContext },
                FactureViewMapper = new FactureViewMapper(),
                FactureMapper = new FactureMapper{
                    FactureItemMapper = new FactureItemMapper { TaxMapper = new TaxMapper() },
                },
                FactureItemMapper = new FactureItemMapper { TaxMapper = new TaxMapper() },
                FirmMapper = new FirmMapper(),
                ExchangeMapper = new ExchangeMapper(),
                FirmViewMapper = new FirmViewMapper(),
                CurrencyRepository = new CurrencyRepository { DataBaseContext = Sm_dataBaseContext },
                FirmAddMapper = new FirmAddMapper(),
            };
        }

        public static IEStoreManagementLogic GetEStoreLogic()
        {
            return new EStoreManagementLogic()
            {
                EStoreRepository = new EStoreRepository { DataBaseContext = Sm_dataBaseContext },
                EStoreMapper = new EStoreMapper(),
                AddEStoreDtoMapper = new AddEStoreDtoMapper(),
                EStoreWarehouseDtoMapper = new EStoreWarehouseDtoMapper(),
                EStoreCategoryRepository = new EStoreCategoryRepository() { DataBaseContext = Sm_dataBaseContext},
                AddEStoreCategoryDtoMapper = new AddEStoreCategoryDtoMapper(),
                EStoreCategoryDtoMapper = new EStoreCategoryDtoMapper(),
                EditEStoreCategoryDtoMapper = new EditEStoreCategoryDtoMapper(),
                AddOrEditEStoreShipmentTypeDtoMapper = new AddOrEditEStoreShipmentTypeDtoMapper(),
                EStoreShipmentTypeDtoMapper = new EStoreShipmentTypeDtoMapper(),
                EStoreShipmentTypeRepository = new EStoreShipmentTypeRepository() { DataBaseContext =  Sm_dataBaseContext}
           
            };
        }

        public static IStoreLogic GetStoreLogic()
        {
            return new StoreLogic
                {
                        DisplayStoreDtoMapper = new DisplayStoreDtoMapper(),
                        StoreRepository = new StoreRepository {DataBaseContext = Sm_dataBaseContext},
                        SimpleDisplayStoreDtoMapper = new SimpleDisplayStoreDtoMapper(),
                        DistributionRepository = new DistributionRepository{DataBaseContext = Sm_dataBaseContext},
                        StoreSellDtoMapper = new StoreSellDtoMapper(),
                        SoldItemDtoMapper = new SoldItemDtoMapper(),
                        CustomerRepository = new CustomerRepository{DataBaseContext = Sm_dataBaseContext},
                        ComplaintRepository = new ComplaintRepository{DataBaseContext = Sm_dataBaseContext},
                        ItemRepository = new ItemRepository{DataBaseContext = Sm_dataBaseContext},
                        ProductWarehouseRepository = new ProductWarehouseRepository{DataBaseContext = Sm_dataBaseContext},
                        ServiceWarehouseRepository = new ServiceWarehouseRepository{DataBaseContext = Sm_dataBaseContext}
                };
        }

        public static IEStoreDisplayLogic GetEStoreDisplayLogic()
        {
            return new EStoreDisplayLogic()
                       {
                           EStoreDisplayRepository = new EStoreDisplayRepository() { DataBaseContext = Sm_dataBaseContext },
                           EStoreDisplayItemDtoMapper = new EStoreDisplayItemDtoMapper(),
                           EStoreDisplayItemTypeDtoMapper = new EStoreDisplayItemTypeDtoMapper(),
                           EStoreDisplayDetailItemDtoMapper = new EStoreDisplayDetailItemDtoMapper()
                       };
        }
    }
}
