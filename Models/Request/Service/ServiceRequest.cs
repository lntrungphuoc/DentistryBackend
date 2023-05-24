﻿using System.ComponentModel.DataAnnotations;

namespace AppDentistry.Models.Request.Service
{
    public class ServiceRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Information { get; set; }
        public float Fee { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
