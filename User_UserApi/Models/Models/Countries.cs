﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace User_UserApi.Models
{
    public class Countries
    {
        [Key]
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        
    }
}