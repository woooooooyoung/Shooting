using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSCameraController : MonoBehaviour // ī�޶��� ������ �����ϱ�
{
    [SerializeField] Transform cameraRoot;   // ī�޶� ��ġ
    [SerializeField] float mouseSensitivity; // ���콺 �ӵ�(���콺 �ΰ���)

    private Vector2 lookDelta;
    private float xRotation;
    private float yRotation;

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ���� ���
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;  // ���콺 Ŀ���� Ǯ���� ESC�� ������ ���콺�� Ǯ��
    }

    private void LateUpdate() // ī�޶�� ������Ʈ�� ������� ��ó���� ������ϱ⶧���� ������Ʈ�� �ƴѾƴ� ����Ʈ ������Ʈ�� ������� // ����Ʈ ������Ʈ�� �ִ°� ��ټ� ī�޶� ������
    {
        Look(); // �� �����Ӹ��� Lookȣ��
    // �÷��̾ ���� �ٶ󺸰� �ִٰ� �밢���� �ٶ󺸰� �ϸ� ī�޶� �ڽİ��ӿ�����Ʈ�̱⶧���� �� ��ġ�� �ٶ󺸰� �ϴ°� ������ // �÷��̾ ���� ȸ���ϰ� ī�޶� ���� ȸ���ϴ°� ������
    // ī�޶� ���� �̵����� �� �÷��̾ ���� ������ �ٶ������� // ī�޶� �̵��ؼ� �÷��̾ �� ������ �ٶ󺸸� �÷��̾ �ٶ�ñ� ������ �����ڽ��� ī�޶� �ٽ� �ٶ󺸰ԵǴ� �������� ȸ���� �Ͼ ���� ����. // �����۵������� �÷��̾�� 360�� ��� ���Ե�
    }

    private void Look() // ĳ���͵� ���� ���������� // �Է¹��� ��ŭ ī������ �Է¹��������� ȸ��
    {
        yRotation += lookDelta.x * mouseSensitivity * Time.deltaTime; // �� ��
        xRotation -= lookDelta.y * mouseSensitivity * Time.deltaTime; // �� �� (����)
                                                                      // xRotation += lookDelta.y * mouseSensitivity * Time.deltaTime; // �� �� ���콺�� ������ ������ ���� �ö� ���콺�� �ø��� ������ �Ʒ��� ������
        
        xRotation = Mathf.Clamp(xRotation, -80f, 80f); // �÷��̾��� ȸ���� ���� ����, ������ �Ȱɸ� ���콺�� �Ʒ��� ��, �¿�� ������ ĳ������ ���� ���� ���·� �ڵ� ���ƺ� �� ����
                                                       // Clamp : �ִ� �ּڰ��� �Ѿ�� �ּڰ��� �ִ밪�� �Ѿ�� �ּڰ��� ��
                                                       // -80f, 80f : �� ���� ������ �Ѿ�� ���̻� �Ѿ �� ���� �Ʒ��� 80�� ���� 80�� ������ ȸ�� ����

        //transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0); // ������ ���⸸ŭ �÷��̾ ���� ȸ����Ŵ
        cameraRoot.localRotation = Quaternion.Euler(xRotation, 0, 0); // �� �Ϸ� ������ �� ī�޶� ȸ��
        transform.localRotation = Quaternion.Euler(0, yRotation, 0);  // ĳ���Ͱ� �� ��� �̵��Ҷ� ĳ������ Ʈ������ ��ü�� ȸ����Ŵ
    }

    private void OnLook(InputValue value)
    {
        lookDelta = value.Get<Vector2>(); // �Ʒ������� �����̸� y�� ���� �ް� ��
    }




}
