import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class CopyableText extends StatelessWidget {
  const CopyableText({
    super.key,
    required this.text,
  });

  final String text;

  @override
  Widget build(BuildContext context) {
    return Row(
      mainAxisSize: MainAxisSize.min,
      children: <Widget>[
        IconButton(
          onPressed: () => Clipboard.setData(ClipboardData(text: text)),
          icon: const Icon(Icons.copy, size: 26, color: Colors.grey),
        ),
        Flexible(
          child: Text(
            text,
            overflow: TextOverflow.clip,
            style: const TextStyle(fontWeight: FontWeight.w600),
          ),
        ),
      ],
    );
  }
}
