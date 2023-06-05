using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.UI.GridLayoutGroup;


public class HomeWark : MonoBehaviour { }
// <Time.deltaTime>
// 프레임 간 이동 사이의 시간 간격
// 현재 fps의 상태를 알고 자동으로 시간간격을 계산해 줌으로써,
// 컴퓨터마다 시간을 통일해 줄 수 있어,
// fps가 다름으로 인해 발생할 수 있는 오류를 방지해 준다.
// <Distance>
// 거리를 구하는 함수
// 

// <타일맵 만들기>
// 2D 패키지가 깔려있어야함
// Hierarchy 우클릭 - 2D 오브젝트 - 타일맵 
// Rectangular              : 바둑판방식의 격자무늬
// Hexagonal - Point - Top  : 육각형 형태의 타일맵 (전략시뮬레이션 장르의 게임에 많이 쓰임)
// Hexagonal - Flat - Top   :
// Isometric                : 마름모형태의 타일맵 (2D게임이지만 3D게임처럼 착시효과를 일으키는데 사용할 수 있음)
// Isometric Z as Y         :

// 스프라이트시트를 타일맵으로 쓰기
// 1. Window - 2D - Tile Palette
// 2. Hirearchy 에서 타일맵을 선택시 신창에서 보라색 큐브모양이 생긴걸 눌러도 됨
// Tile Palette - Create New Palette - 원하는 곳에서 폴더를 만듬 - 원하는 스프라이트 시트를 끈 뒤 원하느 폴더를 만듬

// 타일 팔레트를 잘못만든 경우 타일 팔레트에 있는 Edit를 눌러서 수정할 수 있다. 

// <Rule Tile>
// x      : 이 방향에는 아무것도 없다.2
// 화살표  : 이 방향에는 있다.
// 빈공간  : 상관없다.
// 방향이 바뀐 타일을 쓰는것보단 같은 조건의 타일을 복사해서 반전해주는게 훨씬 쉽다.
// 타일 가운데 버튼 적극 이용

// <Random Tile>
// +로 룰타일처럼 만든 뒤 OutPut에서 Random으로 바꾸고 Size에서 숫자를 입력하면 그 입력한 숫자만큼의 랜덤한 타일을 지정해줄 수 있다.
// 한 룰타일 안에 여러가지의 랜덤타일을 만들 수 있다.
// 타일맵에다 편의기능 넣는법
// 이미지 중에 원하는 이미지를 타일맵에 넣을 수 있다
// 1. 원하는 이미지를 프리팹화
// 2. 타일 팔레트 아래에있는 브러쉬를 게임오브젝트 브러쉬를 선택한다
// 3. GameObject Brush - Cells - Game Object에 원하는 프리팹을 넣어주면 타일맵처럼 사용 가능하다
// 4. 타일맵에 있는게 아니기 때문에 타일맵의 하위자식으로 추가된다. 자식으로 추가되도 밖으로 빼낼 수 있음
// 5. 조심할건 게임오브젝트 브러쉬라 타일맵과는 별개기 때문에 타일맵에 있는건 게임오브젝트 브러쉬로 사용이 안된다.

// <Animated Tile>
// 타일맵에 애니메이션을 넣을 수 있다.
//


// <20230524 상태패턴>

/*private void Update()
{
    // 점프를 하는 몬스터를 만들 때
    if (isPatrol) // 점프하고 맞지도 않고 플레이어를 안만났을 때 순찰
    {

    }
    else
    {

    }
    if (isJumping) // 점프중일 때
    {
        // <피격당함>
        // 점프 중 피격당했을때
        // 점프를 하지 않고 있는데 맞았을 때
        // 점프를 하는 중에 맞았을 떄
        // 맞지않고 점프를 했을때
        if (isHited)// 점프중인데 맞았을떄
        {
            // 점프 중인데 맞았으니 점프를 안하고 맞음
        }
        else
        {
            // 점프중인데 안맞았으니 그대로 점프 함
        }
    }
    else // 점프중이 아닐때
    {
        Move(); // 점프중이 아닐때만 움직일 수 있다.
        if (!IsGroundExist())
        {
            Turn();
        }
    }
}
// 순찰중인데 점프하고있고 맞는 중일땐?
// 이렇게 많은 조건을 사람이 판단하고 만들기는 쉽지 않다.
// 그래서 편하게 관리하기 위해 상태패턴이 있다
// 몬스터를 만들기 위한 하나의 기능이다
*/

