using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BussinessLogic.DTOs;
using BussinessLogic.DTOs.WarehouseDtos.Supplies;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories;
using BussinessLogic.DatabaseLogic.Repositories.Interfaces.WarehouseRepositories.SupplyRepositories;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces;
using BussinessLogic.DatabaseLogic.RepositoriesLogic.Interfaces.Warehouse.Supplies;
using BussinessLogic.Mappers;
using BussinessLogic.Mappers.Warehouse;
using BussinessLogic.Mappers.Warehouse.Product;
using BussinessLogic.Mappers.Warehouse.Supplies;
using Database.Entities.WarehouseEntities;

namespace BussinessLogic.DatabaseLogic.RepositoriesLogic.Warehouse.Supplies
{
    public class SupplyLogic : ISupplyLogic
    {
        public IProductTypeRepository ProductTypesRepository { get; set; }
        public IWarehouseRepository WarehouseRepository { get; set; }
        public ISaleTypeRepository SaleTypesRepository { get; set; }
        public IAttributeRepository AttributesRepository { get; set; }
        public ISupplyRepository SupplyRepository { get; set; }
        public IItemRepository ItemRepository { get; set; }
        public AttributeTypeMapper AttributeTypeMapper { get; set; }
        public AttributeMapper AttributeMapper { get; set; }
        public SupplyAddMapper SupplyAddMapper { get; set; }
        public SupplyViewMapper SupplyViewMapper { get; set; }
        public ProductAddMapper ProductAddMapper { get; set; }
        public ProductMapper ProductMapper { get; set; }
        public SupplyMapper SupplyMapper { get; set; }
        public SupplyEditMapper SupplyEditMapper { get; set; }
        public ProductWarehousesMapper ProductWarehousesMapper { get; set; }
        public WarehousesMapper WarehousesMapper { get; set; }

        public List<SelectListItem> GetProductTypes(int a_idProductType)
        {
            var types = ProductTypesRepository.GetAll();
            var selectList = new List<SelectListItem>();
            foreach (var productType in types)
            {
                if (productType.IdItemType == a_idProductType)
                {
                    selectList.Add(new SelectListItem { Selected = true, Text = productType.Name, Value = productType.IdItemType.ToString() });
                }
                else
                {
                    selectList.Add(new SelectListItem
                                       {
                                               Selected = false,
                                               Text = productType.Name,
                                               Value = productType.IdItemType.ToString()
                                       });
                }
            }
            return selectList;
        }

        public List<SelectListItem> GetSaleTypes()
        {
            var types = SaleTypesRepository.GetAllSaleTypes();
            var selectList = new List<SelectListItem>();
            foreach (var saleType in types)
            {
                selectList.Add(new SelectListItem
                    {
                        Selected = false,
                        Text = saleType.Name,
                        Value = saleType.IdSaleType.ToString()
                    });
                
            }
            return selectList;
        }

        public List<AttributeTypeDto> GetAttributeTypes(int a_idProductType)
        {
            var list = new List<AttributeTypeDto>();
            if(a_idProductType < 1)
            {
                return list;
            }
            var productType = ProductTypesRepository.GetProductType(a_idProductType);
            if(productType == null)
            {
                return list;
            }
            var attributes = productType.AttributeTypes;
            foreach (var attr in attributes)
            {
                list.Add(AttributeTypeMapper.MapEntityToDto(attr));
            }
            return list;
        }

        public List<AttributeDto> GetAttributes(int a_idProductType)
        {
            var list = new List<AttributeDto>();
            if (a_idProductType < 1)
            {
                return list;
            }
            var attributeTypes = GetAttributeTypes(a_idProductType);
            foreach (var attrType in attributeTypes)
            {
                list.Add(new AttributeDto{ IdAttributeType = attrType.IdAttributeType, Name = attrType.Name});
            }
            return list;
        }

        public ProductDto StoreProduct(int a_idProduct)
        {
            var productEntity = SupplyRepository.GetProduct(a_idProduct);
            if(productEntity == null)
            {
                return null;
            }
            var productDto = ProductMapper.MapEntityToDto(productEntity);
            productDto.Attributes = productEntity.Attributes!= null? AttributeMapper.MapEntitiesToDtos(productEntity.Attributes) : new List<AttributeDto>();
            return productDto;
        }
        
