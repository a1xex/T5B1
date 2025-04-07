using System;
using System.Collections.Generic;
using System.Linq;
using OrbCreationExtensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Trainer_v5.Actions;
using Trainer_v5.Configuration;
using Trainer_v5.UI.Helpers;
using Random = System.Random;

namespace Trainer_v5
{
	public class TrainerBehaviour : ModBehaviour
	{
		private static bool _specializationsLoaded;
		private float _defaultEnvironmentISPCostFactor;
		private static float[] _edCost = new float[3] { 600f, 2000f, 5000f };
		private Random _randomInstance;

		private static GameSettings Settings => GameSettings.Instance;
		private static Dictionary<string, bool> TrainerSettingsRef => TrainerSettings.Settings;
		private static Dictionary<string, object> StoresSettingsRef => TrainerSettings.StoresSettings;

		private bool LockAgeEnabled => TrainerSettings.GetBool(TrainerSettings.LockAge);
		private bool NoiseReductionEnabled => TrainerSettings.GetBool(TrainerSettings.NoiseReduction);
		private bool NoWaterElectricityEnabled => TrainerSettings.GetBool(TrainerSettings.NoWaterElectricity);
		private bool DisableFiresEnabled => TrainerSettings.GetBool(TrainerSettings.DisableFires);
		private bool IncreaseBookshelfSkillEnabled => TrainerSettings.GetBool(TrainerSettings.IncreaseBookshelfSkill);
		private bool NoMaintenanceEnabled => TrainerSettings.GetBool(TrainerSettings.NoMaintenance);
		private bool DisableFurnitureStealingEnabled => TrainerSettings.GetBool(TrainerSettings.DisableFurnitureStealing);
		private bool CleanRoomsEnabled => TrainerSettings.GetBool(TrainerSettings.CleanRooms);
		private bool TemperatureLockEnabled => TrainerSettings.GetBool(TrainerSettings.TemperatureLock);
		private bool FullEnvironmentEnabled => TrainerSettings.GetBool(TrainerSettings.FullEnvironment);
		private bool FullRoomBrightnessEnabled => TrainerSettings.GetBool(TrainerSettings.FullRoomBrightness);
		private bool NoSicknessEnabled => TrainerSettings.GetBool(TrainerSettings.NoSickness);
		private bool NoStressEnabled => TrainerSettings.GetBool(TrainerSettings.NoStress);
		private bool FullSatisfactionEnabled => TrainerSettings.GetBool(TrainerSettings.FullSatisfaction);
		private bool NoNeedsEnabled => TrainerSettings.GetBool(TrainerSettings.NoNeeds);
		private bool FreeEmployeesEnabled => TrainerSettings.GetBool(TrainerSettings.FreeEmployees);
		private bool NoVacationEnabled => TrainerSettings.GetBool(TrainerSettings.NoVacation);
		private bool MoreInspirationEnabled => TrainerSettings.GetBool(TrainerSettings.MoreInspiration);
		private bool MoreCreativityEnabled => TrainerSettings.GetBool(TrainerSettings.MoreCreativity);
		private bool IncreaseWalkSpeedEnabled => TrainerSettings.GetBool(TrainerSettings.IncreaseWalkSpeed);
		private bool MoreHostingDealsEnabled => TrainerSettings.GetBool(TrainerSettings.MoreHostingDeals);
		private bool DisableBurglarsEnabled => TrainerSettings.GetBool(TrainerSettings.DisableBurglars);
		private bool AutoEndDesignEnabled => TrainerSettings.GetBool(TrainerSettings.AutoEndDesign);
		private bool AutoEndResearchEnabled => TrainerSettings.GetBool(TrainerSettings.AutoEndResearch);
		private bool AutoEndPatentEnabled => TrainerSettings.GetBool(TrainerSettings.AutoEndPatent);
		private bool FreePrintEnabled => TrainerSettings.GetBool(TrainerSettings.FreePrint);
		private bool IncreasePrintSpeedEnabled => TrainerSettings.GetBool(TrainerSettings.IncreasePrintSpeed);
		private bool NoEducationCostEnabled => TrainerSettings.GetBool(TrainerSettings.NoEducationCost);
		private bool FreeStaffEnabled => TrainerSettings.GetBool(TrainerSettings.FreeStaff);
		private bool NoServerCostEnabled => TrainerSettings.GetBool(TrainerSettings.NoServerCost);
		private bool DisableFireInspectionEnabled => TrainerSettings.GetBool(TrainerSettings.DisableFireInspection);
		private bool DisableForcePauseEnabled => TrainerSettings.GetBool(TrainerSettings.DisableForcePause);
		private bool DisableForceFreezeEnabled => TrainerSettings.GetBool(TrainerSettings.DisableForceFreeze);
		private bool AutoResearchStartEnabled => TrainerSettings.GetBool(TrainerSettings.AutoResearchStart);
		private bool DigitalDistributionMonopolyEnabled => TrainerSettings.GetBool(TrainerSettings.DigitalDistributionMonopol);
		private bool AutoAcceptHostingDealsEnabled => TrainerSettings.GetBool(TrainerSettings.AutoAcceptHostingDeals);
		private bool IncreaseCourierCapacityEnabled => TrainerSettings.GetBool(TrainerSettings.IncreaseCourierCapacity);
		private bool ReduceBoxPriceEnabled => TrainerSettings.GetBool(TrainerSettings.ReduceBoxPrice);
		private bool ReduceISPCostEnabled => TrainerSettings.GetBool(TrainerSettings.ReduceISPCost);
		private bool ReduceExpansionCostEnabled => TrainerSettings.GetBool(TrainerSettings.ReduceExpansionCost);

