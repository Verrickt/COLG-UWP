﻿using System;
using System.Collections.Generic;

namespace Colg_UWP.Model
{
    public class User
    {
        public string UserName { get; set; }
        public string ID { get; set; }
        public string HomeUrl { get; set; }
        public string Avatar { get; set; }
        public List<Credit> Credits { get; set; }
        public DateTimeOffset? TimeRegisted { get; set; }
        public bool IsActive { get; set; }
        public Credential Credential { get; set; }
        public string FormHash { get; set; }
        public UserGroup UserGroup { get; set; }

        public User()
        {
            Credits = new List<Credit>();
            Credential = new Credential();
        }

        public override string ToString()
        {
            return
                $"UserName:{UserName} ID:{ID} Avatar:{Avatar}";
        }
    }

    public class Credit
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }

    public class UserGroup
    {
        public string Title { get; set; }
        public CreditRange CreditRange { get; set; }
        public int ReadPermissionLevel { get; set; }
    }

    public class CreditRange
    {
        public int? LowerBound { get; set; }
        public int? UpperBound { get; set; }
    }
}