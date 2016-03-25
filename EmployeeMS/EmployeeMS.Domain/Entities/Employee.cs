﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EmployeeMS.Domain.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }

        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public byte[] Image { get; set; }
    }
    public enum Gender
    {
        Male,
        Female,
        Other
    }
}