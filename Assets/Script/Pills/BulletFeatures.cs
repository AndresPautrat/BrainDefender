using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFeatures : MonoBehaviour
{
    float atackBuff = 1;
    bool knockBackBullet = false;
    bool poison = false;
    [SerializeField]
    int DmgEnemy1;
    [SerializeField]
    int DmgEnemy2;
    [SerializeField]
    int DmgEnemy3;
    [SerializeField]
    int DmgEnemy4;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setFeatures(float _atackBuff, bool _knockBackBullet, bool _poison)
    {
        atackBuff = 1.5f;
        knockBackBullet = _knockBackBullet;
        poison = _poison;
    }

    public int getDmg(string EnemyID)
    {
        switch (EnemyID)
        {
            case "Enemy1(Clone)":
                return (int)(DmgEnemy1 * atackBuff);
            case "Enemy2(Clone)":
                return (int)(DmgEnemy2 * atackBuff);
            case "Enemy3(Clone)":
                return (int)(DmgEnemy3 * atackBuff);
            case "Enemy4(Clone)":
                return (int)(DmgEnemy4 * atackBuff);
        }
        return 0;
    }
    public bool getKnockBack()
    {
        return knockBackBullet;
    }
    public bool getPoison()
    {
        return poison;
    }
}