//====================================
//##        디자인 패턴 State        ##
//====================================
/*
 * 상태패턴 : 객체에게 한 번에 하나의 상태만을 가지게 하며 ex. 가만히 있는 상태, 점프하는 상태, 맞는 상태, 순찰중인 상태 등등
 * 객체는 현재 상태에 해당하는 행동만을 진행함
 * 구현 :
 * 1. 열거형 자료형으로 객체가 가질 수 있는 상태들을 정의 ex. 기본, 순찰, 점프, 맞음 등, 어디서부터 상태를 시착할지 처음상태를 지정해 줘야 함
 * 2. 현재 사채를 저장하는 변수에 초기 상태를 지정
 * 3. 객체는 행동에 있어서 현재 상태만의 행동을 진행
 * 4. 객체는 현재 상태의 행동을 진행 후 상태 변화에 대해 판단
 * 5. 상태 변화가 있어야 하는 경우 현재 상태를 대상 상태로 지정
 * 6. 상태가 변경된 다음 행동에 있어서 바뀐 상태만의 행동을 진행
 * 장점 :
 * 1. 객체가 진행할 행동을 복잡한 조건문을 상태로 처리가 가능하므로, 조건처리에 대한 부담이 적음
 * 2. 객체가 가지는 여러상태에 대한 연산없이 현재 상태만을 처리하므로, 연산속도가 뛰어남
 * 3. 객체와 관련된 모든 동작을 각각의 상태에 분산시키므로, 코드가 간결하고 가독성이 좋음
 * 단점 :
 * 1. 상태의 구분이 명확하지 않거나 갯수가 많은 경우, 상태 변경 코드가 복잡해질 수 있음
 * 2. 상태를 클래스로 캡슐화 시키지 않은 경우 상태간 간섭이 가능하므로, 개방폐쇄원칙이 준수되지 않음 !캡슐화 필수!
 * 3. 간단한 동작의 객체에 상태패턴을 적용하는 경우, 오히려 관리할 상태 코드량이 증가하게 됨, 너무 간단한선 상태패턴을 ㅗ쓰지말자
 */
