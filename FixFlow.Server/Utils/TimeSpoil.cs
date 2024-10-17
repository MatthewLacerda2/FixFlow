namespace Server.Utils;

/// <summary>
/// Utility class for when dealing with Date/Time in DateTime bullshit
/// </summary>
public class TimeSpoil {

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
	}
}
