using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using System.Reflection;

public class LogAllAttributes : MonoBehaviour
{
    private static readonly string DIR = "./PlopLogs/";
    private static readonly string FILE = "plop.log";
    private FindAllGameObjects finder;

	private static int MAX_LEVEL = 2;
	private static float TICK = 3f;

	private float time_count = 0f;

	private LinkedList<GameObject> selection = null;

    public void Start()
    {
        finder = GameObject.FindGameObjectWithTag("Plop").GetComponent<FindAllGameObjects>();

		if (!Directory.Exists(DIR))
			Directory.CreateDirectory(DIR);
    }

    public void Update()
	{
		//if (!Input.GetKeyDown(KeyCode.P))
		//	return;

		if (time_count < TICK) {
			time_count += Time.deltaTime;
			return;
		}

		try
		{
			logSelection();
		}
		catch (Exception e)
		{
			Debug.LogError(e.StackTrace);
		}

		time_count = 0f;
    }

	public void setTick(float tick) {
		TICK = tick;
	}

	public void setMaxLevel(int max_lvl) {
		MAX_LEVEL = max_lvl;
	}

	public void setSelection(LinkedList<GameObject> _selection) {
		selection = _selection;
	}

	public void logSelection() {
		StringBuilder text = new StringBuilder();

		foreach (GameObject go in selection) //finder.getCurrentMonoBehaviorObjects())
		{
			text.AppendLine(go.ToString());
			Component[] components = go.GetComponents(typeof(MonoBehaviour));

			try
			{
				foreach (Component c in components)
				{
					//reflexion to get all members
					text.Append(reflexion1(c, 0));
				}
			}
			catch (Exception e)
			{
				Debug.LogError(e.StackTrace);
			}
		}

		File.WriteAllText(DIR + myNowToString() + "_" + FILE, text.ToString());
	}

	/*[Obsolete("Use reflexion1(Component, int) instead")]
	private String reflexion(Component c) {
		StringBuilder text = new StringBuilder();
		PropertyInfo[] properties = c.GetType().GetProperties();

		foreach (PropertyInfo p in properties)
		{
			text.AppendLine("\tproperty: " + p.ToString());
			//"\tattributes: " + p.GetCustomAttributes(false));
			foreach (Attribute attr in p.GetCustomAttributes(false))
			{
				text.AppendLine("\t\t TODO!!!!");
			}
		}

		return text.ToString();
	}*/

	/// <summary>
	/// Gets all attributes for object a given object, recursively.
	/// Stops at a given deep level.
	/// </summary>
	/// <param name="c">object on which we perform recursive attributes search</param>
	/// <param name="level">current level of search</param>
	private String reflexion1(object c, int level) {
		StringBuilder text = new StringBuilder();
		PropertyInfo[] properties = c.GetType().GetProperties();
		if (level > MAX_LEVEL || properties == null || properties.Length == 0) return "";
		foreach (PropertyInfo p in properties)
		{
			String propName = p.Name;
			for (int i = 0; i < level+1; ++i)
				text.Append("\t");
			
			text.Append(propName + ": ");
			object propValue = null;
			try
			{
				propValue = c.GetType().GetProperty(propName).GetValue(c, null);
				text.Append(propValue.ToString());
			}
			catch (Exception e)
			{
				Debug.LogWarning(e.StackTrace);
			}
			finally
			{
				text.AppendLine();
				if (propValue != null) {
					text.Append(reflexion1(propValue, level+1));
				}
			}
		}

		return text.ToString();
	}

	/// <summary>
	/// Converts current time to formatted string.
	/// </summary>
	/// <returns>Current time string formatted</returns>
    private string myNowToString()
    {
        return  DateTime.Now.Year.ToString() 	+ "-" +
                DateTime.Now.Month.ToString() 	+ "-" +
                DateTime.Now.Day.ToString() 	+ "_" +
                DateTime.Now.Hour.ToString() 	+ "-" +
                DateTime.Now.Minute.ToString() 	+ "-" +
                DateTime.Now.Second.ToString();
    }
}
