using UnityEngine;
using System.Collections;

public class rtsmove : MonoBehaviour {

    // VARIABLES

    public float panSpeed = 4.0f;		// Vitesse de deplacement de la camera pour la deplacement
    public float zoomSpeed = 4.0f;		// Vitesse de deplacement de la camera pour le zoom

    private Vector3 mouseOrigin;	// Position de la souris au depart du deplacement
    private bool isPanning;		// Est ce que la camera se deplace ?
    private bool isZooming;		// Est ce que la camera zoom ?

    // Public
    public float ZoomSpeed = 3f;
    public int ScrollWheelLimit = 1000;

    // Private
    private int _ScrollWheelminPush = -300;
    private int _ScrollCount = 1;

    // START

    void Start()
    {

    }

    // UPDATE

    void Update()
    {
        /*if (Input.GetMouseButtonDown(0))      // Prend l'action sur le bouton gauche de la souris
        {
            mouseOrigin = Input.mousePosition;      // Prend position de la souris a l'origine
            isRotating = true;
        }*/

        /*if (Input.GetMouseButtonDown(1))        // Prend l'action sur le bouton droit de la souris
        {
            mouseOrigin = Input.mousePosition;      // Prend position de la souris a l'origine
            isPanning = true;
        }

        /*if (Input.GetMouseButtonDown(2))        // Prend l'action sur le bouton du milieu de la souris
        {
            mouseOrigin = Input.mousePosition;      // Prend position de la souris a l'origine
            isZooming = true;
        }*/

        /*if (Input.GetAxis("Mouse ScrollWheel") < 0)     // Mvt molette Zoom - 
        {
            mouseOrigin = Input.mousePosition;      // Prend position de la souris a l'origine
            isPanning = true;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)     // Mvt molette Zoom +
        {
            mouseOrigin = Input.mousePosition;      // Prend position de la souris a l'origine
            isPanning = true;
        }

        // Desactive les mouvements quand boutons activés
        //if (!Input.GetMouseButton(2)) isPanning = false;
        //if (!Input.GetMouseButton(2)) isZooming = false;

        /*if (isZooming)      // Bouge la camera sur l'axe X et Y
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move = pos.y * zoomSpeed * transform.forward;
            transform.Translate(move, Space.World);
        }*/

        /*if (isPanning)      // Bouge la camera sur l'axe XY
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);
            transform.Translate(move, Space.Self);
        }*/

        //  ZOOM

        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(_ScrollCount >= _ScrollWheelminPush && _ScrollCount < ScrollWheelLimit)
            {
            camera.transform.position += new Vector3(0, ZoomSpeed, 0);
            _ScrollCount++;
            }
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (_ScrollCount > _ScrollWheelminPush && _ScrollCount <= ScrollWheelLimit)
            {
                camera.transform.position -= new Vector3(0, ZoomSpeed, 0);
                _ScrollCount--;
            }
        }
    }
}
