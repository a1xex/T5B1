namespace Trainer_v5
{
	public static class Constants
	{
		// --- Layout Constants ---
		public const int ELEMENT_WIDTH = 150;
		public const int ELEMENT_HEIGHT = 32;
		public const int X_SETTINGS_WINDOW = 982;
		public const int X_EMPLOYEESKILLCHANGE_WINDOW = 300;
		public const int FIRST_COLUMN = 1;
		public const int SECOND_COLUMN = 161;
		public const int THIRD_COLUMN = 322;
		public const int FOURTH_COLUMN = 483;
		public const int FIFTH_COLUMN = 644;
		public const int SIXTH_COLUMN = 805;

		// --- Internal UI Element Names --- (Used for finding/naming GameObjects)
		#region UI Element Names
		// Main
		public const string TrainerButtonName = "Trainer";
		public const string EmployeeSkillButtonName = "EmployeeSkill";
		public const string OptionsScreenLabelName = "OptionsScreen";
		// Settings Window
		public const string SettingsWindowName = "TrainerSettings";
		public const string SettingsPanelName = "TrainerSettingsPanel";
		// Col 1 Buttons
		public const string AddMoneyButtonName = "AddMoney";
		public const string SetProductPriceButtonName = "SetProductPrice";
		public const string SetProductStockButtonName = "SetProductStock";
		public const string AddActiveUsersButtonName = "AddActiveUsers";
		public const string UnlockAndClaimRewardsButtonName = "UnlockAndClaimRewards";
		public const string ExtendDeadlineButtonName = "ExtendDeadline";
		public const string UnlockAllSpaceButtonName = "UnlockAllSpace";
		public const string UnlockFurnitureButtonName = "UnlockFurniture";
		public const string FixBugsButtonName = "FixBugs";
		public const string MaxFollowersButtonName = "MaxFollowers";
		public const string ClearLoansButtonName = "ClearLoans";
		// Col 2 Buttons & Toggles
		public const string MaxReputationButtonName = "MaxReputation";
		public const string MaxMarketRecognitionButtonName = "MaxMarketRecognition";
		public const string TakeoverCompanyButtonName = "TakeoverCompany";
		public const string SubDCompanyButtonName = "SubDCompany";
		public const string ForceBankruptButtonName = "ForceBankrupt";
		public const string SellProductStockButtonName = "SellProductStock";
		public const string MonthDaysButtonName = "MonthDays";
		public const string DisableFireInspectionToggleName = "DisableFireInspection";
		public const string DisableForcePauseToggleName = "DisableForcePause";
		public const string DisableForceFreezeToggleName = "DisableForceFreeze";
		public const string DisableBurglarsToggleName = "DisableBurglars";
		// Col 3 Buttons & Toggles
		public const string NoStressToggleName = "NoStress";
		public const string NoVacationToggleName = "NoVacation";
		public const string NoSicknessToggleName = "NoSickness";
		public const string FullSatisfactionToggleName = "FullSatisfaction";
		public const string NoNeedsToggleName = "NoNeeds";
		public const string FreeEmployeesToggleName = "FreeEmployees";
		public const string LockAgeToggleName = "LockAge";
		public const string IncreaseWalkSpeedToggleName = "IncreaseWalkSpeed";
		public const string MoreInspirationToggleName = "MoreInspiration";
		public const string DisableFurnitureStealingToggleName = "DisableFurnitureStealing";
		public const string ResetAgeButtonName = "ResetAge";
		public const string EmployeesToMaxButtonName = "EmployeesToMax";
		public const string HREmployeesButtonName = "HREmployees";
		// Col 4 Toggles
		public const string FullRoomBrightnessToggleName = "FullRoomBrightness";
		public const string CleanRoomsToggleName = "CleanRooms";
		public const string FullEnvironmentToggleName = "FullEnvironment";
		public const string NoiseReductionToggleName = "NoiseReduction";
		public const string TemperatureLockToggleName = "TemperatureLock";
		public const string NoWaterElectricityToggleName = "NoWaterElectricity";
		public const string NoMaintenanceToggleName = "NoMaintenance";
		public const string DisableFiresToggleName = "DisableFires";
		public const string IncreaseBookshelfSkillToggleName = "IncreaseBookshelfSkill";
		public const string MoreHostingDealsToggleName = "MoreHostingDeals";
		public const string AutoAcceptHostingDealsToggleName = "AutoAcceptHostingDeals";
		public const string IncreaseCourierCapacityToggleName = "IncreaseCourierCapacity";
		// Col 5 Controls & Toggles
		public const string ReduceISPCostToggleName = "ReduceISPCost";
		public const string IncreasePrintSpeedToggleName = "IncreasePrintSpeed";
		public const string FreePrintToggleName = "FreePrint";
		public const string NoServerCostToggleName = "NoServerCost";
		public const string ReduceExpansionCostToggleName = "ReduceExpansionCost";
		public const string NoEducationCostToggleName = "NoEducationCost";
		public const string AutoEndDesignToggleName = "AutoEndDesign";
		public const string AutoEndResearchToggleName = "AutoEndResearch";
		public const string AutoEndPatentToggleName = "AutoEndPatent";
		public const string ReduceBoxPriceToggleName = "ReduceBoxPrice";
		public const string AutoResearchStartToggleName = "AutoResearchStart";
		public const string DigitalDistributionMonopolToggleName = "DigitalDistributionMonopol";
		public const string FreeStaffToggleName = "FreeStaff";
		public const string EfficiencyLabelName = "Efficiency";
		public const string EfficiencyComboBoxName = "Efficiency";
		public const string LeadEfficiencyLabelName = "LeadEfficiency";
		public const string LeadEfficiencyComboBoxName = "LeadEfficiency";
		// Col 6 Buttons & Toggles
		public const string ExperimentalToggleName = "Experimental";
		public const string TestButtonName = "Test";
		public const string TestButton2Name = "Test2";
		public const string MoreCreativityToggleName = "MoreCreativity";
		// Employee Skill Change Window
		public const string EmployeeSkillChangeWindowName = "EditEmployee";
		public const string EmployeeSkillChangePanelName = "EditEmployeePanel";
		public const string RolesLabelName = "Roles";
		public const string SpecializationsLabelName = "Specializations";
		public const string SetSkillsButtonName = "SetSkills";
		public const string SetBaseSkillsButtonName = "SetBaseSkills";
		// Employee Trait Change Window
		public const string EmployeeTraitChangeWindowName = "EditTrait";
		public const string EmployeeTraitChangePanelName = "EditTraitPanel";
		public const string GoodTraitsLabelName = "GoodTraits";
		public const string NeutralTraitsLabelName = "NeutralTraits";
		public const string BadTraitsLabelName = "BadTraits";
		public const string RefreshTraitsButtonName = "RefreshTraits";
		// Employee Demand Change Window
		public const string EmployeeDemandChangeWindowName = "EditDemands";
		public const string EmployeeDemandChangePanelName = "EditDemandsPanel";
		public const string DemandsLabelName = "Demands";
		public const string RefreshDemandsButtonName = "RefreshDemands";
		// Employee Lead Spec Change Window
		public const string EmployeeLeadSpecChangeWindowName = "EditLeadSpec";
		public const string EmployeeLeadSpecChangePanelName = "EditLeadSpecPanel";
		public const string LeadSpecLabelName = "LeadSpec";
		public const string AllLeadSpecButtonName = "AllLeadSpec";
		public const string NoneLeadSpecButtonName = "NoneLeadSpec";
		public const string SetLeadSpecButtonName = "SetLeadSpec";
		#endregion UI Element Names

		// --- Localization Keys --- (Used as the key for LocDef lookups)
		#region Localization Keys
		// Main
		public const string SkillChangeKey = "SkillChange";
		public const string OptionsScreenInfoKey = "OptionsScreenInfo";
		public const string EmployeeSkillButtonKey = "EmployeeSkillButton";
		// Settings Window Titles/Labels/Toggle Texts
		public const string NoStressKey = "NoStress";
		public const string NoVacationKey = "NoVacation";
		public const string NoSicknessKey = "NoSickness";
		public const string FullSatisfactionKey = "FullSatisfaction";
		public const string NoNeedsKey = "NoNeeds";
		public const string FreeEmployeesKey = "FreeEmployees";
		public const string LockAgeKey = "LockAge";
		public const string IncreaseWalkSpeedKey = "IncreaseWalkSpeed";
		public const string MoreInspirationKey = "MoreInspiration";
		public const string MoreCreativityKey = "MoreCreativity";
		public const string FullRoomBrightnessKey = "FullRoomBrightness";
		public const string CleanRoomsKey = "CleanRooms";
		public const string FullEnvironmentKey = "FullEnvironment";
		public const string NoiseReductionKey = "NoiseReduction";
		public const string TemperatureLockKey = "TemperatureLock";
		public const string NoWaterElectricityKey = "NoWaterElectricity";
		public const string NoMaintenanceKey = "NoMaintenance";
		public const string DisableFiresKey = "DisableFires";
		public const string DisableFurnitureStealingKey = "DisableFurnitureStealing";
		public const string IncreaseBookshelfSkillKey = "IncreaseBookshelfSkill";
		public const string MoreHostingDealsKey = "MoreHostingDeals";
		public const string IncreaseCourierCapacityKey = "IncreaseCourierCapacity";
		public const string ReduceISPCostKey = "ReduceISPCost";
		public const string IncreasePrintSpeedKey = "IncreasePrintSpeed";
		public const string FreePrintKey = "FreePrint";
		public const string NoServerCostKey = "NoServerCost";
		public const string ReduceExpansionCostKey = "ReduceExpansionCost";
		public const string NoEducationCostKey = "NoEducationCost";
		public const string AutoEndDesignKey = "AutoEndDesign";
		public const string AutoEndResearchKey = "AutoEndResearch";
		public const string AutoEndPatentKey = "AutoEndPatent";
		public const string ReduceBoxPriceKey = "ReduceBoxPrice";
		public const string AutoResearchStartKey = "AutoResearchStart";
		public const string DigitalDistributionMonopolKey = "DigitalDistributionMonopol";
		public const string DisableFireInspectionKey = "DisableFireInspection";
		public const string DisableForcePauseKey = "DisableForcePause";
		public const string DisableForceFreezeKey = "DisableForceFreeze";
		public const string AutoAcceptHostingDealsKey = "AutoAcceptHostingDeals";
		public const string DisableBurglarsKey = "DisableBurglars";
		public const string ExperimentalKey = "Experimental";
		public const string FreeStaffKey = "FreeStaff";
		public const string EfficiencyKey = "Efficiency";
		public const string LeadEfficiencyKey = "LeadEfficiency";
		// Settings Window Button Texts
		public const string AddMoneyKey = "AddMoney";
		public const string SetProductPriceKey = "SetProductPrice";
		public const string SetProductStockKey = "SetProductStock";
		public const string AddActiveUsersKey = "AddActiveUsers";
		public const string UnlockAndClaimRewardsKey = "UnlockAndClaimRewards";
		public const string ExtendDeadlineKey = "ExtendDeadline";
		public const string UnlockAllSpaceKey = "UnlockAllSpace";
		public const string UnlockFurnitureKey = "UnlockFurniture";
		public const string FixBugsKey = "FixBugs";
		public const string MaxFollowersKey = "MaxFollowers";
		public const string ClearLoansKey = "ClearLoans";
		public const string MaxReputationKey = "MaxReputation";
		public const string MaxMarketRecognitionKey = "MaxMarketRecognition";
		public const string TakeoverCompanyKey = "TakeoverCompany";
		public const string SubDCompanyKey = "SubDCompany";
		public const string ForceBankruptKey = "ForceBankrupt";
		public const string SellProductStockKey = "SellProductStock";
		public const string MonthDaysKey = "MonthDays";
		public const string ResetAgeKey = "ResetAge";
		public const string EmployeesToMaxKey = "EmployeesToMax";
		public const string HREmployeesKey = "HREmployees";
		public const string TestKey = "Test";
		public const string TestButtonKey = "TestButton";
		// Employee Skill Change Window
		public const string EmployeeSkillChangeTitleKey = "EmployeeSkillChangeTitle";
		public const string RolesKey = "Roles";
		public const string SpecializationsKey = "Specializations";
		public const string SetSkillsKey = "SetSkills";
		public const string SetBaseSkillsKey = "SetBaseSkills";
		// Skill Dialogs
		public const string SetBaseSkillPromptKey = "SetBaseSkillPrompt";
		public const string SetBaseSkillTitleKey = "SetBaseSkillTitle";
		public const string SetSkillPromptKey = "SetSkillPrompt";
		public const string SetSkillTitleKey = "SetSkillTitle";
		// Employee Trait Change Window
		public const string EditTraitsTitleKey = "EditTraitsTitle";
		public const string EditTraitsDefaultTitleKey = "EditTraitsDefaultTitle";
		public const string GoodTraitsKey = "GoodTraits";
		public const string NeutralTraitsKey = "NeutralTraits";
		public const string BadTraitsKey = "BadTraits";
		public const string RefreshKey = "Refresh";
		// Employee Demand Change Window
		public const string EditDemandsTitleKey = "EditDemandsTitle";
		public const string EditDemandsDefaultTitleKey = "EditDemandsDefaultTitle";
		public const string DemandsKey = "Demands";
		// Employee Lead Spec Change Window
		public const string EditLeadSpecTitleKey = "EditLeadSpecTitle";
		public const string EditLeadSpecDefaultTitleKey = "EditLeadSpecDefaultTitle";
		public const string LeadSpecKey = "LeadSpec";
		public const string AllKey = "All";
		public const string NoneKey = "None";
		public const string SetLeadSpecKey = "SetLeadSpec";
		// Lead Spec Dialogs
		public const string SetLeadSpecPromptKey = "SetLeadSpecPrompt";
		public const string SetLeadSpecTitleKey = "SetLeadSpecTitle";
		// Common Dialog Errors/Info Keys
		public const string InvalidInputErrorKey = "InvalidInputError";
		public const string SelectEmployeeErrorKey = "SelectEmployeeError";
		public const string SelectRoleErrorKey = "SelectRoleError";
		public const string SelectSpecializationErrorKey = "SelectSpecializationError";
		public const string InvalidSkillAmountErrorKey = "InvalidSkillAmountError";
		public const string InvalidBaseSkillAmountErrorKey = "InvalidBaseSkillAmountError";
		public const string SelectLeadSpecErrorKey = "SelectLeadSpecError";
		public const string InvalidLeadSpecAmountErrorKey = "InvalidLeadSpecAmountError";
		public const string InvalidInputMinValueErrorKey = "InvalidInputMinValueError";
		public const string InvalidInputMaxValueErrorKey = "InvalidInputMaxValueError";
		// Popup Message Keys
		public const string AllLoansClearedMessageKey = "AllLoansClearedMessage";
		public const string AllPlotsUnlockedMessageKey = "AllPlotsUnlockedMessage";
		public const string AllFurnitureUnlockedMessageKey = "AllFurnitureUnlockedMessage";
		public const string DeadlinesExtendedMessageKey = "DeadlinesExtendedMessage";
		public const string MoneyAddedMessageKey = "MoneyAddedMessage";
		public const string MaxReputationAppliedMessageKey = "MaxReputationAppliedMessage";
		public const string MaxMarketRecognitionAppliedMessageKey = "MaxMarketRecognitionAppliedMessage";
		public const string AllRewardsUnlockedMessageKey = "AllRewardsUnlockedMessage";
		public const string DesignDocIterationTestExecutedMessageKey = "DesignDocIterationTestExecutedMessage";
		public const string AddQualityTestExecutedMessageKey = "AddQualityTestExecutedMessage";
		public const string SellStockMessageKey = "SellStockMessage";
		public const string RemoveSoftDisabledMessageKey = "RemoveSoftDisabledMessage";
		public const string BugsFixedMessageKey = "BugsFixedMessage";
		public const string FollowersMaxedMessageKey = "FollowersMaxedMessage";
		public const string PriceSetMessageKey = "PriceSetMessage";
		public const string StockSetMessageKey = "StockSetMessage";
		public const string ActiveUsersSetMessageKey = "ActiveUsersSetMessage";
		public const string AIBankruptMessageKey = "AIBankruptMessage";
		public const string CompanyNotFoundMessageKey = "CompanyNotFoundMessage";
		public const string CannotTakeoverOwnCompanyMessageKey = "CannotTakeoverOwnCompanyMessage";
		public const string CannotBuyoutCompanyMessageKey = "CannotBuyoutCompanyMessage";
		public const string CompanyTakenOverMessageKey = "CompanyTakenOverMessage";
		public const string CannotSubsidiaryOwnCompanyMessageKey = "CannotSubsidiaryOwnCompanyMessage";
		public const string AlreadySubsidiaryMessageKey = "AlreadySubsidiaryMessage";
		public const string SubsidiaryMadeMessageKey = "SubsidiaryMadeMessage";
		public const string CannotBankruptOwnCompanyMessageKey = "CannotBankruptOwnCompanyMessage";
		public const string CompanyBankruptStatusMessageKey = "CompanyBankruptStatusMessage";
		public const string EmployeeAgeResetMessageKey = "EmployeeAgeResetMessage";
		public const string AllEmployeesMaxSkilledMessageKey = "AllEmployeesMaxSkilledMessage";
		public const string LeadersHRSetMessageKey = "LeadersHRSetMessage";
		public const string EmployeeSkillsSetMessageKey = "EmployeeSkillsSetMessage";
		public const string EmployeeBaseSkillsSetMessageKey = "EmployeeBaseSkillsSetMessage";
		public const string DiscordInviteMessageKey = "DiscordInviteMessage";
		public const string MonthDaysRestartWarningKey = "MonthDaysRestartWarning";
		public const string AlphaProductNotFoundMessageKey = "AlphaProductNotFoundMessage";
		public const string BetaProductNotFoundMessageKey = "BetaProductNotFoundMessage";
		public const string ActiveAlphaProductNotFoundMessageKey = "ActiveAlphaProductNotFoundMessage";
		public const string ProductNotFoundMessageKey = "ProductNotFoundMessage";
		// Input Dialog Prompt/Title Keys
		public const string MonthDaysPromptKey = "MonthDaysPrompt";
		public const string MonthDaysTitleKey = "MonthDaysTitle";
		public const string AddMoneyPromptKey = "AddMoneyPrompt";
		public const string AddMoneyTitleKey = "AddMoneyTitle";
		public const string AreYouSurePromptKey = "AreYouSurePrompt";
		public const string ConfirmationTitleKey = "ConfirmationTitle";
		public const string MaxRepConfirmActionKey = "MaxRepConfirmAction";
		public const string AddQualityPromptKey = "AddQualityPrompt";
		public const string AddQualityTitleKey = "AddQualityTitle";
		public const string FixBugsPromptKey = "FixBugsPrompt";
		public const string FixBugsTitleKey = "FixBugsTitle";
		public const string MaxFollowersPromptKey = "MaxFollowersPrompt";
		public const string MaxFollowersTitleKey = "MaxFollowersTitle";
		public const string SetPriceNamePromptKey = "SetPriceNamePrompt";
		public const string SetPriceNameTitleKey = "SetPriceNameTitle";
		public const string SetPriceAmountPromptKey = "SetPriceAmountPrompt";
		public const string SetPriceAmountTitleKey = "SetPriceAmountTitle";
		public const string SetStockNamePromptKey = "SetStockNamePrompt";
		public const string SetStockNameTitleKey = "SetStockNameTitle";
		public const string SetStockAmountPromptKey = "SetStockAmountPrompt";
		public const string SetStockAmountTitleKey = "SetStockAmountTitle";
		public const string SetActiveUsersNamePromptKey = "SetActiveUsersNamePrompt";
		public const string SetActiveUsersNameTitleKey = "SetActiveUsersNameTitle";
		public const string SetActiveUsersAmountPromptKey = "SetActiveUsersAmountPrompt";
		public const string SetActiveUsersAmountTitleKey = "SetActiveUsersAmountTitle";
		public const string TakeoverCompanyNamePromptKey = "TakeoverCompanyNamePrompt";
		public const string TakeoverCompanyTitleKey = "TakeoverCompanyTitle";
		public const string SubsidiaryNamePromptKey = "SubsidiaryNamePrompt";
		public const string SubsidiaryTitleKey = "SubsidiaryTitle";
		public const string ForceBankruptPromptKey = "ForceBankruptPrompt";
		public const string ForceBankruptTitleKey = "ForceBankruptTitle";
		#endregion Localization Keys

		// --- Default UI Text Strings --- (Used as default values for LocDef)
		#region Default UI Text
		// Main
		public const string SkillChangeText = "Skill Change";
		public const string OptionsScreenInfoText = "Please load the game and press 'Trainer' button.";
		public const string EmployeeSkillButtonText = "Skill Change";
		// Settings Window Titles/Labels/Toggle Texts
		public const string NoStressText = "No Stress";
		public const string NoVacationText = "No Vacation";
		public const string NoSicknessText = "No Sickness";
		public const string FullSatisfactionText = "Full Satisfaction";
		public const string NoNeedsText = "No Needs";
		public const string FreeEmployeesText = "Free Employees";
		public const string LockAgeText = "Lock Age";
		public const string IncreaseWalkSpeedText = "Increase Walk Speed";
		public const string MoreInspirationText = "More Inspiration";
		public const string MoreCreativityText = "More Creativity";
		public const string FullRoomBrightnessText = "Full Room Brightness";
		public const string CleanRoomsText = "Clean Rooms";
		public const string FullEnvironmentText = "Full Environment";
		public const string NoiseReductionText = "Noise Reduction";
		public const string TemperatureLockText = "Temperature Lock";
		public const string NoWaterElectricityText = "No Water/Electricity";
		public const string NoMaintenanceText = "No Maintenance";
		public const string DisableFiresText = "Disable Fires";
		public const string DisableFurnitureStealingText = "Disable Furniture Stealing";
		public const string IncreaseBookshelfSkillText = "Increase Bookshelf Skill";
		public const string MoreHostingDealsText = "More Hosting Deals";
		public const string IncreaseCourierCapacityText = "Increase Courier Capacity";
		public const string ReduceISPCostText = "Reduce ISP Cost";
		public const string IncreasePrintSpeedText = "Increase Print Speed";
		public const string FreePrintText = "Free Print";
		public const string NoServerCostText = "No Server Cost";
		public const string ReduceExpansionCostText = "Reduce Expansion Cost";
		public const string NoEducationCostText = "No Education Cost";
		public const string AutoEndDesignText = "Auto End Design";
		public const string AutoEndResearchText = "Auto End Research";
		public const string AutoEndPatentText = "Auto End Patent";
		public const string ReduceBoxPriceText = "Reduce Box Price";
		public const string AutoResearchStartText = "Auto Research Start";
		public const string DigitalDistributionMonopolText = "Digital Distribution Monopol";
		public const string DisableFireInspectionText = "Disable Fire Inspection";
		public const string DisableForcePauseText = "Disable Force Pause";
		public const string DisableForceFreezeText = "Disable Force Freeze";
		public const string AutoAcceptHostingDealsText = "Auto Accept Hosting Deals";
		public const string DisableBurglarsText = "Disable Burglars";
		public const string ExperimentalText = "Experimental Features";
		public const string FreeStaffText = "Free Staff";
		public const string EfficiencyText = "Efficiency";
		public const string LeadEfficiencyText = "Lead Efficiency";
		// Settings Window Button Texts
		public const string AddMoneyText = "Add Money";
		public const string SetProductPriceText = "Set Product Price";
		public const string SetProductStockText = "Set Product Stock";
		public const string AddActiveUsersText = "Add Active Users";
		public const string UnlockAndClaimRewardsText = "Unlock and Claim Rewards";
		public const string ExtendDeadlineText = "Extend Deadline";
		public const string UnlockAllSpaceText = "Unlock All Space";
		public const string UnlockFurnitureText = "Unlock Furniture";
		public const string FixBugsText = "Fix Bugs";
		public const string MaxFollowersText = "Max Followers";
		public const string ClearLoansText = "Clear Loans";
		public const string MaxReputationText = "Max Reputation";
		public const string MaxMarketRecognitionText = "Max Market Recognition";
		public const string TakeoverCompanyText = "Takeover Company";
		public const string SubDCompanyText = "Make Subsidiary";
		public const string ForceBankruptText = "Force Bankrupt";
		public const string SellProductStockText = "Sell Product Stock";
		public const string MonthDaysText = "Change Days per Month";
		public const string ResetAgeText = "Reset Employee Age";
		public const string EmployeesToMaxText = "Max All Employee Skills";
		public const string HREmployeesText = "Set HR Spec. on Leaders";
		public const string TestText = "Test";
		public const string TestButtonText = "Test Button";
		// Employee Skill Change Window
		public const string EmployeeSkillChangeTitleText = "Employee Skill Change, by Trawis";
		public const string RolesText = "Roles";
		public const string SpecializationsText = "Specializations";
		public const string SetSkillsText = "Set Skills";
		public const string SetBaseSkillsText = "Set Base Skills";
		// Skill Dialogs
		public const string SetBaseSkillPromptText = "How many base skill do you want?\nMin = 0, Max = 1.0";
		public const string SetBaseSkillTitleText = "Set base skill for {0} actor(s)";
		public const string SetSkillPromptText = "How many specialization stars do you want?\nMin = -3, Max = 3";
		public const string SetSkillTitleText = "Stars amount";
		// Employee Trait Change Window
		public const string EditTraitsTitleText = "Edit traits for {0}";
		public const string EditTraitsDefaultTitleText = "Edit traits for Nobody";
		public const string GoodTraitsText = "Good";
		public const string NeutralTraitsText = "Neutral";
		public const string BadTraitsText = "Bad";
		public const string RefreshText = "Refresh";
		// Employee Demand Change Window
		public const string EditDemandsTitleText = "Edit demands for {0}";
		public const string EditDemandsDefaultTitleText = "Edit demands for Nobody";
		public const string DemandsText = "Demands";
		// Employee Lead Spec Change Window
		public const string EditLeadSpecTitleText = "Edit lead specialization for {0}";
		public const string EditLeadSpecDefaultTitleText = "Edit lead specialization for Nobody";
		public const string LeadSpecText = "Lead Spec";
		public const string AllText = "All";
		public const string NoneText = "None";
		public const string SetLeadSpecText = "Set LeadSpec";
		// Lead Spec Dialogs
		public const string SetLeadSpecPromptText = "How many LeadSpec do you want?\nMin = 0, Max = 1.0";
		public const string SetLeadSpecTitleText = "Set {0} LeadSpec(s) for {1}";
		// Common Dialog Errors/Info Default Text
		public const string InvalidInputErrorText = "Invalid input!";
		public const string SelectEmployeeErrorText = "Select one or more employees.";
		public const string SelectRoleErrorText = "Select one or more roles.";
		public const string SelectSpecializationErrorText = "Select one or more specializations.";
		public const string InvalidSkillAmountErrorText = "Invalid input!\nAllowed inputs are: -3, -2, -1, 1, 2, 3";
		public const string InvalidBaseSkillAmountErrorText = "Invalid input. Please enter a number between 0.0 and 1.0.";
		public const string SelectLeadSpecErrorText = "Select one or more LeadSpec.";
		public const string InvalidLeadSpecAmountErrorText = "Invalid input. Please enter a number between 0.0 and 1.0.";
		public const string InvalidInputMinValueErrorText = "Invalid input!\nmin value is {0}";
		public const string InvalidInputMaxValueErrorText = "Invalid input!\nmax value is {0}";
		// Popup Message Default Text
		public const string AllLoansClearedMessageText = "Trainer: All loans are cleared!";
		public const string AllPlotsUnlockedMessageText = "Trainer: All plots has been unlocked!";
		public const string AllFurnitureUnlockedMessageText = "Trainer: All furniture has been unlocked!";
		public const string DeadlinesExtendedMessageText = "Trainer: Deadlines extended by 1 year for active contracts!";
		public const string MoneyAddedMessageText = "Trainer: Money has been added in category Deals!";
		public const string MaxReputationAppliedMessageText = "Trainer: Max reputation is applied to all categories";
		public const string MaxMarketRecognitionAppliedMessageText = "Trainer: Max market recognition is applied to all software types and categories.";
		public const string AllRewardsUnlockedMessageText = "Trainer: All rewards are unlocked and claimed.";
		public const string DesignDocIterationTestExecutedMessageText = "Trainer: Test action executed (Design Doc Iteration).";
		public const string AddQualityTestExecutedMessageText = "Trainer: Test action executed (Add Quality) for '{0}'.";
		public const string SellStockMessageText = "Stock of products with no active users were sold at half the price.";
		public const string RemoveSoftDisabledMessageText = "RemoveSoft function is currently disabled (marked as broken in original code).";
		public const string BugsFixedMessageText = "Trainer: All bugs fixed for '{0}'!";
		public const string FollowersMaxedMessageText = "Trainer: Followers maxed for '{0}'!";
		public const string PriceSetMessageText = "Trainer: Price for '{0}' set to {1:C}!";
		public const string StockSetMessageText = "Trainer: Stock for '{0}' set to {1}!";
		public const string ActiveUsersSetMessageText = "Trainer: Active users for '{0}' set to {1}!";
		public const string AIBankruptMessageText = "Trainer: All AI companies forced into bankruptcy!";
		public const string CompanyNotFoundMessageText = "Trainer: Company '{0}' not found!";
		public const string CannotTakeoverOwnCompanyMessageText = "Trainer: Cannot takeover your own company!";
		public const string CannotBuyoutCompanyMessageText = "Trainer: Company '{0}' can't be bought out (already owned, too expensive, or other reason).";
		public const string CompanyTakenOverMessageText = "Trainer: Company '{0}' has been taken over!";
		public const string CannotSubsidiaryOwnCompanyMessageText = "Trainer: Cannot make your own company a subsidiary!";
		public const string AlreadySubsidiaryMessageText = "Trainer: Company '{0}' is already your subsidiary!";
		public const string SubsidiaryMadeMessageText = "Trainer: Company '{0}' is now your subsidiary!";
		public const string CannotBankruptOwnCompanyMessageText = "Trainer: Cannot force bankrupt your own company!";
		public const string CompanyBankruptStatusMessageText = "Trainer: Company '{0}' is now {1}!";
		public const string EmployeeAgeResetMessageText = "Trainer: Employees age has been reset!";
		public const string AllEmployeesMaxSkilledMessageText = "Trainer: All employees are now max skilled!";
		public const string LeadersHRSetMessageText = "Trainer: All leaders are now HRed!";
		public const string EmployeeSkillsSetMessageText = "Trainer: Employee skills/specializations are set!";
		public const string EmployeeBaseSkillsSetMessageText = "Trainer: Employee base skills set!";
		public const string DiscordInviteMessageText = "Join us on our discord server\n{0}";
		public const string MonthDaysRestartWarningText = "You have changed days per month. Please restart the game.";
		public const string AlphaProductNotFoundMessageText = "Trainer: Alpha product '{0}' (not in beta) not found.";
		public const string BetaProductNotFoundMessageText = "Trainer: Alpha product '{0}' in beta not found.";
		public const string ActiveAlphaProductNotFoundMessageText = "Trainer: Active alpha product '{0}' not found.";
		public const string ProductNotFoundMessageText = "Trainer: Product '{0}' not found.";
		// Input Dialog Prompt/Title Default Text
		public const string MonthDaysPromptText = "How many days per month do you want?";
		public const string MonthDaysTitleText = "Days per month";
		public const string AddMoneyPromptText = "How much money do you want to add?";
		public const string AddMoneyTitleText = "Add Money";
		public const string AreYouSurePromptText = "Are you sure you want to {0}?";
		public const string ConfirmationTitleText = "Confirmation";
		public const string MaxRepConfirmActionText = "max out all business reputations";
		public const string AddQualityPromptText = "Type product name in alpha (not beta):";
		public const string AddQualityTitleText = "Add Quality (Test)";
		public const string FixBugsPromptText = "Type product name (in Beta):";
		public const string FixBugsTitleText = "Fix Bugs";
		public const string MaxFollowersPromptText = "Type product name (in Alpha):";
		public const string MaxFollowersTitleText = "Max Followers";
		public const string SetPriceNamePromptText = "Type product name:";
		public const string SetPriceNameTitleText = "Set Product Price - Name";
		public const string SetPriceAmountPromptText = "Type new price for '{0}':";
		public const string SetPriceAmountTitleText = "Set Product Price - Price";
		public const string SetStockNamePromptText = "Type product name:";
		public const string SetStockNameTitleText = "Set Product Stock - Name";
		public const string SetStockAmountPromptText = "Type new stock for '{0}':";
		public const string SetStockAmountTitleText = "Set Product Stock - Amount";
		public const string SetActiveUsersNamePromptText = "Type product name:";
		public const string SetActiveUsersNameTitleText = "Set Active Users - Name";
		public const string SetActiveUsersAmountPromptText = "Type new active user count for '{0}':";
		public const string SetActiveUsersAmountTitleText = "Set Active Users - Amount";
		public const string TakeoverCompanyNamePromptText = "Type company name:";
		public const string TakeoverCompanyTitleText = "Takeover Company";
		public const string SubsidiaryNamePromptText = "Type company name:";
		public const string SubsidiaryTitleText = "Make Subsidiary";
		public const string ForceBankruptPromptText = "Type company name:";
		public const string ForceBankruptTitleText = "Toggle Force Bankrupt";
		#endregion Default UI Text
	}
}
