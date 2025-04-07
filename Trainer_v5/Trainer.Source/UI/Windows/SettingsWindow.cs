using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Trainer_v5;
using Trainer_v5.Actions;
using Trainer_v5.Utils;
using Trainer_v5.Configuration;
using Trainer_v5.UI.Helpers;
using Trainer_v5.UI.Extensions;

namespace Trainer_v5.UI.Windows
{
	public class SettingsWindow : MonoBehaviour
	{
		private static readonly string _title = $"Trainer Settings, by Trawis (v{Trainer_v5.Helpers.Version}) | GAME VERSION: {GeneralUtils.GetGameVersion()}";

		public static GUIWindow Window { get; set; }
		public static bool Shown { get; set; }

		public static void Toggle()
		{
			if (Window == null)
			{
				Init();
			}
			else
			{
				Window.Toggle();
			}

			Shown = Window.Shown;
		}

		private static void Init()
		{
			var settings = TrainerSettings.Settings;
			var storesSettings = TrainerSettings.StoresSettings;
			var efficiencySelectItems = TrainerSettings.EfficiencySelectItems;

			Window = WindowManager.SpawnWindow();
			Window.InitialTitle = Window.TitleText.text = Window.NonLocTitle = _title;
			Window.name = Constants.SettingsWindowName;
			Window.MainPanel.name = Constants.SettingsPanelName;
			Window.MinSize.x = Constants.X_SETTINGS_WINDOW;

			if (Window.name == Constants.SettingsWindowName)
			{
				Window.GetComponentsInChildren<Button>()
				  .SingleOrDefault(x => x.name == "CloseButton")
				  ?.onClick.AddListener(() => Shown = false);
			}

			bool experimental = TrainerSettings.GetBool(TrainerSettings.Experimental) || Trainer_v5.Helpers.IsDebug;

			List<GameObject> column1 = PopulateColumn1();
			List<GameObject> column2 = PopulateColumn2();
			List<GameObject> column3 = PopulateColumn3();
			List<GameObject> column4 = PopulateColumn4();
			List<GameObject> column5 = PopulateColumn5();
			List<GameObject> column6 = PopulateColumn6();

			AddColumnsToWindow(Window, column1, column2, column3, column4, column5, column6);
		}

		private static List<GameObject> PopulateColumn1()
		{
			var column = new List<GameObject>
			{
				UIHelper.CreateButton(Constants.AddMoneyKey.LocDef(Constants.AddMoneyDefaultText), MiscActions.IncreaseMoney, Constants.AddMoneyButtonName),
				UIHelper.CreateButton(Constants.SetProductPriceKey.LocDef(Constants.SetProductPriceDefaultText), ProductActions.SetProductPrice, Constants.SetProductPriceButtonName),
				UIHelper.CreateButton(Constants.SetProductStockKey.LocDef(Constants.SetProductStockDefaultText), ProductActions.SetProductStock, Constants.SetProductStockButtonName),
				UIHelper.CreateButton(Constants.AddActiveUsersKey.LocDef(Constants.AddActiveUsersDefaultText), ProductActions.AddActiveUsers, Constants.AddActiveUsersButtonName),
				UIHelper.CreateButton(Constants.UnlockAndClaimRewardsKey.LocDef(Constants.UnlockAndClaimRewardsDefaultText), MiscActions.UnlockAndClaimAllRewards, Constants.UnlockAndClaimRewardsButtonName),
				UIHelper.CreateButton(Constants.ExtendDeadlineKey.LocDef(Constants.ExtendDeadlineDefaultText), MiscActions.ExtendDeadline, Constants.ExtendDeadlineButtonName),
				UIHelper.CreateButton(Constants.UnlockAllSpaceKey.LocDef(Constants.UnlockAllSpaceDefaultText), MiscActions.UnlockAllSpace, Constants.UnlockAllSpaceButtonName),
				UIHelper.CreateButton(Constants.UnlockFurnitureKey.LocDef(Constants.UnlockFurnitureDefaultText), MiscActions.UnlockFurniture, Constants.UnlockFurnitureButtonName),
				UIHelper.CreateButton(Constants.FixBugsKey.LocDef(Constants.FixBugsDefaultText), ProductActions.FixBugs, Constants.FixBugsButtonName),
				UIHelper.CreateButton(Constants.MaxFollowersKey.LocDef(Constants.MaxFollowersDefaultText), ProductActions.MaxFollowers, Constants.MaxFollowersButtonName),
				UIHelper.CreateButton(Constants.ClearLoansKey.LocDef(Constants.ClearLoansDefaultText), MiscActions.ClearLoans, Constants.ClearLoansButtonName)
			};
			return column;
		}

