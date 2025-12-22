using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

public class GameManager : MonoBehaviour
{
    public string playablePath;
    
    public string weaponPath;
    public string activePath;
    public string oneTimePath;
    public List<string> passivePath = new List<string>();

    

    void Awake()
    {
        
    }

    private void Start()
    {
    }


    public void SetPlayablePath(GameObject player)
    {
        playablePath = player.GetComponent<AddressableTag>().AddressKey;
    }
    public string GetPlayablePath()
    {
        return playablePath;
    }
    public void SetWeaponPath(GameObject weapon) 
    {
        weaponPath= weapon.GetComponent<AddressableTag>().AddressKey;
    }
    public string GetWeaponPath()
    {
        return weaponPath;
    }

    public void SetActivePath(GameObject active)
    {
        activePath = active.GetComponent<AddressableTag>().AddressKey;
    }
    public string GetActivePath()
    {
        return activePath;
    }

    public void SetoneTimePath(GameObject oneTime)
    {
        oneTimePath = oneTime.GetComponent<AddressableTag>().AddressKey;
    }
    public string GetoneTImePath()
    {
        return oneTimePath;
    }
    public void RemoveOneTimePath()
    {
        oneTimePath = null;
    }

    public void SetPassivePath(GameObject passive)
    {
            passivePath.Add(passive.GetComponent<AddressableTag>().AddressKey);        
    }
    public List<string> GetPassivePath()
    {
        return passivePath;
    }
}
