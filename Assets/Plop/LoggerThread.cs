using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.IO;
using System.Text;
using System;

public class LoggerThread {

	private static int count = 0;
	private int id = 0;

	private LinkedList<GameObject> selection;
	private ReflectionStructure reflection;
	private Thread myThread;

	private static string DIR = "./PlopLogs/";
	private static string FILE = "plop.log";

	/// <summary>
	/// Initializes a new instance of the <see cref="Logger"/> class.
	/// </summary>
	/// <param name="_selection">Selection.</param>
	/// <param name="_reflection">Reflection.</param>
	public LoggerThread(LinkedList<GameObject> _selection, ReflectionStructure _reflection) {
		selection = _selection;
		reflection = _reflection;
		myThread = new Thread(this.doJob);
		id = count+1;
		count++;
	}

	/// <summary>
	/// Starts the logger thread and process.
	/// </summary>
	public void start() {
		myThread.Start();
		//myThread.Join(); //TODO Should it join?
	}

	/// <summary>
	/// Does the job:
	/// 	- builds the text from the reflection structure and selection
	/// 	- writes the text in a given file.
	/// This job is done in myThread
	/// </summary>
	private void doJob() {
		writeText(buildText(), buildFullFileName());
	}

	/// <summary>
	/// Builds the text from selection and reflection structure.
	/// </summary>
	/// <returns>The text.</returns>
	private string buildText() {
		StringBuilder builder = new StringBuilder();

		foreach (GameObject go in selection) {
			ReflectionObject ro = reflection.getReflectionObject(go);
			builder.Append(ro.ToString());
		}

		return builder.ToString();
	}

	/// <summary>
	/// Builds the name of the file: "DIR/id_date_time_plop.log"
	/// </summary>
	/// <returns>The file name.</returns>
	private string buildFullFileName() {
		return DIR + id + "_" + myNowToString() + "_" + FILE;
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

	/// <summary>
	/// Writes the text given in parameter in the file speficied as a parameter.
	/// </summary>
	/// <param name="text">Text.</param>
	/// <param name="fileName">File name.</param>
	private void writeText(string text, string fileName) {
		File.WriteAllText(fileName, text);
	}
}
