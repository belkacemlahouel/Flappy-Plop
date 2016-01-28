using UnityEngine;
using System.Collections;

public class TestFindAllGameObjects : MonoBehaviour
{
    private FindAllGameObjects finder;

	public void Start()
    {
        finder = GameObject.FindGameObjectWithTag("Plop").GetComponent<FindAllGameObjects>();
    }

    public void Update()
    {
        Debug.Log(finder.getNumberOfCurrentMonoBehaviorObjects());
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            foreach (GameObject go in finder.getCurrentMonoBehaviorObjects())
            {
                Debug.Log(go);
            }
        }
    }
}
