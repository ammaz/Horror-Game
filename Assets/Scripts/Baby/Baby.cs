using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baby : MonoBehaviour
{
    public Transform Player;
    public static int BabyNumHideSeek;

    void Start()
    {
        BabyNumHideSeek = 0;
    }

    public void BabyTPSit()
    {
        gameObject.transform.position = new Vector3(-7.042f, 0.849f, -7.13f);
    }

    public void BabyTP(float x,float y,float z)
    {
        gameObject.transform.position = new Vector3(x, y, z);
    }

    public void BabyHideAndSeek()
    {
        if (Vector3.Distance(Player.position, this.transform.position) < 4f)
        {
            BabyTP(Random.Range(-8,8),2f,Random.Range(-6,9));
            BabyNumHideSeek++;
            //BabyFindMe Sound
            BabySound.PlaySound("BabyHappy");
        }
    }
}
