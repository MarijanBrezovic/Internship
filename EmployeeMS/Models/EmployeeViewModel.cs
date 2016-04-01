using EmployeeMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeMS.Models
{
    public class EmployeeViewModel
    {
        [Required(ErrorMessage="Please enter your name")]
        [DisplayName("Full Name")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Please Enter The BirthDay")]
        [DisplayName("Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required(ErrorMessage = "You must chose")]
        [DisplayName("Chose a picture")]
        public byte[] Image { get; set; }

    }
}