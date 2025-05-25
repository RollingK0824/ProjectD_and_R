using UnityEngine;

public class SimpleCameraController : MonoBehaviour
{
    //��� Ŭ�� �׽�Ʈ������ ����Ƽ���� ������

    public float moveSpeed = 10f;             // �̵� �ӵ�
    public float zoomSpeed = 20f;             // �� �ӵ�
    public float rotationSpeed = 3f;          // ȸ�� �ӵ�

    private float pitch = 0f;                 // ���� ȸ���� (�� ���/���̱�)
    private float yaw = 0f;                   // �¿� ȸ����

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;
    }

    void Update()
    {
        // �̵� (WASD)
        float h = Input.GetAxis("Horizontal"); // A, D
        float v = Input.GetAxis("Vertical");   // W, S
        Vector3 moveDir = (transform.forward * v + transform.right * h).normalized;
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        // �� (���콺 ��)
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        transform.position += transform.forward * scroll * zoomSpeed;

        // ȸ�� (���콺 ��Ŭ�� �巡��)
        if (Input.GetMouseButton(1)) // ��Ŭ��
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            yaw += mouseX * rotationSpeed;
            pitch -= mouseY * rotationSpeed;
            pitch = Mathf.Clamp(pitch, -80f, 80f); // �� �ʹ� ������ �ʵ��� ����

            transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        }
    }
}
