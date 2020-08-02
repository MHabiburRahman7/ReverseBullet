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
    public Transform AimSprite;
    public float RotationSpeed;
    public AimCtrl m_aimCtrl;
    public GameObject bulletPrefab, front;
    public List<GameObject> bulletAway_gameObject;

    //values for internal use
    private Quaternion _lookRotation;
    private Vector3 _direction;
    private bool bulletAway;

    // Start is called before the first frame update
    void Start()
    {
        bulletAway = false;
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
        if (bulletAway && Input.GetKeyDown(KeyCode.Space))
        {
            pullTheBullet();
        }
        if (bulletAway)
        {
            if (bulletAway_gameObject.Count == 0)
                bulletAway = false;
        }
    }

    void pullTheBullet()
    {
        for(int i=0; i<bulletAway_gameObject.Count; i++)
        {
            Debug.Log("Pulling bullet: " + i);
            bulletAway_gameObject[i].GetComponent<BulletCtrl>().pullTheBullet();
            if (bulletAway_gameObject[i].GetComponent<BulletCtrl>().isClose)
            {
                GameObject tempBullet = bulletAway_gameObject[i]; 
                bulletAway_gameObject.RemoveAt(i);
                Destroy(tempBullet);
            }
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
        _direction = (m_aimCtrl.worldPosition - transform.position);
        //_direction = (m_aimCtrl.worldPosition - front.transform.position).normalized;

        //create the rotation we need to be in to look at the target
        //_lookRotation = Quaternion.LookRotation(_direction);

        //_direction = new Vector3(0, 0, _direction.z);
        //transform.LookAt(_direction, transform.forward);
        transform.up = _direction;

        //rotate us over time according to speed until we are in the required rotation
        //transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * RotationSpeed);
        //Quaternion LookAtRotationOnly_Z = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, _lookRotation.eulerAngles.z);

        //Debug.Log("euler angels before: " + _lookRotation.eulerAngles);
        //_lookRotation.eulerAngles = new Vector3(0, 0, _lookRotation.eulerAngles.z);
        //Debug.Log("euler angels: " + _lookRotation.eulerAngles);
        //transform.rotation = _lookRotation;

        //transform.rotation = new Quaternion(0, 0, _lookRotation.z, _lookRotation.w);
        //transform.rotation = Quaternion.Slerp(transform.rotation, LookAtRotationOnly_Z, Time.deltaTime * RotationSpeed);
    }

    void AimFace()
    {
        AimSprite.transform.position = m_aimCtrl.worldPosition;
    }

    void fireBullet()
    {
        Vector3 direction = m_aimCtrl.worldPosition;
        //Vector3 direction = new Vector3(-2.0f, 0, 0);

        //Debug.Log("Velocity: " + direction);
        GameObject projectile = (GameObject)Instantiate(bulletPrefab, front.transform.position, Quaternion.identity);
        bulletAway_gameObject.Add(projectile);
        projectile.GetComponent<BulletCtrl>().SetupBullet(direction, speed, this.gameObject);
        //Rigidbody bullet_rb = projectile.GetComponent<Rigidbody>();
        //bullet_rb.velocity = direction * 1f;
        bulletAway = true;
    }
}