		private static List<GameObject> PopulateColumn2()
		{
			var column = new List<GameObject>
			{
				UIHelper.CreateButton(Constants.MaxReputationKey.LocDef(Constants.MaxReputationDefaultText), MiscActions.MaxReputation, Constants.MaxReputationButtonName),
				UIHelper.CreateButton(Constants.MaxMarketRecognitionKey.LocDef(Constants.MaxMarketRecognitionDefaultText), MiscActions.MaxMarketRecognition, Constants.MaxMarketRecognitionButtonName),
				UIHelper.CreateButton(Constants.TakeoverCompanyKey.LocDef(Constants.TakeoverCompanyDefaultText), CompanyActions.TakeoverCompany, Constants.TakeoverCompanyButtonName),
				UIHelper.CreateButton(Constants.SubDCompanyKey.LocDef(Constants.SubDCompanyDefaultText), CompanyActions.SubDCompany, Constants.SubDCompanyButtonName),
				UIHelper.CreateButton(Constants.ForceBankruptKey.LocDef(Constants.ForceBankruptDefaultText), CompanyActions.ForceBankrupt, Constants.ForceBankruptButtonName),
				UIHelper.CreateButton(Constants.SellProductStockKey.LocDef(Constants.SellProductStockDefaultText), ProductActions.SellProductStock, Constants.SellProductStockButtonName),
				UIHelper.CreateButton(Constants.MonthDaysKey.LocDef(Constants.MonthDaysDefaultText), MiscActions.MonthDays, Constants.MonthDaysButtonName),
				UIHelper.CreateToggle(Constants.DisableFireInspectionKey.LocDef(Constants.DisableFireInspectionDefaultText), TrainerSettings.GetBool(TrainerSettings.DisableFireInspection), a => TrainerSettings.ToggleBool(TrainerSettings.DisableFireInspection), Constants.DisableFireInspectionToggleName),
				UIHelper.CreateToggle(Constants.DisableForcePauseKey.LocDef(Constants.DisableForcePauseDefaultText), TrainerSettings.GetBool(TrainerSettings.DisableForcePause), a => TrainerSettings.ToggleBool(TrainerSettings.DisableForcePause), Constants.DisableForcePauseToggleName),
				UIHelper.CreateToggle(Constants.DisableForceFreezeKey.LocDef(Constants.DisableForceFreezeDefaultText), TrainerSettings.GetBool(TrainerSettings.DisableForceFreeze), a => TrainerSettings.ToggleBool(TrainerSettings.DisableForceFreeze), Constants.DisableForceFreezeToggleName),
				UIHelper.CreateToggle(Constants.DisableBurglarsKey.LocDef(Constants.DisableBurglarsDefaultText), TrainerSettings.GetBool(TrainerSettings.DisableBurglars), a => TrainerSettings.ToggleBool(TrainerSettings.DisableBurglars), Constants.DisableBurglarsToggleName)
			};
			return column;
		}

