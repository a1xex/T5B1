﻿using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Trainer_v5.Configuration;
using Trainer_v5.UI.Windows;
using Trainer_v5.UI.Helpers;
using Trainer_v5.UI.Extensions;

namespace Trainer_v5
{
	public class Main : ModMeta
	{
		public override string Name => "TrainerByTrawis";
		public static Button TrainerButton { get; set; }
		public static Button SkillChangeButton { get; set; }

		public override void Initialize(ModController.DLLMod parentMod)
		{
			base.Initialize(parentMod);
		}

		public static void OpenSettingsWindow()
		{
			if (!SettingsWindow.Shown)
			{
				SettingsWindow.Toggle();
			}
		}

		public static void CloseSettingsWindow()
		{
			if (SettingsWindow.Shown)
			{
				SettingsWindow.Toggle();
			}
		}

		public static void CreateUIButtons()
		{
			TrainerButton = UIHelper.CreateButton(Helpers.TrainerVersion, () => SettingsWindow.Toggle(), Constants.TrainerButtonName).GetComponent<Button>();
			SkillChangeButton = UIHelper.CreateButton(Constants.EmployeeSkillButtonKey.LocDef(Constants.EmployeeSkillButtonText), () => EmployeeSkillChangeWindow.Show(), Constants.EmployeeSkillButtonName).GetComponent<Button>();

			TrainerButton.gameObject.AddToElement("MainPanel/Holder/FanPanel", new Rect(164, 0, 100, 32));
			SkillChangeButton.gameObject.AddToElement("ActorWindow/ContentPanel/Panel", new Rect(0, 0, 100, 32));
		}

		public override void ConstructOptionsScreen(RectTransform parent, bool inGame)
		{
			Text label = WindowManager.SpawnLabel();
			label.text = Constants.OptionsScreenInfoKey.LocDef(Constants.OptionsScreenInfoText);
			label.name = Constants.OptionsScreenLabelName;

			WindowManager.AddElementToElement(label.gameObject, parent.gameObject, new Rect(0, 0, 400, 128),
					new Rect(0, 0, 0, 0));
		}

		public override WriteDictionary Serialize(GameReader.LoadMode mode)
		{
			var data = new WriteDictionary();
			foreach (var setting in TrainerSettings.Settings)
			{
				data[setting.Key] = TrainerSettings.GetProperty(TrainerSettings.Settings, setting.Key);
			}

			foreach (var store in TrainerSettings.StoresSettings)
			{
				data[store.Key] = TrainerSettings.GetProperty(TrainerSettings.StoresSettings, store.Key);
			}

			return data;
		}

		public override void Deserialize(WriteDictionary data, GameReader.LoadMode mode)
		{
			var settings = TrainerSettings.Settings.Keys.ToList();
			foreach (var setting in settings)
			{
				TrainerSettings.SetProperty(TrainerSettings.Settings, setting, data.Get(setting, TrainerSettings.GetProperty(TrainerSettings.Settings, setting)));
			}

			var stores = TrainerSettings.StoresSettings.Keys.ToList();
			foreach (var store in stores)
			{
				TrainerSettings.SetProperty(TrainerSettings.StoresSettings, store, data.Get(store, TrainerSettings.GetProperty(TrainerSettings.StoresSettings, store)));
			}
		}
	}
}
