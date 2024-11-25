import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

class DateTimeUtils {
  void debug() {
    print("debug");
  }

  static DateTime getNextDayAtMidnight(int targetWeekday) {
    final DateTime now = DateTime.now();
    targetWeekday = targetWeekday.clamp(1, 7);

    int daysUntilTarget = (targetWeekday - now.weekday) % 7;

    if (daysUntilTarget == 0) {
      daysUntilTarget = 7;
    }

    final DateTime nextDay = now.add(Duration(days: daysUntilTarget));
    return DateTime(nextDay.year, nextDay.month, nextDay.day);
  }

  static DateTime setDate(DateTime to, DateTime from) {
    return DateTime(from.year, from.month, from.day, to.hour, to.minute,
        to.second, to.millisecond, to.microsecond);
  }

  static DateTime setTime(TimeOfDay to, DateTime from) {
    return DateTime(from.year, from.month, from.day, to.hour, to.minute);
  }

  static String dateOnlyString(DateTime date) {
    return DateFormat('dd/MM/yy').format(date);
  }
}
