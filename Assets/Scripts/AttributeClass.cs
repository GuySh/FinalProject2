using UnityEngine;
using System.Collections;

public class AttributeClass {

	string name;	// for class name
	string code;	// for class code
	bool on;

	public AttributeClass(string name, string code)
	{
		this.name = name;
		this.code = code;
		this.on = false;
	}

	public string Name
	{
		get
		{
			return this.name;
		}
		set
		{
			this.name = value;
		}
	}

	public string Code
	{
		get
		{
			return this.code;
		}
		set
		{
			this.code = value;
		}
	}

	public bool On
	{
		get
		{
			return this.on;
		}
		set
		{
			this.on = value;
		}
	}


}
