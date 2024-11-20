using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//坐标类型转换
public class PlayerMove : MonoBehaviour
{
    public float speed = 3.0f;

    public float MaxSpeed = 3;

    public float MinSpeed =2;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        //// up
        //if (Input.GetKey(KeyCode.W))
        //{
        //    rb.velocity = transform.forward * speed;
        //}
        //// down
        //else if (Input.GetKey(KeyCode.S))
        //{
        //    rb.velocity = -transform.forward * speed;
        //}
        //// left
        //else if (Input.GetKey(KeyCode.A))
        //{
        //    rb.velocity = -transform.right * speed;
        //}
        //// right
        //else if (Input.GetKey(KeyCode.D))
        //{
        //    rb.velocity = transform.right * speed;
        //}
        //else
        //{
        //    rb.velocity = Vector3.zero;
        //}
        // 获取输入轴的值
        float moveHorizontal = 0.0f;
        float moveVertical = 0.0f;

        // 检测各个方向的输入
        if (Input.GetKey(KeyCode.W)) // 向上
        {
            moveVertical += 1.0f;
        }
        if (Input.GetKey(KeyCode.S)) // 向下
        {
            moveVertical -= 1.0f;
        }
        if (Input.GetKey(KeyCode.A)) // 向左
        {
            moveHorizontal -= 1.0f;
        }
        if (Input.GetKey(KeyCode.D)) // 向右
        {
            moveHorizontal += 1.0f;
        }

        // 使用输入的方向计算最终的移动向量
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical).normalized * speed;

        // 设置 Rigidbody 的速度
        rb.velocity = movement;
    }

    //
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Enemy")
        {
          EnemyAI enemy =  other.GetComponent<EnemyAI>();
            if (enemy!=null)
            {
                if (enemy.IsStar)
                {
                    GameSys.Ins.GameLoseUI.SetTrue();
                    this.gameObject.SetFalse();
                }
                else
                {
                    enemy.SetActiveF();
                }
               
            }
        }
    }
}