		private void Start()
		{
			_randomInstance = new Random();

			if (!isActiveAndEnabled)
			{
				return;
			}

			SceneManager.sceneLoaded += OnLevelFinishedLoading;
		}

		private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
		{
			if (isActiveAndEnabled)
			{
				switch (scene.name)
				{
					case "MainMenu":
						if (Main.TrainerButton != null)
						{
							Destroy(Main.TrainerButton.gameObject);
							Destroy(Main.SkillChangeButton.gameObject);
						}
						UnsubscribeFromEvents();
						break;
					case "MainScene":
						Main.CreateUIButtons();
						InitializeTrainerStateOnLoad();
						SubscribeToEvents();
						break;
					case "Customization":
						ActorCustomization.StartYears = new[] { 1970, 1975, 1980, 1985, 1990, 1995, 2000, 2005, 2010, 2015, 2020, 2025, 2030, 2035, 2040, 2045, 2050, 2060, 2070, 2080, 2090, 2100 };
						ActorCustomization.StartLoans = new[] { 0, 1000, 2000, 5000, 10000, 20000, 50000, 100000, 200000, 500000, 1000000, 5000000, 10000000 };
						break;
					default:
						goto case "MainMenu";
				}
			}
		}

		private void SubscribeToEvents()
		{
			TimeOfDay.OnHourPassed += OnHourPassed;
			TimeOfDay.OnDayPassed += OnDayPassed;
			TimeOfDay.OnMonthPassed += OnMonthPassed;
		}

		private void UnsubscribeFromEvents()
		{
			TimeOfDay.OnHourPassed -= OnHourPassed;
			TimeOfDay.OnDayPassed -= OnDayPassed;
			TimeOfDay.OnMonthPassed -= OnMonthPassed;
		}

		private void OnHourPassed(object obj, EventArgs args)
		{
			if (!isActiveAndEnabled || !Helpers.IsGameLoaded) return;

			ApplyHourlyWorkItemUpdates();
			ApplyHourlyCompanyUpdates();
			ApplyHourlyWorldSettingsUpdates();
			ApplyHourlyTimedEvents();
		}