/*
public class State
{
    public class Mobile  // 핸드폰을 충전상태를 예로 듬
    {
        public enum State { Off, Normal, Charge, FullCharged } // 꺼짐, 동작중, 충전중, 완전충전

        private State state = State.Normal; // 처음 시작상태를 Normal로 함
        private bool charging = false;      // 처음 시작상태의 충전 여부 
        private float battery = 50.0f;      // 배터리 상태 50

        private void Update()
        {

            // if (charging)
            // {
            //     if (battery == 100)
            //     {
            //         // 충전중일 떄 배터리가 100%라면 완전히 충전된 상태고 더이상 충전을 안함
            //     }
            //     else
            //     {
            //         // 충전중인데 배터리 상태가 100%가 아니면 충전을 진행한다.
            //     }
            // }
            // else    // 충전중이 아닌 경우
            // {
            //     if (battery == 0)
            //     {
            //         // 배터리가 0%니 완전히 방전되서 동작이 안되고 충전을 기다린다.
            //     }
            //     else
            //     {
            //         // 배터리가 0%가 아니니 충전은 안하고 동작만 한다.
            //     }
            // }


            switch (state)              // 현재상테에서 할 업데이트들만 진행
            {
                case State.Off:         // 현재상태가 Off일 경우 OffUpdate에 대한 내용을 진행
                    OffUpdate();
                    break;
                case State.Normal:      // 현재상태가 Norma일 경우 NormaUpdate에 대한 내용을 진행
                    NormalUpdate();
                    break;
                case State.Charge:      // 현재상태가 Charge일 경우 ChargeUpdate에 대한 내용을 진행
                    ChargeUpdate();
                    break;
                case State.FullCharged: // 현재상태가 FullCharge일 경우 FullChargeUpdate에 대한 내용을 진행
                    FullChargedUpdate();
                    break;
            }
        }
        private void OffUpdate()
        {
            // Off work     
            // Do nothing   
            // Off 상태일 때 할일을 정해줄 수 있다.

            if (charging) // 충전중일때
            {
                state = State.Charge; // 현재상태를 충전상태로 전환한다
            }
        }
        private void NormalUpdate()
        {
            // Normal work
            // Normal 상태일 때 할일을 정해줄 수 있다.
            battery -= 1.5f * Time.deltaTime; // 배터리를 1.5씩 계속 소모해준다. 

            if (charging) // 충전중일때
            {
                state = State.Charge; // 현재상태를 충전상태로 전환한다
            }
            else if (battery <= 0) // battery가 0보다 작거나 같을 때
            {
                state = State.Off; // 현재상태를 Off상태로 전환한다
            }

        }
        private void ChargeUpdate()
        {
            // Charge work
            // Charge 상태일 때 할일을 정해준다.
            battery += 2.5f * Time.deltaTime; // 배터리소모를 안하고 2.5씩 충전해준다

            if (!charging) // 충전을 안할 때
            {
                state = State.Normal; // 현재상태를 Normal상태로 전환한다
            }
            else if (battery >= 100) // battery가 100보다 크거나 같을 때
            {
                state = State.FullCharged; // 현재상태를 FullCharged상태로 전환한다
            }
        }
        private void FullChargedUpdate()
        {
            // FullCharged
            // FullCharged 상태일 때 할일을 정해준다.
            if (!charging) // 충전을 하지 않는다
            {
                state = State.Normal; // 현재상태를 Normal상태로 전환한다
            }
            // 배터리가 FullCharged상태니 else는 넣어주지 않아도 된다.
        }
        public void ConnectCharger()        // 충전기 연결
        {
            charging = true;                // 충전중
        }
        public void DisConnectCharger()     // 충전기 연결 안함
        {
            charging = false;               // 충전안함
        }
    }

    // <공중몬스터 구현>
    // 몬스터 상속하기
    //public class Monster : MonoBehaviour
    //{ }
    //public class Bee : Monster
    //{ } 
    // 상속받으면 컴포넌트 못쓰는게 아니라 Monster자체가 MonoBehaviour이기 때문에 MonoBehaviour가 쓰는 컴포넌트는 다 쓸 수 있음
    // 유니티창 : Bee한테 텍스트 준뒤 캔버스의 카메라는 월드스페이스주고 크기 조정 위치는 자신의 위치인 0, 0, 0 폰트사이즈 줄이기 내용은 Idle 이벤트카메라는 메인카메라주고 가운데정렬
    public class Bee : MonoBehaviour
    {
        // 1. 플레이어가 멀리 있을땐 가만히 있기
        // 2. 플레이어가 어느정도 가까워지면, 플레이어를 공격하도록 추적
        // 2-1. 추적 중에 너무 멀어지면 다시 제자리
        // 2-2. 추적중에 공격범위 안에 들어오면 공격함
        public enum State { Idle, Trace, Return, Attack, Patrol }    // 가만히, 추적, 돌아오기, 공격, 순찰

        [SerializeField] private TMP_Text text;             // 텍스트를 이용해 현재 무엇을 하고있나 표시함 switch와 연동
        [SerializeField] private float detectRange;         // 이 영역 안에 있으면 쫓아가게 하기 위해 썻음
        [SerializeField] private float attackRange;
        [SerializeField] private float moveSpeed;
        [SerializeField] private Transform[] patrolPoints;  // 패트롤 포인트가 몇개나 만들지 몰라서 배열로 정해준다.

        private State curState;                             // 지금상태를 보관하는 현재상태
        // private PlayerController player;                 // PlayerController스크립트가 달린 플레이어를 찾기위해 씀
        private Transform player;                           // Player의 Tag가 달린 플레이어의 현재위치를 찾기위해 사용
        private Vector3 returnPosition;                     // 돌아갈 위치(지금 있어야 할 위치) 현재위치가 아닌 돌아가야할 위치로 가야하기때문에 트랜스폼대신 백터를 씀
        private int patroIndex = 0;                         // 현재 내가 몇번째 위치를 순찰중인지 아는 patroIndex 0번째부터 시작
        private void Start()
        {
            curState = State.Idle;                          // 시작하자마자 상태
            player = GameObject.FindGameObjectWithTag("Player").transform; //Player의 Tag가 달린 플레이어의 현재위치를 탐색, 플레이어 자체를 따라다니는것보다 위치를 찾는게 중요함 몬스터는 시작하자마자 플레이어 게임오브젝트의 트랜스폼 컴포넌트를 갖게 됨
            returnPosition = transform.position;            // 시작하자마자 위치로 지정해야 있었던 위치로 지정되기때문에 추격에 실패하면 원래 있었어야할 위치로 인식해서 돌아감
        }
        private void Update()
        {
            switch (curState)                               // 각각의 상태에서 현재상태의 작업
            {
                case State.Idle:
                    text.text = "Idle";
                    IdleUpdate();
                    break;
                case State.Trace:
                    text.text = "Trace";
                    TraceUpdate();
                    break;
                case State.Return:
                    text.text = "Return";
                    ReturnUpdate();
                    break;
                case State.Attack:
                    text.text = "Attack";
                    AttackUpdate();
                    break;
                case State.Patrol:
                    PatrolUpdate();
                    break;
            }
        }
        float idleTime = 0; // idleTime를 0으로 선언
        private void IdleUpdate()   // 평소상태
        {
            // 아무것도 안하기

            // 순찰시간이 되었을 때
            if (idleTime > 2) // 2초동안 아무것도 안할시
            {
                idleTime = 0;            // 0으로 만듬
                patroIndex = (patroIndex + 1) % patrolPoints.Length; // patroIndex가 patrolPoints의 갯수를 넘어가면 안돼니 patrolIndex를 +1을 해주고 %로 patrolPoints를 Length만큼 나머지 연산을 해줌 ex. 패트롤 포인트의 갯수는 2개인데 2개를 넘어가면 안되니 3번째 포인트로 가는게 아닌 0번째 포인트로 가줌
                curState = State.Patrol; // 순찰상태로 전환
            }
            idleTime += Time.deltaTime;

            // 플레이어와 가까워졌을 때
            if (Vector2.Distance(player.position, transform.position) < detectRange) // 플레이어의 포지션과 지금 나의 포지션이 detectRange보다 작으면 플레이어가 가까워졌다고 인식함
            {
                curState = State.Trace; // 따라가는게 아닌 추적상태로 변환함
            }
        }
        private void TraceUpdate()  // 추격상태
        {
            // 플레이어 쫓아가기
            // normalized : 백터가 크든 작든 크기가 1인 백터로 만들어서 정규화시켜줌
            Vector2 dir = (player.position - transform.position).normalized;  // 플레이어의 포지션을 내 위치만큼 뺸위치만큼 가줌, 플레이어가 있는 방향으로 가는 방법은 도착지 - 출발지가 출발지로부터 그 도착지로 가기위한 방향성 백터임(백터연산을 알아야 잘이해가 됨)
            transform.Translate(dir * moveSpeed * Time.deltaTime); // 방향과 속도 시간을 가지고 곱해줌

            // 플레이어가 멀어졌을 때
            if (Vector2.Distance(player.position, transform.position) > detectRange) // 플레이어의 포지션과 지금 나의 포지션이 detectRange보다 작으면 플레이어가 커져서 벗어났기 때문에 범위안에 플레이어를 찾을수가 없음
            {
                curState = State.Return; // 리턴상태로 전환
            }

            // 공격범위 안에 있을 때
            else if (Vector2.Distance(player.position, transform.position) < attackRange) // 플레이어의 포지션과 지금 나의 포지션이 attackRange보다 작으면 플레이어가 공격범위안에 들어왔다고 인식함
            {
                curState = State.Attack; // 공격상태로 전환
            }
        }
        private void ReturnUpdate() // 돌아올떄
        {
            // 원래 자리로 돌아가기
            Vector2 dir = (returnPosition - transform.position).normalized;
            transform.Translate(dir * moveSpeed * Time.deltaTime);

            // 원래 자리로 도착했으면
            if (Vector2.Distance(transform.position, returnPosition) < 0.02f) // 돌아가고자하는 위치가 0.02보다 작으면 도착했다고 본다.
                                                                              // if (transform.position == returnPosition) 돌아가고자 하는 위치가 정확하게 현재위치라면 맞지만 float는 대략적인 소수점을 표현하는 방식이다 보니 조금만 틀어져도 달랐다고 나오고 유니티는 프레임 기반으로 돌아가다 보니 도착위치가 한프레임의 속도와 딱 맞지 않는 위치에 있다면 역방으로 돌아왔다갔다 하다보니 계속 떨리기만 하고 반복하다보니 근접했다고 쓰는게 낫다
            {
                curState = State.Idle; // 평소상태로 전환
            }
            else if (Vector2.Distance(player.position, transform.position) < detectRange) // 돌아가는 중에도 detectRange안에 있는게 확인이 됐을 때
            {
                curState = State.Trace; // 추격상태로 전환
            }
        }
        float lastAttackTime = 0;
        private void AttackUpdate() //공격할때
        {
            // 공격하기
            if (lastAttackTime > 3) // lastAttackTime이 3보다 클때만 공격, 여기선 3초이상지나야 공격함
            {
                Debug.Log("공격");
                lastAttackTime = 0; // 3보다 커졌으니 다시 0으로 만듬
            }
            lastAttackTime += Time.deltaTime;  // lastAttackTime을 Time.deltaTime만큼 더해줌, 1초가 됐을때 1만큼 누적
            // 폴링방식의 시간을 누적시켜서 하는 방식

            if (Vector2.Distance(player.position, transform.position) > attackRange) // 공격하다가 공격범위를 벗어나면, 추격상태로 바꾸기 위해 쓰려면 detectRange가 attackRange보다 커야함
            {
                curState = State.Trace; // 추격상태로 전환
            }
        }
        private void PatrolUpdate() //순찰할때
        {
            // 빈게임 오브젝트를 만들어서 술찰위치를 지정해줄 수 있다.
            // 순찰진행
            // patroIndex++; 다음위치를 순찰해야하니 하나를 늘려줌
            Vector2 dir = (patrolPoints[patroIndex].position - transform.position).normalized; // patrolPointsd의 patroIndex의 자리로 가줌
            transform.Translate(dir * moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, patrolPoints[patroIndex].position) < 0.02f)  //patrolPoints의 patroIndex의 위치로 가면

            {
                curState = State.Idle; // 평소상태로 전환

            }
            else if (Vector2.Distance(player.position, transform.position) < detectRange) // 돌아가는 도중에 플레이어를 발견하면
            {
                curState = State.Trace; // 추격상태로 전환
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;                            // 노란색
            Gizmos.DrawWireSphere(transform.position, detectRange); // 내위치로부터 detectRange 만큼 동그랗게 그려줌 DrawWireSphere : 동그라미
            Gizmos.color = Color.red;                               // 빨간색
            Gizmos.DrawWireSphere(transform.position, attackRange); // 내위치로부터 attackRange 만큼 동그랗게 그려줌 DrawWireSphere : 동그라미
        }

        // 상태패턴가지고 구현했을경우 업데이트끼리 간섭을 하는 문제가 있을 수 있음
        // 현재 위에 Bee를 만든 경우 Bee라는 몬스터 안에 Idle와 Trace를 넣어놔서 둘의 서로간의 정보교환이 가능하다.
        // Idle에서 건드리지 않아야 할 변수를 Trace에서 건드렸을 때 문제가 될 수 있다.
        // 각각의 상태를 따로따로 캡슐화를 시켜주고 싶은 경우 각각을 class로 만들어줘야 할 필요성이 있다.
        // 상태클래스 StateBase를 하나 만들어주고 StateBase 상속하도록 IdleBase, TraceBase와 같은 다른 상태들을 만들어주고 Bee가 만들어진 상태들을 왕복하게 만들어준다

        public interface IState // 상태 자체를 interface 쓰는 경우도 있다. public interface IState나 public class StatBase 둘다 괜찮은 방식이다
        {
            // 행동들만 설계해도 됨
        }
        // public abstract class StateBase // 상태들의 부모클래스 설정 가능 // 추상클래스로 구현한 이유는 Bee에 있는 함수들을 가져다 쓸 수  있지만 단지 취향 차이임
        // public abstract class StatBase<TOwner> where TOwner : MonoBehaviour // 이 상태가 어떤 컴포넌트의 상태인지를 일반화 시켜서 가지고 있는 경우 도움되는 경우가 있다. // TOwner가 MonoBehaviour이다
        public abstract class StateBase<TOwner> where TOwner : MonoBehaviour
        {
            // private TOwner owner; // StatBase의 어떤 컴포넌트가 이 상태를 가지고있는지
            protected TOwner owner;
            public StateBase(TOwner owner) // StatBase를 생성할 당시 TOwner owner를 같이 준다
            {
                this.owner = owner;
            }
            // 각 상태마다 진행해야 하는 업데이트 함수
            public abstract void Setup();   // 초기세팅 // 컨셉에 맞춰 Start로 해도 된다.
            public abstract void Enter();   // 진입 // 상태에 진입했을 떄 호출받을 수 있는 Enter함수 구현
            public abstract void Update();  // 동작 // abstract를 통해 추상함수를 쓴다하면 StatBase를 쓰는 함수는 반드시 업데이트를 구현할수밖에 없다.
            public abstract void Exit();    // 탈출 // 그 상태에서 벗어났을때 쓰는 Exit 함수
            // 이 함수들을 따로 관리해주면 좋다.
        }

        //************************************************************************************************************************************************
        // 위에서 사용한 Bee를 캡슐화하기
        // 업데이트로 구현한 방식이 아니다 보니 상태들에 대해 여러 상태를 들고 있을 수 있게 뒤에서 구현
        public class Bee : MonoBehaviour
        {
            public enum State { Idle, Trace, Return, Attack, Patrol, Size } // Size는 상태는 아니고 열거형의 몇개를 가지고 있는지 확인할 때 쓴다. Size는 열거형의 꼭 맨 뒤에 둬야한다. Size를 넣으면 열거형을 int로 변환할 수 있다.

            [SerializeField] public TMP_Text text;
            [SerializeField] public float detectRange;
            [SerializeField] public float attackRange;
            [SerializeField] public float moveSpeed;
            [SerializeField] public Transform[] patrolPoints;

            *
             * 이런 정보들 같은경우는 MVC패턴을 적용한다하면 Bee모델에만 적용해주고 Bee자체는 컨트롤러로 써주는게 좋다
             * public TMP_Text text;
             * public float detectRange;
             * public float attackRange;
             * public float moveSpeed;
             * public Transform[] patrolPoints;
             * public Transform player;
             * public Vector3 returnPosition;
             * public int patroIndex = 0;
            *

            private StateBase<Bee>[] states; // Bee의 StateBase를 states로 배열로 들고있는다. // 딕셔너리 형태로 쓰는 경우도 있다.
            private StateBase<Bee> curState;

            public Transform player;            // private Transform player;          // 모델로 따로 빼주는게 좋지만 지금은 Bee 하나에서 모델이랑 컨트롤러 한번에 쓴다.
            public Vector3 returnPosition;      // private Vector3 returnPosition;    // 모델로 따로 빼주는게 좋지만 지금은 Bee 하나에서 모델이랑 컨트롤러 한번에 쓴다.
            public int patroIndex = 0;          // private int patroIndex = 0;        // 모델로 따로 빼주는게 좋지만 지금은 Bee 하나에서 모델이랑 컨트롤러 한번에 쓴다.
            private void Awake() // Awake에서 상태를 가질 수 있는 배열을 만든다
            {
                states = new StateBase<Bee>[(int)State.Size]; // State의 Size는 5개다. 여기서 5개만 써서 그렇지 더 쓰면 더 올라간다. 내가 가지고 있는 상태의 갯수만큼 변환할 수 있는 역할로 만들 수 있다.
            }
            private void Start()
            {
                player = GameObject.FindGameObjectWithTag("Player").transform;
                returnPosition = transform.position;
            }
            // 아래 캡슐화에서 상태전환이 있을 때 Bee컨트롤러에 추가
            public void ChangeState(State state)
            {
                curState.Exit(); // 현재상태를 우선 빠져나간다.
            }


            private void OnDrawGizmos()
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(transform.position, detectRange);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, attackRange);
            }
        }
    }
}
}
namespace BeeState //IdleState라는 이름자체가 다른곳에서 많이 쓰일 수 있기때문에 namespace를 사용한다.
{
public class IdleState : StateBase<Bee> // IdleState는 StateBase의 Bee를 상속받음
{
    // 캡슐화가 완료되었을 경우 다른클래스에 있는걸 쓰고 싶어도 쓸 수가 없어진다
    private float idleTime;
    public IdleState(Bee owner) : base(owner)
    {

    }
    public override void Setup()
    {

    }
    public override void Enter() // 상태에 진입했을 때 단 한번만 할 일
    {
        idleTime = 0; // 진입했으니 idleTime은 0부터 진행
    }
    public override void Update() // 업데이트일 때 할일 
    {
        // 아무것도 안하기

        idleTime += Time.deltaTime;

        // 순찰시간이 되었을 때
        if (idleTime > 2)
        {
            idleTime = 0;
            owner.patroIndex = (owner.patroIndex + 1) % owner.patrolPoints.Length; // owner자체가 Bee가 되어 Bee에 있는 patroIndex를 써준다.
            // curState = State.Patrol;
        }

        else if (Vector2.Distance(owner.player.position, owner.transform.position) < owner.detectRange)
        {
            // curState = State.Trace;
        }
    }
    public override void Exit() // 벗어났을 때 할일
    {

    }
}

public class TraceState : StateBase<Bee>
{

    public TraceState(BeeState owner) : base(owner)
    {

    }
    public override void Setup()
    {
        idleTime = 1; // 캡슐화를 했을 때 IdleState에서 만든 idleTime는 아예 다른클래스라 고칠수가 없다.
    }
    public override void Enter()
    {

    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
}
}
*/



