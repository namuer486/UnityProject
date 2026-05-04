using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRotation : MonoBehaviour
{
    public float rotateSpeed;

    void OnEnable()
    {
        EventCenter.Instance.Add<Quaternion>(this, "MonsterRotation", RotateSmooth);
    }
    private void OnDisable()
    {
        EventCenter.Instance.RemoveAll(this);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void RotateSmooth(Quaternion EndRatation)
    {

        transform.rotation = Quaternion.Lerp(transform.rotation, EndRatation, rotateSpeed * Time.deltaTime);
        
    }


}