		private void ApplyHourlyWorkItemUpdates()
		{
			if (AutoEndDesignEnabled)
			{
				var designDocuments = Settings.MyCompany.WorkItems
									.OfType<DesignDocument>()
									.Where(d => d.HasFinished)
									.ToList();

				designDocuments.ForEach(designDocument => designDocument.PromoteAction());
			}

			if (AutoEndResearchEnabled)
			{
				var researchWorks = Settings.MyCompany.WorkItems
									.OfType<ResearchWork>()
									.Where(rw => rw.Finished)
									.ToList();

				researchWorks.ForEach(researchWork =>
				{
					Settings.MyCompany.AddResearch(researchWork.Spec, researchWork.Year);
					TechLevel tech = Settings.simulation.AddTechLevel(researchWork.Spec, researchWork.Year, SDateTime.Now(), true);
					if (tech != null)
					{
						LegalWork legalWork = new LegalWork(tech);
						Settings.MyCompany.WorkItems.Add(legalWork);
						Settings.ApplyDefaultTeams(legalWork, ((int)legalWork.Type).ToString() + "Team");
					}
					researchWork.Kill(false);
				});
			}

			if (AutoEndPatentEnabled)
			{
				var legalWorks = Settings.MyCompany.WorkItems
								   .OfType<LegalWork>()
								   .Where(lw => lw.CurrentStage() == "Finished" && lw.Type == LegalWork.WorkType.Patent)
								   .ToList();

				legalWorks.ForEach(legalWork => legalWork.PatentNow());
			}
		}

		private void ApplyHourlyCompanyUpdates()
		{
			if (AutoAcceptHostingDealsEnabled)
			{
				AcceptHostingDealsAutomatically();
			}
		}

		private void ApplyHourlyWorldSettingsUpdates()
		{
			if (DisableBurglarsEnabled)
			{
				foreach (var burglar in Settings.sActorManager.Others["Burglars"].ToList())
				{
					burglar.Despawned = true;
					Settings.sActorManager.RemoveFromAwaiting(burglar);
				}
			}

			if (DisableFireInspectionEnabled)
			{
				foreach (var fireInspector in Settings.sActorManager.Others["FireInspector"].ToList())
				{
					fireInspector.Despawned = true;
					Settings.sActorManager.RemoveFromAwaiting(fireInspector);
				}
				Settings.ActiveFireReport.Reset();
				Settings.PassedFireInspection = true;
			}
		}

		private void ApplyHourlyTimedEvents()
		{
			if (MoreHostingDealsEnabled)
			{
				int inGameHour = TimeOfDay.Instance.Hour;

				if ((inGameHour == 9 || inGameHour == 15) && !MiscActions.DealIsPushed)
				{
					MiscActions.PushDeal();
				}
				else if (inGameHour != 9 && inGameHour != 15 && MiscActions.DealIsPushed)
				{
					MiscActions.DealIsPushed = false;
				}

				if (!MiscActions.RewardIsGained && inGameHour == 12)
				{
					MiscActions.PushReward();
				}
				else if (inGameHour != 12 && MiscActions.RewardIsGained)
				{
					MiscActions.RewardIsGained = false;
				}
			}
		}

		private void OnDayPassed(object obj, EventArgs args)
		{
			if (!isActiveAndEnabled || !Helpers.IsGameLoaded) return;

			ApplyDailyActorUpdates();
			ApplyDailyWorkItemUpdates();
			ApplyDailyCompanyUpdates();
			ApplyDailyWorldSettingsUpdates();
		}

