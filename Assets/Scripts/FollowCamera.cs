using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������� ��û���ϣ�
/// </summary>
public class FollowCamera : MonoBehaviour
{
    public Transform target;
    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
    public float height = 10.0f;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    void LateUpdate()
    {
        if (target != null)
        {
            // ����Ŀ��λ��
            Vector3 targetPosition = target.position;
            targetPosition.y = height;
            // ����XZ����
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
            targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);
            // ƽ���ƶ��������Ŀ��λ��
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            // ʼ�ձ��ָ��ӽǶ�
            transform.LookAt(target);
        }
    }
}
