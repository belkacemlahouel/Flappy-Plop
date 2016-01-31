using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;
using System.Reflection;

public class LogAllAttributes : MonoBehaviour
{
    private static readonly string DIR = "./PlopLogs/";
    private static readonly string FILE = "plop.log";
    private FindAllGameObjects finder;

    public void Start()
    {
        finder = GameObject.FindGameObjectWithTag("Plop").GetComponent<FindAllGameObjects>();

		if (!Directory.Exists("./PlopLogs"))
			Directory.CreateDirectory("./PlopLogs/");
    }

    public void Update()
    {
        if (!Input.GetKeyDown(KeyCode.P))
            return;

        StringBuilder text = new StringBuilder();
        foreach (GameObject go in finder.getCurrentMonoBehaviorObjects())
        {
            text.AppendLine(go.ToString());
            Component[] components = go.GetComponents(typeof(MonoBehaviour));
            foreach (Component c in components)
            {
                PropertyInfo[] properties = GetType().GetProperties();
                foreach (PropertyInfo p in properties)
                {
                    text.Append("\t" + p.ToString());
                    text.AppendLine(": value (TODO)");
                }
            }
            
        }
        File.WriteAllText(DIR + myNowToString() + "_" + FILE, text.ToString());
    }

    private string myNowToString()
    {
        return DateTime.Now.Year.ToString() + "-" +
                DateTime.Now.Month.ToString() + "-" +
                DateTime.Now.Day.ToString() + "_" +
                DateTime.Now.Hour.ToString() + "-" +
                DateTime.Now.Minute.ToString() + "-" +
                DateTime.Now.Second.ToString();
    }
}
