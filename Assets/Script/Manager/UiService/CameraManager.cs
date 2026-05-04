using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;
    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this);
    }
    public GameObject tank;
    public GameObject ui_camera;

    public Transform target;

    // 相机距离坦克的距离
    public float distance = 8f;

    // 相机高度
    public float height = 3f;

    //小地图高度
    public float smallheight = 18f;

    // 旋转灵敏度
    public float mouseSensitivity = 2f;

    // 相机平滑度（越大越硬，越小越软）
    public float smoothSpeed = 8f;

    // 上下俯仰限制
    public float minAngle = -15f;
    public float maxAngle = 50f;
    public float maxleftAngle = -60f;
    public float maxrightAngle = 60f;

    // 内部记录旋转角度
    private float yaw;   // 左右
    private float pitch; // 上下

    public void LookAtPlayer()
    {
        //小地图
        Vector3 pos_uiheigh = new Vector3(0, smallheight, 0);
        Vector3 pos_tank = tank.transform.position + Vector3.up;
        ui_camera.transform.position = pos_uiheigh + target.transform.position;

        if (target == null) return;

        // 1. 鼠标控制视角
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minAngle, maxAngle);
        yaw = Mathf.Clamp(yaw, maxleftAngle, maxrightAngle);

        // 2. 计算相机要去的位置
        float tank_euler = tank.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(pitch, yaw + tank_euler, 0);
        Vector3 targetPos = target.position - rotation * Vector3.forward * distance + Vector3.up * height;

        // 3. 平滑移动过去
        transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);

        // 4. 相机看向坦克
        transform.LookAt(target.position + Vector3.up * 1.5f - Vector3.forward * 1.5f);
    }
}
