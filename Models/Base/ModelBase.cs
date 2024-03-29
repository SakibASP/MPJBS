﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPJBS.Models.Base
{
    public class ModelBase
    {
        // Specify the time zone for Bangladesh
        private static readonly TimeZoneInfo bdTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Bangladesh Standard Time");
        private DateTime? _modifiedDate;

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string? CreatedBy { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.Local, bdTimeZone);
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate {
            get => _modifiedDate;
            set => _modifiedDate = value.HasValue ? TimeZoneInfo.ConvertTime((DateTime)value, TimeZoneInfo.Local, bdTimeZone) : null; }
    }
}
