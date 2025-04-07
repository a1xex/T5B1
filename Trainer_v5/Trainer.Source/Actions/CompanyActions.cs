using System.Linq;
using UnityEngine;

namespace Trainer_v5.Actions
{
	public static class CompanyActions
	{
		private static GameSettings Settings => GameSettings.Instance;

		public static void AIBankrupt()
		{
			SimulatedCompany[] Companies = Settings.simulation.Companies.Values.ToArray();

			for (int i = 0; i < Companies.Length; i++)
			{
				if (Companies[i] != Settings.MyCompany)
				{
					Companies[i].Bankrupt = true;
				}
			}
			HUD.Instance.AddPopupMessage(Constants.AIBankruptMessageKey.LocDef(Constants.AIBankruptMessageText), "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}

		public static void TakeoverCompanyAction(string input)
		{
			var simulatedCompany = Settings.simulation.Companies
				.FirstOrDefault(simCompany => simCompany.Value.Name.Equals(input, System.StringComparison.OrdinalIgnoreCase)).Value;

			if (simulatedCompany == null)
			{
				WindowManager.SpawnDialog(string.Format(Constants.CompanyNotFoundMessageKey.LocDef(Constants.CompanyNotFoundMessageText), input), false, DialogWindow.DialogType.Information);
				return;
			}

			if (simulatedCompany == Settings.MyCompany)
			{
				WindowManager.SpawnDialog(Constants.CannotTakeoverOwnCompanyMessageKey.LocDef(Constants.CannotTakeoverOwnCompanyMessageText), false, DialogWindow.DialogType.Information);
				return;
			}

			if (!simulatedCompany.CanBuyOut(Settings.MyCompany))
			{
				WindowManager.SpawnDialog(string.Format(Constants.CannotBuyoutCompanyMessageKey.LocDef(Constants.CannotBuyoutCompanyMessageText), input), false, DialogWindow.DialogType.Information);
				return;
			}

			var simulatedCompanyWorth = simulatedCompany.GetPossibleStockWorth();

			simulatedCompany.BuyOut(
				new Company[] { Settings.MyCompany },
				false,
				SDateTime.Now(),
				true
			);

			Settings.MyCompany.MakeTransaction(-simulatedCompanyWorth, Company.TransactionCategory.Stocks, "Takeover: " + simulatedCompany.Name, false);
			WindowManager.SpawnDialog(string.Format(Constants.CompanyTakenOverMessageKey.LocDef(Constants.CompanyTakenOverMessageText), input), false, DialogWindow.DialogType.Information);
		}

		public static void TakeoverCompany()
		{
			WindowManager.SpawnInputDialog(Constants.TakeoverCompanyNamePromptKey.LocDef(Constants.TakeoverCompanyNamePromptText),
				Constants.TakeoverCompanyTitleKey.LocDef(Constants.TakeoverCompanyTitleText), "", TakeoverCompanyAction);
		}

		public static void SubDCompanyAction(string input)
		{
			SimulatedCompany companyToSub =
				Settings.simulation.Companies.FirstOrDefault(company => company.Value.Name.Equals(input, System.StringComparison.OrdinalIgnoreCase)).Value;

			if (companyToSub == null)
			{
				WindowManager.SpawnDialog(string.Format(Constants.CompanyNotFoundMessageKey.LocDef(Constants.CompanyNotFoundMessageText), input), false, DialogWindow.DialogType.Information);
				return;
			}

			if (companyToSub == Settings.MyCompany)
			{
				WindowManager.SpawnDialog(Constants.CannotSubsidiaryOwnCompanyMessageKey.LocDef(Constants.CannotSubsidiaryOwnCompanyMessageText), false, DialogWindow.DialogType.Information);
				return;
			}

			bool isAlreadySubsidiary = false;
			if (Settings.MyCompany.Subsidiaries != null && companyToSub != null)
			{
				foreach (uint subId in Settings.MyCompany.Subsidiaries)
				{
					if (subId == companyToSub.ID)
					{
						isAlreadySubsidiary = true;
						break;
					}
				}
			}

			if (isAlreadySubsidiary)
			{
				WindowManager.SpawnDialog(string.Format(Constants.AlreadySubsidiaryMessageKey.LocDef(Constants.AlreadySubsidiaryMessageText), input), false, DialogWindow.DialogType.Information);
				return;
			}

			companyToSub.MakeSubsidiary(Settings.MyCompany, SDateTime.Now());
			HUD.Instance.AddPopupMessage(string.Format(Constants.SubsidiaryMadeMessageKey.LocDef(Constants.SubsidiaryMadeMessageText), companyToSub.Name), "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}

		public static void SubDCompany()
		{
			WindowManager.SpawnInputDialog(Constants.SubsidiaryNamePromptKey.LocDef(Constants.SubsidiaryNamePromptText),
				Constants.SubsidiaryTitleKey.LocDef(Constants.SubsidiaryTitleText), "", SubDCompanyAction);
		}

		public static void ForceBankruptAction(string input)
		{
			SimulatedCompany companyToBankrupt =
				Settings.simulation.Companies.FirstOrDefault(company => company.Value.Name.Equals(input, System.StringComparison.OrdinalIgnoreCase)).Value;

			if (companyToBankrupt == null)
			{
				WindowManager.SpawnDialog(string.Format(Constants.CompanyNotFoundMessageKey.LocDef(Constants.CompanyNotFoundMessageText), input), false, DialogWindow.DialogType.Information);
				return;
			}

			if (companyToBankrupt == Settings.MyCompany)
			{
				WindowManager.SpawnDialog(Constants.CannotBankruptOwnCompanyMessageKey.LocDef(Constants.CannotBankruptOwnCompanyMessageText), false, DialogWindow.DialogType.Information);
				return;
			}

			companyToBankrupt.Bankrupt = !companyToBankrupt.Bankrupt;
			string status = companyToBankrupt.Bankrupt ? "bankrupt" : "no longer bankrupt";
			HUD.Instance.AddPopupMessage(string.Format(Constants.CompanyBankruptStatusMessageKey.LocDef(Constants.CompanyBankruptStatusMessageText), companyToBankrupt.Name, status), "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}

		public static void ForceBankrupt()
		{
			WindowManager.SpawnInputDialog(Constants.ForceBankruptPromptKey.LocDef(Constants.ForceBankruptPromptText),
				Constants.ForceBankruptTitleKey.LocDef(Constants.ForceBankruptTitleText), "", ForceBankruptAction);
		}
	}
}
