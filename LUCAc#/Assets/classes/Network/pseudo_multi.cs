using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UIInput))]

public class pseudo_multi : MonoBehaviour
{
	public static string pseudos = "Joueur";
	
	UIInput mInput;

	void Start ()
	{
		mInput = GetComponent<UIInput>();
		mInput.label.maxLineCount = 1;
	}

	public void OnChange ()
	{
		if (pseudos != null && pseudos != "Pseudo")
		{
			pseudos = NGUIText.StripSymbols(mInput.value);
		}
	}
}

