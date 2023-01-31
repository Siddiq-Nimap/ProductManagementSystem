using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CrudOperations.Models
{
    public class Catagory
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Catagory Name is required")]
        [DisplayName("Catagory Name")]
        public string CatagoryName { get; set; }

        public bool IsActive { get; set; }
    }
}