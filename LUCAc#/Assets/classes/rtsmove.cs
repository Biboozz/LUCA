using UnityEngine;
using System.Collections;

public class rtsmove : MonoBehaviour {

    // VARIABLES

    public float panSpeed = 10.0f;		// Vitesse de deplacement de la camera pour la deplacement
    public float ZoomSpeed = 1.0f;		// Vitesse de deplacement de la camera pour le zoom
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
        //  MOVE

        transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * panSpeed * -20, 0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * panSpeed * -20);  //Deplacement XY

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