// <20230531 3D 슈팅 - 캐릭터 컨트롤러, FPS, TPS, 3D 애니메이션, 휴머노이드 모델>
// 리지드바디를 이용한 경우
// 슈팅게임 같은경우 리지드바디를 통해 움직일때 힘이 별로 중요하지 않은 경우가 있다.
// 충분한 힘이 없을 경우 작은 턱이 있으면 걸려서 움직이지 못하게 되거나 달리는중에 돌이있는 경우 걸려서 멈칫하게 된다. 
//
//
//
// 트랜스폼을 이용한 경우
// 주변 물체를 무시하고 움직이게 된다.
// 통과하면 안되는 문같은 경우 무시하고 통과하게 된다.
//
// <Character Controller(캐릭터컨트롤러)>
// 1인칭 또는 3인칭 게임의 캐릭터는 바닥을 뚫고 추락하거나 벽을 통과하지 않도록 충돌에 기반한 물리적 행동이 필요합니다.
//  그러나 보통 캐릭터의 가속도와 행동이 물리적으로 현실성을 띠지 않기 때문에 캐릭터가 운동량에 관계 없이 가속,
//  감속하며 거의 즉시 방향을 바꿀 수 있습니다.
// 3D 물리에서 캐릭터 컨트롤러(Character Controller) 를 사용해 이런 타입의 동작을 만들어낼 수 있습니다.
//  이 컴포넌트로 캐릭터에게 단순한 형태와 항상 똑바로 서 있는 형태의 캡슐 형태의 콜라이더를 부여합니다. 
//  컨트롤러는 특별한 기능을 통해 오브젝트의 속도 및 방향을 설정합니다.
//  그러나 진짜 콜라이더와는 다르게 리지드바디를 필요로 하지 않으며 운동량 효과도 현실적이지 않습니다.
// 캐릭터 컨트롤러는 씬 안에서 캐릭터가 정적 콜라이더를 투과하도록 할 수 없으며 이를 통해 바닥을 딛고 벽에 가로막히도록 합니다.
//  움직이는 동안 리지드바디 오브젝트를 옆으로 밀어낼 수는 있지만 다가오는 콜라이더에 밀려 가속되지는 않습니다.
//  이 말은 표준 3D 콜라이더를 사용하는 컨트롤러를 통해 씬 안에서 캐릭터가 걸어다니지만 캐릭터 자체는 현실적인 물리적 동작에 제한되지 않는다는 뜻입니다.
// FPS나 TPS같은 슈팅게임을 만들때 게임 오브젝트를 위치를 움직이는 컴포넌트
// 물리적으로 힘을줘서 움직이는 방식이 아님
// 어느방향으로 움직인다만 설정해주는 방식
// 트랜스폼처럼 무조건 그방향으로 움직이는 컨셉이 아닌 주변 환경을 고려해서 움직이는 컴포넌트
// 주변 상황에 의해 갈 수 있는 지형과 갈 수 없는 지형을 설정해줄 수 있는 컴토넌트
// 계단이나 오를 수 있는 언덕들을 체크해서 올라갈 수 있는지 없는지 체크해서 진행한다.
// 캐릭터컨트롤러에서 중력을 받기위해 리지드바지를 붙이는건 좋지 않다. 리지드 바디를 붙이는 순간 힘을 받는 강체가 적용되기 때문이다. 케릭터 컨트롤러를 쓰는 이유가 외부의 힘을 받는게 아닌 주변의 상황에서 갈 수 있는지 없는지를 체크하기 위해서다
// 리지드바디를 쓴다면 캐릭터 컨트롤러의 의미가 사라진다
// Rigidbody 물리를 활용하지 않는 3인 또는 1인 플레이어에 주로 사용됩니다.

