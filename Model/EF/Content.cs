namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Content")]
    public partial class Content
    {
        public int ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Content_Name", ResourceType = typeof(StaticResource.Resources))]
        public string Name { get; set; }

        [StringLength(250)]
        [Display(Name = "Content_MetaTitle", ResourceType = typeof(StaticResource.Resources))]
        public string MetaTitle { get; set; }

        [StringLength(50)]
        [Display(Name = "Content_Description", ResourceType = typeof(StaticResource.Resources))]
        public string Description { get; set; }

        [StringLength(250)]
        [Display(Name = "Content_Image", ResourceType = typeof(StaticResource.Resources))]
        public string Image { get; set; }

        [Display(Name = "Content_CategoryID", ResourceType = typeof(StaticResource.Resources))]
        public long? CategoryID { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Content_Details", ResourceType = typeof(StaticResource.Resources))]
        public string Details { get; set; }

        [Display(Name = "Content_Warranty", ResourceType = typeof(StaticResource.Resources))]
        public int? Warranty { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(250)]
        [Display(Name = "Content_MetaKeywords", ResourceType = typeof(StaticResource.Resources))]
        public string MetaKeywords { get; set; }

        [StringLength(250)]
        [Display(Name = "Content_MetaDescription", ResourceType = typeof(StaticResource.Resources))]
        public string MetaDescription { get; set; }

        [Display(Name = "Content_Status", ResourceType = typeof(StaticResource.Resources))]
        public bool Status { get; set; }

        [Display(Name = "Content_TopHot", ResourceType = typeof(StaticResource.Resources))]
        public DateTime? TopHot { get; set; }

        [Display(Name = "Content_ViewCount", ResourceType = typeof(StaticResource.Resources))]
        public int? ViewCount { get; set; }

        public string Language { get; set; }

        [StringLength(500)]
        public string Tags { get; set; }
    }
}
