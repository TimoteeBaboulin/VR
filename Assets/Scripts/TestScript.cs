using System;
using UnityEngine;

public class TestScript : MonoBehaviour{
    public Personne Jean;
    public Arme Lit;

    private WeaponParameters GetWeaponParams(Arme arme){
        switch (arme){
            case Arme.FAP:
                return new WeaponParameters(){ IsBoomBoom = false, IsPan = true, IsRatatatatatata = false };
            case Arme.Fusil:
                return new WeaponParameters(){ IsBoomBoom = false, IsPan = false, IsRatatatatatata = true };
            case Arme.LanceTaMere:
                return new WeaponParameters(){ IsBoomBoom = true, IsPan = false, IsRatatatatatata = false };
        }

        return new WeaponParameters();
    }
}

[Serializable]
public struct Personne{
    public string Name;
    public int Age;
    public bool IsDead;
}

public enum Arme{
    FAP,
    Fusil,
    LanceTaMere
}

public struct WeaponParameters{
     public bool IsBoomBoom;
     public bool IsPan;
     public bool IsRatatatatatata;
}

// public class WeaponParameters : ScriptableObject{
//     public bool IsBoomBoom;
//     public bool IsPan;
//     public bool IsRatatatatatata;
// }