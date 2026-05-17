using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int HP;
    public int MP;
    public int Level;
    public int Exp;
    public Transform BirthPos;
    private PlayerFSM fsm;
    public float speed = 10f;
    public float attack_cd = .1f;
    public int AKT = 10;
    public float angleDelta = 45f;
    public float rotateTime = 1f;
    public float rotateSpeed = 90f;
    public Animator animator {  get; private set; }

    public Vector3 DiePos=new Vector3(110,1110,10);

    public void Init()
    {
        animator = GetComponent<Animator>();
        fsm =new PlayerFSM(this);
        transform.position = BirthPos.position;
        EventCenter.Instance.Add<int>(this, "PlayerHurt", OnHurt);
        EventCenter.Instance.Add<int>(this, "PlayerExpUp", ExpUp);

        EventCenter.Instance.OnTriggerEven("HPUpDate", HP);
        EventCenter.Instance.OnTriggerEven("MPUpDate", MP);
        EventCenter.Instance.OnTriggerEven("LevelUpDate", Level);
        animator.Play("Idel");
    }
    private void OnReset()
    {
        HP = 100;
        MP = 50;
        Level = 0;
        Exp = 0;
    }
    private void Update()
    {
        if (fsm == null)
            return;
        fsm.curruntstatu.OnUpdate();

    }
    private void OnDestroy()
    {
        EventCenter.Instance.RemoveAll(this);
    }

    private void OnHurt(int AkT)
    {
        HP -=AkT;
        if (HP < 0) 
        { 
            HP = 0;
            fsm.IsDie = true;
        }
        EventCenter.Instance.OnTriggerEven("HPUpDate", HP);
    }
    private void MPHurt(int value)
    {
        MP -=value;
        if (MP < 0) {MP = 0; }
        EventCenter.Instance.OnTriggerEven("MPUpDate", MP);
    }
    private void LevelUp()
    {
        Level++;
        EventCenter.Instance.OnTriggerEven("LevelUpDate", Level);
    }
    private void ExpUp(int exp)
    {
        Exp += exp;
        if(Exp > 100)
        {
            Exp -= 100;
            LevelUp();
        }
        Debug.Log("EXP:" + Exp);
        EventCenter.Instance.OnTriggerEven("ExpUpDate", Exp);
    }
    public void Die()
    {
        OnReset();
        transform.position = DiePos;
        
    }
    public void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(h, 0, v);
        inputDir.Normalize();
        Rigidbody rb = GetComponent<Rigidbody>();//TODO:迁移到输入服务里
        rb.velocity = transform.rotation * inputDir * speed;
    }
    public void RightRotation()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);

    }
    public void LeftRotation()
    {
        transform.Rotate(0, -rotateSpeed * Time.deltaTime, 0);

    }
    public void Attack()
    {
        GameObject mybullet = BulletPool.instance.GetBullet();
        if (mybullet == null)
        {
            Debug.Log("子弹未设置");
            return;
        }
        StartCoroutine(AttackAni(mybullet));
        fsm.IsAttack = false;
    }
    IEnumerator AttackAni(GameObject mybullet)
    {
        animator.SetBool("IsAttack", true);
        yield return new WaitForSeconds(attack_cd);
        mybullet.transform.position = transform.Find("fort").Find("posshoot").position;
        mybullet.transform.rotation = transform.rotation;
        mybullet.SetActive(true);
        
    }
}
