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

    private const int ScrollArea = 25;

    // START

    void Start()
    {

    }

    // UPDATE

    void Update()
    {
        //  MOVE

        if (Input.mousePosition.x < ScrollArea || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * panSpeed * 20, Space.World);
        }

        if (Input.mousePosition.x >= Screen.width - ScrollArea || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * panSpeed * -20, Space.World);
        }

        if (Input.mousePosition.y < ScrollArea || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * panSpeed * 20, Space.World);
        }

        if (Input.mousePosition.y > Screen.height - ScrollArea || Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * panSpeed * -20, Space.World);
        }

        //  ZOOM

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (_ScrollCount >= _ScrollWheelminPush && _ScrollCount < ScrollWheelLimit)
            {
                camera.transform.position += new Vector3(0, ZoomSpeed * 40, 0);
                _ScrollCount++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (_ScrollCount > _ScrollWheelminPush && _ScrollCount <= ScrollWheelLimit)
            {
                camera.transform.position -= new Vector3(0, ZoomSpeed * 40, 0);
                _ScrollCount--;
            }
        }
    }
}
