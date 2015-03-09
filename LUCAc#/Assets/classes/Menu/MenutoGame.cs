using UnityEngine;
using System.Collections;

public class SceneChange : MonoBehaviour
{
    public GameObject target;
    public bool state = true;

    void OnClick()
    {
        if (target != null)
        {
            Application.LoadLevel("test1soir");
        }
    }
}
