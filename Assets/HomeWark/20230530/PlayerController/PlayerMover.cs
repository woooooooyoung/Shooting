using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float moveSpeed;                // 속력(moveSpeed)
    [SerializeField] float jumpSpeed;

    private CharacterController characterController; // 캐릭터컨트롤러 컴포넌트를 사용하기 위함
    private Vector3 moveDir;                         // 움직이는 방향 받기
    private float ySpeed = 0;                        // y축을 기준으로 갖는 속도

    private void Awake()
    {
        characterController = GetComponent<CharacterController>(); // 캐릭터 컨트롤러 컴포넌트 가져오기
    }

    private void Update()
    {
        Move(); // 업데이트하는동안 지속적으로 Move 실행
        Jump();
    }

    private void Move()
    {
        // 캐릭터 컨트롤러가 아닌 transform으로 구현했을 경우
        // transform.Translate(moveDir * moveSpeed * Time.deltaTime);   // 절대적인 이동이기 때문에 주변환경(오브젝트)에 상관없이 모두 통과함 계단을 오른다는 행동이 안됨

        // 캐릭터컨트롤러로 구현했을 경우
        characterController.Move(moveDir * moveSpeed * Time.deltaTime); // moveDir 방향으로 움직이기 
                                                                        // 속력(moveSpeed)을 넣어주지 않으면 순식간에 이동함
                                                                        // 단위시간(deltaTime)을 곱해주기
                                                                        // 속력 만들기 = 원하는수치(moveSpeed) * 단위시간(Time.deltaTime)
                                                                        // Slope Limit       : 이 수치만큼의 각도를 오를 수 있다.
                                                                        // Step Offset       : 이 수치만큼의 위치면 오를 수 있다.

        

    }
    private void OnMove(InputValue value)
    {
        // 입력 받았을때 moveDir를
        Vector2 input = value.Get<Vector2>();       // input같은 경우 value의 Get을 통해 Vector2값을 받아준다
        moveDir = new Vector3(input.x, 0, input.y); // 받은 값의 x값은 x축으로 y축값은 0 받은 값의 y값을 z축에 할당
    }
    private void Jump()
    {
        // 게임적인 점프(올라갔다 떨어짐)와 포물선 점프(완만하게 점프해서 완하게 떨어짐)도 있다.
        // 포물선 구현(y위치) // 중력(y)의 방향을 향한 가속 // 받은 속력만큼 y축으로 이동
        ySpeed += Physics.gravity.y * Time.deltaTime; // 중력방향의 y방향으로 계속해서 속력을 가짐 // 속력의 개념이기 때문에 deltaTime을 가짐 // 아래쪽으로 계속해서 빨라짐 // 설정에 있는 Physics - gravity의 값을 가짐

        // if (characterController.isGrounded) 캐릭터컨트롤러에는 isGrounded는 쓰지말 것 작동이 잘 안됨 조금만 틀어져도 안먹히고 조금만 부딪혀도 먹힘
        if (GroundCheck()) // 캐릭터컨트롤러가 땅에 붙어있는 경우 ySpeed는 0으로 설정 // 아래로 떨어지다가 땅에 충돌해서 땅 위에 있는 경우 속력을 0으로 세팅해서 위 아래 (y) 로 움직이지 않음
            ySpeed = 0; 

        characterController.Move(Vector3.up * ySpeed * Time.deltaTime); // y축(up방향)에 해당하는 방향으로 계속해서 움직여줌
    }
    private void OnJump()
    {
        ySpeed = jumpSpeed; // ySpeed를 가해주면 점프를 함
    }
    private bool GroundCheck()
    {
        RaycastHit hit;
        return Physics.SphereCast(transform.position, 0.5f, Vector3.down, out hit, 0.5f); // 반환할 때 부딪혔으면 true 아니면 false
        // (어디부터 쏘는지, 어느정도 둘레, 어느방향, 어느정도의 길이)
        // 동그라미를 날려서 동그라미로 부딪힌다.

    }

}
