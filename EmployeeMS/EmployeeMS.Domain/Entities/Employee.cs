using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace EmployeeMS.Domain.Entities
{
    public class Employee
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Gender Gender { get; set; }
        public byte[] Image { get; set; }

        //public virtual User User
        //{
        //    get { return _user; }
        //    set
        //    {
        //        _user = value;
        //        UserId = value.UserId.ToString();
        //    }
        //}
    }
    public enum Gender
    {
        Male,
        Female,
        Other
    }
}