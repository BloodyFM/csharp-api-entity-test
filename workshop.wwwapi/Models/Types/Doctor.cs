﻿using System.ComponentModel.DataAnnotations.Schema;

namespace workshop.wwwapi.Models.Types;

[Table("doctor")]
public class Doctor
{
    [Column("id")]
    public int Id { get; set; }
    [Column("full_name")]
    public string FullName { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
}
