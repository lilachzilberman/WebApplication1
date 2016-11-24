namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Products
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public virtual int SupplierId { get; set; }

        [ForeignKey("SupplierId")]
        public virtual Supplier Supplier { get; set; }

        [Required]
        public virtual int AnimalsId { get; set; }

        [ForeignKey("AnimalsId")]
        public virtual Animals Animals { get; set; }

        [Required]
        public virtual int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Categories Category { get; set; }

        [StringLength(100)]
        [Display(Name = "Picture Path")]
        public string PicturePath { get; set; }
    }
}
