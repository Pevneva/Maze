using System;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Player))]
public class PlayerView : MonoBehaviour
{
    [SerializeField] private Material _protectedMaterial;
    [SerializeField] private GameObject _smallCubesPlayer;

    private MeshRenderer _mesh;
    private Material _startMaterial;

    public void Init()
    {
        _mesh = GetComponent<MeshRenderer>();
        _startMaterial = _mesh.material;
    }

    public void SetBigCubeEnabling(bool isEnabled)
    {
        _mesh.enabled = isEnabled;
    }

    public void SetSmallCubesEnabling(bool isEnabled)
    {
        _smallCubesPlayer.SetActive(isEnabled);
    }

    public void SetProtectedColor()
    {
        _mesh.material = _protectedMaterial;
    }

    public void SetUsualColor()
    {
        _mesh.material = _startMaterial;
    }
}
