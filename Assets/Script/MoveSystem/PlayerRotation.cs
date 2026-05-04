using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public float angleDelta = 45f;
    public float rotateTime = 1f;
    private bool isRotating = false;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDisable()
    {
        EventCenter.Instance.RemoveAll(this);
    }
    public void Init()
    {
        EventCenter.Instance.Add(this, "PlayerRightRotation", RotationRight);
        EventCenter.Instance.Add(this, "PlayerLeftRotation", RotationLeft);
    }
    private void RotationRight()
    {
        if (angleDelta < 0)
            angleDelta = -angleDelta;
        if (!isRotating)
            StartCoroutine(RotateCoroutine());
    }
    private void RotationLeft()
    {
        if (angleDelta > 0)
            angleDelta = -angleDelta;
        if (!isRotating)
            StartCoroutine(RotateCoroutine());
    }

    private IEnumerator RotateCoroutine()
    {
        isRotating = true;

        float startY = transform.eulerAngles.y;
        float targetY = startY + angleDelta;
        float timeElapsed = 0f;

        while (timeElapsed < rotateTime)
        {
            timeElapsed += Time.deltaTime;
            float t = timeElapsed / rotateTime;

            float currentY = Mathf.LerpAngle(startY, targetY, t);
            transform.rotation = Quaternion.Euler(0, currentY, 0);

            yield return null;
        }

        // 确保最后精准到位
        transform.rotation = Quaternion.Euler(0, targetY, 0);

        isRotating = false;
    }

    // 判断是否正在旋转
    public bool IsRotating() => isRotating;
}
