using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReflectionStructure {

	private Dictionary<GameObject, ReflectionObject> myGameObjects;

	/// <summary>
	/// Initializes a new instance of the <see cref="ReflectionStructure"/> class.
	/// </summary>
	public ReflectionStructure(LinkedList<GameObject> _selection) {
		myGameObjects = new Dictionary<GameObject, ReflectionObject>();
		foreach (GameObject gameObject in _selection) {
			addGameObject(gameObject, gameObject.ToString());
		}
	}

	/// <summary>
	/// Adds the game object to the reflection structure.
	/// </summary>
	/// <param name="_gameObject">Game object.</param>
	public void addGameObject(GameObject _gameObject, string _name) {
		ReflectionObject reflectionObject = new ReflectionObject(_gameObject, _name);
		myGameObjects.Add(_gameObject, reflectionObject);
		reflectionObject.updateProperties(Finder.DEPTH_OF_SEARCH);
	}

	/// <summary>
	/// Gets the reflection object from a given GameObject.
	/// </summary>
	/// <returns>The reflection object.</returns>
	public ReflectionObject getReflectionObject(GameObject _gameObject) {
		ReflectionObject reflectionObject = null;
		myGameObjects.TryGetValue(_gameObject, out reflectionObject);
		return reflectionObject;
	}
}
