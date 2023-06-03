using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPSCameraController : MonoBehaviour // 3인칭 카메라 구현하기
{
    //[SerializeField] Transform lookTarget; // 잠깐설명을 위해 씀
    [SerializeField] Transform cameraRoot;
    //[SerializeField] float cameraSensitivityX; // 카메라 감도
    [SerializeField] float mouseSensitivity;
    [SerializeField] float lookDistance; // 값이 적을수록 더 가까운 방향을 바라보게 된다.

    private Vector2 lookDelta;
    private float xRotation; // 좌 우 회전값
    private float yRotation; // 상 하 회전값
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked; // 게임시작시 중앙으로 마우스 고정
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None; // ESC로 마우스 풀기
    }
    // 카메라가 바라보고 있는 위치를 플레이어가 따라서 보는 방식

    private void Update() // 플레이어를 회전시키는건 업데이트에서
    {
        Vector3 lookPoint = Camera.main.transform.position + Camera.main.transform.forward * lookDistance; // 바라보는 위치 선정 // 캐릭터가 바라보는위치(lookPoint)에서는 메인 카메라에서 메인카메라의 위치에서 카메라가 바라보고 있는 앞방향으로 lookDistance의 값만큼 떨어진 위치를 본다. // 여기서 오류가 뜰 때는 태그가 메인카메라인지 확인한다.
        lookPoint.y = 0;    // 플레이어가 너무 가까운 바닥이나 하늘을 볼경우 눕는경우가 생길수도 있으니 방지하기 위해 y의 위치는 0으로 설정해준다. // 카메라가 바라보는 방향의 y가 0이면 평면을 바라보게된다. // 기준플레이어의 위치가 0이기 때문에y위치가 0인곳을 바라보면 고꾸러지며나 들리지 않는다. 
        //lookTarget.position = lookPoint; // 설명을 위해 룩포인트와 똑같은 위치를 만듬
        transform.LookAt(lookPoint);
    }
    private void LateUpdate()
    {
        Look(); // Look를 레이트업데이트에 구현을 안하면 캐릭터가 마구 흔들거릴수있다
    }
    private void Look()
    {
        yRotation += lookDelta.x * mouseSensitivity * Time.deltaTime;
        xRotation -= lookDelta.y * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        //cameraRoot.localRotation = Quaternion.Euler(xRotation, yRotation, 0); // 여기선 localRotation으로 했을 경우 캐릭터가 마구 흔들릴수가 있다.
        cameraRoot.rotation = Quaternion.Euler(xRotation, yRotation, 0); // 카메라루트를 기준으로 잡아야하기 때문에 로컬이 아닌 그냥 로테이션이다. // cameraRoot를 쓴 이유는  플레이어가 바라본곳과 상관없이 회전하기위해
    }
    private void OnLook(InputValue value)
    {
        lookDelta = value.Get<Vector2>(); // x의 값을 가지게 됨 아래쪽으로 움직이면 y의 값을 받게 함
    }




    /* 플레이어를 상하좌우로 회전시키는 방식
    [SerializeField] float mouseSensitivity;
    private void LateUpdate()
    {
        Look();
    }
    private void Look()
    {
        yRotation += lookDelta.x * mouseSensitivity * Time.deltaTime;
        xRotation -= lookDelta.y * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.localRotation = Quaternion.Euler(0, yRotation, 0); 
    }
    */
}