		private void ApplyDailyActorUpdates()
		{
			bool noSicknessDaily = NoSicknessEnabled;
			if (noSicknessDaily)
			{
				TimeOfDay.Instance.Sick.Clear();
			}

			for (int i = 0; i < Settings.sActorManager.Actors.Count; i++)
			{
				Actor actor = Settings.sActorManager.Actors[i];
				Employee employee = actor.employee;

				if (noSicknessDaily)
				{
					if (actor.SpecialState == Actor.HomeState.Sick)
						actor.SpecialState = Actor.HomeState.Default;

					actor.GermAdd = 0f;
					actor.GermCount = 0f;
					actor.SickDays = 0;
				}

				if (NoVacationEnabled)
				{
					actor.VacationMonth = SDateTime.NextMonth(24);
				}
			}
		}

		private void ApplyDailyWorkItemUpdates()
		{
			if (AutoResearchStartEnabled)
			{
				StartAutoResearch();
			}
		}

		private void ApplyDailyCompanyUpdates()
		{
			if (FreePrintEnabled)
			{
				Settings.ProductPrinters.ForEach(p => p.PrintPrice = 0f);
			}

			if (IncreasePrintSpeedEnabled)
			{
				Settings.ProductPrinters.ForEach(p => p.PrintSpeed = 2f);
			}

			if (NoEducationCostEnabled)
			{
				EducationWindow.EdCost = new[] { 0f, 0f, 0f };
			}
			else
			{
				EducationWindow.EdCost = _edCost;
			}

			if (DigitalDistributionMonopolyEnabled)
			{
				ApplyDigitalDistributionMonopoly();
			}
		}

		private void ApplyDailyWorldSettingsUpdates()
		{
			GameSettings.MaxFloor = 100;

			AI.MaxBoxes = IncreaseCourierCapacityEnabled ? 108 : 54;
			AI.MaxBoxCarry = IncreaseCourierCapacityEnabled ? 18 : 9;
			AI.BoxPrice = ReduceBoxPriceEnabled ? 62.5f : 125;

			if (!_defaultEnvironmentISPCostFactor.IsZero())
			{
				Settings.Environment.ISPCostFactor = ReduceISPCostEnabled ? _defaultEnvironmentISPCostFactor / 2f : _defaultEnvironmentISPCostFactor;
			}

			Settings.ExpansionCost = ReduceExpansionCostEnabled ? 175f : 350f;
		}

		private void OnMonthPassed(object obj, EventArgs args)
		{
			if (!isActiveAndEnabled || !Helpers.IsGameLoaded) return;

			ApplyMonthlyActorUpdates();
			ApplyMonthlyCompanyUpdates();
		}

		private void ApplyMonthlyActorUpdates()
		{
			if (FreeEmployeesEnabled)
			{
				for (int i = 0; i < Settings.sActorManager.Actors.Count; i++)
				{
					Actor actor = Settings.sActorManager.Actors[i];
					Employee employee = actor.employee;

					actor.NegotiateSalary = false;
					if (employee.Salary > 0f)
					{
						employee.ChangeSalary(0f, 0f, actor, false);
					}
					employee.AskedFor = 0f;
					employee.Demanded = 0f;
					employee.UpfrontDemand = 0f;
				}
			}

			if (LockAgeEnabled)
			{
				Settings.sActorManager.Actors.ForEach(x => x.employee.BirthDate += 1);
			}
		}

		private void ApplyMonthlyCompanyUpdates()
		{
			if (FreeStaffEnabled)
			{
				Settings.StaffSalaryDue = 0f;
			}

			if (NoServerCostEnabled)
			{
				Settings.ServerCost = 0f;
			}

			if (NoWaterElectricityEnabled)
			{
				Settings.ElectricityBill = 0f;
				Settings.Waterbill = 0f;
				Settings.Gasbill = 0f;
			}
		}

		private void Update()
		{
			if (!isActiveAndEnabled || !Helpers.IsGameLoaded)
			{
				return;
			}

			HandleInput();

			ApplyFrameActorUpdates();
			ApplyFrameFurnitureUpdates();
			ApplyFrameRoomUpdates();
			ApplyFrameWorldSettingsUpdates();
		}

