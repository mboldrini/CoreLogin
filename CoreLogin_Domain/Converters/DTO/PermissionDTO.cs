﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CoreLogin_Domain.Converters.DTO
{
  public sealed class PermissionResultDTO
  {
    [Required]
    [DefaultValue("Allow")]
    public string Type { get; set; }

    [Required]
    [DefaultValue("Create")]
    public string Operation { get; set; }
  }

  public sealed class PermissionRequestDTO
  {
    [Required]
    [DefaultValue("Allow")]
    public string Type { get; set; }

    [Required]
    [DefaultValue("Create")]
    public string Operation { get; set; }
  }
}
