using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Init()
    {
        EventCenter.Instance.Add(this, "PlayerMove", Move);
    }
    private void Move()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(h, 0, v);
        inputDir.Normalize();
        if (inputDir.magnitude > 0.1f)
        {
            // 移动

            rb.velocity = transform.rotation*inputDir * speed;
        }
        else
        {
            // 没输入时停止
            rb.velocity = Vector3.zero;
        }
    }
}
