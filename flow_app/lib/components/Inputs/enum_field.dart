import 'package:flutter/material.dart';

class EnumField extends StatefulWidget {
  const EnumField(
      {super.key,
      required this.description,
      required this.options,
      required this.characterLimit});
  final String description;
  final List<String> options;
  final int characterLimit;

  @override
  EnumFieldState createState() => EnumFieldState();
}

class EnumFieldState extends State<EnumField> {
  final TextEditingController _textEditingController = TextEditingController();
  final List<String> _items = <String>[];

  @override
  void dispose() {
    _textEditingController.dispose();
    super.dispose();
  }

  void _addItem(String item) {
    setState(() {
      _items.add(item);
      _textEditingController.clear();
    });
  }

  void _removeItem(String item) {
    setState(() {
      _items.remove(item);
    });
  }

  @override
  Widget build(BuildContext context) {
    return Column(
      children: <Widget>[
        TextField(
          controller: _textEditingController,
          decoration: InputDecoration(
            hintText: widget.description,
            suffixIcon: IconButton(
              icon: const Icon(Icons.add, size: 30),
              onPressed: () {
                _addItem(_textEditingController.text);
              },
            ),
            border: const OutlineInputBorder(
              borderRadius: BorderRadius.all(Radius.circular(8)),
              borderSide: BorderSide(color: Colors.grey, width: 2.0),
            ),
          ),
          onChanged: (String value) {
            if (value.endsWith(',')) {
              _addItem(value.substring(0, value.length - 1));
            }
          },
          maxLength: widget.characterLimit,
        ),
        Align(
          alignment: Alignment.topLeft,
          child: Wrap(
            spacing: 8,
            runSpacing: 2,
            children: _items.map((String item) {
              return Chip(
                label: Text(item, style: const TextStyle(color: Colors.black)),
                padding: const EdgeInsets.all(2),
                backgroundColor: Colors.grey[200],
                deleteIcon: const Icon(Icons.close),
                onDeleted: () {
                  _removeItem(item);
                },
              );
            }).toList(),
          ),
        ),
      ],
    );
  }
}
