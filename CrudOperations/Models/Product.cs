using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudOperations.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Product Generic Name is required ")]
        [DisplayName("Generic Name")]
        public string ProductGenericName { get; set; }


        [Required(ErrorMessage = "Product Description is required ")]
        [DisplayName("Description")]
        public string ProductDescription { get; set; }


        [Required(ErrorMessage = "Title is required ")]
        [DisplayName("Title")]
        public string ProductTitle { get; set; }


        [Required(ErrorMessage = "Product Weight is required ")]
        [DisplayName("Weight")]
        public int ProductWeight { get; set; }


        [Required(ErrorMessage = "Product price is required ")]
        [DisplayName("Price")]
        public int ProductPrice { get; set; }


        [Required(ErrorMessage = "Please Upload Image Name is required ")]
        [DisplayName("Choose Image")]

        public string ImagePath { get; set; }

        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        [ForeignKey("Logins")]
        public int UserID { get; set; }

        public Logins Logins { get; set; }

    }
}