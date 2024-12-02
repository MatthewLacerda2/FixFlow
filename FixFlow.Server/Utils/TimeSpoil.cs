using Server.Models.DTO;

namespace Server.Utils;

/// <summary>
/// Utility class for when dealing with Date/Time in DateTime bullshit
/// </summary>
public static class TimeSpoil {

	public static bool DoTimeSpansIDsMatch(BusinessWeek truth, BusinessWeek tested) {

		if (truth.Sunday.Id != tested.Sunday.Id) {
			return false;
		}
		if (truth.Monday.Id != tested.Monday.Id) {
			return false;
		}
		if (truth.Tuesday.Id != tested.Tuesday.Id) {
			return false;
		}
		if (truth.Wednesday.Id != tested.Wednesday.Id) {
			return false;
		}
		if (truth.Thursday.Id != tested.Thursday.Id) {
			return false;
		}
		if (truth.Friday.Id != tested.Friday.Id) {
			return false;
		}
		if (truth.Saturday.Id != tested.Saturday.Id) {
			return false;
		}

		return true;

	}

	public static DateTime GetNextDayOfWeekWithTime(DayOfWeek dayOfWeek, TimeSpan timeOfDay) {
		DateTime now = DateTime.Now;
		int currentDayOfWeek = (int)now.DayOfWeek;
		int targetDayOfWeek = (int)dayOfWeek;

		// Calculate the difference in days between today and the target day
		int daysUntilTarget = targetDayOfWeek - currentDayOfWeek;

		// If the target day is today, but the time of day has already passed, we go to the next week
		if (daysUntilTarget == 0 && now.TimeOfDay > timeOfDay) {
			daysUntilTarget = 7;
		}
		else if (daysUntilTarget < 0) // If the target day is earlier in the week, go to next week
		{
			daysUntilTarget += 7;
		}

		// Get the next occurrence of the target day
		DateTime nextTargetDay = now.AddDays(daysUntilTarget).Date;

		// Return the DateTime with the specified time of day
		return nextTargetDay.Add(timeOfDay);
	}/*

	public static DateTime GetNextAvailableBusinessDayAtTime(DateTime date, BusinessWeek businessWeek) {

		for (int i = 1; i <= 7; i++) {
			var auxDate = date.AddDays(i);
			var bdayTimeSpan = businessWeek.GetTimeForDayOfWeek(auxDate.DayOfWeek);

			if (bdayTimeSpan.IsActive == false) {
				continue;
			}

			if (bdayTimeSpan.Start <= auxDate.TimeOfDay && bdayTimeSpan.Finish >= auxDate.TimeOfDay) {
				return auxDate;
			}
		}

		return GetNextAvailableBusinessDay(date, businessWeek);
	}

	public static DateTime GetNextAvailableBusinessDay(DateTime date, BusinessWeek businessWeek) {

		for (int i = 1; i <= 7; i++) {
			var auxDate = date.AddDays(i);
			var bdayTimeSpan = businessWeek.GetTimeForDayOfWeek(auxDate.DayOfWeek);

			if (bdayTimeSpan.IsActive == true) {
				DateOnly dOnly = new DateOnly(auxDate.Year, auxDate.Month, auxDate.Day);
				TimeOnly tOnly = new TimeOnly(auxDate.TimeOfDay.Hours, auxDate.TimeOfDay.Minutes, auxDate.TimeOfDay.Seconds);
				auxDate = new DateTime(dOnly, tOnly);
				return auxDate;
			}
		}

		return date.AddDays(7);
	}*/
}
