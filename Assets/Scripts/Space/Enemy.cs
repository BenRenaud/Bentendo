using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f; //the speed in m/s
    public float firerate = 0.3f; // seconds/shot(unused)
    public float health = 10;
    public float showDamageDuration = 0.1f;
    public float powerUpDropChance = 1f;
    
    [Header("Set Dynamically: Enemy")]
    public Color[] orginalColors;
    public Material[] materials;
    public bool showingDamage = false;
    public float damageDoneTime;
    public bool notifiedofDestruction = false;
    protected BoundsCheck bndCheck;
    public int E0Score = 20;

    //this is a Property: A method that acts like a field
    public Vector3 pos{
        get{
            return(this.transform.position);
        }
        set{
            this.transform.position = value;
        }
    }

    void Awake(){
        bndCheck = GetComponent<BoundsCheck>();
        materials = Utils.GetAllMaterials(gameObject);
        orginalColors = new Color[materials.Length];
        for(int i = 0; i < materials.Length; i++){
            if(gameObject.name.Equals("Enemy_0(Clone)"))
            {
                if(Main.E0color.Equals("blue"))
                {
                    orginalColors[i] = Color.blue;
                }

                else if(Main.E0color.Equals("yellow"))
                {
                    orginalColors[i] = Color.yellow;
                }

                else if(Main.E0color.Equals("green"))
                {
                    orginalColors[i] = Color.green;
                }

                else
                {
                    orginalColors[i] = Color.white;
                }
            }
            
            else if(gameObject.name.Equals("Enemy_1(Clone)"))
            {
                if(Main.E1color.Equals("blue"))
                {
                    orginalColors[i] = Color.blue;
                }

                else if(Main.E1color.Equals("yellow"))
                {
                    orginalColors[i] = Color.yellow;
                }

                else if(Main.E1color.Equals("green"))
                {
                    orginalColors[i] = Color.green;
                }

                else
                {
                    orginalColors[i] = Color.white;
                }
            }

            else if(gameObject.name.Equals("Enemy_2(Clone)"))
            {
                if(Main.E2color.Equals("blue"))
                {
                    orginalColors[i] = Color.blue;
                }

                else if(Main.E2color.Equals("yellow"))
                {
                    orginalColors[i] = Color.yellow;
                }

                else if(Main.E2color.Equals("green"))
                {
                    orginalColors[i] = Color.green;
                }

                else
                {
                    orginalColors[i] = Color.white;
                }
            }

            else if(gameObject.name.Equals("Enemy_3(Clone)"))
            {
                if(Main.E3color.Equals("blue"))
                {
                    orginalColors[i] = Color.blue;
                }

                else if(Main.E3color.Equals("yellow"))
                {
                    orginalColors[i] = Color.yellow;
                }

                else if(Main.E3color.Equals("green"))
                {
                    orginalColors[i] = Color.green;
                }

                else
                {
                    orginalColors[i] = Color.white;
                }
            }

            else
            {
                if(Main.E4color.Equals("blue"))
                {
                    orginalColors[i] = Color.blue;
                }

                else if(Main.E4color.Equals("yellow"))
                {
                    orginalColors[i] = Color.yellow;
                }

                else if(Main.E4color.Equals("green"))
                {
                    orginalColors[i] = Color.green;
                }

                else
                {
                    orginalColors[i] = Color.white;
                }
            }

            }
        }

    void Update()
    {
        Move();
        if(showingDamage && Time.time > damageDoneTime){
            UnShowDamage();
        }
        if(bndCheck != null && bndCheck.offDown) {
            Main.NumofEonScreen--;
            Destroy(gameObject);
        }
    }

    public virtual void Move(){
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
        
    }
    
    void OnCollisionEnter(Collision coll){
        GameObject otherGO = coll.gameObject;
        switch(otherGO.tag){
            case "ProjectilePlayer":
            Projectile p = otherGO.GetComponent<Projectile>();
            
            if(!bndCheck.isOnScreen){
                Destroy(otherGO);
                break;
            }
            
            ShowDamage();
            
            health -= Main.GetWeaponDefinition(p.type).damageOnHit;
            if(health <= 0){
                
                if(!notifiedofDestruction){
                    Main.S.shipDestroyed(this);
                }
                notifiedofDestruction = true;
                
                
                Destroy(this.gameObject);
                
            }
            Destroy(otherGO);
            break;

            default:
            print("Enemy hit by non-ProjectilePlayer: " + otherGO.name);
            break;
        }
    }

    void ShowDamage(){
        foreach(Material m in materials){
            m.color = Color.red;
        }
        showingDamage = true;
        damageDoneTime = Time.time + showDamageDuration;
    }

    void UnShowDamage(){
        for(int i = 0; i < materials.Length; i++){
            materials[i].color = orginalColors[i];
        }
        showingDamage = false;
    }
}
