using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Trainer_v5; // Add this for Constants

namespace Trainer_v5.UI.Extensions
{
    // Contains UI-specific extension methods.
	public static class UIExtensions
	{
		public static void AddToElement(this GameObject gameObject, string elementPath, Rect location)
		{
			WindowManager.AddElementToElement(gameObject, WindowManager.FindElementPath(elementPath).gameObject, location, new Rect(0, 0, 0, 0));
		}

		public static void AddToWindow(this List<GameObject> gameObjects, GUIWindow window, int column, bool isComboBox = false)
		{
            int elementHeight = Constants.ELEMENT_HEIGHT;
            int elementWidth = Constants.ELEMENT_WIDTH; // Use ELEMENT_WIDTH or a specific width?

			for (int i = 0; i < gameObjects.Count; i++)
			{
				var gameObject = gameObjects[i];

				WindowManager.AddElementToWindow(gameObject, window,
						new Rect(column, (i - (isComboBox ? 1 : 0)) * elementHeight + (isComboBox && i % 2 == 0 ? 16 : 0), elementWidth, elementHeight),
						new Rect(0, 0, 0, 0));
			}
		}

		public static void SetWindowSize(this GUIWindow window, int rows, int xWindowSize)
		{
            int elementHeight = Constants.ELEMENT_HEIGHT;

			window.MinSize.x = xWindowSize;
			window.MinSize.y = (rows + 1) * elementHeight;
		}
	}
} 