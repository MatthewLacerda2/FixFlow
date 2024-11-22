import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

class DateTimeUtils {
  void debug() {
    print("debug");
  }

  static DateTime setDate(DateTime to, DateTime from) {
    return DateTime(from.year, from.month, from.day, to.hour, to.minute,
        to.second, to.millisecond, to.microsecond);
  }

  static DateTime setTime(TimeOfDay to, DateTime from) {
    return DateTime(from.year, from.month, from.day, to.hour, to.minute);
  }

  static String dateOnlyString(DateTime date) {
    return DateFormat('dd-MM-yy').format(date);
  }
}
