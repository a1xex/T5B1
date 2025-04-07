using System;
using System.Collections.Generic;
using System.Linq;
using OrbCreationExtensions;
using UnityEngine;

namespace Trainer_v5.Actions
{
	public static class MiscActions
	{
		private static GameSettings Settings => GameSettings.Instance;
		internal static bool RewardIsGained { get; set; }
		internal static bool DealIsPushed { get; set; }

		public static void ClearLoans()
		{
			Settings.Loans.Clear();
			HUD.Instance.AddPopupMessage(Constants.AllLoansClearedMessageKey.LocDef(Constants.AllLoansClearedMessageText), "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}

		public static void PushReward()
		{
			var Deals = HUD.Instance.dealWindow.GetActiveDeals().Where(deal => deal is ServerDeal).ToArray();

			if (!Deals.Any())
			{
				return;
			}

			for (int i = 0; i < Deals.Length; i++)
			{
				Settings.MyCompany.MakeTransaction(Helpers.Random.Next(500, 50000), Company.TransactionCategory.Deals);
			}

			RewardIsGained = true;
		}

		public static void PushDeal()
		{
			SoftwareProduct[] Products = Settings.simulation.GetAllProducts(false).Where(pr =>
				  MarketSimulation.Active.SoftwareTypes.ContainsKey(pr.Type.ToString())
				&& pr.Userbase > 0
				&& pr.DevCompany.Name != Settings.MyCompany.Name
				&& pr.ServerReq > 0
				&& !pr.ExternalHostingActive)
					  .ToArray();

			if (Products.Length == 0)
				return;

			int index = Helpers.Random.Next(0, Products.Length);
			var dealExist = HUD.Instance.dealWindow.AllDeals.Values.Any(x => x is ServerDeal && ((ServerDeal)x).Product.Name == Products[index].Name);
			if (!dealExist)
			{
				ServerDeal deal = new ServerDeal(Products[index]) { Request = true };
				deal.StillValid(true);
				deal.PerPower *= 2f;

				HUD.Instance.dealWindow.InsertDeal(deal);

				DealIsPushed = true;
			}
		}

		public static void UnlockAllSpace()
		{
			if (!Helpers.IsGameLoaded)
			{
				return;
			}

			Example.TakeAllLand();
			HUD.Instance.AddPopupMessage(Constants.AllPlotsUnlockedMessageKey.LocDef(Constants.AllPlotsUnlockedMessageText), "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}

		public static void UnlockFurniture()
		{
			if (!Helpers.IsGameLoaded)
			{
				return;
			}

			Example.UnlockFurniture();
			Cheats.UnlockFurn = true;
			HUD.Instance.UpdateFurnitureButtons();
			HUD.Instance.AddPopupMessage(Constants.AllFurnitureUnlockedMessageKey.LocDef(Constants.AllFurnitureUnlockedMessageText), "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}

		public static void MonthDaysAction(string input)
		{
			int i;
			if (!int.TryParse(input, out i))
			{
				return;
			}

			GameSettings.DaysPerMonth = i;
			WindowManager.SpawnDialog(Constants.MonthDaysRestartWarningKey.LocDef(Constants.MonthDaysRestartWarningText), false, DialogWindow.DialogType.Warning);
		}

		public static void MonthDays()
		{
			WindowManager.SpawnInputDialog(Constants.MonthDaysPromptKey.LocDef(Constants.MonthDaysPromptText),
				Constants.MonthDaysTitleKey.LocDef(Constants.MonthDaysTitleText), "2", MonthDaysAction);
		}

		public static void ExtendDeadline()
		{
			foreach (var work in Settings.MyCompany.WorkItems)
			{
				var contract = work.contract;
				if (contract == null) continue;
				var deadline = contract.Deadline;
				contract.Deadline = new SDateTime(deadline.Year + 1, deadline.Month, deadline.Day);
			}
			HUD.Instance.AddPopupMessage(Constants.DeadlinesExtendedMessageKey.LocDef(Constants.DeadlinesExtendedMessageText), "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}

		public static void IncreaseMoneyAction(string input)
		{
			Settings.MyCompany.MakeTransaction(input.ConvertToIntDef(100000), Company.TransactionCategory.Deals);
			HUD.Instance.AddPopupMessage(Constants.MoneyAddedMessageKey.LocDef(Constants.MoneyAddedMessageText), "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}
		public static void IncreaseMoney()
		{
			WindowManager.SpawnInputDialog(Constants.AddMoneyPromptKey.LocDef(Constants.AddMoneyPromptText),
				Constants.AddMoneyTitleKey.LocDef(Constants.AddMoneyTitleText), "100000", IncreaseMoneyAction);
		}

		public static void MaxReputation()
		{
			Action<string> action = (input) =>
			{
				if (input.Equals("YES", StringComparison.OrdinalIgnoreCase))
				{
					Settings.MyCompany.ChangeBusinessRep(1f, "Publisher", 1f);
					Settings.MyCompany.ChangeBusinessRep(1f, "Deal", 1f);
					Settings.MyCompany.ChangeBusinessRep(1f, "Printing", 1f);
					Settings.MyCompany.ChangeBusinessRep(1f, "Lawsuit", 1f);
					Settings.MyCompany.ChangeBusinessRep(1f, "Contract", 1f);
					Settings.MyCompany.ChangeBusinessRep(1f, "Hosting", 1f);
					WindowManager.SpawnDialog(Constants.MaxReputationAppliedMessageKey.LocDef(Constants.MaxReputationAppliedMessageText), false, DialogWindow.DialogType.Information);
				}
			};

			string question = string.Format(Constants.AreYouSurePromptKey.LocDef(Constants.AreYouSurePromptText),
				Constants.MaxRepConfirmActionKey.LocDef(Constants.MaxRepConfirmActionText));
			AreYouSureAction(question, action);
		}

		public static void MaxMarketRecognition()
		{
			var softwareTypes = MarketSimulation.Active.SoftwareTypes.Values.Where(value => !value.OneClient).ToList();
			foreach (var softwareType in softwareTypes)
			{
				foreach (var category in softwareType.Categories.ToList())
				{
					Example.AddReputation(softwareType.Name, category.Key, int.MaxValue);
				}
			}

			WindowManager.SpawnDialog(Constants.MaxMarketRecognitionAppliedMessageKey.LocDef(Constants.MaxMarketRecognitionAppliedMessageText), false, DialogWindow.DialogType.Information);
		}

		public static void UnlockAndClaimAllRewards()
		{
			GameSettings.Instance.CompletedTasks.AddRange(GameData.Tasks.Select(x => x.Name));
			GameSettings.Instance.ClaimedRewards.AddRange(GameData.Tasks.Select(x => x.Name));
			HUD.Instance.RefreshBuildButtons();

			WindowManager.SpawnDialog(Constants.AllRewardsUnlockedMessageKey.LocDef(Constants.AllRewardsUnlockedMessageText), false, DialogWindow.DialogType.Information);
		}

		private static void AreYouSureAction(string question, Action<string> confirmationAction)
		{
			WindowManager.SpawnInputDialog(question, Constants.ConfirmationTitleKey.LocDef(Constants.ConfirmationTitleText), "YES", confirmationAction);
		}

		public static void Test()
		{
			var designDocuments = Settings.MyCompany.WorkItems.OfType<DesignDocument>().Where(d => !d.HasFinished).ToList();

			foreach (var designDocument in designDocuments)
			{
				for (int i = 0; i < DesignDocument.MaxIteration; i++)
				{
					if (designDocument.Parent == null && designDocument.Iteration < 3)
					{
						for (int index = 0; index < designDocument.Features.Length; index++)
						{
							designDocument.Features[index].ArtDone = designDocument.Features[index].CodeDone = false;
							designDocument.Features[index].Progress = 1f;
							designDocument.Features[index].DevTime = 1f;
							designDocument.Features[index].Qual = 1f;
							designDocument.Features[index].LastIterationProg = 1f;
						}
						DevConsole.Console.Log(designDocument.GetProgress());
						designDocument.Iteration++;
					}
				}
			}
			HUD.Instance.AddPopupMessage(Constants.DesignDocIterationTestExecutedMessageKey.LocDef(Constants.DesignDocIterationTestExecutedMessageText), "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}


		public static void TestAction(string input)
		{
			WorkItem workItem = Settings.MyCompany.WorkItems
				.OfType<SoftwareAlpha>().FirstOrDefault(item =>
					item.Name.Equals(input, StringComparison.OrdinalIgnoreCase) && !item.InBeta);

			if (workItem == null)
			{
				WindowManager.SpawnDialog(string.Format(Constants.AlphaProductNotFoundMessageKey.LocDef(Constants.AlphaProductNotFoundMessageText), input), false, DialogWindow.DialogType.Error);
				return;
			}

			var softwareAlpha = ((SoftwareAlpha)workItem);
			softwareAlpha.AddQuality(10f, 10f, false);
			HUD.Instance.AddPopupMessage(string.Format(Constants.AddQualityTestExecutedMessageKey.LocDef(Constants.AddQualityTestExecutedMessageText), input), "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}

		public static void TestButton()
		{
			WindowManager.SpawnInputDialog(Constants.AddQualityPromptKey.LocDef(Constants.AddQualityPromptText),
				Constants.AddQualityTitleKey.LocDef(Constants.AddQualityTitleText), "", TestAction);
		}
	}
}
