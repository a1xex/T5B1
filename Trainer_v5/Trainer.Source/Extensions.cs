using System.Collections.Generic;
using System;
using System.Linq;
using OrbCreationExtensions;
using Trainer_v5.Utils;

namespace Trainer_v5
{
	public static class Extensions
	{
		public static object Get(this Dictionary<string, object> settings, string key)
		{
			object value;
			if (settings.TryGetValue(key, out value))
			{
				return value;
			}
			return null;
		}

		public static bool Get(this Dictionary<string, bool> settings, string key)
		{
			bool value;
			if (settings.TryGetValue(key, out value))
			{
				return value;
			}
			return false;
		}


		public static void Set(this Dictionary<string, object> settings, string key, object value)
		{
			settings[key] = value;
		}

		public static int GetIndex(this Dictionary<string, object> items, Dictionary<string, object> properties, string storeKey, ValueDataTypeEnum valueType)
		{
			try
			{
				object propertyValue = properties.Get(storeKey);
				if (propertyValue == null)
				{
					$"Property '{storeKey}' not found or is null in GetIndex.".Log();
					return -1;
				}

				var values = new List<KeyValuePair<string, object>>(items);

				switch (valueType)
				{
					case ValueDataTypeEnum.Int:
						int intValue = propertyValue.MakeInt();
						return values.FindIndex(x => x.Value != null && x.Value.MakeInt() == intValue);
					case ValueDataTypeEnum.Float:
						float floatValue = propertyValue.MakeFloat();
						return values.FindIndex(x => x.Value != null && x.Value.MakeFloat() == floatValue);
					case ValueDataTypeEnum.String:
						string stringValue = propertyValue.MakeString();
						return values.FindIndex(x => x.Value != null && x.Value.MakeString() == stringValue);
					case ValueDataTypeEnum.Bool:
						bool boolValue = propertyValue.MakeBool();
						return values.FindIndex(x => x.Value != null && x.Value.MakeBool() == boolValue);
					default:
						$"Method GetIndex received an unknown value type: {valueType}".Log();
						return -1;
				}
			}
			catch (Exception ex)
			{
				$"Error in GetIndex for store '{storeKey}' and type '{valueType}'".Log();
				ex.LogException();
				return -1;
			}
		}

		public static Employee.EmployeeRole ToEmployeeRole(this string str)
		{
			try
			{
				return (Employee.EmployeeRole)Enum.Parse(typeof(Employee.EmployeeRole), str);
			}
			catch (Exception ex)
			{
				$"Error parsing EmployeeRole for string '{str}'".Log();
				ex.LogException();
				return Employee.EmployeeRole.Designer;
			}
		}

		public static TValue Get<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default(TValue))
		{
			TValue value;
			return dict.TryGetValue(key, out value) ? value : defaultValue;
		}

		public static void Toggle(this Dictionary<string, bool> settings, string key)
		{
			if (settings.ContainsKey(key))
			{
				settings[key] = !settings[key];
			}
			else
			{
				$"Warning: Attempted to toggle non-existent setting '{key}'".Log();
			}
		}

		public static bool GetOrDefault(this Dictionary<string, bool> dict, string key, bool defaultValue = false)
		{
			bool value;
			return dict.TryGetValue(key, out value) ? value : defaultValue;
		}
	}
}
