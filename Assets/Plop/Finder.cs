using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Finder : MonoBehaviour {

	public static float TICK = 1f;
	public static int DEPTH_OF_SEARCH = 1;

	public float deltaTime = 0f;

	private HashSet<GameObject> allObjects;
	private LinkedList<GameObject> selection, nonSelection;
	private ReflectionStructure reflector;

	/// <summary>
	/// Starts this instance.
	/// </summary>
	public void Start() {
		allObjects = new HashSet<GameObject>();
		selection = new LinkedList<GameObject>();
		nonSelection = new LinkedList<GameObject>();
		reflector = new ReflectionStructure(selection);

		GameObject[] allObjectsArray = UnityEngine.Object.FindObjectsOfType<GameObject>();
		foreach (GameObject go in allObjectsArray) {
			if (go != gameObject && go.activeInHierarchy && go.GetComponent<MonoBehaviour>() != null) {
				allObjects.Add(go);
				nonSelection.AddFirst(go);
			}
		}
	}

	/// <summary>
	/// Updates this instance.
	/// </summary>
	//TODO Bug: Multi-thread and Logging (ERROR: ReflectionObject...)
	public void Update() {
		deltaTime += Time.deltaTime;
		if (deltaTime < TICK) return;
		deltaTime = 0f;

		/*//updating objects (eg. if new objects found/old objects removed)
		HashSet<GameObject> tmpAllObjects = new HashSet<GameObject>();
		LinkedList<GameObject> tmpSelection = new LinkedList<GameObject>();
		LinkedList<GameObject> tmpNonSelection = new LinkedList<GameObject>();

		GameObject[] tmpAllObjectsArray = UnityEngine.Object.FindObjectsOfType<GameObject>();
		foreach (GameObject go in tmpAllObjectsArray) {
			if (go != gameObject && go.activeInHierarchy && go.GetComponent<MonoBehaviour>() != null) {
				if (!tmpAllObjects.Contains(go)) {
					tmpAllObjects.Add(go);
					tmpNonSelection.AddFirst(go);
				}
			}
		}

		allObjects = tmpAllObjects;
		selection = tmpSelection;
		nonSelection = tmpNonSelection;*/

		foreach (GameObject go in selection) {
			reflector.addGameObject(go, go.ToString());
		}

		(new Logger(selection, reflector)).start();
	}

	/// <summary>
	/// Gets all objects in the scene.
	/// </summary>
	/// <returns>all objects from scene.</returns>
	public HashSet<GameObject> getAllObjects() {
		return allObjects;
	}

	/// <summary>
	/// Gets the non selection.
	/// </summary>
	/// <returns>The non selection.</returns>
	public LinkedList<GameObject> getNonSelection() {
		return nonSelection;
	}

	/// <summary>
	/// Gets the selection.
	/// </summary>
	/// <returns>The selection.</returns>
	public LinkedList<GameObject> getSelection() {
		return selection;
	}

	/// <summary>
	/// Updates the selection.
	/// </summary>
	/// <param name="_go">Game object to (un) selected.</param>
	/// <param name="_selected">parameter describing whether we select this object or not</param>
	private void updateSelection(GameObject _go, bool _selected) {
		if (_selected) {
			if (nonSelection.Contains(_go))
				nonSelection.Remove(_go);

			if (!selection.Contains(_go))
				selection.AddFirst(_go);
		} else {
			if (selection.Contains(_go))
				selection.Remove(_go);

			if (!nonSelection.Contains(_go))
				nonSelection.AddFirst(_go);
		}
	}

	public void updateSelection(string _go_name, bool _selected) {
		LinkedList<GameObject> chosen = new LinkedList<GameObject>();

		if (_selected) {
			foreach (GameObject go in nonSelection) {
				if (go.name.Equals(_go_name)) {
					chosen.AddFirst(go);
				}
			}
		} else {
			foreach (GameObject go in selection) {
				if (go.name.Equals(_go_name)) {
					chosen.AddFirst(go);
				}
			}
		}

		foreach (GameObject go in chosen) {
			updateSelection(go, _selected);
		}
	}
}
