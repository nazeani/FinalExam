﻿using System.ComponentModel.DataAnnotations;

namespace LumiaMVC1.ViewModels
{
    public class AdminLoginVM
    {
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsPersistent { get; set; }
    }
}
