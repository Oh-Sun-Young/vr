using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
/*
 * 참고 사항
 * - UnityEngine.Input 오류 수신 : https://www.reddit.com/r/Unity3D/comments/lt7gng/recieving_error_for_unityengineinput/
 * - [Unity] 오브젝트의 회전에 대하여(Rotation, Quaternion, Euler) : https://killu.tistory.com/12
 * - 유니티에서 오일러각을 사용할때 주의할점(transform.eulerangles) : https://learnandcreate.tistory.com/10
 */
public class CameraRotation : MonoBehaviour
{
    public FixedJoystick joystick;

    private float x;
    private float y;
    private float z;
    private float speed = 30f;
    private Quaternion destination;

    public float eulerAnglesX;
    float destinationX;

    private void Update()
    {
        x = joystick.Horizontal;
        y = joystick.Vertical;
        z = 0;

        if (x != 0 || y != 0)
        {
            eulerAnglesX = 180 < transform.eulerAngles.x ? transform.eulerAngles.x : 360 - transform.eulerAngles.x;

            if (
                (transform.eulerAngles.x < 30 || 180 < transform.eulerAngles.x && 360 - transform.eulerAngles.x < 30)
                || (30 < transform.eulerAngles.x && transform.eulerAngles.x < 180 && 0 < y)
                || (180 < transform.eulerAngles.x && 30 < (360 - transform.eulerAngles.x) && y < 0)
               )
            {
                destinationX = ChangeRotation(-y);
            }
            else
            {
                destinationX = 0;
            }
            transform.Rotate(destinationX, ChangeRotation(x), z);
            destination = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, z);
            transform.rotation = destination;
        }
    }

    private float ChangeRotation(float num)
    {
        return num * speed * Time.deltaTime;
    }
}