using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public Material NormalMirror;
    public Material HorrorMirror;

    public void MirrorChangeToHorror()
    {
        Renderer rend = this.GetComponent<Renderer>();
        rend.material = HorrorMirror;
    }

    public void MirrorChangeToNormal()
    {
        Renderer rend = this.GetComponent<Renderer>();
        rend.material = NormalMirror;
    }
}
