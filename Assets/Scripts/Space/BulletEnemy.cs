using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BulletEnemy : MonoBehaviour
{
    public Rigidbody rigid;
    private BoundsCheck bndcheck;
    public float speed = 15f;

    void Start()
    {
        bndcheck = GetComponent<BoundsCheck>();
        rigid.velocity = Vector3.down * speed;
    }

    void Update()
    {
        if(bndcheck.offDown)
        {
            Destroy(gameObject);
        }
    }
}