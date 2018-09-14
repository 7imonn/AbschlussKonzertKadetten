﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AbschlussKonzertKadetten.Models
{
    public class ViewModelOrder
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(100)]
        public string Bemerkung { get; set; }
        [Required]
        public List<ViewModelTicket> Tickets { get; set; }
    }
}