﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CoreLogin_Domain.Converters.DTO
{
  public sealed class ModuleResultDTO
  {
    [Key]
    [JsonIgnore]
    public int Id { get; set; }

    [DefaultValue("Administrative")]
    public string Name { get; set; }

    [DefaultValue("Example of Module Name - Administrative")]
    public string? Description { get; set; }

    [DefaultValue(true)]
    public bool Active { get; set; } = true;

    [DefaultValue(new string[] { "Administrative", "Users" })]

    public IEnumerable<string>? Groups { get; set; }

    public DateTime Created_at { get; set; } = DateTime.Now;

    public DateTime Updated_at { get; set; } = DateTime.Now;

  }

  public sealed class ModuleRequestDTO
  {
    [Required(ErrorMessage = "This field is required")]
    [MaxLength(40, ErrorMessage = "This field must have a maximum of 40 characters")]
    [MinLength(3, ErrorMessage = "This field must have a minimum of 3 characters")]
    [DefaultValue("Administrative")]
    public string Name { get; set; }

    [DefaultValue("Example of Module Name - Administrative")]
    public string? Description { get; set; }

    [DefaultValue(true)]
    [ReadOnly(true)]
    public bool Active { get; set; } = true;

    [DefaultValue(new string[] { "Administrators" })]
    public IEnumerable<string>? Groups { get; set; }

  }

}