		private void ApplyFrameActorUpdates()
		{
			if (!Settings?.sActorManager?.Actors.Any() ?? true) return;

			foreach (var actor in Settings.sActorManager.Actors)
			{
				if (actor?.employee == null) continue;

				UpdateSingleActorFrame(actor, actor.employee);
			}
		}

		private void UpdateSingleActorFrame(Actor actor, Employee employee)
		{
			actor.WalkSpeed = IncreaseWalkSpeedEnabled ? 4f : 2f;

			if (NoiseReductionEnabled)
			{
				actor.Noisiness = 0;
			}

			if (MoreInspirationEnabled)
			{
				employee.LastInpirationUse = new SDateTime(0);
			}

			if (MoreCreativityEnabled)
			{
				employee.RevealCreativity(1f);
			}

			UpdateEmployeeFrameProperties(actor, employee);
		}

		private void UpdateEmployeeFrameProperties(Actor actor, Employee employee)
		{
			if (NoStressEnabled)
			{
				employee.Stress = 0f;
			}

			float? efficiency = GetEfficiency(employee, StoresSettingsRef);
			if (efficiency.HasValue)
			{
				actor.Effectiveness = efficiency.Value;
			}

			if (FullSatisfactionEnabled)
			{
				UpdateEmployeeSatisfaction(actor, employee);
			}

			if (NoNeedsEnabled)
			{
				UpdateEmployeeNeeds(actor, employee);
			}

			if (FreeEmployeesEnabled)
			{
				if (actor != null) actor.NegotiateSalary = false;
			}
		}

		private void UpdateEmployeeSatisfaction(Actor actor, Employee employee)
		{
			employee.JobSatisfaction = 2f;
			employee.ActiveComplaint = false;

			if (employee.Thoughts != null && employee.Thoughts.Count > 0)
			{
				List<string> keysToRemove = new List<string>();
				List<string> currentKeys = new List<string>(employee.Thoughts.Keys);

				foreach (string key in currentKeys)
				{
					Employee.ThoughtEffect thought;
					if (employee.Thoughts.TryGetValue(key, out thought))
					{
						if (thought != null && thought.Mood != null &&
							(thought.Mood.Negative || thought.Mood.Sue || !string.IsNullOrEmpty(thought.Mood.QuitReason)))
						{
							keysToRemove.Add(key);
						}
					}
				}

				foreach (string key in keysToRemove)
				{
					employee.Thoughts.Remove(key);
				}
			}
			employee.SetMood("LoveWork", actor, 1f);
		}

		private void UpdateEmployeeNeeds(Actor actor, Employee employee)
		{
			if (actor != null) actor.NextSmell = 0f;
			employee.Bladder = 1f;
			employee.Hunger = 1f;
			employee.Energy = 1f;
			employee.Social = 1f;
			employee.Posture = 1f;
			employee.ActiveComplaint = false;
			employee.HadProperFood = true;
		}

		private float? GetEfficiency(Employee employee, Dictionary<string, object> storesSettings)
		{
			if (employee.RoleString.Contains("Lead"))
			{
				object setting = storesSettings.Get(TrainerSettings.LeadEfficiencyStore);
				return setting?.MakeFloat();
			}
			else
			{
				object setting = storesSettings.Get(TrainerSettings.EfficiencyStore);
				return setting?.MakeFloat();
			}
		}

