﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MyWCFServices.RealNorthwindEntities;
using MyWCFServices.RealNorthwindLogic;

namespace MyWCFServices.RealNorthwindService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class ProductService : IProductService
    {
        ProductLogic productLogic = new ProductLogic();

        public Product GetProduct(int id)
        {
            ProductLogic productLogic = new ProductLogic();
            ProductEntity productEntity = productLogic.GetProduct(id);
            if (productEntity == null)
            {
                //throw new Exception("No product found with id " + id);
                if (id != 999)
                    throw new FaultException<ProductFault>(new ProductFault("No product found with id " + id), "Product Fault");
                else
                    throw new Exception("Test Exception");
            }

            Product product = new Product();
            TranslateProductEntityToProductContractData(productEntity, product);
            return product;
        }

        public bool UpdateProduct(Product product)
        {
            ProductLogic productLogic = new ProductLogic();
            ProductEntity productEntity = new ProductEntity();
            TranslateProductContractDataToProductEntity(product, productEntity);
            return productLogic.UpdateProduct(productEntity);
        }

        private void TranslateProductEntityToProductContractData(ProductEntity productEntity,Product product)
        {
            product.ProductID = productEntity.ProductID;
            product.ProductName = productEntity.ProductName;
            product.QuantityPerUnit = productEntity.QuantityPerUnit;
            product.UnitPrice = productEntity.UnitPrice;
            product.Discontinued = productEntity.Discontinued;
        }

        private void TranslateProductContractDataToProductEntity(Product product,ProductEntity productEntity)
        {
            productEntity.ProductID = product.ProductID;
            productEntity.ProductName = product.ProductName;
            productEntity.QuantityPerUnit = product.QuantityPerUnit;
            productEntity.UnitPrice = product.UnitPrice;
            productEntity.Discontinued = product.Discontinued;
        }
    }
}
