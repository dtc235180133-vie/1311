using System.ComponentModel.DataAnnotations;

namespace _1311.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Phải nhập tên danh mục")]
        [StringLength(100)]
        public string CategoryName { get; set; }

        // Navigation property: 1 Category có nhiều Product
        public ICollection<Product>? Products { get; set; }
    }
}