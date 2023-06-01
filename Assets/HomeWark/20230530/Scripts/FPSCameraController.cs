using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSCameraController : MonoBehaviour // 카메라의 움직임 구현하기
{
    [SerializeField] Transform cameraRoot;   // 카메라 위치
    [SerializeField] float mouseSensitivity; // 마우스 속도(마우스 민감도)

    private Vector2 lookDelta;
    private float xRotation;
    private float yRotation;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서를 잠금
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;  // 마우스 커서를 풀어줌 ESC를 누르면 마우스가 풀림
    }

    private void LateUpdate() // 카메라는 업데이트는 해줘야함 후처리로 해줘야하기때문에 업데이트가 아닌아닌 레이트 업데이트가 해줘야함 // 레이트 업데이트가 있는건 대다수 카메라 때문임
    {
        Look(); // 매 프레임마다 Look호출
    // 플레이어가 앞을 바라보고 있다가 대각선을 바라보게 하면 카메라도 자식게임오브젝트이기때문에 그 위치를 바라보게 하는건 괜찮음 // 플레이어가 먼저 회전하고 카메라가 같이 회전하는건 괜찮음
    // 카메라가 먼저 이동했을 떄 플레이어가 같은 방향을 바라봐줘야함 // 카메라가 이동해서 플레이어가 그 방향을 바라보면 플레이어가 바라봤기 때문에 하위자식인 카메라도 다시 바라보게되는 연쇄적인 회전이 일어날 수가 있음. // 연쇄작동때문에 플레이어는 360도 계속 돌게됨
    }

    private void Look() // 캐릭터도 같이 움직여야함 // 입력받은 만큼 카레마를 입력받은곳으로 회전
    {
        yRotation += lookDelta.x * mouseSensitivity * Time.deltaTime; // 좌 우
        xRotation -= lookDelta.y * mouseSensitivity * Time.deltaTime; // 상 하 (반전)
                                                                      // xRotation += lookDelta.y * mouseSensitivity * Time.deltaTime; // 상 하 마우스를 내리면 시점이 위로 올라감 마우스를 올리면 시점이 아래로 내려감
        
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); // 플레이어의 회전에 대한 제약, 제약을 안걸면 마우스를 아래나 위, 좌우로 움직여 캐릭터의 목을 꺽은 상태로 뒤도 돌아볼 수 있음
                                                       // Clamp : 최대 최솟값을 넘어가면 최솟값을 최대값을 넘어가면 최솟값을 줌
                                                       // -80f, 80f : 각 값의 각도를 넘어가면 더이상 넘어갈 수 없음 아래로 80도 위로 80도 까지만 회전 가능

        //transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0); // 움직인 방향만큼 플레이어도 같이 회전시킴
        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0, 0); // 상 하로 움직일 땐 카메라만 회전
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);  // 캐릭터가 좌 우로 이동할때 캐릭터의 트랜스폼 자체를 회전시킴
    }

    private void OnLook(InputValue value)
    {
        lookDelta = value.Get<Vector2>(); // 아래쪽으로 움직이면 y의 값을 받게 함
    }




}
