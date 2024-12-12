using Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Application.DTO
{
    public sealed class StockMovementsDTO : BaseDTO
    {
        [Required(ErrorMessage = "A quantidade é obrigatório")]
        [Range(1, 999999999, ErrorMessage = "A quantidade deve ser entre 1 a 999.999.999")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "O produto é obrigatório")]
        public int IdProduct { get; set; }

        public string MovementType { get; set; } = string.Empty;
        public DateTime MovementDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string UnitType { get; set; } = string.Empty;


        public List<StockMovementsDTO> ListStockMovements { get; set; } = new List<StockMovementsDTO>();
        public List<ProductDTO> ListProduct { get; set; } = new List<ProductDTO>();

        public void ValidatedDTO() => Validate(this);

        public IEnumerable<SelectListItem> GetListProduct()
        {
            List<SelectListItem> list = new List<SelectListItem>();

            this.ListProduct.ForEach(item => {
                list.Add(new SelectListItem
                {
                    Text = item.Name,
                    Value = $"{item.Id}"
                });
            });

            return list;
        }
    }
}