		private static List<GameObject> PopulateColumn3()
		{
			var column = new List<GameObject>
			{
				UIHelper.CreateToggle(Constants.NoStressKey.LocDef(Constants.NoStressDefaultText), TrainerSettings.GetBool(TrainerSettings.NoStress), a => TrainerSettings.ToggleBool(TrainerSettings.NoStress), Constants.NoStressToggleName),
				UIHelper.CreateToggle(Constants.NoVacationKey.LocDef(Constants.NoVacationDefaultText), TrainerSettings.GetBool(TrainerSettings.NoVacation), a => TrainerSettings.ToggleBool(TrainerSettings.NoVacation), Constants.NoVacationToggleName),
				UIHelper.CreateToggle(Constants.NoSicknessKey.LocDef(Constants.NoSicknessDefaultText), TrainerSettings.GetBool(TrainerSettings.NoSickness), a => TrainerSettings.ToggleBool(TrainerSettings.NoSickness), Constants.NoSicknessToggleName),
				UIHelper.CreateToggle(Constants.FullSatisfactionKey.LocDef(Constants.FullSatisfactionDefaultText), TrainerSettings.GetBool(TrainerSettings.FullSatisfaction), a => TrainerSettings.ToggleBool(TrainerSettings.FullSatisfaction), Constants.FullSatisfactionToggleName),
				UIHelper.CreateToggle(Constants.NoNeedsKey.LocDef(Constants.NoNeedsDefaultText), TrainerSettings.GetBool(TrainerSettings.NoNeeds), a => TrainerSettings.ToggleBool(TrainerSettings.NoNeeds), Constants.NoNeedsToggleName),
				UIHelper.CreateToggle(Constants.FreeEmployeesKey.LocDef(Constants.FreeEmployeesDefaultText), TrainerSettings.GetBool(TrainerSettings.FreeEmployees), a => TrainerSettings.ToggleBool(TrainerSettings.FreeEmployees), Constants.FreeEmployeesToggleName),
				UIHelper.CreateToggle(Constants.LockAgeKey.LocDef(Constants.LockAgeDefaultText), TrainerSettings.GetBool(TrainerSettings.LockAge), a => TrainerSettings.ToggleBool(TrainerSettings.LockAge), Constants.LockAgeToggleName),
				UIHelper.CreateToggle(Constants.IncreaseWalkSpeedKey.LocDef(Constants.IncreaseWalkSpeedDefaultText), TrainerSettings.GetBool(TrainerSettings.IncreaseWalkSpeed), a => TrainerSettings.ToggleBool(TrainerSettings.IncreaseWalkSpeed), Constants.IncreaseWalkSpeedToggleName),
				UIHelper.CreateToggle(Constants.MoreInspirationKey.LocDef(Constants.MoreInspirationDefaultText), TrainerSettings.GetBool(TrainerSettings.MoreInspiration), a => TrainerSettings.ToggleBool(TrainerSettings.MoreInspiration), Constants.MoreInspirationToggleName),
				UIHelper.CreateToggle(Constants.DisableFurnitureStealingKey.LocDef(Constants.DisableFurnitureStealingDefaultText), TrainerSettings.GetBool(TrainerSettings.DisableFurnitureStealing), a => TrainerSettings.ToggleBool(TrainerSettings.DisableFurnitureStealing), Constants.DisableFurnitureStealingToggleName),
				UIHelper.CreateButton(Constants.ResetAgeKey.LocDef(Constants.ResetAgeDefaultText), EmployeeActions.ResetAgeOfEmployees, Constants.ResetAgeButtonName),
				UIHelper.CreateButton(Constants.EmployeesToMaxKey.LocDef(Constants.EmployeesToMaxDefaultText), EmployeeActions.EmployeesToMax, Constants.EmployeesToMaxButtonName),
				UIHelper.CreateButton(Constants.HREmployeesKey.LocDef(Constants.HREmployeesDefaultText), EmployeeActions.HREmployees, Constants.HREmployeesButtonName)
			};
			return column;
		}

