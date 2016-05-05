using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

//[InitializeOnLoad]
public class LogSelectionEditor : EditorWindow {

	LogAllAttributes logger = null;
	Vector2 scrollPos = Vector2.zero;
	LinkedList<MenuCase> cases = null;

	float tick = 0.1f;
	int maxLevelOfSearch = 2;

	static LogSelectionEditor()
	{
		ShowWindow();
	}

	// Add menu item named "My Window" to the Window menu
	[MenuItem("Window/Log Selection")]
	public static void ShowWindow()
	{
		//Show existing window instance. If one doesn't exist, make one.
		EditorWindow.GetWindow(typeof(LogSelectionEditor));
	}

	void OnGUI()
	{
		if (cases == null) {
			FindAllGameObjects finder = GameObject.FindGameObjectWithTag("Plop").GetComponent<FindAllGameObjects>();
			cases = new LinkedList<MenuCase>();
			foreach (GameObject go in finder.getCurrentMonoBehaviorObjects()) {
				MenuCase menuCase = new MenuCase(go);
				cases.AddFirst(menuCase);
			}

			if (logger == null)
				logger = GameObject.FindGameObjectWithTag("Plop").GetComponent<LogAllAttributes>();
		}

		GUILayout.Label("Settings", EditorStyles.boldLabel);
		tick = EditorGUILayout.Slider("TickSlider", tick, 0f, 5f);
		maxLevelOfSearch = EditorGUILayout.IntSlider("MaxLevelOfSearchSlider", maxLevelOfSearch, 0, 3);
		logger.setMaxLevel(maxLevelOfSearch);
		logger.setTick(tick);

		GUILayout.Label("Selection", EditorStyles.boldLabel);
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		foreach (MenuCase menuCase in cases) {
			menuCase.isSelected = EditorGUILayout.Toggle(menuCase.gameObject.ToString(), menuCase.isSelected);
		}
		EditorGUILayout.EndScrollView();

		//LogAllAttributes update
		LinkedList<GameObject> selection = new LinkedList<GameObject>();
		foreach (MenuCase menuCase in cases) {
			if (menuCase.isSelected)
				selection.AddFirst(menuCase.gameObject);
		}
		logger.setSelection(selection);
	}
}
