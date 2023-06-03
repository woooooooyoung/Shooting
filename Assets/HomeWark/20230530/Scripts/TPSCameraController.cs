using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPSCameraController : MonoBehaviour // 3��Ī ī�޶� �����ϱ�
{
    //[SerializeField] Transform lookTarget; // ��񼳸��� ���� ��
    [SerializeField] Transform cameraRoot;
    //[SerializeField] float cameraSensitivityX; // ī�޶� ����
    [SerializeField] float mouseSensitivity;
    [SerializeField] float lookDistance; // ���� �������� �� ����� ������ �ٶ󺸰� �ȴ�.

    private Vector2 lookDelta;
    private float xRotation; // �� �� ȸ����
    private float yRotation; // �� �� ȸ����
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked; // ���ӽ��۽� �߾����� ���콺 ����
    }
    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None; // ESC�� ���콺 Ǯ��
    }
    // ī�޶� �ٶ󺸰� �ִ� ��ġ�� �÷��̾ ���� ���� ���

    private void Update() // �÷��̾ ȸ����Ű�°� ������Ʈ����
    {
        Vector3 lookPoint = Camera.main.transform.position + Camera.main.transform.forward * lookDistance; // �ٶ󺸴� ��ġ ���� // ĳ���Ͱ� �ٶ󺸴���ġ(lookPoint)������ ���� ī�޶󿡼� ����ī�޶��� ��ġ���� ī�޶� �ٶ󺸰� �ִ� �չ������� lookDistance�� ����ŭ ������ ��ġ�� ����. // ���⼭ ������ �� ���� �±װ� ����ī�޶����� Ȯ���Ѵ�.
        lookPoint.y = 0;    // �÷��̾ �ʹ� ����� �ٴ��̳� �ϴ��� ����� ���°�찡 ������� ������ �����ϱ� ���� y�� ��ġ�� 0���� �������ش�. // ī�޶� �ٶ󺸴� ������ y�� 0�̸� ����� �ٶ󺸰Եȴ�. // �����÷��̾��� ��ġ�� 0�̱� ������y��ġ�� 0�ΰ��� �ٶ󺸸� ��ٷ����糪 �鸮�� �ʴ´�. 
        //lookTarget.position = lookPoint; // ������ ���� ������Ʈ�� �Ȱ��� ��ġ�� ����
        transform.LookAt(lookPoint);
    }
    private void LateUpdate()
    {
        Look(); // Look�� ����Ʈ������Ʈ�� ������ ���ϸ� ĳ���Ͱ� ���� ���Ÿ����ִ�
    }
    private void Look()
    {
        yRotation += lookDelta.x * mouseSensitivity * Time.deltaTime;
        xRotation -= lookDelta.y * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        //cameraRoot.localRotation = Quaternion.Euler(xRotation, yRotation, 0); // ���⼱ localRotation���� ���� ��� ĳ���Ͱ� ���� ��鸱���� �ִ�.
        cameraRoot.rotation = Quaternion.Euler(xRotation, yRotation, 0); // ī�޶��Ʈ�� �������� ��ƾ��ϱ� ������ ������ �ƴ� �׳� �����̼��̴�. // cameraRoot�� �� ������  �÷��̾ �ٶ󺻰��� ������� ȸ���ϱ�����
    }
    private void OnLook(InputValue value)
    {
        lookDelta = value.Get<Vector2>(); // x�� ���� ������ �� �Ʒ������� �����̸� y�� ���� �ް� ��
    }




    /* �÷��̾ �����¿�� ȸ����Ű�� ���
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