		private static List<GameObject> PopulateColumn4()
		{
			var column = new List<GameObject>
			{
				UIHelper.CreateToggle(Constants.FullRoomBrightnessKey.LocDef(Constants.FullRoomBrightnessDefaultText), TrainerSettings.GetBool(TrainerSettings.FullRoomBrightness), a => TrainerSettings.ToggleBool(TrainerSettings.FullRoomBrightness), Constants.FullRoomBrightnessToggleName),
				UIHelper.CreateToggle(Constants.CleanRoomsKey.LocDef(Constants.CleanRoomsDefaultText), TrainerSettings.GetBool(TrainerSettings.CleanRooms), a => TrainerSettings.ToggleBool(TrainerSettings.CleanRooms), Constants.CleanRoomsToggleName),
				UIHelper.CreateToggle(Constants.FullEnvironmentKey.LocDef(Constants.FullEnvironmentDefaultText), TrainerSettings.GetBool(TrainerSettings.FullEnvironment), a => TrainerSettings.ToggleBool(TrainerSettings.FullEnvironment), Constants.FullEnvironmentToggleName),
				UIHelper.CreateToggle(Constants.NoiseReductionKey.LocDef(Constants.NoiseReductionDefaultText), TrainerSettings.GetBool(TrainerSettings.NoiseReduction), a => TrainerSettings.ToggleBool(TrainerSettings.NoiseReduction), Constants.NoiseReductionToggleName),
				UIHelper.CreateToggle(Constants.TemperatureLockKey.LocDef(Constants.TemperatureLockDefaultText), TrainerSettings.GetBool(TrainerSettings.TemperatureLock), a => TrainerSettings.ToggleBool(TrainerSettings.TemperatureLock), Constants.TemperatureLockToggleName),
				UIHelper.CreateToggle(Constants.NoWaterElectricityKey.LocDef(Constants.NoWaterElectricityDefaultText), TrainerSettings.GetBool(TrainerSettings.NoWaterElectricity), a => TrainerSettings.ToggleBool(TrainerSettings.NoWaterElectricity), Constants.NoWaterElectricityToggleName),
				UIHelper.CreateToggle(Constants.NoMaintenanceKey.LocDef(Constants.NoMaintenanceDefaultText), TrainerSettings.GetBool(TrainerSettings.NoMaintenance), a => TrainerSettings.ToggleBool(TrainerSettings.NoMaintenance), Constants.NoMaintenanceToggleName),
				UIHelper.CreateToggle(Constants.DisableFiresKey.LocDef(Constants.DisableFiresDefaultText), TrainerSettings.GetBool(TrainerSettings.DisableFires), a => TrainerSettings.ToggleBool(TrainerSettings.DisableFires), Constants.DisableFiresToggleName),
				UIHelper.CreateToggle(Constants.IncreaseBookshelfSkillKey.LocDef(Constants.IncreaseBookshelfSkillDefaultText), TrainerSettings.GetBool(TrainerSettings.IncreaseBookshelfSkill), a => TrainerSettings.ToggleBool(TrainerSettings.IncreaseBookshelfSkill), Constants.IncreaseBookshelfSkillToggleName),
				UIHelper.CreateToggle(Constants.MoreHostingDealsKey.LocDef(Constants.MoreHostingDealsDefaultText), TrainerSettings.GetBool(TrainerSettings.MoreHostingDeals), a => TrainerSettings.ToggleBool(TrainerSettings.MoreHostingDeals), Constants.MoreHostingDealsToggleName),
				UIHelper.CreateToggle(Constants.AutoAcceptHostingDealsKey.LocDef(Constants.AutoAcceptHostingDealsDefaultText), TrainerSettings.GetBool(TrainerSettings.AutoAcceptHostingDeals), a => TrainerSettings.ToggleBool(TrainerSettings.AutoAcceptHostingDeals), Constants.AutoAcceptHostingDealsToggleName),
				UIHelper.CreateToggle(Constants.IncreaseCourierCapacityKey.LocDef(Constants.IncreaseCourierCapacityDefaultText), TrainerSettings.GetBool(TrainerSettings.IncreaseCourierCapacity), a => TrainerSettings.ToggleBool(TrainerSettings.IncreaseCourierCapacity), Constants.IncreaseCourierCapacityToggleName)
			};
			return column;
		}

