using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;

    void OnEnable()
    {
        EventCenter.Instance.Add<Vector3, Vector3>(this, "MonsterMove", Move);

    }
    private void OnDisable()
    {
        EventCenter.Instance.RemoveAll(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Move(Vector3 Birpos,Vector3 Endpos)
    {
        if(transform.position != Birpos)
        {
            transform.position = Vector3.MoveTowards(transform.position, Birpos, Time.deltaTime * speed);
            transform.LookAt(Endpos);
            return;
        }
        transform.position = Vector3.MoveTowards(Birpos, Endpos, Time.deltaTime * speed);
    }

}
