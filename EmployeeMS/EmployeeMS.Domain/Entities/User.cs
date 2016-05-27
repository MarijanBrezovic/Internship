using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace EmployeeMS.Domain.Entities
{
    public class User
    {
        #region Fields
        private ICollection<Claim> _claims;
        private ICollection<ExternalLogin> _externalLogins;
        private ICollection<Role> _roles;
        private ICollection<Employee> _employees;
        #endregion

        #region Scalar Properties
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }
        public string UserName { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string SecurityStamp { get; set; }
        #endregion

        #region Navigation Properties
        public virtual ICollection<Claim> Claims
        {
            get { return _claims ?? (_claims = new List<Claim>()); }
            set { _claims = value; }
        }

        public virtual ICollection<ExternalLogin> Logins
        {
            get
            {
                return _externalLogins ??
                    (_externalLogins = new List<ExternalLogin>());
            }
            set { _externalLogins = value; }
        }

        public virtual ICollection<Role> Roles
        {
            get { return _roles ?? (_roles = new List<Role>()); }
            set { _roles = value; }
        }
        public virtual ICollection<Employee>Employees
        {
            get { return _employees ?? (_employees = new List<Employee>()); }
            set { _employees = value; }
           
        }
        #endregion
    }
}