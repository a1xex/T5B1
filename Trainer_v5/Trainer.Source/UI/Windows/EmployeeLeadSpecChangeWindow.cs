using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Trainer_v5.UI.Helpers;
using Trainer_v5.UI.Extensions;

namespace Trainer_v5.UI.Windows
{
	public class EmployeeLeadSpecChangeWindow : MonoBehaviour
	{
		public static EmployeeLeadSpecChangeWindow Instance => _instance.Value;
		private static readonly Lazy<EmployeeLeadSpecChangeWindow> _instance = new Lazy<EmployeeLeadSpecChangeWindow>(() => new EmployeeLeadSpecChangeWindow());

		private GUIWindow _window;
		private Dictionary<string, SoftwareType> _softwareTypes;
		private Dictionary<string, Toggle> _specToggles;
		private Actor _actor;
		private Employee _currentEmployee;

		public void Show()
		{
			if (_window == null)
				CreateWindow();
			else
				_window.Toggle();
			Refresh();
		}

		public void Refresh()
		{
			var window = _window;
			if (window == null || !window.Shown)
			{
				_actor = null;
				return;
			}

			var selectedActors = SelectorController.Instance.Selected.OfType<Actor>();
			_actor = selectedActors.Any() ? selectedActors.First() : null;
			var employee = _actor?.employee;

			_currentEmployee = employee;

			string title = employee != null
				? string.Format(Constants.EditLeadSpecTitleKey.LocDef(Constants.EditLeadSpecTitleText), employee.Name)
				: Constants.EditLeadSpecDefaultTitleKey.LocDef(Constants.EditLeadSpecDefaultTitleText);
			window.InitialTitle = window.TitleText.text = window.NonLocTitle = title;
		}

		private void CreateWindow()
		{
			var self = this;
			var window = WindowManager.SpawnWindow();
			window.InitialTitle = window.TitleText.text = window.NonLocTitle = Constants.EditLeadSpecDefaultTitleKey.LocDef(Constants.EditLeadSpecDefaultTitleText);
			window.name = Constants.EmployeeLeadSpecChangeWindowName;
			window.MainPanel.name = Constants.EmployeeLeadSpecChangePanelName;

			var softwareTypes = MarketSimulation.Active.SoftwareTypes;
			var toggles = new Dictionary<string, Toggle>();
			var columnItems = new List<GameObject>();

			columnItems.Add(UIHelper.CreateLabel(Constants.LeadSpecKey.LocDef(Constants.LeadSpecText), name: Constants.LeadSpecLabelName));

			foreach (var pair in softwareTypes)
			{
				var toggleGO = UIHelper.CreateToggle(pair.Key.LocDef(pair.Key), false, isOn => self.OnToggle(pair.Key, isOn), pair.Key);
				toggles[pair.Key] = toggleGO.GetComponent<Toggle>();
				columnItems.Add(toggleGO);
			}

			columnItems.Add(UIHelper.CreateButton(Constants.AllKey.LocDef(Constants.AllText), () => self.ToggleAll(true), Constants.AllLeadSpecButtonName));
			columnItems.Add(UIHelper.CreateButton(Constants.NoneKey.LocDef(Constants.NoneText), () => self.ToggleAll(false), Constants.NoneLeadSpecButtonName));
			columnItems.Add(UIHelper.CreateButton(Constants.SetLeadSpecKey.LocDef(Constants.SetLeadSpecText), () => self.SetLeadSpec(), Constants.SetLeadSpecButtonName));

			columnItems.AddToWindow(window, 0);

			window.SetWindowSize(columnItems.Count, 260 - 1);

			_window = window;
			_softwareTypes = softwareTypes;
			_specToggles = toggles;
		}

		private void OnToggle(string key, bool isOn)
		{
			_specToggles[key].isOn = isOn;
		}

		private void ToggleAll(bool isOn)
		{
			foreach (var toggle in _specToggles.Values)
				toggle.isOn = isOn;
		}

		private void SetLeadSpec()
		{
			if (_actor == null)
				return;
			var employee = _actor.employee;

			var selectTypes = _specToggles
				.Where(p => p.Value.isOn)
				.Select(p => _softwareTypes[p.Key])
				.ToArray();

			if (selectTypes.Length == 0)
			{
				UIHelper.ShowError(Constants.SelectLeadSpecErrorKey.LocDef(Constants.SelectLeadSpecErrorText));
				return;
			}

			string prompt = Constants.SetLeadSpecPromptKey.LocDef(Constants.SetLeadSpecPromptText);
			string title = string.Format(Constants.SetLeadSpecTitleKey.LocDef(Constants.SetLeadSpecTitleText), selectTypes.Length, employee.Name);
			string defaultVal = "1.0";

			Action<string> action = (input) =>
			{
				float val;
				if (!float.TryParse(input, out val) || val < 0f || val > 1f)
				{
					UIHelper.ShowError(Constants.InvalidLeadSpecAmountErrorKey.LocDef(Constants.InvalidLeadSpecAmountErrorText));
					return;
				}

				foreach (var type in selectTypes)
				{
#if DEBUG || SWINCBETA1_7 || SWINCBETA1_8 || SWINCBETA1_9 || SWINCBETA1_10
					employee.LeadSpecializationFix[type.ToString()] = val;
#else
					employee.LeadSpecialization[type] = val;
#endif
				}
			};

			WindowManager.SpawnInputDialog(prompt, title, defaultVal, action);
		}
	}
}
 