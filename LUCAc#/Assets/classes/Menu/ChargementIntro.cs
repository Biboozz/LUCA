using UnityEngine;
using System.Collections;

public class ChargementIntro : MonoBehaviour
{
    private float t = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        t = t + Time.deltaTime;

        if (t > 6)
        {
            Application.LoadLevel(2);
        }
    }
}
