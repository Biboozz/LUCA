using UnityEngine;
using System.Collections;

public class rtsmove : MonoBehaviour
{

    // VARIABLES

    public float panSpeed = 10.0f;		// Vitesse de deplacement de la camera pour la deplacement
    public float ZoomSpeed = 1.0f;		// Vitesse de deplacement de la camera pour le zoom
    public int ScrollWheelLimit = 2000;
    private int _ScrollWheelminPush = 0;
    private int _ScrollCount = 1;
    private const int ScrollArea = 25;  //Zone défini du déplacement
    public GameObject TreeShow;

    // START

    void Start()
    {

    }

    // UPDATE

    void Update()
    {
        //  MOVE - Deplace la caméra lorsque la souris est dans les bords ou avec les flèches directionnelles

        if (!TreeShow.GetComponent<displayPerkTree>().shown)
        {
            if ((Input.mousePosition.x < ScrollArea || Input.GetKey(KeyCode.LeftArrow)) && (transform.localPosition.x >= 50))     //Left
            {
                transform.Translate(Vector3.left * Time.deltaTime * (_ScrollCount + 1) * panSpeed * 7, Space.World);
            }

			if ((Input.mousePosition.x >= Screen.width - ScrollArea || Input.GetKey(KeyCode.RightArrow)) && (transform.localPosition.x <= 1950))     //Right
            {
                transform.Translate(Vector3.left * Time.deltaTime * (_ScrollCount + 1) * panSpeed * -7, Space.World);
            }

			if ((Input.mousePosition.y < ScrollArea || Input.GetKey(KeyCode.DownArrow)) && (transform.localPosition.y >= 50)/*&& (GameObject.Find("PointRepere").transform.position.y - transform.localPosition.z >= -950)*/)      //Down
            {
                transform.Translate(Vector3.down * Time.deltaTime * (_ScrollCount + 1) * panSpeed * 7, Space.World);
            }

			if ((Input.mousePosition.y > Screen.height - ScrollArea || Input.GetKey(KeyCode.UpArrow)) && (transform.localPosition.y <= 1950)/*&& (GameObject.Find("PointRepere").transform.position.y - transform.localPosition.z <= 950)*/)       //Top
            {
                transform.Translate(Vector3.down * Time.deltaTime * (_ScrollCount + 1) * panSpeed * -7, Space.World);
            }
        }

        //  ZOOM

        if (Input.GetAxis("Mouse ScrollWheel") < 0)     //Zoom -
        {
            if (_ScrollCount >= _ScrollWheelminPush && _ScrollCount < ScrollWheelLimit)
            {
				//GetComponent<Camera>().transform.position -= new Vector3(0, 0, ZoomSpeed * 40);
				Camera.main.orthographicSize += ZoomSpeed * 30;
                _ScrollCount++;
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0)     //Zoom +
        {
            if (_ScrollCount > _ScrollWheelminPush && _ScrollCount <= ScrollWheelLimit)
            {
				//GetComponent<Camera>().transform.position += new Vector3(0, 0, ZoomSpeed * 40);
				Camera.main.orthographicSize -= ZoomSpeed * 30;
                _ScrollCount--;
            }
        }
    }
}
