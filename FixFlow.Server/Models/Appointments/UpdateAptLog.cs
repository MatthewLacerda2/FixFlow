using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Server.Models.Appointments;

public class UpdateAptLog {

	[Required]
	public string Id { get; set; }

	public DateTime dateTime { get; set; } = DateTime.UtcNow;

	public string? Service { get; set; }

	[Required]
	public float Price { get; set; }

	public string? Description { get; set; }

	public UpdateAptLog(string Id, DateTime dateTime, string? service, float price, string? description) {
		this.Id = Id;
		this.dateTime = dateTime;
		Price = price;
		Service = service;
		Description = description;
	}
}
