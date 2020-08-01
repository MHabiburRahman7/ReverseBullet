using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed, bulletSpeed;
    public int bullet_num;

    private float input_x;
    private float input_y;

    //see to the mouse
    //values that will be set in the Inspector
    public Transform Target, AimSprite;
    public float RotationSpeed;
    public AimCtrl m_aimCtrl;
    public GameObject bulletPrefab;

    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        playerMove();
        playerFace();
        AimFace();

        if (Input.GetMouseButtonDown(0))
        {
            fireBullet();
        }
    }

    void playerMove()
    {
        input_x = Input.GetAxisRaw("Horizontal");
        input_y = Input.GetAxisRaw("Vertical");

        //next up, animations
        //anim.SetBool("isWalking", isWalk);
        //if (isWalk && isMovable)
        //{
        //    anim.SetFloat("x", input_x);
        //    anim.SetFloat("y", input_y);

            transform.position += new Vector3(input_x * speed, input_y * speed, 0) * Time.deltaTime;
        //}
    }

    void playerFace()
    {

        // find the vector pointing from our position to the target
        _direction = (m_aimCtrl.worldPosition - transform.position).normalized;

        //create the rotation we need to be in to look at the target
        _lookRotation = Quaternion.LookRotation(_direction);

        //rotate us over time according to speed until we are in the required rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
    }

    void AimFace()
    {
        AimSprite.transform.position = m_aimCtrl.worldPosition;
    }

    void fireBullet()
    {
        Vector3 direction = m_aimCtrl.worldPosition;
        GameObject projectile = (GameObject)Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.identity);
        Rigidbody bullet_rb = projectile.GetComponent<Rigidbody>();
        bullet_rb.velocity = direction * speed;
    }
}
