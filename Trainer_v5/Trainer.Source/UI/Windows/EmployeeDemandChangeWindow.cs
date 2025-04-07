using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Trainer_v5.UI.Helpers;
using Trainer_v5.UI.Extensions;

namespace Trainer_v5.UI.Windows
{
	public class EmployeeDemandChangeWindow : MonoBehaviour
	{
		public static EmployeeDemandChangeWindow Instance => _instance.Value;
		private static readonly Lazy<EmployeeDemandChangeWindow> _instance = new Lazy<EmployeeDemandChangeWindow>(() => new EmployeeDemandChangeWindow());

		public static GUIWindow Window { get; private set; }
		private static Employee _currentEmployee;

		public static void Show(Actor actor)
		{
			if (Window == null)
			{
				Window = Create(actor?.employee);
			}
			else
			{
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
				? string.Format(Constants.EditDemandsTitleKey.LocDef(Constants.EditDemandsTitleText), employee.Name)
				: Constants.EditDemandsDefaultTitleKey.LocDef(Constants.EditDemandsDefaultTitleText);
			Window.InitialTitle = Window.TitleText.text = Window.NonLocTitle = title;

			foreach (Transform child in Window.MainPanel.transform)
			{
				GameObject.Destroy(child.gameObject);
			}
			BuildWindowContent(Window, employee);
		}

		private static GUIWindow CreateWindow(Employee employee)
		{
			_currentEmployee = employee;
			string title = employee != null
				? string.Format(Constants.EditDemandsTitleKey.LocDef(Constants.EditDemandsTitleText), employee.Name)
				: Constants.EditDemandsDefaultTitleKey.LocDef(Constants.EditDemandsDefaultTitleText);

			Window = WindowManager.SpawnWindow();
			Window.InitialTitle = Window.TitleText.text = Window.NonLocTitle = title;
			Window.name = Constants.EmployeeDemandChangeWindowName;
			Window.MainPanel.name = Constants.EmployeeDemandChangePanelName;
			Window.MinSize.x = 160;

			BuildWindowContent(Window, employee);

			return Window;
		}

		private static void BuildWindowContent(GUIWindow window, Employee employee)
		{
			var columnItems = CreateDemandItems(employee);
			columnItems.AddToWindow(window, Constants.FIRST_COLUMN);
			window.SetWindowSize(columnItems.Count, 160);
		}

		private static List<GameObject> CreateDemandItems(Employee employee)
		{
			var list = new List<GameObject>();
			list.Add(UIHelper.CreateLabel(Constants.DemandsKey.LocDef(Constants.DemandsText), name: Constants.DemandsLabelName));

			foreach (var demand in Trainer_v5.Helpers.Demands)
			{
				bool hasDemand = employee != null && (employee.DemandResults & demand) > 0;
				list.Add(UIHelper.CreateToggle(demand.ToString().LocDef(demand.ToString()), hasDemand, x => ToggleDemand(_currentEmployee, demand, x), demand.ToString()));
			}

			list.Add(UIHelper.EmptyBox());
			list.Add(UIHelper.CreateButton(Constants.RefreshKey.LocDef(Constants.RefreshText), () => Update(_currentEmployee), Constants.RefreshDemandsButtonName));
			return list;
		}

		private static void ToggleDemand(Employee employee, LeadDesignDemands.Demand demand, bool on)
		{
			if (employee == null)
				return;

			var has = (employee.DemandResults & demand) > 0;

			if (on && !has)
			{
				employee.DemandResults |= demand;
			}
			else if (!on && has)
			{
				employee.DemandResults &= ~demand;
			}
		}
	}
} 