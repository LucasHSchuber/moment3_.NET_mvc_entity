using System.ComponentModel.DataAnnotations;

namespace moment3_mvc_entity.Models;

public class ErrorViewModel
{
    //properties
    [Required]
    [MinLength(2)]
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
