using System;
using System.Linq;
using OrbCreationExtensions;
using UnityEngine;
using Trainer_v5.UI.Helpers;

namespace Trainer_v5.Actions
{
	public static class ProductActions
	{
		private static GameSettings Settings => GameSettings.Instance;

		public static void SellProductStock()
		{
			WindowManager.SpawnDialog(Constants.SellStockMessageKey.LocDef(Constants.SellStockMessageText),
				false, DialogWindow.DialogType.Information);

			SoftwareProduct[] Products = Settings.MyCompany.Products
												 .Where(product => product.Userbase == 0)
												 .ToArray();

			if (Products.Length == 0)
			{
				return;
			}

			long totalRevenue = 0;
			for (int i = 0; i < Products.Length; i++)
			{
				SoftwareProduct product = Products[i];
				long revenueFromProduct = (long)product.PhysicalCopies * (long)(product.Price / 2);
				totalRevenue += revenueFromProduct;

				product.PhysicalCopies = 0;
			}

			if (totalRevenue > 0)
			{
				Settings.MyCompany.MakeTransaction(totalRevenue, Company.TransactionCategory.Sales, "Sold obsolete stock");
			}
		}

		public static void RemoveSoft()
		{
			WindowManager.SpawnDialog(Constants.RemoveSoftDisabledMessageKey.LocDef(Constants.RemoveSoftDisabledMessageText), false, DialogWindow.DialogType.Information);
			return;
		}

