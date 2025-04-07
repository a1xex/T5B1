using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Trainer_v5.Configuration; // For Constants
using Trainer_v5.UI.Windows; // Added for TextStyle (since WindowStyles was moved here)

namespace Trainer_v5.UI.Helpers // Updated namespace
{
	// Merged UI Helper containing element creation, input dialogs, notifications, and styles.
	public static partial class UIHelper // Using partial to potentially split later if needed
	{
		// --- Element Creation (from original UIHelper) ---
		public static GameObject CreateLabel(string text, string name = null, TextStyle style = null)
		{
			var control = WindowManager.SpawnLabel();
			control.name = name ?? text;
			control.text = text ?? string.Empty;

			if (style != null)
			{
				if (style.Alignment.HasValue)
					control.alignment = style.Alignment.Value;
				if (style.FontStyle.HasValue)
					control.fontStyle = style.FontStyle.Value;
			}

			return control.gameObject;
		}

		public static GameObject CreateButton(string text, UnityAction action, string name = null)
		{
			var control = WindowManager.SpawnButton();
			control.name = name ?? text;
			control.GetComponentInChildren<Text>().text = text ?? string.Empty;
			control.onClick.AddListener(action);

			return control.gameObject;
		}

		public static GameObject EmptyBox(string name = "EmptyBox")
		{
			var label = WindowManager.SpawnLabel();
			label.name = name;
			label.text = string.Empty;
			return label.gameObject;
		}

		public static GameObject CreateInputBox(string text, UnityAction<string> action, string name = null)
		{
			var control = WindowManager.SpawnInputbox();
			control.name = name ?? text;
			control.text = text;
			control.onValueChanged.AddListener(action);

			return control.gameObject;
		}

		public static GameObject CreateToggle(string text, bool isOn, UnityAction<bool> action, string name = null)
		{
			var control = WindowManager.SpawnCheckbox();
			control.name = name ?? text;
			control.GetComponentInChildren<Text>().text = text;
			control.isOn = isOn;
			control.onValueChanged.AddListener(action);

			return control.gameObject;
		}

		public static GameObject MultilineInput(string name = "MultilineInput")
		{
			var r = WindowManager.SpawnInputbox();
			r.name = name;
			r.lineType = InputField.LineType.MultiLineNewline;
			return r.gameObject;
		}

		public static GUICombobox CreateComboBox(Dictionary<string, object> selectableItems, int selection, string name = null)
		{
			var comboBox = WindowManager.SpawnComboBox();
			comboBox.name = name ?? "ComboBox";
			comboBox.UpdateContent(selectableItems.Select(x => x.Key));
			comboBox.UpdateSelection(selection);

			return comboBox;
		}

		// --- Input Dialogs (from InputHelper) ---
		public static void RequestFloat(string prompt, string title, Action<float> onFinish, float min, float max)
		{
			WindowManager.SpawnInputDialog(prompt, title, string.Empty, input =>
			{
				var val = TryParseAndValidate(input, float.TryParse, min, max);
				if (val.HasValue) onFinish.Invoke(val.Value);
			});
		}
		public static void RequestFloat(string prompt, string title, Action<float> onFinish)
		{
			WindowManager.SpawnInputDialog(prompt, title, string.Empty, input =>
			{
				var val = TryParseAndValidate<float>(input, float.TryParse);
				if (val.HasValue) onFinish.Invoke(val.Value);
			});
		}
		private delegate bool TryParse<T>(string s, out T result) where T : struct;
		private static T? TryParseAndValidate<T>(string str, TryParse<T> tryParse) where T : struct, IComparable<T>
		{ 
			T val;
			if (str.Length == 0 || !tryParse(str, out val))
			{
				ShowError(Constants.InvalidInputErrorKey.LocDef(Constants.InvalidInputErrorText)); 
				return null;
			}
			return val;
		}
		private static T? TryParseAndValidate<T>(string str, TryParse<T> tryParse, T min, T max) where T : struct, IComparable<T>
		{ 
			T val;
			if (str.Length == 0 || !tryParse(str, out val)) { ShowError(Constants.InvalidInputErrorKey.LocDef(Constants.InvalidInputErrorText)); return null; }
			if (val.CompareTo(min) < 0) { ShowError(string.Format(Constants.InvalidInputMinValueErrorKey.LocDef(Constants.InvalidInputMinValueErrorText), min)); return null; } 
			if (val.CompareTo(max) > 0) { ShowError(string.Format(Constants.InvalidInputMaxValueErrorKey.LocDef(Constants.InvalidInputMaxValueErrorText), max)); return null; } 
			return val;
		}

		// --- Notifications (from Notification) ---
		public static void Popup(string msg, string icon)
		{
			HUD.Instance.AddPopupMessage(msg, icon, PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}
		public static void ShowError(string msg)
		{
			WindowManager.SpawnDialog(msg, false, DialogWindow.DialogType.Error);
		}

		// --- Window Styles (from WindowStyles) ---
		public class TextStyle // Nested class
		{
			public TextAnchor? Alignment { get; set; }
			public FontStyle? FontStyle { get; set; }
		}
		public static readonly TextStyle TitleStyle = new TextStyle
		{
			Alignment = TextAnchor.MiddleCenter,
			FontStyle = FontStyle.Bold
		};
	}
} 