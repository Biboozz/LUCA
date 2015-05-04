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
		if (pseudos != null && pseudos != "Pseudo")
		{
			pseudos = NGUIText.StripSymbols(mInput.value);
			//gameObject.GetComponent<CellBehaviour>().pseudo = pseudos;
			Debug.Log(pseudos);
		}
	}
}

