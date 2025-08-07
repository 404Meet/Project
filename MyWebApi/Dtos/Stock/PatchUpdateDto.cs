using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyWebApi.Dtos.Stock
{
public class PatchUpdateDto
{
    [Required]
    [MaxLength(10,ErrorMessage ="Symbol cannot be over 10 overs characters")]
    public string? symbol { get; set; }
    [Required]
    [MaxLength(10,ErrorMessage ="Company Name cannot be over 10 overs characters")]
    public string? CompanyName { get; set; }
    public decimal? Purchase { get; set; }
    public decimal? Lastdiv { get; set; }
    [Required]
    [MaxLength(10,ErrorMessage ="Industry cannot be over 10 overs characters")]
    public string? Industry { get; set; }
    public long? MarketCap { get; set; }
}

}