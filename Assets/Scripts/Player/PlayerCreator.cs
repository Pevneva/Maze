using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreator : MonoBehaviour
{

    private Player Create(Player prefab, PlayerState state, Transform parent)
    {
        return Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
    }
}

enum PlayerState
{
    Alive = 0,
    Died = 1
}
