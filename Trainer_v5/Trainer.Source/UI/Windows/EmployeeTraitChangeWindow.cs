using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Trainer_v5.UI.Helpers;
using Trainer_v5.UI.Extensions;

namespace Trainer_v5.UI.Windows
{
	public class EmployeeTraitChangeWindow : MonoBehaviour
	{
		public static EmployeeTraitChangeWindow Instance => _instance.Value;
		private static readonly Lazy<EmployeeTraitChangeWindow> _instance = new Lazy<EmployeeTraitChangeWindow>(() => new EmployeeTraitChangeWindow());

		public static GUIWindow Window { get; private set; }
		private static Employee _currentEmployee; // Keep track of the employee being edited

		public static void Show(Actor actor)
		{
			if (Window == null)
			{
				Window = Create(actor?.employee); // Pass employee to Create
			}
			else
			{
				// If window exists, update it for the new employee
				Update(actor?.employee);
			}
			Window?.Toggle();
		}

		private static GUIWindow Create(Employee employee)
		{
			return CreateWindow(employee);
		}

		private static void Update(Employee employee)
		{
			if (Window == null) return;

			_currentEmployee = employee;

			string title = employee != null
				? string.Format(Constants.EditTraitsTitleKey.LocDef(Constants.EditTraitsTitleText), employee.Name)
				: Constants.EditTraitsDefaultTitleKey.LocDef(Constants.EditTraitsDefaultTitleText);
			Window.InitialTitle = Window.TitleText.text = Window.NonLocTitle = title;

			// Clear existing content (optional, could update existing toggles)
			foreach (Transform child in Window.MainPanel.transform)
			{
				GameObject.Destroy(child.gameObject);
			}

			// Rebuild content
			BuildWindowContent(Window, employee);
		}

		private static GUIWindow CreateWindow(Employee employee)
		{
			_currentEmployee = employee;
			string title = employee != null
				? string.Format(Constants.EditTraitsTitleKey.LocDef(Constants.EditTraitsTitleText), employee.Name)
				: Constants.EditTraitsDefaultTitleKey.LocDef(Constants.EditTraitsDefaultTitleText);

			Window = WindowManager.SpawnWindow();
			Window.InitialTitle = Window.TitleText.text = Window.NonLocTitle = title;
			Window.name = Constants.EmployeeTraitChangeWindowName;
			Window.MainPanel.name = Constants.EmployeeTraitChangePanelName;
			Window.MinSize.x = 490;

			BuildWindowContent(Window, employee);

			return Window;
		}

		private static void BuildWindowContent(GUIWindow window, Employee employee)
		{
			var goodTraitItems = GoodTraitsColumn(employee);
			var neutralTraitItems = NeutralTraitsColumn(employee);
			var badTraitItems = BadTraitsColumn(employee);

			goodTraitItems.AddToWindow(window, Constants.FIRST_COLUMN);
			neutralTraitItems.AddToWindow(window, Constants.SECOND_COLUMN);
			badTraitItems.AddToWindow(window, Constants.THIRD_COLUMN);

			int maxRows = Math.Max(goodTraitItems.Count, Math.Max(neutralTraitItems.Count, badTraitItems.Count));
			window.SetWindowSize(maxRows, 490);
		}

		private static List<GameObject> GoodTraitsColumn(Employee employee)
		{
			var list = new List<GameObject>();
			list.Add(UIHelper.CreateLabel(Constants.GoodTraitsKey.LocDef(Constants.GoodTraitsText), name: Constants.GoodTraitsLabelName));

			foreach (var trait in Trainer_v5.Helpers.GoodTraits)
			{
				bool hasTrait = employee != null && employee.HasTrait(trait);
				list.Add(UIHelper.CreateToggle(trait.ToString().LocDef(trait.ToString()), hasTrait, x => ToggleTrait(_currentEmployee, trait, x), trait.ToString()));
			}
			return list;
		}

		private static List<GameObject> NeutralTraitsColumn(Employee employee)
		{
			var list = new List<GameObject>();
			list.Add(UIHelper.CreateLabel(Constants.NeutralTraitsKey.LocDef(Constants.NeutralTraitsText), name: Constants.NeutralTraitsLabelName));

			foreach (var trait in Trainer_v5.Helpers.NeutralTraits)
			{
				bool hasTrait = employee != null && employee.HasTrait(trait);
				list.Add(UIHelper.CreateToggle(trait.ToString().LocDef(trait.ToString()), hasTrait, x => ToggleTrait(_currentEmployee, trait, x), trait.ToString()));
			}
			return list;
		}

		private static List<GameObject> BadTraitsColumn(Employee employee)
		{
			var list = new List<GameObject>();
			list.Add(UIHelper.CreateLabel(Constants.BadTraitsKey.LocDef(Constants.BadTraitsText), name: Constants.BadTraitsLabelName));

			foreach (var trait in Trainer_v5.Helpers.BadTraits)
			{
				bool hasTrait = employee != null && employee.HasTrait(trait);
				list.Add(UIHelper.CreateToggle(trait.ToString().LocDef(trait.ToString()), hasTrait, x => ToggleTrait(_currentEmployee, trait, x), trait.ToString()));
			}

			list.Add(UIHelper.EmptyBox());
			list.Add(UIHelper.CreateButton(Constants.RefreshKey.LocDef(Constants.RefreshText), () => Update(_currentEmployee), Constants.RefreshTraitsButtonName));
			return list;
		}

		private static void ToggleTrait(Employee employee, Employee.Trait trait, bool on)
		{
			if (employee == null)
				return;

			var hasTrait = (employee.Traits & trait) > 0;

			if (on && !hasTrait)
			{
				employee.Traits |= trait;
			}
			else if (!on && hasTrait)
			{
				employee.Traits &= ~trait;
			}
		}
	}
} 