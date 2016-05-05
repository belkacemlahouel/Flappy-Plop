using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FindAllGameObjects : MonoBehaviour
{

    private LinkedList<GameObject> currentMonoBehaviorObjects;
    private int number;

	public void Start()
    {
        currentMonoBehaviorObjects = new LinkedList<GameObject>();
        number = 0;
    }

    public void Update()
    {
        currentMonoBehaviorObjects.Clear();
        number = 0;

        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>();
        foreach (GameObject go in allObjects)
        {
            if (go != gameObject && go.activeInHierarchy && go.GetComponent<MonoBehaviour>() != null)
            {
				currentMonoBehaviorObjects.AddFirst(go);
                ++number;
            }
        }
    }

    public LinkedList<GameObject> getCurrentMonoBehaviorObjects()
    {
        //Computation is done inside Update to make sure computation is not too heavy
        return currentMonoBehaviorObjects;
    }

    public int getNumberOfCurrentMonoBehaviorObjects()
    {
        return number;
    }
}
