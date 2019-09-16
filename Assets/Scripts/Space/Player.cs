using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    static public Player S;

    [Header("Set in Inspector")]
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;
    public Weapon[] weapons;

    [Header("Set Dynamically")]
    [SerializeField]
    private float _shieldLevel = 1;
    private GameObject lastTriggerGo = null;
    public delegate void WeaponFireDelegate();
    public WeaponFireDelegate fireDelegate;
    public AudioSource PewSFX, boom, boomE;
    public static AudioClip Pew, Pew2, Pew3, Boom, Boom2, Boom3;
    public AudioClip[] SpacePewBG = {Pew, Pew2, Pew3};
    public AudioClip[] SpaceBoomBG = {Boom, Boom2, Boom3};
    public static List<GameLog> Spacelogs = new List<GameLog>();

    void Awake()
    {
        int GetPew = PlayerPrefs.GetInt("SpaceBGPew");
        int GetBoom = PlayerPrefs.GetInt("SpaceBGBoom");
        float GetBoomV = PlayerPrefs.GetFloat("SpaceBGBoomV");
        float GetPewV = PlayerPrefs.GetFloat("SpaceBGPewV");
        boomE.volume = GetBoomV;
        boomE.clip = SpaceBoomBG[GetBoom];
        PewSFX.volume = GetPewV;
        PewSFX.clip = SpacePewBG[GetPew];
    }

    void Start() {
        if (S == null){
            S = this;
        }
        ClearWeapons();
        weapons[0].SetType(WeaponType.blaster);

        string Spacepath = Application.streamingAssetsPath + "/SpaceLogs.json";
        string SpacejsonString = File.ReadAllText(Spacepath);
        gameDataShooter Spacedata = JsonUtility.FromJson<gameDataShooter>(SpacejsonString);
        Spacelogs.Clear();
        if (!(Spacedata == null))
        {
            foreach (GameLog log in Spacedata.SpaceGame)
            {
                Spacelogs.Add(log);
            }
        }
    }

    void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;   

        transform.rotation = Quaternion.Euler(yAxis*pitchMult,xAxis*rollMult,0);

        if(Input.GetAxis("Jump") == 1 && fireDelegate != null){
            fireDelegate();
            PewSFX.Play();
        }     
    }
    void TempFire(){
        GameObject projGO = Instantiate<GameObject>(projectilePrefab);
        projGO.transform.position = transform.position;
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        Projectile proj = projGO.GetComponent<Projectile>();
        proj.type = WeaponType.blaster;
        float tSpeed = Main.GetWeaponDefinition(proj.type).velocity;
        rigidB.velocity = Vector3.up * tSpeed;
    }

    void OnTriggerEnter(Collider other){
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        if(go == lastTriggerGo){
            return;
        }
        lastTriggerGo = go;

        if(go.tag == "Enemy"){
            shieldLevel--;
            Main.NumofEonScreen--;
            boomE.Play();
            Destroy(go);
        }
        else if(go.tag == "PowerUp"){
            AbsorbPowerUp(go);
        }

        else if(go.tag == "ProjectileEnemy")
        {
            shieldLevel--;
            Destroy(go);
        }
        else{
            print("Triggered by non-Enemy: " + go.name);
        }
    }

    public void AbsorbPowerUp(GameObject go){
        PowerUp pu = go.GetComponent<PowerUp>();
        switch(pu.type){
            case WeaponType.shield:
            shieldLevel++;
            break;

            default:
            if(pu.type == weapons[0].type){
                Weapon w = GetEmptyWeaponSlot();
                if(w != null){
                    w.SetType(pu.type);
                }
            }
            
            else{
                ClearWeapons();
                weapons[0].SetType(pu.type);
            }
            break;
        }
        pu.AbsorbedBy(this.gameObject);
    }

    public float shieldLevel{

        get{
            return(_shieldLevel);
        }

        set{
            _shieldLevel = Mathf.Min(value, 4);

            if(value < 0){

                if (Users.CurrentUser.username == "admin")
                {
                    Main.Spacelogs.Add(new GameLog("admin", System.DateTime.Now.ToString(), Main.score.ToString(), Main.lvl));
                } 
                
                else
                {
                    Main.Spacelogs.Add(new GameLog(Users.CurrentUser.username, System.DateTime.Now.ToString(), Main.score.ToString(), Main.lvl));
                }
                
                Main.SaveGameData();
                Destroy(this.gameObject);
                boom.Play();
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    Weapon GetEmptyWeaponSlot(){
        for(int i = 0; i < weapons.Length; i++){
            if(weapons[i].type == WeaponType.none){
                return(weapons[i]);
            }
        }
        return(null);
    }

    void ClearWeapons(){
        foreach(Weapon w in weapons){
            w.SetType(WeaponType.none);
        }
    }
}