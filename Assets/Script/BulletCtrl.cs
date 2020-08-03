using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public Vector3 target;
    public GameObject player;
    public float init_speed;
    private Rigidbody _rb;
    public bool isPulled, isClose;
    public int Damage;
    // Start is called before the first frame update
    void Awake()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        Invoke("LaunchBullet", 0.2f);
    }

    void LaunchBullet()
    {
        _rb.velocity = target * init_speed;
    }

    public void SetupBullet(Vector3 target_pos, float speed, GameObject p)
    {
        target = target_pos;
        init_speed = speed;
        player = p;

        isPulled = false;
        isClose = false;
    }

    public void pullTheBullet()
    {
        isPulled = true;
        _rb.velocity = new Vector3(0f,0f,0f);
    }

    void moveBackTothePlayer()
    {//WRONG
    }

    // Update is called once per frame
    void Update()
    {
        if (isPulled)
        {
            moveBackTothePlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyAi>().GetDamaged(1);
        }
    }
}