		private void ApplyFrameFurnitureUpdates()
		{
			foreach (Furniture furniture in Settings.sRoomManager.AllFurniture)
			{
				if (NoiseReductionEnabled)
				{
					furniture.ActorNoise = 0f;
					furniture.EnvironmentNoise = 0f;
					furniture.FinalNoise = 0f;
					furniture.Noisiness = 0;
				}

				if (IncreaseBookshelfSkillEnabled && furniture.Type == "Bookshelf")
				{
					furniture.AuraValues[1] = 0.75f;
				}

				if (NoMaintenanceEnabled)
				{
					if (furniture.HasUpg && (furniture.upg.Quality < 0.8f || furniture.upg.Broken))
					{
						furniture.upg.RepairMe();
					}
					if (furniture.Type == "Chair" && furniture.Comfort < 1.2f)
					{
					furniture.Comfort = 1.5f;
					}
				}

				if (DisableFurnitureStealingEnabled)
				{
					furniture.CanSteal = false;
				}

				if (DisableFiresEnabled)
				{
					if (furniture.HasUpg && furniture.upg.FireStarter > 0.0f)
					{
						furniture.upg.FireStarter = 0.0f;
					}
					if (furniture.Parent.IsOnFire)
					{
						if (furniture.Parent.Temperature > 40f)
						{
							furniture.Parent.Temperature = 21f;
						}
						furniture.Parent.StopFire();
					}
				}
			}
		}

		private void ApplyFrameRoomUpdates()
		{
			for (int i = 0; i < Settings.sRoomManager.Rooms.Count; i++)
			{
				Room room = Settings.sRoomManager.Rooms[i];

				if (CleanRoomsEnabled)
				{
					room.ClearDirt();
					room.Smell = 0f;
				}

				if (TemperatureLockEnabled)
				{
					room.Temperature = 21f;
				}

				if (FullEnvironmentEnabled)
				{
					room.FurnEnvironment = 8;
				}

				if (FullRoomBrightnessEnabled)
				{
					room.IndirectLighting = 16;
				}

				if (NoSicknessEnabled)
				{
					room.GermCount = 0f;
				}
			}
		}

		private void ApplyFrameWorldSettingsUpdates()
		{
			if (DisableForcePauseEnabled)
			{
				GameSettings.ForcePause = false;
			}

			if (DisableForceFreezeEnabled)
			{
				GameSettings.FreezeGame = false;
			}
		}

		private void HandleInput()
		{
			if (Input.GetKey(KeyCode.F1))
			{
				Main.OpenSettingsWindow();
			}

			if (Input.GetKey(KeyCode.F2))
			{
				Main.CloseSettingsWindow();
			}
		}

		private void InitializeTrainerStateOnLoad()
		{
			if (!_specializationsLoaded && Settings.MyCompany != null)
			{
				LoadSpecializations();
				ShowDiscordInvite(displayAsPopup: true);
			}

			if (_defaultEnvironmentISPCostFactor.IsZero())
			{
				_defaultEnvironmentISPCostFactor = Settings.Environment.ISPCostFactor;
			}
		}

        private void StartAutoResearch()
        {
            var activeTechLevels = MarketSimulation.Active.TechLevels;
            var defaultResearchTeams = Settings.GetDefaultTeams("Research");
            var currentYear = TimeOfDay.Instance.Year;

            if (activeTechLevels.Count > 0 && defaultResearchTeams.Count > 0)
            {
                foreach (var activeTechLevel in activeTechLevels)
                {
                    if (!Settings.IsResearching(activeTechLevel.Key))
                    {
                        int latestResearchYear = Settings.MyCompany.GetLatestResearch(activeTechLevel.Key, -1);
                        if (latestResearchYear < currentYear)
                        {
                            var researchWork = new ResearchWork(activeTechLevel.Key, currentYear);
                            researchWork.AddDevTeams(defaultResearchTeams);
                            Settings.MyCompany.AddWorkItem(researchWork);
                        }
                    }
                }
            }
        }