		private static List<GameObject> PopulateColumn5()
		{
			var column = new List<GameObject>
			{
				UIHelper.CreateToggle(Constants.ReduceISPCostKey.LocDef(Constants.ReduceISPCostDefaultText), TrainerSettings.GetBool(TrainerSettings.ReduceISPCost), a => TrainerSettings.ToggleBool(TrainerSettings.ReduceISPCost), Constants.ReduceISPCostToggleName),
				UIHelper.CreateToggle(Constants.IncreasePrintSpeedKey.LocDef(Constants.IncreasePrintSpeedDefaultText), TrainerSettings.GetBool(TrainerSettings.IncreasePrintSpeed), a => TrainerSettings.ToggleBool(TrainerSettings.IncreasePrintSpeed), Constants.IncreasePrintSpeedToggleName),
				UIHelper.CreateToggle(Constants.FreePrintKey.LocDef(Constants.FreePrintDefaultText), TrainerSettings.GetBool(TrainerSettings.FreePrint), a => TrainerSettings.ToggleBool(TrainerSettings.FreePrint), Constants.FreePrintToggleName),
				UIHelper.CreateToggle(Constants.NoServerCostKey.LocDef(Constants.NoServerCostDefaultText), TrainerSettings.GetBool(TrainerSettings.NoServerCost), a => TrainerSettings.ToggleBool(TrainerSettings.NoServerCost), Constants.NoServerCostToggleName),
				UIHelper.CreateToggle(Constants.ReduceExpansionCostKey.LocDef(Constants.ReduceExpansionCostDefaultText), TrainerSettings.GetBool(TrainerSettings.ReduceExpansionCost), a => TrainerSettings.ToggleBool(TrainerSettings.ReduceExpansionCost), Constants.ReduceExpansionCostToggleName),
				UIHelper.CreateToggle(Constants.NoEducationCostKey.LocDef(Constants.NoEducationCostDefaultText), TrainerSettings.GetBool(TrainerSettings.NoEducationCost), a => TrainerSettings.ToggleBool(TrainerSettings.NoEducationCost), Constants.NoEducationCostToggleName),
				UIHelper.CreateToggle(Constants.AutoEndDesignKey.LocDef(Constants.AutoEndDesignDefaultText), TrainerSettings.GetBool(TrainerSettings.AutoEndDesign), a => TrainerSettings.ToggleBool(TrainerSettings.AutoEndDesign), Constants.AutoEndDesignToggleName),
				UIHelper.CreateToggle(Constants.AutoEndResearchKey.LocDef(Constants.AutoEndResearchDefaultText), TrainerSettings.GetBool(TrainerSettings.AutoEndResearch), a => TrainerSettings.ToggleBool(TrainerSettings.AutoEndResearch), Constants.AutoEndResearchToggleName),
				UIHelper.CreateToggle(Constants.AutoEndPatentKey.LocDef(Constants.AutoEndPatentDefaultText), TrainerSettings.GetBool(TrainerSettings.AutoEndPatent), a => TrainerSettings.ToggleBool(TrainerSettings.AutoEndPatent), Constants.AutoEndPatentToggleName),
				UIHelper.CreateToggle(Constants.ReduceBoxPriceKey.LocDef(Constants.ReduceBoxPriceDefaultText), TrainerSettings.GetBool(TrainerSettings.ReduceBoxPrice), a => TrainerSettings.ToggleBool(TrainerSettings.ReduceBoxPrice), Constants.ReduceBoxPriceToggleName),
				UIHelper.CreateToggle(Constants.AutoResearchStartKey.LocDef(Constants.AutoResearchStartDefaultText), TrainerSettings.GetBool(TrainerSettings.AutoResearchStart), a => TrainerSettings.ToggleBool(TrainerSettings.AutoResearchStart), Constants.AutoResearchStartToggleName),
				UIHelper.CreateToggle(Constants.DigitalDistributionMonopolKey.LocDef(Constants.DigitalDistributionMonopolDefaultText), TrainerSettings.GetBool(TrainerSettings.DigitalDistributionMonopol), a => TrainerSettings.ToggleBool(TrainerSettings.DigitalDistributionMonopol), Constants.DigitalDistributionMonopolToggleName),
				UIHelper.CreateToggle(Constants.FreeStaffKey.LocDef(Constants.FreeStaffDefaultText), TrainerSettings.GetBool(TrainerSettings.FreeStaff), a => TrainerSettings.ToggleBool(TrainerSettings.FreeStaff), Constants.FreeStaffToggleName),
				UIHelper.CreateLabel(Constants.EfficiencyKey.LocDef(Constants.EfficiencyDefaultText), name: Constants.EfficiencyLabelName),
				UIHelper.CreateComboBox(TrainerSettings.EfficiencySelectItems, TrainerSettings.EfficiencySelectItems.GetIndex(TrainerSettings.StoresSettings, TrainerSettings.EfficiencyStore, ValueDataTypeEnum.Float), name: Constants.EfficiencyComboBoxName).gameObject,
				UIHelper.CreateLabel(Constants.LeadEfficiencyKey.LocDef(Constants.LeadEfficiencyDefaultText), name: Constants.LeadEfficiencyLabelName),
				UIHelper.CreateComboBox(TrainerSettings.EfficiencySelectItems, TrainerSettings.EfficiencySelectItems.GetIndex(TrainerSettings.StoresSettings, TrainerSettings.LeadEfficiencyStore, ValueDataTypeEnum.Float), name: Constants.LeadEfficiencyComboBoxName).gameObject
			};
			return column;
		}

