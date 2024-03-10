using System;
using System.Text.Json.Serialization;

namespace TextInterpolationService;

public class DomainDatasource
{
	public long Number { get; set; }

	public string? Name { get; set; } 

	public DateTime Date { get; set; } 
}
