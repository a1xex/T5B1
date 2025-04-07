using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Trainer_v5.Actions;
using Trainer_v5.Configuration;
using Trainer_v5.UI.Helpers;
using Trainer_v5.UI.Extensions;

namespace Trainer_v5.UI.Windows
{
	public static class EmployeeSkillChangeWindow
	{
		private static readonly string _title = Constants.EmployeeSkillChangeTitleKey.LocDef(Constants.EmployeeSkillChangeTitleText);

		public static GUIWindow Window { get; private set; }

		public static void Show()
		{
			if (Window == null)
			{
				Window = Create();
			}
			else
			{
				Window.Toggle();
			}
		}

		private static GUIWindow Create()
		{
			var window = WindowManager.SpawnWindow();
			window.InitialTitle = window.TitleText.text = window.NonLocTitle = _title;
			window.name = Constants.EmployeeSkillChangeWindowName;
			window.MainPanel.name = Constants.EmployeeSkillChangePanelName;

			var firstColumn = FirstColumn();
			var secondColumn = SecondColumn();

			firstColumn.AddToWindow(window, Constants.FIRST_COLUMN);
			secondColumn.AddToWindow(window, Constants.SECOND_COLUMN + 100);

			var maxRows = Math.Max(firstColumn.Count, secondColumn.Count);
			window.SetWindowSize(maxRows, 510);

			return window;
		}

		private static List<GameObject> FirstColumn()
		{
			List<GameObject> list = new List<GameObject>();
			list.Add(UIHelper.CreateLabel(Constants.RolesKey.LocDef(Constants.RolesText), Constants.RolesLabelName));

			foreach (var item in TrainerSettings.RolesList)
			{
				list.Add(UIHelper.CreateToggle(item.Key.LocDef(item.Key), item.Value, x => TrainerSettings.RolesList[item.Key] = x, item.Key));
			}

			list.Add(UIHelper.CreateButton(Constants.SetSkillsKey.LocDef(Constants.SetSkillsText), SetSkillPerEmployee, Constants.SetSkillsButtonName));
			list.Add(UIHelper.CreateButton(Constants.SetBaseSkillsKey.LocDef(Constants.SetBaseSkillsText), SetBaseSkillPerEmployee, Constants.SetBaseSkillsButtonName));

			return list;
		}

		private static List<GameObject> SecondColumn()
		{
			List<GameObject> list = new List<GameObject>();
			list.Add(UIHelper.CreateLabel(Constants.SpecializationsKey.LocDef(Constants.SpecializationsText), Constants.SpecializationsLabelName));

			foreach (var item in TrainerSettings.SpecializationsList)
			{
				list.Add(UIHelper.CreateToggle(item.Key.LocDef(item.Key), item.Value, x => TrainerSettings.SpecializationsList[item.Key] = x, item.Key));
			}

			return list;
		}

		private static void SetSkillPerEmployee()
		{
			WindowManager.SpawnInputDialog(
				Constants.SetSkillPromptKey.LocDef(Constants.SetSkillPromptText),
				Constants.SetSkillTitleKey.LocDef(Constants.SetSkillTitleText),
				"3",
				EmployeeActions.SetSkillPerEmployeeAction
			);
		}

		private static void SetBaseSkillPerEmployee()
		{
			UIHelper.RequestFloat(
				Constants.SetBaseSkillPromptKey.LocDef(Constants.SetBaseSkillPromptText),
				string.Format(Constants.SetBaseSkillTitleKey.LocDef(Constants.SetBaseSkillTitleText), SelectorController.Instance.Selected.OfType<Actor>().Count()),
				SetBaseSkillPerEmployeeAction,
				0, 1
			);
		}

		private static void SetBaseSkillPerEmployeeAction(float val)
		{
			var selectedActors = SelectorController.Instance.Selected.OfType<Actor>().ToList();
			var selectedRoles = TrainerSettings.RolesList
				.Where(r => r.Value)
				.Select(e => e.Key.ToEmployeeRole())
				.ToList();

			if (selectedActors.Count == 0)
			{
				UIHelper.ShowError(Constants.SelectEmployeeErrorKey.LocDef(Constants.SelectEmployeeErrorText));
				return;
			}
			else if (selectedRoles.Count == 0)
			{
				UIHelper.ShowError(Constants.SelectRoleErrorKey.LocDef(Constants.SelectRoleErrorText));
				return;
			}

			selectedActors.ForEach(actor => selectedRoles.ForEach(role =>
			{
				actor.employee.SkillCeiling = 1f;
				actor.employee.ChangeSkillDirect(role, val);
			}));

			HUD.Instance.AddPopupMessage(Constants.EmployeeBaseSkillsSetMessageKey.LocDef(Constants.EmployeeBaseSkillsSetMessageText), "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}
	}
}