		private static List<GameObject> PopulateColumn6()
		{
			var column = new List<GameObject>
			{
				UIHelper.CreateToggle(Constants.ExperimentalKey.LocDef(Constants.ExperimentalDefaultText), TrainerSettings.GetBool(TrainerSettings.Experimental), a => TrainerSettings.ToggleBool(TrainerSettings.Experimental), Constants.ExperimentalToggleName),
				UIHelper.CreateButton(Constants.TestKey.LocDef(Constants.TestDefaultText), MiscActions.Test, Constants.TestButtonName),
				UIHelper.CreateButton(Constants.TestButtonKey.LocDef(Constants.TestButtonDefaultText), MiscActions.TestButton, Constants.TestButton2Name),
				UIHelper.CreateToggle(Constants.MoreCreativityKey.LocDef(Constants.MoreCreativityDefaultText), TrainerSettings.GetBool(TrainerSettings.MoreCreativity), a => TrainerSettings.ToggleBool(TrainerSettings.MoreCreativity), Constants.MoreCreativityToggleName)
			};
			return column;
		}

		private static void AddColumnsToWindow(GUIWindow window, params List<GameObject>[] columns)
		{
			int maxHeight = columns.Max(col => col.Count);
			window.SetWindowSize(maxHeight, Constants.X_SETTINGS_WINDOW);

			for (int i = 0; i < columns.Length; i++)
			{
				int currentColumnX = Constants.FIRST_COLUMN + i * Constants.SECOND_COLUMN; // Adjusted calculation
				bool isComboBoxColumn = i == 4; // Check if it's the column with combo boxes (index 4 for column 5)
				columns[i].AddToWindow(window, currentColumnX, isComboBoxColumn);
			}
		}
	}
} 