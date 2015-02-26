using UnityEngine;
using System.Collections;

public class rtsmove : MonoBehaviour
{

    // VARIABLES

    public float panSpeed = 10.0f;		// Vitesse de deplacement de la camera pour la deplacement
    public float ZoomSpeed = 1.0f;		// Vitesse de deplacement de la camera pour le zoom
    public int ScrollWheelLimit = 1000;
    private int _ScrollWheelminPush = 0;
    private int _ScrollCount = 1;
    private const int ScrollArea = 25;  //Zone défini du déplacement

    // START

    void Start()
    {

    }

    // UPDATE

    void Update()
    {
        //  MOVE - Deplace la caméra lorsque la souris est dans les bords ou avec les flèches directionnelles

        if (Input.mousePosition.x < ScrollArea || Input.GetKey(KeyCode.LeftArrow))      //Left
        {
            transform.Translate(Vector3.right * Time.deltaTime * (_ScrollCount + 1) * panSpeed * 7, Space.World);
        }

        if (Input.mousePosition.x >= Screen.width - ScrollArea || Input.GetKey(KeyCode.RightArrow))     //Right
        {
            transform.Translate(Vector3.right * Time.deltaTime * (_ScrollCount + 1) * panSpeed * -7, Space.World);
        }

        if (Input.mousePosition.y < ScrollArea || Input.GetKey(KeyCode.DownArrow))      //Down
        {
            transform.Translate(Vector3.forward * Time.deltaTime * (_ScrollCount + 1) * panSpeed * 7, Space.World);
        }

        if (Input.mousePosition.y > Screen.height - ScrollArea || Input.GetKey(KeyCode.UpArrow))        //Top
        {
            transform.Translate(Vector3.forward * Time.deltaTime * (_ScrollCount + 1) * panSpeed * -7, Space.World);
        }

        //  ZOOM

        if (Input.GetAxis("Mouse ScrollWheel") < 0)     //Zoom -
        {
            if (_ScrollCount >= _ScrollWheelminPush && _ScrollCount < ScrollWheelLimit)
            {
                camera.transform.position += new Vector3(0, ZoomSpeed * 40, 0);
                _ScrollCount++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)     //Zoom +
        {
            if (_ScrollCount > _ScrollWheelminPush && _ScrollCount <= ScrollWheelLimit)
            {
                camera.transform.position -= new Vector3(0, ZoomSpeed * 40, 0);
                _ScrollCount--;
            }
        }
    }
}