// Slope Limit       : 이 수치만큼의 각도를 오를 수 있다. 콜라이더가 명시된 값보다 작은 경사(단위:도)의 슬로프만 오르도록 제한합니다.
// Step Offset       : 이 수치만큼의 위치면 오를 수 있다. 명시된 값보다 계단이 땅에 가까울 경우에만 캐릭터가 계단을 오릅니다. 이 값은 캐릭터 컨트롤러의 높이보다 커서는 안됩니다. 값이 더 클 경우 오류가 발생합니다.
// Skin width        : 캐릭터 컨트롤러의 피부두께 0으로 설정하면 피부가없어서 충돌했는지 알 수 없게 됨. 두 콜라이더가 서로 스킨 너비 만큼 관통할 수 있습니다. 스킨 너비가 클수록 지터링이 감소합니다. 스킨 너비가 작을 경우에는 캐릭터가 움직이지 못할 수 있습니다. 스킨 너비 값을 반지름의 10%로 설정하는 것이 좋습니다.
// Min Move Distance : 캐릭터가 지정한 값보다 낮게 움직이려고 할 경우 아예 움직이지 않게 됩니다. 지터링을 줄이기 위해 이 옵션을 사용할 수 있습니다. 대부분의 경우 이 값은 0으로 두어야 합니다.
// Center            : 월드 공간에서 캡슐 콜라이더를 오프셋하며, 캐릭터의 피벗에는 영향을 주지 않습니다.
// Radius            : 캡슐 콜라이더의 반지름 길이입니다. 본질적으로는 콜라이더의 너비입니다.
// Height            : 캐릭터의 Capsule Collider 높이입니다. 이 값을 변경하면 콜라이더가 Y축을 따라 양의 방향과 음의 방향으로 스케일합니다.
//
// 트랜스폼은 트랜슬레이트롤 통해 이동
// 리지드바지는 벨로시티나 에드포스로 이동
// 캐릭터 컨트롤러는 Move라는 함수로 이동
//
// 조이스틱을 이용해 만들때 Input에서 방향 Stick [Gamepad]로 설정해줄 수 있다.
//
// 캐릭터 컨트롤러에 isGrounded를 쓰면 문제가 많아서 isGrounded대신 그라운드체크를 하기 위해 충돌체 크리거를 발아래에 트리거방식으로 두거나 레이캐스트를 통해 바닥방향으로쏴서 근처에있으면 바닥에있다는 식을 ㅗ해주는 것도 좋다
// 2D와 3D는 Raycast이 다르다
// 3D가 2D보다 조금 더 다름
// 2D에서는 Physics2D에 레이캐스트를 쐇을 때 RaycastHit2D를 쓴다.
// 2D에서는 RaycastHit2D가 반환으로 정보를 알려주지만 3D에서는 RaycastHitr가 출력전용 파라메터로 구성됨
// 3D에서는 Raycast의 결과가 Bool이다.
// 2D에서는 레이캐스트 힛의 충돌체가
//
// 3D캐릭터를 만들때는 반드시 기준위치를 발밑에 둬야 계산하기가 편하다.
// 발밑이 아닌 중간 위를 기준으로 잡으면 더 큰 캐릭터가 범위공격시 분명 닺는 위치인데 위치가 짧아 안맞는 경우가 발생한다.
// 기준을 발밑에 두려면 포지션에서 y를 0으로 두고 캐릭터 컨트롤러에서 센터의 위치 y를 1로 둔다.
// 보여지는 형태와 연산의 위치가 다르면 하위자식으로 캡슐을 넣어주고 포지션의 y를 1로 변경한다.
// 상위인 부모에게선 보여지는 capsule 와 meshrenderer을 지워준다
// 캐릭터 컨트롤러가 달려있으면 충돌체인 콜라이더와 무게를 주는 리지드 바디는 필요없다.
//
// SphereCast같은 경우 드로우레이를 지원하지 않기 때문에 기즈모를 이용해줘야함
// RayCast 
// <카메라>
// 1인칭 슈팅게임의 화면을 만드는 가장 쉬운방법은 카메라 자체를 플레이어에 달아둔 뒤 위치를 플레이어한테 맞춘뒤 눈높이로 올려둔다.
// 인풋 액션에서 Delta는 가만히 있다가 조금이라도 움직이면 그만큼의 값이 나옴.// 그만큼의 변화량
// 카메라는 업데이트가 아닌 레이트 업데이트에 둬야함 
// 업데이트에 둘다 구현할 시 잘못하면 플레이어가 360도로 무한으로 돌게 됨
// 레이트 업데이트에다 구현해서 후처리를 하게 해야함
//
//
// 마우스의 Delta와 포인트의 Delta 차이
// 마우스 : 기기의 마우스만 적용됨
// 포인트 : 가리키는 모든게 적용이 됨
// 시네머신을 사용하고 있으면 화면 움직임을 더 쉽게 구현함
// 버추얼카메라에서 플레이어와 똑같은 시점 위치를 맞춘 뒤 Aim에서 POV(Point Of View)를 넣어주면 마우스위치에 따라 카메라도 같이 움직이는 쉬운 방법이 있다.(시네머신을 사용하고 있는 경우만 가능)
//
// <인터넷 정보검색 연산자>
// AND  : 두 단어가 모두 포함
// OR   : 두개 중 하나만 포함
// NOT  : 해당 단어를 포함하지 않은 내용
// NEAR : 해당 단어와 인접해 있는 내용 

