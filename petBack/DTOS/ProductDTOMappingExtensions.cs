

using petBack.DTOS.Mappings;
using petBack.Models;

namespace petBack.DTOS
{
    public static class ProductDTOMappingExtensions
    {
        public static ProductDTO? ToProductDTO(this Product product)
        {
            if (product is null)
            {
                return null;
            }

            return new ProductDTO
            {
                ProductId = product.ProductId,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                ImagemUrl = product.ImagemUrl
            };
        }




        public static Product? ToProduct(this ProductDTO productDto)
        {
            if (productDto is null)
            {
                return null;
            }

            return new Product
            {
                ProductId = productDto.ProductId,
                Title = productDto.Title,
                Description = productDto.Description,
                Price = productDto.Price,
                ImagemUrl = productDto.ImagemUrl
            };
        }




        public static IEnumerable<ProductDTO> ToProductDTOList(this IEnumerable<Product> products)
        {

            if (products is null || !products.Any())
            {
                return new List<ProductDTO>();
            }


            return products.Select(ag => new ProductDTO
            {
                ProductId = ag.ProductId,
                Title = ag.Title,
                Description = ag.Description,
                Price = ag.Price,
                ImagemUrl = ag.ImagemUrl

            }).ToList();

        }






    }
}