        private void ApplyDigitalDistributionMonopoly()
        {
#if DEBUG || SWINCBETA1_7 || SWINCBETA1_8 || SWINCBETA1_9 || SWINCBETA1_10
            foreach (var company in Settings.simulation.Companies.Values.ToList())
            {
                if (company.Bankrupt && company.Distribution != null)
                {
                    MarketSimulation.Active.DistributionPlatforms.Remove(company.Distribution);
                    HUD.Instance.digitalDistributionWindow.PlatformList.Items.Remove(company.Distribution);
                }

                if (company == Settings.MyCompany || company.Distribution == null || !company.Distribution.Open)
                    continue;

                company.Distribution.SetCut(1f);
                company.Distribution.SetAutoAcceptClients(false);
                company.Distribution.AvailableBandwidth = 0f;
                company.Distribution.ItemSales = 0f;
                company.Distribution.ActualItemSales = 0f;
                company.Distribution.LastLoad = 0f;
                company.Distribution.MarketShare = 0f;
                MarketSimulation.Active.ClosePlatform(company.Distribution);
            }
#endif
        }

        private void AcceptHostingDealsAutomatically()
        {
#if DEBUG || SWINCBETA1_7 || SWINCBETA1_8 || SWINCBETA1_9 || SWINCBETA1_10
            var serverGroups = Settings.GetAllServerGroups().ToList();
            if (serverGroups.Count == 0) return;

            ServerGroup mostPowerfulServerGroup = serverGroups.OrderByDescending(sg => sg.PowerSum).FirstOrDefault();
            if (mostPowerfulServerGroup == null) return;

            var availableServerDeals = HUD.Instance.dealWindow.AllDeals.Values.OfType<ServerDeal>().ToList();
            if (availableServerDeals.Count == 0) return;

            var activeServerDealProducts = HUD.Instance.dealWindow.GetActiveDeals()
                                             .OfType<ServerDeal>()
                                             .Select(d => d.Product)
                                             .ToHashSet();

            foreach (var serverDeal in availableServerDeals)
            {
                if (!activeServerDealProducts.Contains(serverDeal.Product))
                {
                    HUD.Instance.dealWindow.ActuallyAcceptDeal(serverDeal, true);
                    Settings.RegisterWithServer(mostPowerfulServerGroup.Name, serverDeal);
                }
            }
#endif
        }

		private static void LoadSpecializations()
		{
			if (TrainerSettings.SpecializationsList != null && TrainerSettings.SpecializationsList.Count() > 0)
			{
				return;
			}

			var specializations = new Dictionary<string, bool>();

			foreach (var role in TrainerSettings.RolesList)
			{
				foreach (var specialization in Settings.GetAllSpecializations(role.Key.ToEmployeeRole()))
				{
					if (!specializations.ContainsKey(specialization))
					{
						specializations.Add(specialization, false);
					}
				}
			}

			TrainerSettings.SpecializationsList = specializations;

			_specializationsLoaded = true;
		}

		private void ShowDiscordInvite(bool displayAsPopup = false)
		{
			string message = string.Format(Constants.DiscordInviteMessageKey.LocDef(Constants.DiscordInviteMessageText), Helpers.DiscordUrl);
			if (displayAsPopup)
			{
				HUD.Instance.AddPopupMessage(message, "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
			}
			else
			{
				WindowManager.SpawnDialog(message, false, DialogWindow.DialogType.Information);
			}
		}

		public static void SetSkillPerEmployeeAction(string input)
		{
			var selectedActors = SelectorController.Instance.Selected.OfType<Actor>().ToList();
			var selectedRoles = TrainerSettings.RolesList.Where(r => r.Value).ToList();
			var selectedSpecializations = TrainerSettings.SpecializationsList.Where(s => s.Value).ToList();

			int amount;
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
			else if (selectedSpecializations.Count == 0)
			{
				UIHelper.ShowError(Constants.SelectSpecializationErrorKey.LocDef(Constants.SelectSpecializationErrorText));
				return;
			}
			else if (!int.TryParse(input, out amount) || amount == 0 || amount < -3 || amount > 3)
			{
				UIHelper.ShowError(Constants.InvalidSkillAmountErrorKey.LocDef(Constants.InvalidSkillAmountErrorText));
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

		public override void OnActivate() { }

		public override void OnDeactivate() { }
	}
}
