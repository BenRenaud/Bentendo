using	System.Collections;	
using	System.Collections.Generic;
using	UnityEngine;

public class Enemy_1 : Enemy {
    [Header("Set in Inspector: Enemy_1")]
    public float waveFrequency = 2;
    public float waveWidth = 4;
    public float waveRotY = 45;
    private float x0;
    private float birthTime;
    public float lifeTime = 10;
    public int E1Score = 50;

    void Start(){
        x0 = pos.x;
        birthTime = Time.time;
    }

    public override void Move() {
        Vector3 tempPos = pos;
        float age = Time.time - birthTime;
        float theta = Mathf.PI * 2 * age/waveFrequency;
        float sin = Mathf.Sin(theta);
        tempPos.x = x0 + waveWidth * sin;
        pos = tempPos;

        if((age/lifeTime) > 1){
            Destroy(this.gameObject);
            Main.NumofEonScreen--;
            return;
        }

        Vector3 rot = new Vector3(0, sin*waveRotY, 0);
        this.transform.rotation = Quaternion.Euler(rot);

        base.Move();
    }
}