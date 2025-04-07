using System.Collections.Generic;
using Trainer_v5.Utils;
using System.Linq;

namespace Trainer_v5.Configuration
{
	public static class TrainerSettings
	{
		// --- Setting Key Constants --- (Moved from nested class)
		public const string NoStress = "NoStress";
		public const string NoVacation = "NoVacation";
		public const string NoSickness = "NoSickness";
		public const string FullSatisfaction = "FullSatisfaction";
		public const string NoNeeds = "NoNeeds";
		public const string FreeEmployees = "FreeEmployees";
		public const string LockAge = "LockAge";
		public const string IncreaseWalkSpeed = "IncreaseWalkSpeed";
		public const string MoreInspiration = "MoreInspiration";
		public const string MoreCreativity = "MoreCreativity";
		public const string FullRoomBrightness = "FullRoomBrightness";
		public const string CleanRooms = "CleanRooms";
		public const string FullEnvironment = "FullEnvironment";
		public const string NoiseReduction = "NoiseReduction";
		public const string TemperatureLock = "TemperatureLock";
		public const string NoWaterElectricity = "NoWaterElectricity";
		public const string NoMaintenance = "NoMaintenance";
		public const string DisableFires = "DisableFires";
		public const string DisableFurnitureStealing = "DisableFurnitureStealing";
		public const string IncreaseBookshelfSkill = "IncreaseBookshelfSkill";
		public const string MoreHostingDeals = "MoreHostingDeals";
		public const string IncreaseCourierCapacity = "IncreaseCourierCapacity";
		public const string ReduceISPCost = "ReduceISPCost";
		public const string IncreasePrintSpeed = "IncreasePrintSpeed";
		public const string FreePrint = "FreePrint";
		public const string NoServerCost = "NoServerCost";
		public const string ReduceExpansionCost = "ReduceExpansionCost";
		public const string NoEducationCost = "NoEducationCost";
		public const string AutoEndDesign = "AutoEndDesign";
		public const string AutoEndResearch = "AutoEndResearch";
		public const string AutoEndPatent = "AutoEndPatent";
		public const string ReduceBoxPrice = "ReduceBoxPrice";
		public const string AutoResearchStart = "AutoResearchStart";
		public const string DigitalDistributionMonopol = "DigitalDistributionMonopol";
		public const string DisableFireInspection = "DisableFireInspection";
		public const string DisableForcePause = "DisableForcePause";
		public const string DisableForceFreeze = "DisableForceFreeze";
		public const string AutoAcceptHostingDeals = "AutoAcceptHostingDeals";
		public const string DisableBurglars = "DisableBurglars";
		public const string Experimental = "Experimental";
		public const string FreeStaff = "FreeStaff";
		public const string EfficiencyStore = "EfficiencyStore";
		public const string LeadEfficiencyStore = "LeadEfficiencyStore";
		public const string ProductPriceName = "ProductPriceName";

		public static Dictionary<string, bool> Settings { get; } = new Dictionary<string, bool>
		{
			{NoStress, false},
			{NoVacation, false},
			{NoSickness, false},
			{FullSatisfaction, false},
			{NoNeeds, false},
			{FreeEmployees, false},
			{LockAge, false},
			{IncreaseWalkSpeed, false},
			{MoreInspiration, false},
			{MoreCreativity, false},
			{FullRoomBrightness, false},
			{CleanRooms, false},
			{FullEnvironment, false},
			{NoiseReduction, false},
			{TemperatureLock, false},
			{NoWaterElectricity, false},
			{NoMaintenance, false},
			{DisableFires, false},
			{DisableFurnitureStealing, false},
			{IncreaseBookshelfSkill, false},
			{MoreHostingDeals, false},
			{IncreaseCourierCapacity, false},
			{ReduceISPCost, false},
			{IncreasePrintSpeed, false},
			{FreePrint, false},
			{NoServerCost, false},
			{ReduceExpansionCost, false},
			{NoEducationCost, false},
			{AutoEndDesign, false},
			{AutoEndResearch, false},
			{AutoEndPatent, false},
			{ReduceBoxPrice, false},
			{AutoResearchStart, false},
			{DigitalDistributionMonopol, false},
			{DisableFireInspection, false},
			{DisableForcePause, false},
			{DisableForceFreeze, false},
			{AutoAcceptHostingDeals, false},
			{DisableBurglars, false},
			{Experimental, false},
			{FreeStaff, false}
		};

		public static Dictionary<string, object> StoresSettings { get; } = new Dictionary<string, object>
		{
			{EfficiencyStore, 1.0f},
			{LeadEfficiencyStore, 1.0f},
			{ProductPriceName, ""}
		};

		public static Dictionary<string, bool> RolesList { get; } = new Dictionary<string, bool>
		{
			{"Lead", false},
			{"Service", false},
			{"Programmer", false},
			{"Artist", false},
			{"Designer", false}
		};

		public static Dictionary<string, bool> SpecializationsList { get; set; } = new Dictionary<string, bool>();

		public static Dictionary<string, object> EfficiencySelectItems { get; } = new Dictionary<string, object>
		{
			{"Default", 1.0f},
			{"100%", 1.0f},
			{"200%", 2.0f},
			{"300%", 3.0f},
			{"400%", 4.0f},
			{"500%", 5.0f},
			{"1000%", 10.0f},
			{"2000%", 20.0f},
			{"4000%", 40.0f},
			{"5000%", 50.0f},
			{"8000%", 80.0f}
		};

		public static bool GetProperty(Dictionary<string, bool> properties, string key)
		{
			bool value;
			return properties.TryGetValue(key, out value) && value;
		}

		public static object GetProperty(Dictionary<string, object> properties, string key)
		{
			object value;
			return properties.TryGetValue(key, out value) ? value : null;
		}

		public static void SetProperty(Dictionary<string, bool> properties, string key, bool value)
		{
			if (properties.ContainsKey(key))
			{
				properties[key] = value;
			}
			else
			{
				Trainer_v5.Utils.Logger.Log($"Warning: Attempted to set non-existent boolean property '{key}' in TrainerSettings");
			}
		}

		public static void SetProperty(Dictionary<string, object> properties, string key, object value)
		{
			if (properties.ContainsKey(key))
			{
				properties[key] = value;
			}
			else
			{
				Trainer_v5.Utils.Logger.Log($"Warning: Attempted to set non-existent object property '{key}' in TrainerSettings");
			}
		}

		public static bool GetBool(string key, bool defaultValue = false)
		{
			bool value;
			return Settings.TryGetValue(key, out value) ? value : defaultValue;
		}

		public static object GetObject(string key, object defaultValue = null)
		{
			object value;
			return StoresSettings.TryGetValue(key, out value) ? value : defaultValue;
		}

		public static void SetBool(string key, bool value)
		{
			SetProperty(Settings, key, value);
		}

		public static void SetObject(string key, object value)
		{
			SetProperty(StoresSettings, key, value);
		}

		public static void ToggleBool(string key)
		{
			if (Settings.ContainsKey(key))
			{
				Settings[key] = !Settings[key];
			}
			else
			{
				Trainer_v5.Utils.Logger.Log($"Warning: Attempted to toggle non-existent setting '{key}' in TrainerSettings");
			}
		}

		public static string GetString(string key, string defaultValue = "")
		{
			object value;
			if (StoresSettings.TryGetValue(key, out value))
			{
				if (value is string)
				{
					return (string)value;
				}
			}
			return defaultValue;
		}

		public static void SetString(string key, string value)
		{
			SetProperty(StoresSettings, key, value);
		}
	}
} 