		public static void FixBugsAction(string input)
		{
			SoftwareAlpha workItem = Settings.MyCompany.WorkItems
				.OfType<SoftwareAlpha>()
				.FirstOrDefault(item => item.Name.Equals(input, System.StringComparison.OrdinalIgnoreCase) && item.InBeta);

			if (workItem == null)
			{
				UIHelper.ShowError(string.Format(Constants.BetaProductNotFoundMessageKey.LocDef(Constants.BetaProductNotFoundMessageText), input));
				return;
			}

			workItem.FixedBugs = workItem.MaxBugs;
			HUD.Instance.AddPopupMessage(string.Format(Constants.BugsFixedMessageKey.LocDef(Constants.BugsFixedMessageText), workItem.Name), "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}

		public static void FixBugs()
		{
			WindowManager.SpawnInputDialog(Constants.FixBugsPromptKey.LocDef(Constants.FixBugsPromptText),
				Constants.FixBugsTitleKey.LocDef(Constants.FixBugsTitleText), "", FixBugsAction);
		}

		public static void MaxFollowersAction(string input)
		{
			SoftwareAlpha alpha = Settings.MyCompany.WorkItems
				.OfType<SoftwareAlpha>()
				.FirstOrDefault(item => item.Name.Equals(input, System.StringComparison.OrdinalIgnoreCase) && !item.Paused);

			if (alpha == null)
			{
				UIHelper.ShowError(string.Format(Constants.ActiveAlphaProductNotFoundMessageKey.LocDef(Constants.ActiveAlphaProductNotFoundMessageText), input));
				return;
			}

			float followersToAdd = 100000000f;
			alpha.MaxFollowers += (uint)followersToAdd;
			alpha.ReEvaluateMaxFollowers();

			alpha.FollowerChange += followersToAdd;
			alpha.Followers += followersToAdd;

			HUD.Instance.AddPopupMessage(string.Format(Constants.FollowersMaxedMessageKey.LocDef(Constants.FollowersMaxedMessageText), alpha.Name), "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}

		public static void MaxFollowers()
		{
			WindowManager.SpawnInputDialog(Constants.MaxFollowersPromptKey.LocDef(Constants.MaxFollowersPromptText),
				Constants.MaxFollowersTitleKey.LocDef(Constants.MaxFollowersTitleText), "", MaxFollowersAction);
		}

		private static string _productNameForPrice;

		public static void SetProductPriceAction(string input)
		{
			SoftwareProduct product =
				Settings.MyCompany.Products.FirstOrDefault(p => p.Name.Equals(_productNameForPrice, System.StringComparison.OrdinalIgnoreCase));

			if (product == null)
			{
				UIHelper.ShowError(string.Format(Constants.ProductNotFoundMessageKey.LocDef(Constants.ProductNotFoundMessageText), _productNameForPrice));
				return;
			}

			product.Price = input.ConvertToFloatDef(product.Price);
			HUD.Instance.AddPopupMessage(string.Format(Constants.PriceSetMessageKey.LocDef(Constants.PriceSetMessageText), product.Name, product.Price), "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
			_productNameForPrice = null;
		}

		public static void SetProductPrice()
		{
			WindowManager.SpawnInputDialog(Constants.SetPriceNamePromptKey.LocDef(Constants.SetPriceNamePromptText),
				Constants.SetPriceNameTitleKey.LocDef(Constants.SetPriceNameTitleText), "", (productName) =>
			{
				SoftwareProduct product = Settings.MyCompany.Products.FirstOrDefault(p => p.Name.Equals(productName, System.StringComparison.OrdinalIgnoreCase));
				if (product == null)
				{
					UIHelper.ShowError(string.Format(Constants.ProductNotFoundMessageKey.LocDef(Constants.ProductNotFoundMessageText), productName));
					return;
				}
				_productNameForPrice = productName;
				WindowManager.SpawnInputDialog(string.Format(Constants.SetPriceAmountPromptKey.LocDef(Constants.SetPriceAmountPromptText), productName),
					Constants.SetPriceAmountTitleKey.LocDef(Constants.SetPriceAmountTitleText), product.Price.ToString("F2"), SetProductPriceAction);
			});
		}

		private static string _productNameForStock;
		public static void SetProductStockAction(string input)
		{
			SoftwareProduct product =
				Settings.MyCompany.Products.FirstOrDefault(p => p.Name.Equals(_productNameForStock, System.StringComparison.OrdinalIgnoreCase));

			if (product == null)
			{
				UIHelper.ShowError(string.Format(Constants.ProductNotFoundMessageKey.LocDef(Constants.ProductNotFoundMessageText), _productNameForStock));
				return;
			}

			int stockValue;
			if (int.TryParse(input, out stockValue))
			{
				product.PhysicalCopies = (uint)Math.Max(0, stockValue);
			}

			HUD.Instance.AddPopupMessage(string.Format(Constants.StockSetMessageKey.LocDef(Constants.StockSetMessageText), product.Name, product.PhysicalCopies), "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
			_productNameForStock = null;
		}

		public static void SetProductStock()
		{
			WindowManager.SpawnInputDialog(Constants.SetStockNamePromptKey.LocDef(Constants.SetStockNamePromptText),
				Constants.SetStockNameTitleKey.LocDef(Constants.SetStockNameTitleText), "", (productName) =>
			{
				SoftwareProduct product = Settings.MyCompany.Products.FirstOrDefault(p => p.Name.Equals(productName, System.StringComparison.OrdinalIgnoreCase));
				if (product == null)
				{
					UIHelper.ShowError(string.Format(Constants.ProductNotFoundMessageKey.LocDef(Constants.ProductNotFoundMessageText), productName));
					return;
				}
				_productNameForStock = productName;
				WindowManager.SpawnInputDialog(string.Format(Constants.SetStockAmountPromptKey.LocDef(Constants.SetStockAmountPromptText), productName),
					Constants.SetStockAmountTitleKey.LocDef(Constants.SetStockAmountTitleText), product.PhysicalCopies.ToString(), SetProductStockAction);
			});
		}

		private static string _productNameForUsers;
		public static void AddActiveUsersAction(string input)
		{
			SoftwareProduct product =
				Settings.MyCompany.Products.FirstOrDefault(p => p.Name.Equals(_productNameForUsers, System.StringComparison.OrdinalIgnoreCase));

			if (product == null)
			{
				UIHelper.ShowError(string.Format(Constants.ProductNotFoundMessageKey.LocDef(Constants.ProductNotFoundMessageText), _productNameForUsers));
				return;
			}

			int userbaseValue;
			if (int.TryParse(input, out userbaseValue))
			{
				product.Userbase = Math.Max(0, userbaseValue);
			}

			HUD.Instance.AddPopupMessage(string.Format(Constants.ActiveUsersSetMessageKey.LocDef(Constants.ActiveUsersSetMessageText), product.Name, product.Userbase), "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
			_productNameForUsers = null;
		}

		public static void AddActiveUsers()
		{
			WindowManager.SpawnInputDialog(Constants.SetActiveUsersNamePromptKey.LocDef(Constants.SetActiveUsersNamePromptText),
				Constants.SetActiveUsersNameTitleKey.LocDef(Constants.SetActiveUsersNameTitleText), "", (productName) =>
			{
				SoftwareProduct product = Settings.MyCompany.Products.FirstOrDefault(p => p.Name.Equals(productName, System.StringComparison.OrdinalIgnoreCase));
				if (product == null)
				{
					UIHelper.ShowError(string.Format(Constants.ProductNotFoundMessageKey.LocDef(Constants.ProductNotFoundMessageText), productName));
					return;
				}
				_productNameForUsers = productName;
				WindowManager.SpawnInputDialog(string.Format(Constants.SetActiveUsersAmountPromptKey.LocDef(Constants.SetActiveUsersAmountPromptText), productName),
					Constants.SetActiveUsersAmountTitleKey.LocDef(Constants.SetActiveUsersAmountTitleText), product.Userbase.ToString(), AddActiveUsersAction);
			});
		}
	}
}
