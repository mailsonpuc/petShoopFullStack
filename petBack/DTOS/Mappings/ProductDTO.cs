
using System.ComponentModel.DataAnnotations;


namespace petBack.DTOS.Mappings
{
    public class ProductDTO
    {
        [Key]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Titulo é obrigatorio")]
        public string? Title { get; set; }


        [Required(ErrorMessage = "Descrição é obrigatorio")]
        public string? Description { get; set; }


        [Required(ErrorMessage = "Preço é obrigatorio")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "ImagemUrl é obrigatorio")]
        public string? ImagemUrl { get; set; }

    }
}