// <커서 잠금>
// Cursor.lockState = CursorLockMode.None;     : 게임과 상관없이 평소처럼 게임밖으로 나갈수도 있음. ESC를 누르면 마우스가 풀림
// Cursor.lockState = CursorLockMode.Locked;   : 게임 정 가운데에다 잡아둠    ex. FPS
// Cursor.lockState = CursorLockMode.Confined; : 테두리 밖으로 벗어날 수 없음 ex. 롤
//
// <1인칭 시점의 이동>
// 1인칭 시점의 카메라는 좌 우 같은 경우에는 실제로 플레이어를 좌 우로 회전시키고, 위 아래 같은 경우에는 카메라만 회전시켜서 시점을 구현함
// 플레이어의 움직임을 할 때 좌 우로 회전시켜서 플레이어를 그 위치에서 회전시켰으니 플레이어가 바라보고 있는 방향에서 앞방향, 오른쪽 방향에서의 좌 우움직임으로 구현할 수 있다.
//
// <3인칭 시점>
// 버추얼카메라를 만들어주고 Follow에는 해당 플레이어를 넣어준 뒤 Body에는 3rd PersonFollow를 설정해준다
// 버추얼 카메라의 빨간 공을 오른쪽 어깨위로 맞춰주면 되는데 이건 취향껏 맞추면 된다.
// 1인칭처럼 똑같이 할경우 좌우 회전은 되는데 상하회전이 안돼는 경우 플레이어에 하위자식으로 빈 오브젝트를 만들고 버추얼카메라의 팔로우를 방금만든 오브젝트에 넣은다음 플레이어 무브에서 카메라루트에 만들었던 빈 오브젝트를 넣으면 된다.
// 벽뒤에 있을 시 벽 안쪽을 보는 상태가 될수도 있고 벽에 가려 캐릭터가 안보이는 상태가 될수도있다.
// 보통의 경우 해결법은 벽 자체를 살짝 투명처리하는 방법이 있다.
// ㄴ 플레이어를 기준으로 카메라쪽을 레이캐스트한다음 부딪힌 물체가 있으면 투명화처리한다.(어려움)
// 캐릭터가 멀리 있어서 중간에 물체가 끼어드는경우 벽앞에서 보게하는 카메라 당기기가 있다.(쉬움,시네머신에도 있는기능)
// <Add Extension>
// 시네머신 콜라이더를 달아줬을때도 가능하다
//
//
// <3rd Person Follow>
// <Obstacles> : 3rd Person Follow에 기본적으로 제공되는 옵션으로 충돌시 캄라를 당길수도있다.
// 
// 
// 
// 
// 
// 
// 
//
// <슈팅게임>
// 카메라가 움직인 즉시 움직이는게 좋다. 카메라 바디설정에 댐핑을 0으로 해서 즉시 움직이게 해주는게 좋다.
//
//
// <모델파일포멧>
// .fdx
// .dae(Collada)
// .dxf
// .obj
// 이 외의 포맷은 유니티에서 지원을 안하다보니 적절하지 않는 툴이다.
//
// <모델창> : 이 분은 건들지 말것
// Scale Factor  : 모델의 크기 조정
// Convert Units : 유니티 표준 단위 사용 여부
// Bake Axis Conversion
// Import BlendShapes
// Import Visibility
// Import Cameras
// Import Lights
// Preserve Hierarchy
// Sort Hierarchy By Name
// 
// <Rig>
// 애니메이션 타입
// none : 애니메이션이 없음
// 제네릭 : 뼈대에 구성이 없는경우
// 레거시 : 유니티 5.0이전에 쓰던 예전방식타입이라 건드리지 않아도 됨
// 휴머노이드 : 인간형 관절구조를 공유할시 어떤 애니메이션이든 적용 가능
// 
// 이름이 통일되어있지 않을시에난 아바타를 만들어서 아바타에다가 유니티에서 지정해준 관절에 직접 할당을 시킬수 있다.
// T포즈 모델이면 애니메이션과 모델만 있으면 된다.
// 카툰풍으로 캐릭터를 바꿔줘야할때 카툰풍의 자식으로다가 스킨매쉬랜더러 컴포넌트가 있는것들을 하위자식을 ㅗ붙여주면 된다 관절구조가 있는것들도 똑같이 복사해서 하위자식으로 넣어준다. 스킨매쉬랜더러들과 관절구조가 있는 개체들이 같이 있게 된다. 
// 
// 
// 
// 
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//

//
//
//
//



