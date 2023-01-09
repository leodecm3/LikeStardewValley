using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/SOcrops")]
public class SOcrops : SOobject {

    public float timeToGrow;
    public Sprite[] growingSprite;

    public Sprite[] GetGrowingSprite() {
        return growingSprite.Concat(new Sprite[] {sprite}).ToArray();
    }
    

}
