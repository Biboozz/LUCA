using UnityEngine;
using System.Collections;

public class rtsmove : MonoBehaviour {

    // VARIABLES

    public float panSpeed = 10.0f;		// Vitesse de deplacement de la camera pour la deplacement
    public float zoomSpeed = 4.0f;		// Vitesse de deplacement de la camera pour le zoom

    private Vector3 mouseOrigin;	// Position de la souris au depart du deplacement
    private bool isPanning;		// Est ce que la camera se deplace ?
    private bool isZooming;		// Est ce que la camera zoom ?

    public float ZoomSpeed = 1f;
    public int ScrollWheelLimit = 1000;

    private int _ScrollWheelminPush = 0;
    private int _ScrollCount = 1;

    // START

    void Start()
    {

    }

    // UPDATE

    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)     // Mvt molette Zoom - 
        {
            mouseOrigin = Input.mousePosition;      // Prend position de la souris a l'origine
            isPanning = true;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)     // Mvt molette Zoom +
        {
            mouseOrigin = Input.mousePosition;      // Prend position de la souris a l'origine
            isPanning = true;
        }

        //  DEPLACEMENT

        if (isPanning)      // Bouge la camera sur l'axe XY
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move = new Vector3(pos.x * panSpeed * 10, pos.y * panSpeed * 10, 0);
            transform.Translate(move, Space.Self);
        }

        //  ZOOM

        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(_ScrollCount >= _ScrollWheelminPush && _ScrollCount < ScrollWheelLimit)
            {
            camera.transform.position += new Vector3(0, ZoomSpeed * 40, 0);
            _ScrollCount++;
            }
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (_ScrollCount > _ScrollWheelminPush && _ScrollCount <= ScrollWheelLimit)
            {
                camera.transform.position -= new Vector3(0, ZoomSpeed * 40, 0);
                _ScrollCount--;
            }
        }
    }
}
