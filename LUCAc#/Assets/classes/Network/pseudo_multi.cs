using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIInput))]

public class pseudo_multi : MonoBehaviour
{
	public string pseudos = "Guest";
	
	UIInput mInput;

	void Start ()
	{
		mInput = GetComponent<UIInput>();
		mInput.label.maxLineCount = 1;
	}

	public void OnChange ()
	{
		pseudos = NGUIText.StripSymbols(mInput.value);
		if (pseudos != null && pseudos != "Pseudo")
		{
			GetComponent<SwitchScene>().pseudo = pseudos;
			Debug.Log(pseudos);
		}
	}
}

