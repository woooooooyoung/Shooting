using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] float moveSpeed;                // �ӷ�(moveSpeed)
    [SerializeField] float jumpSpeed;

    private CharacterController characterController; // ĳ������Ʈ�ѷ� ������Ʈ�� ����ϱ� ����
    private Vector3 moveDir;                         // �����̴� ���� �ޱ�
    private float ySpeed = 0;                        // y���� �������� ���� �ӵ�

    private void Awake()
    {
        characterController = GetComponent<CharacterController>(); // ĳ���� ��Ʈ�ѷ� ������Ʈ ��������
    }

    private void Update()
    {
        Move(); // ������Ʈ�ϴµ��� ���������� Move ����
        Jump();
    }

    private void Move()
    {
        // ĳ���� ��Ʈ�ѷ��� �ƴ� transform���� �������� ���
        // transform.Translate(moveDir * moveSpeed * Time.deltaTime);   // �������� �̵��̱� ������ �ֺ�ȯ��(������Ʈ)�� ������� ��� ����� ����� �����ٴ� �ൿ�� �ȵ�

        // ĳ������Ʈ�ѷ��� �������� ���
        characterController.Move(moveDir * moveSpeed * Time.deltaTime); // moveDir �������� �����̱� 
                                                                        // �ӷ�(moveSpeed)�� �־����� ������ ���İ��� �̵���
                                                                        // �����ð�(deltaTime)�� �����ֱ�
                                                                        // �ӷ� ����� = ���ϴ¼�ġ(moveSpeed) * �����ð�(Time.deltaTime)
                                                                        // Slope Limit       : �� ��ġ��ŭ�� ������ ���� �� �ִ�.
                                                                        // Step Offset       : �� ��ġ��ŭ�� ��ġ�� ���� �� �ִ�.

        

    }
    private void OnMove(InputValue value)
    {
        // �Է� �޾����� moveDir��
        Vector2 input = value.Get<Vector2>();       // input���� ��� value�� Get�� ���� Vector2���� �޾��ش�
        moveDir = new Vector3(input.x, 0, input.y); // ���� ���� x���� x������ y�ప�� 0 ���� ���� y���� z�࿡ �Ҵ�
    }
    private void Jump()
    {
        // �������� ����(�ö󰬴� ������)�� ������ ����(�ϸ��ϰ� �����ؼ� ���ϰ� ������)�� �ִ�.
        // ������ ����(y��ġ) // �߷�(y)�� ������ ���� ���� // ���� �ӷ¸�ŭ y������ �̵�
        ySpeed += Physics.gravity.y * Time.deltaTime; // �߷¹����� y�������� ����ؼ� �ӷ��� ���� // �ӷ��� �����̱� ������ deltaTime�� ���� // �Ʒ������� ����ؼ� ������ // ������ �ִ� Physics - gravity�� ���� ����

        // if (characterController.isGrounded) ĳ������Ʈ�ѷ����� isGrounded�� ������ �� �۵��� �� �ȵ� ���ݸ� Ʋ������ �ȸ����� ���ݸ� �ε����� ����
        if (GroundCheck()) // ĳ������Ʈ�ѷ��� ���� �پ��ִ� ��� ySpeed�� 0���� ���� // �Ʒ��� �������ٰ� ���� �浹�ؼ� �� ���� �ִ� ��� �ӷ��� 0���� �����ؼ� �� �Ʒ� (y) �� �������� ����
            ySpeed = 0; 

        characterController.Move(Vector3.up * ySpeed * Time.deltaTime); // y��(up����)�� �ش��ϴ� �������� ����ؼ� ��������
    }
    private void OnJump()
    {
        ySpeed = jumpSpeed; // ySpeed�� �����ָ� ������ ��
    }
    private bool GroundCheck()
    {
        RaycastHit hit;
        return Physics.SphereCast(transform.position, 0.5f, Vector3.down, out hit, 0.5f); // ��ȯ�� �� �ε������� true �ƴϸ� false
        // (������ �����, ������� �ѷ�, �������, ��������� ����)
        // ���׶�̸� ������ ���׶�̷� �ε�����.

    }

}
