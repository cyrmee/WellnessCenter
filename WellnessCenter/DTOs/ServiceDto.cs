using System.ComponentModel.DataAnnotations;

namespace WellnessCenter.DTOs;

public class ServiceDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    [DataType(DataType.Currency)]
    public double Price { get; set; }
    public required string Description { get; set; }

}