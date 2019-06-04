﻿using System;
using System.Collections.Generic;

namespace DapperExtension
{
    public partial class Role
    {
        public Role()
        {
            RolePermissionMapping = new HashSet<RolePermissionMapping>();
            UserRoleMapping = new HashSet<UserRoleMapping>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public string CreateByUserName { get; set; }
        public DateTime UpdateTime { get; set; }
        public string UpdateByUserName { get; set; }
        public bool IsSuperAdministrator { get; set; }
        public bool IsEnable { get; set; }
        public bool IsForbidden { get; set; }
        public int UpdateByUserId { get; set; }
        public int CreateByUserId { get; set; }

        public virtual User CreateByUser { get; set; }
        public virtual User UpdateByUser { get; set; }
        public virtual ICollection<RolePermissionMapping> RolePermissionMapping { get; set; }
        public virtual ICollection<UserRoleMapping> UserRoleMapping { get; set; }
    }
}
