using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
    //잠깐 클릭 테스트용으로 지피티한테 가져옴

    public float moveSpeed = 10f;             // 이동 속도
    public float zoomSpeed = 20f;             // 줌 속도
    public float rotationSpeed = 3f;          // 회전 속도

    private float pitch = 0f;                 // 상하 회전값 (고개 들기/숙이기)
    private float yaw = 0f;                   // 좌우 회전값

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void Update()
    {
        // 이동 (WASD)
        float h = Input.GetAxis("Horizontal"); // A, D
        float v = Input.GetAxis("Vertical");   // W, S
        Vector3 moveDir = (transform.forward * v + transform.right * h).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        // 줌 (마우스 휠)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.position += transform.forward * scroll * zoomSpeed;

        // 회전 (마우스 우클릭 드래그)
        if (Input.GetMouseButton(1)) // 우클릭
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            yaw += mouseX * rotationSpeed;
            pitch -= mouseY * rotationSpeed;
            pitch = Mathf.Clamp(pitch, -80f, 80f); // 고개 너무 꺾이지 않도록 제한

            transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        }
    }
}
