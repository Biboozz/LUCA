using UnityEngine;
using System.Collections;

public class CoordRightClic : MonoBehaviour
{
    public GameObject goTerrain;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (goTerrain.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity))
            {
                transform.position = hit.point;
            }
        }
    }
}
