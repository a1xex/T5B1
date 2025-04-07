using System;
using System.Linq;
using UnityEngine;
using Trainer_v5.Configuration;

namespace Trainer_v5.Actions
{
	public static class EmployeeActions
	{
		private static GameSettings Settings => GameSettings.Instance;

		public static void ResetAgeOfEmployees()
		{
			var currentTime = SDateTime.Now();
			foreach (var actor in Settings.sActorManager.Actors)
			{
				actor.employee.BirthDate = currentTime - Employee.Youngest * 12;
				actor.UpdateAgeLook();
			}

			HUD.Instance.AddPopupMessage(Constants.EmployeeAgeResetMessageKey.LocDef(Constants.EmployeeAgeResetMessageText), "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}

		public static void EmployeesToMax()
		{
			SoftwareType[] softwareTypes = MarketSimulation.Active.SoftwareTypes.Values.ToArray();
			Employee.EmployeeRole[] employeeRoles = (Employee.EmployeeRole[])Enum.GetValues(typeof(Employee.EmployeeRole));

			if (!Helpers.IsGameLoaded || SelectorController.Instance == null)
			{
				return;
			}

			foreach (Actor actor in Settings.sActorManager.Actors.ToArray())
			{
				actor.employee.CreativityKnown = 1f;

				foreach (SoftwareType t in softwareTypes)
				{
#if DEBUG || SWINCBETA1_7 || SWINCBETA1_8 || SWINCBETA1_9 || SWINCBETA1_10
					actor.employee.LeadSpecializationFix[t.ToString()] = 1f;
#else
					actor.employee.LeadSpecialization[t] = 1f;
#endif
				}

				foreach (Employee.EmployeeRole employeeRole in employeeRoles)
				{
					actor.employee.ChangeSkillDirect(employeeRole, 1f);

					string[] specializations = Settings.GetAllSpecializations(employeeRole);
					foreach (string specialization in specializations)
					{
						actor.employee.AddSpecialization(employeeRole, specialization, false, true, 3);
					}
				}
			}

			HUD.Instance.AddPopupMessage(Constants.AllEmployeesMaxSkilledMessageKey.LocDef(Constants.AllEmployeesMaxSkilledMessageText), "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0, 1);
		}

		public static void HREmployees()
		{
			if (!Helpers.IsGameLoaded || SelectorController.Instance == null)
			{
				return;
			}

			Actor[] Actors = Settings.sActorManager.Actors
									 .Where(actor => actor.employee.RoleString.Contains("Lead"))
									 .ToArray();

			if (Actors.Length == 0)
			{
				return;
			}

			for (var i = 0; i < Actors.Length; i++)
			{
				Actors[i].employee.SetSpecialization(Actors[i].employee.GetRoleOrNatural(), "HR", 5);
			}

			HUD.Instance.AddPopupMessage(Constants.LeadersHRSetMessageKey.LocDef(Constants.LeadersHRSetMessageText), "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0, 1);
		}

		public static void SetSkillPerEmployeeAction(string input)
		{
			var selectedActors = SelectorController.Instance.Selected.OfType<Actor>().ToList();
			var selectedRoles = TrainerSettings.RolesList.Where(r => r.Value).ToList();
			var selectedSpecializations = TrainerSettings.SpecializationsList.Where(s => s.Value).ToList();

			int amount;
			if (selectedActors.Count == 0)
			{
				WindowManager.SpawnDialog(Constants.SelectEmployeeErrorKey.LocDef(Constants.SelectEmployeeErrorText), false, DialogWindow.DialogType.Error);
				return;
			}
			else if (selectedRoles.Count == 0)
			{
				WindowManager.SpawnDialog(Constants.SelectRoleErrorKey.LocDef(Constants.SelectRoleErrorText), false, DialogWindow.DialogType.Error);
				return;
			}
			else if (selectedSpecializations.Count == 0)
			{
				WindowManager.SpawnDialog(Constants.SelectSpecializationErrorKey.LocDef(Constants.SelectSpecializationErrorText), false, DialogWindow.DialogType.Error);
				return;
			}
			else if (!int.TryParse(input, out amount) || amount == 0 || amount < -3 || amount > 3)
			{
				WindowManager.SpawnDialog(Constants.InvalidSkillAmountErrorKey.LocDef(Constants.InvalidSkillAmountErrorText), false, DialogWindow.DialogType.Error);
				return;
			}
			else
			{
				selectedActors.ForEach(actor =>
				{
					foreach (var role in selectedRoles)
					{
						foreach (var specialization in selectedSpecializations)
						{
							actor.employee.AddSpecialization(role.Key.ToEmployeeRole(), specialization.Key, false, true, amount);
						}
					}
				});

				HUD.Instance.AddPopupMessage(Constants.EmployeeSkillsSetMessageKey.LocDef(Constants.EmployeeSkillsSetMessageText), "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
			}
		}

		public static void SetSkillPerEmployee()
		{
			WindowManager.SpawnInputDialog(Constants.SetSkillPromptKey.LocDef(Constants.SetSkillPromptText),
				Constants.SetSkillTitleKey.LocDef(Constants.SetSkillTitleText),
				"3",
				SetSkillPerEmployeeAction);
		}
	}
}
