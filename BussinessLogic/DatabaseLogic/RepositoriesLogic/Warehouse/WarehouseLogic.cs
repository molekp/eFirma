using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BussinessLogic.DTOs.WarehouseDtos;
using BussinessLogic.DatabaseLogic.Repositories;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Warehouse;
using BussinessLogic.Mappers.Warehouse;
using BussinessLogic.Mappers.Warehouse.Product;
using Database.Entities.WarehouseEntities.Product;
using Database.Entities.WarehouseEntities.Service;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Warehouse
{
    public class WarehouseLogic : IWarehouseLogic
    {
        public IWarehouseRepository WarehouseRepository { get; set; }
        public IProductWarehouseRepository ProductWarehouseRepository { get; set; }
        public IServiceWarehouseRepository ServiceWarehouseRepository { get; set; }
        public IItemRepository ItemRepository { get; set; }
        public AddWarehouseDtoMapper AddWarehouseDtoMapper { get; set; }
        public DisplayWarehouseMapper DisplayWarehouseMapper { get; set; }
        public EditWarehouseDtoMapper EditWarehouseDtoMapper { get; set; }
        public DisplaySpecyficWarehouseMapper DisplaySpecyficWarehouseMapper { get; set; }
        public ManageProductWarehouseMapper ManageProductWarehouseMapper { get; set; }
        public ProductItemForManageProductWarehouseMapper ProductItemForManageProductWarehouseMapper { get; set; }

        public bool AddWarehouse(AddWarehouseDto a_addWarehouse)
        {
            var newWarehouse = AddWarehouseDtoMapper.MapDtoToEntity(a_addWarehouse);

            return WarehouseRepository.AddWarehouse(newWarehouse);
        }

        public IEnumerable<DisplayWarehouseDto> GetAllWarehouses()
        {
            var warehouses = WarehouseRepository.GetAll();
            return warehouses.Select(warehouse => DisplayWarehouseMapper.MapEntityToDto(warehouse)).ToList();
        }

        public EditWarehouseDto GetWarehouseForManage(int a_idWarehouse)
        {
            var warehouse = WarehouseRepository.Get(a_idWarehouse);
            if (null == warehouse)
            {
                return null;
            }
            var productDtos = new List<DisplaySpecyficWarehouse>();
            var freeProductWarehouses = ProductWarehouseRepository.GetAllFreeProductWarehouses().ToList();
            foreach (var productWarehouse in warehouse.ProductWarehouses)
            {
                productDtos.Add(DisplaySpecyficWarehouseMapper.MapEntityToDto(productWarehouse));
            }

            var selectLChoicesProw = new List<SelectListItem>
                {
                        new SelectListItem {Selected = true, Text = "Wybierz product warehouse", Value = "0"}
                };

            selectLChoicesProw.AddRange(freeProductWarehouses.Select(a_productWarehouse => new SelectListItem
                {
                        Selected = false,
                        Text = a_productWarehouse.Name,
                        Value = a_productWarehouse.IdProductWarehouse.ToString()
                }).ToList());

            var servicesWDtos = new List<DisplaySpecyficWarehouse>();
            var freeServiceWarehouses = ServiceWarehouseRepository.GetAllFreeServiceWarehouses();
            foreach (var serviceWarehouse in warehouse.ServiceWarehouses)
            {
                servicesWDtos.Add(DisplaySpecyficWarehouseMapper.MapEntityToDto(serviceWarehouse));
            }

            var selectLChoicesServiceW = new List<SelectListItem>
                {
                        new SelectListItem {Selected = true, Text = "Wybierz service warehouse", Value = "0"}
                };
            selectLChoicesServiceW.AddRange(freeServiceWarehouses.Select(a_serviceWarehouse => new SelectListItem
                {
                        Selected = false,
                        Text = a_serviceWarehouse.Name,
                        Value = a_serviceWarehouse.IdServiceWarehouse.ToString()
                }).ToList());

            return EditWarehouseDtoMapper.MapEntityToDto(warehouse, productDtos, servicesWDtos, selectLChoicesProw,
                                                         selectLChoicesServiceW);
        }

        public bool EditWarehouse(EditWarehouseDto a_editWarehouse)
        {
            var warehouse = WarehouseRepository.Get(a_editWarehouse.IdWarehouse);
            if (null == warehouse)
            {
                return false;
            }

            warehouse.Name = a_editWarehouse.Name;
            warehouse.Address = a_editWarehouse.Address;

            if (a_editWarehouse.ChoiceIdProductWarehouse != 0)
            {
                var productWarehouse = ProductWarehouseRepository.Get(a_editWarehouse.ChoiceIdProductWarehouse);
                if (null != productWarehouse)
                {
                    warehouse.ProductWarehouses.Add(productWarehouse);
                }
            }

            if (a_editWarehouse.ChoiceIdServiceWarehouse != 0)
            {
                var serviceWarehouse = ServiceWarehouseRepository.Get(a_editWarehouse.ChoiceIdServiceWarehouse);
                if (null != serviceWarehouse)
                {
                    warehouse.ServiceWarehouses.Add(serviceWarehouse);
                }
            }

            if (null != a_editWarehouse.CreateProductWarehouseName &&
                false == a_editWarehouse.CreateProductWarehouseName.Equals(""))
            {
                warehouse.ProductWarehouses.Add(new ProductWarehouse
                    {
                            Name = a_editWarehouse.CreateProductWarehouseName,
                            ProductItems = new List<ProductItem>()
                    });
            }

            if (null != a_editWarehouse.CreateServiceWarehouseName &&
                false == a_editWarehouse.CreateServiceWarehouseName.Equals(""))
            {
                warehouse.ServiceWarehouses.Add(new ServiceWarehouse
                    {
                            Name = a_editWarehouse.CreateServiceWarehouseName,
                            ServiceItems = new List<ServiceItem>()
                    });
            }

            return WarehouseRepository.EditWarehouse(warehouse);
        }

        public bool RemoveProductWarehouseFromWarehouse(int a_idWarehouse, int a_idProductWarehouse)
        {
            var warehouse = WarehouseRepository.Get(a_idWarehouse);
            if (null == warehouse)
            {
                return false;
            }

            var productWarehouse = ProductWarehouseRepository.Get(a_idProductWarehouse);
            if (null == productWarehouse)
            {
                return false;
            }

            return WarehouseRepository.RemoveProductWarehouseFromWarehouse(warehouse, productWarehouse);
        }

        public bool RemoveServiceWarehouseFromWarehouse(int a_idWarehouse, int a_idServiceWarehouse)
        {
            var warehouse = WarehouseRepository.Get(a_idWarehouse);
            if (null == warehouse)
            {
                return false;
            }

            var serviceWarehouse = ServiceWarehouseRepository.Get(a_idServiceWarehouse);
            if (null == serviceWarehouse)
            {
                return false;
            }

            return WarehouseRepository.RemoveServiceWarehouseFromWarehouse(warehouse, serviceWarehouse);
        }

        public ManageProductWarehouseDto GetProductWarehouseForManage(int a_idProductWarehouse, int a_idWarehouse)
        {
            var warehouse = WarehouseRepository.Get(a_idWarehouse);
            if (warehouse == null) return null;
            var productWarehouse = warehouse.ProductWarehouses.FirstOrDefault(x => x.IdProductWarehouse == a_idProductWarehouse);
            if (null == productWarehouse) return null;

            var products =
                    productWarehouse.ProductItems.Select(x => ProductItemForManageProductWarehouseMapper.MapEntityToDto(x))
                             .ToList();

            var mapped = ManageProductWarehouseMapper.MapEntityToDto(productWarehouse, products);
            mapped.IdWarehouse = a_idWarehouse;
            return mapped;
        }

        public bool EditProductWarehouse(ManageProductWarehouseDto a_manageProductWarehouse)
        {
            var warehouse = ProductWarehouseRepository.Get(a_manageProductWarehouse.IdProductWarehouse);
            if (warehouse == null)
            {
                return false;
            }

            warehouse.Name = a_manageProductWarehouse.Name;
            if (a_manageProductWarehouse.AddProductItem != 0)
            {
                warehouse.ProductItems.Add(ItemRepository.GetProduct(a_manageProductWarehouse.AddProductItem));
            }
            return ProductWarehouseRepository.Edit(warehouse);
        }

        public bool RemoveProductItemFromProductWarehouse(int a_itemid, int a_idProductWarehouse)
        {
            var productWarehouse = ProductWarehouseRepository.Get(a_idProductWarehouse);
            if (null == productWarehouse)
            {
                return false;
            }

            var product = productWarehouse.ProductItems.FirstOrDefault(x => x.IdItem == a_itemid);
            if (null == product)
            {
                return false;
            }

            if (false == productWarehouse.ProductItems.Remove(product))
            {
                return false;
            }

            if (false == ItemRepository.Remove(product))
            {
                return false;
            }

            return ProductWarehouseRepository.Edit(productWarehouse);
        }
    }
}