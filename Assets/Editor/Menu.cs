using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(Finder))]
public class Menu : Editor {

	private bool isEnabled = false;
	private float deltaTime = 0f;
	private Finder finder = null;
	private Vector2 scrollPos = Vector2.zero;
	private Dictionary<string, bool> cases = null;

	/// <summary>
	/// When editor (in inspector) is enabled.
	/// Happens whenever Unity wants to.
	/// </summary>
	public void OnEnable() {
		
	}

	/// <summary>
	/// When Unity needs to Update GUI.
	/// </summary>
	public override void OnInspectorGUI() {
		isEnabled = EditorGUILayout.Toggle("Activate", isEnabled);
		if (!isEnabled) return;

		if (finder == null) {
			finder = (Finder) target;
			cases = new Dictionary<string, bool>();
		}

		if (finder != null) {
			foreach (GameObject go in finder.getAllObjects()) {
				if (!cases.ContainsKey(go.name)) {
					cases.Add(go.name, false);
				}
			}
		}

		EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
		Finder.TICK = EditorGUILayout.Slider("Tick (sec)", Finder.TICK, 0f, 5f);
		Finder.DEPTH_OF_SEARCH = EditorGUILayout.IntSlider("Depth of Search", Finder.DEPTH_OF_SEARCH, 0, 3);

		EditorGUILayout.LabelField("Selection", EditorStyles.boldLabel);
		scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
		//keys to iterate over: cannot modify dictionary while iterating over it
		string[] keys = new string[cases.Keys.Count];
		cases.Keys.CopyTo(keys, 0);
		foreach (string go_name in keys) {
			cases[go_name] = EditorGUILayout.Toggle(go_name, cases[go_name]);
			//finder's selection update
			finder.updateSelection(go_name, cases[go_name]);
		}
		EditorGUILayout.EndScrollView();
	}
}
