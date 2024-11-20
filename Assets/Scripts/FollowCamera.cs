using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 摄像机控制 （没用上）
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
            // 计算目标位置
            Vector3 targetPosition = target.position;
            targetPosition.y = height;
            // 限制XZ坐标
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
            targetPosition.z = Mathf.Clamp(targetPosition.z, minZ, maxZ);
            // 平滑移动摄像机到目标位置
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            // 始终保持俯视角度
            transform.LookAt(target);
        }
    }
}
