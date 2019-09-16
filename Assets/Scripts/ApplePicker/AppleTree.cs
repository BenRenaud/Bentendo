using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour{
    [Header("Set in Inspector")]
    // Prefab for instantiating apples
    public GameObject applePrefab;

    //speed at which the appleTree moves
    public float speed = 10f;

    //distance where appleTree turns around
    public float leftAndRightEdge = 15f;

    //chance that the appleTree will change directions
    public float chanceToChangeDirections = 0.1f;

    //rate at which Apples will be instantiated
    public float secondsBetweenAppleDrops = 1f;

    void Start () {
        // dropping apples every second
        Invoke("DropApple", 2f);
    }

    void DropApple(){
        GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position;
        Invoke("DropApple", secondsBetweenAppleDrops);
    }
    

    void Update (){
        // basic movement
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;
        //change direction
        if (pos.x < -leftAndRightEdge){
            speed = Mathf.Abs(speed);
        }
        else if (pos.x > leftAndRightEdge) {
            speed = -Mathf.Abs(speed);
        }
    }

    void FixedUpdate(){
        // changing direction randomly is now time based because of fixed update
        if (Random.value < chanceToChangeDirections) {
            speed *= -1;
        }
    }
}