﻿using System;
using Trainer_v5.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Trainer_v5
{
	public enum ValueDataTypeEnum { Int = 1, Float = 2, String = 3, Bool = 4 }

	public static class Helpers
	{
		public static bool IsGameLoaded => GameSettings.Instance != null && HUD.Instance != null;
		public static string Version => "5.2.2";
		public static string TrainerVersion => $"Trainer v{Version}";
		public static bool IsDebug => false;
		public static string DiscordUrl => "https://discord.com/invite/J584aG";
		public static System.Random Random = new System.Random();

		#region Traits
		public static IEnumerable<Employee.Trait> Traits
			=> Enum.GetValues(typeof(Employee.Trait)).Cast<Employee.Trait>();

		public static IEnumerable<Employee.Trait> GoodTraits
			=> Traits.Where(t => 0 != (Employee.GoodTraits & t));

		public static IEnumerable<Employee.Trait> NeutralTraits
			=> Traits.Where(t => 0 != (Employee.NeutralTraits & t));

		public static IEnumerable<Employee.Trait> BadTraits
			=> Traits.Where(t => 0 != (Employee.BadTraits & t));

		public static bool IsGood(this Employee.Trait self)
		{
			return (self & Employee.GoodTraits) > 0;
		}

		public static bool IsNeutral(this Employee.Trait self)
		{
			return (self & Employee.NeutralTraits) > 0;
		}

		public static bool IsBad(this Employee.Trait self)
		{
			return (self & Employee.BadTraits) > 0;
		}

		#endregion

		#region LeadDesign Demands

		public static IEnumerable<LeadDesignDemands.Demand> Demands
			=> Enum.GetValues(typeof(LeadDesignDemands.Demand)).Cast<LeadDesignDemands.Demand>();

		#endregion
	}
}
