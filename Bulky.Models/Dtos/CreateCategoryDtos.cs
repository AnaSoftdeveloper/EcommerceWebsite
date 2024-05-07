using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Bulky.Models.Dtos
{
    public class CreateCategoryDtos
    {
        [Required]
        [MaxLength(30)]
        [DisplayName("Category name")]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100 , ErrorMessage ="Diplay Order must be between 1-100")]
        public int DisplayOrder { get; set; }
    }
}
