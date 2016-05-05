using UnityEngine;

public class MenuCase {

	public bool isSelected = false;
	public GameObject gameObject = null;

	public MenuCase(GameObject _gameObject) {
		gameObject = _gameObject;
	}
}
