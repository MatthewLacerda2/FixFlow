using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Server.Models.Appointments;

public class UpdateAptContact {

	[Required]
	public string Id { get; set; }

	[Required]
	public DateTime dateTime { get; set; }

	/// <summary>
	/// Whether or not we did Contact the Customer as we were supposed to
	/// </summary>
	[Required]
	public bool Done { get; set; }

	public UpdateAptContact() : this("id", DateTime.Now, false) { }

	public UpdateAptContact(string _Id, DateTime _dateTime, bool _beenDone) {
		Id = _Id;
		dateTime = _dateTime;
		Done = _beenDone;
	}
}