        public int StoreProduct(ProductDto a_productDto)
        {
            var productEntity = ProductMapper.MapDtoToEntity(a_productDto);
            productEntity.Attributes = new List<ValueAttributeType>();
            if(a_productDto.Attributes!= null)
                foreach (var attr in a_productDto.Attributes)
                {
                    productEntity.Attributes.Add(AttributeMapper.MapDtoToEntity(attr));
                }
            var productItem = ItemRepository.GetProduct(productEntity.IdItem);
            if (ItemRepository.StoreProduct(productEntity, productItem))
            {
                if(WarehouseRepository.AddProductToProductWarehouse(productItem, a_productDto.IdProductWarehouse))
                {
                    return SupplyRepository.StoreProduct(productEntity);
                }
            }
            return 0;
        }

        public SupplyViewDto ViewSupply(int a_idSupply)
        {
            var supply = SupplyRepository.GetSupply(a_idSupply);
            if(supply == null)
            {
                return null;
            }
            return SupplyViewMapper.MapEntityToDto(supply);
        }

        public bool AddSupply(SupplyAddDto a_addSupplyDto)
        {

            return SupplyRepository.AddSupply(SupplyAddMapper.MapDtoToEntity(a_addSupplyDto));
        }

        public List<SupplyDto> GetAllSupplies()
        {
            var supplyDtos = new List<SupplyDto>();
            SupplyRepository.GetAllSupplies().ToList().ForEach(i => supplyDtos.Add(SupplyMapper.MapEntityToDto(i)));
            return supplyDtos;
        }

        public SupplyEditDto GetSupply(int a_idSupply)
        {
            var entity = SupplyRepository.GetSupply(a_idSupply);
            if(entity == null)
            {
                return null;
            }
            return SupplyEditMapper.MapEntityToDto(entity);
        }

        public bool SaveSupply(SupplyEditDto a_supplyEditDto)
        {
            return SupplyRepository.SaveSupply(SupplyEditMapper.MapDtoToEntity(a_supplyEditDto));
        }

        public bool RemoveSupply(int a_idSupply)
        {
            return SupplyRepository.RemoveSupply(SupplyRepository.GetSupply(a_idSupply));
        }

        public bool SendSupply(int a_idSupply)
        {
            return SupplyRepository.SendSupply(a_idSupply);
        }

        public int AddProduct(ProductAddDto a_productAddDto)
        {
            return SupplyRepository.AddProduct(ProductAddMapper.MapDtoToEntity(a_productAddDto, ProductTypesRepository, SaleTypesRepository));
        }

        public bool AddProductToSupply(int a_idSupply, int a_idProduct)
        {
            if(SupplyRepository.AddProductToSupply(a_idSupply, a_idProduct))
            {
                return SupplyRepository.ReadyToSendSupply(a_idSupply);
            }
            return false;
        }

        public bool RemoveProduct(int a_idProduct, int a_idSupply)
        {
            RemoveProductFromSupply(a_idProduct, a_idSupply);
            var supply = SupplyRepository.GetSupply(a_idSupply);
            if(supply == null)
            {
                return SupplyRepository.RemoveProduct(a_idProduct);
            }
            if(supply.ProductItems.Count == 0)
            {
                SupplyRepository.setState(1, supply);
            }
            return SupplyRepository.RemoveProduct(a_idProduct);
        }

        public List<SelectListItem> GetWarehousesWithProducts(int a_idWarehouse)
        {
            var ws = WarehousesMapper.MapEntitiesToWarehousesWithProductDtos(WarehouseRepository.GetAll());
            var selectList = new List<SelectListItem>();
            foreach (var w in ws)
            {
                bool flag = w.IdWarehouse == a_idWarehouse;
                selectList.Add(new SelectListItem
                {
                    Selected = flag,
                    Text = w.Name,
                    Value = w.IdWarehouse.ToString()
                });
            }
            return selectList;
        }

        public List<SelectListItem> GetProductWarehouses(int a_idWarehouse, int a_idProductWarehouse)
        {
            var w = WarehouseRepository.Get(a_idWarehouse);
            var selectList = new List<SelectListItem>();
            if (w != null)
            {
                foreach (var pw in w.ProductWarehouses)
                {
                    var pwDto = ProductWarehousesMapper.MapEntityToDto(pw);
                    bool flag = pwDto.IdProductWarehouse == a_idProductWarehouse;
                    selectList.Add(new SelectListItem
                                       {
                                               Selected = flag,
                                               Text = pwDto.Name,
                                               Value = pwDto.IdProductWarehouse.ToString()
                                       });
                }
            }
            return selectList;
        }

        public bool RemoveProductFromSupply(int a_idProduct, int a_idSupply)
        {
            var supply = SupplyRepository.GetSupply(a_idSupply);
            var product = SupplyRepository.GetProduct(a_idProduct);
            if(supply == null || product == null)
            {
                return false;
            }
            return SupplyRepository.RemoveProductFromSupply(supply, product);
        }
    }

}