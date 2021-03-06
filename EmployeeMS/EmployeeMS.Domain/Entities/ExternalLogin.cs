﻿using MongoDB.Bson;
using System;

namespace EmployeeMS.Domain.Entities
{
    public class ExternalLogin
    {
        
        private User _user;
        public virtual int Id { get; set; }
        #region Scalar Properties
        public virtual string LoginProvider { get; set; }
        public virtual string ProviderKey { get; set; }
        
        public virtual ObjectId UserId { get; set; }
        #endregion

        #region Navigation Properties
        public virtual User User
        {
            get { return _user; }
            set
            {
                _user = value;
                UserId = value._id;
            }
        }
        #endregion
    }
}