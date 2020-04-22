using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 30f;
    public float scrollSpeed = 5f;
    public float borderOffset = 10f;

    public float minY = 10f;
    public float maxY = 85f;

    private bool readyToMove = true;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            readyToMove = !readyToMove;

        if (!readyToMove)
            return;

        //TODO: сцена и всё объекты развёрнуты по y на 90, поэтому небольшая путаница с направлениями
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - borderOffset)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= borderOffset)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - borderOffset)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= borderOffset)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        }


        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        transform.position = pos;
    }
}
