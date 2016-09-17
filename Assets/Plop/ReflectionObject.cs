using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System;

public class ReflectionObject {

	private object myValue;
	private string myName;
	private LinkedList<ReflectionObject> myProperties;
	private int depthLevel;

	/// <summary>
	/// Initializes a new instance of the <see cref="ReflectionObject"/> class.
	/// </summary>
	/// <param name="_value">reference to the object</param>
	public ReflectionObject(object _value, string _name) {
		myValue = _value;
		myName = _name;
		myProperties = null;
		depthLevel = 0;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="ReflectionObject"/> class.
	/// </summary>
	/// <param name="_value">Value.</param>
	/// <param name="_depthLevel">Depth level of this object.</param>
	public ReflectionObject(object _value, string _name, int _depthLevel) {
		myValue = _value;
		myName = _name;
		myProperties = null;
		depthLevel = _depthLevel;
	}

	/// <summary>
	/// Gets the value of the current object.
	/// </summary>
	/// <returns>value of the current object</returns>
	public object getValue() {
		return myValue;
	}

	/// <summary>
	/// Gets the name.
	/// </summary>
	/// <returns>The name.</returns>
	public string getName() {
		return myName;
	}

	/// <summary>
	/// Returns the list of properties for this object.
	/// </summary>
	/// <returns>list of properties for this object</returns>
	public LinkedList<ReflectionObject> getProperties() {
		return myProperties;
	}

	/// <summary>
	/// Updates the properties recursively.
	/// </summary>
	/// <param name="_maxDepthLevel">Max depth level.</param>
	public void updateProperties(int _maxDepthLevel) {
		//small disclaimer
		if (myValue == null) return;
		if (depthLevel > _maxDepthLevel) return;

		PropertyInfo[] propertyInfos = myValue.GetType().GetProperties();
		if (propertyInfos == null || propertyInfos.Length == 0) return;

		//update function beginning
		if (myProperties == null) myProperties = new LinkedList<ReflectionObject>();

		foreach (PropertyInfo p in propertyInfos) {
			object pValue = null;
			try {
				//tmp = myValue + "." + myValue.GetType() + "." + myValue.GetType().GetProperty(p.Name) + "." + myValue.GetType().GetProperty(p.Name).GetValue(myValue, null) + "\n";
				pValue = myValue.GetType().GetProperty(p.Name).GetValue(myValue, null);
			} catch (Exception e) {
				//Debug.LogError("ERROR: ReflectionObject.updateProperties");
				Debug.LogError(e.StackTrace);
			} finally {
				if (pValue != null) {
					ReflectionObject child = new ReflectionObject(pValue, p.Name, depthLevel+1);
					myProperties.AddFirst(child);
					child.updateProperties(_maxDepthLevel);
				}
			}
		}
	}

	/// <summary>
	/// Returns a <see cref="System.String"/> that represents the current <see cref="ReflectionObject"/>.
	/// Goes recursively.
	/// </summary>
	/// <returns>A <see cref="System.String"/> that represents the current <see cref="ReflectionObject"/>.</returns>
	public override string ToString() {
		if (depthLevel > Finder.DEPTH_OF_SEARCH) return "";

		//recursive algorithm to print reflection objects tree
		StringBuilder builder = new StringBuilder();
		for (int i = 0; i < depthLevel; ++i) {
			builder.Append("\t");
		}
		builder.Append(myName).Append(": ").Append(myValue.ToString());
		builder.AppendLine();
		foreach (ReflectionObject ro in myProperties) {
			builder.Append(ro.ToString());
		}
		return builder.ToString();
	}
}
