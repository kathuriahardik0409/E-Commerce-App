using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace E_Commerce.Models
{
    public class Product
    {
        [Key]
        public int Id{get;set;}

        [Required]
        public string Title{get;set;}
        public string Description{get;set;}

        [Required]
        public string ISBN{get;set;}
        [Required]
        public string Author{get;set;}

        [Required]
        [Display(Name = "Display Price")]
        public double ListPrice{get;set;}

        [Required]
        [Display(Name = "Price for 1-50")]
        public double Price{get;set;}

        [Required]
        [Display(Name = "Price for 50+")]
        public double Price50{get;set;}

        [Required]
        [Display(Name = "Price for 100+")]
        public double Price100{get;set;}

    }
}