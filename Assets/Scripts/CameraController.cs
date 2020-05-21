using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Attributes")]
    public float speed = 30f;
    public float scrollSpeed = 5f;
    public float borderOffset = 10f;

    Vector3 startPosition;
    private void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }

        if (Input.GetKey("r"))
        {
            transform.position = startPosition;
        }

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - borderOffset)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= borderOffset)
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - borderOffset)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= borderOffset)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
        }

        //Находим значение колёсика мышки
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 currentPosition = transform.position;
        //Расчитываем изменения высоты при помощи скорости и фактора
        currentPosition.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;
        //Ограничиваем значения y
        currentPosition.y = Mathf.Clamp(currentPosition.y, 10f, 85f);
        currentPosition.z = Mathf.Clamp(currentPosition.z, -110f, -10f);
        currentPosition.x = Mathf.Clamp(currentPosition.x, 10f, 90f);
        //Применяем высоту
        transform.position = currentPosition;
    }
}
