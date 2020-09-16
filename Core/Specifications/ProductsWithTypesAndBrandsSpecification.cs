using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
        : base (x =>
              (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search))&&
              (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
              (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)
        
        )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderByDescending(x => x.Id);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex -1),
            productParams.PageSize);
            if(!string.IsNullOrEmpty(productParams.Sort)){
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    case "nameAZ":
                        AddOrderBy(p => p.Name);
                        break;
                    default:
                        AddOrderByDescending(x => x.Id);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) 
        : base(x => x.Id == id)
        {
           AddInclude(x => x.ProductType);
           AddInclude(x => x.ProductBrand); 
        }
    }
}