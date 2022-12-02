using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(Player))]
public class PlayerView : MonoBehaviour
{
    [SerializeField] private ScreenUiPanel _screenUiPanel;
    [SerializeField] private Material _protectedMaterial;
    [SerializeField] private GameObject _smallCubesPlayer;

    private MeshRenderer _mesh;
    private Material _startMaterial;
    
    private void OnEnable()
    {
        _mesh = GetComponent<MeshRenderer>();
        _startMaterial = _mesh.material;
        _screenUiPanel.ShieldButtonPressed += OnShieldButtonPressed;
        _screenUiPanel.ShieldButtonUnpressed += OnShieldButtonUnressed;
    }

    public void SetBigCubeEnabling(bool isEnabled)
    {
        _mesh.enabled = isEnabled;
    }

    public void SetSmallCubesEnabling(bool isEnabled)
    {
        _smallCubesPlayer.SetActive(isEnabled);
    }

    private void OnShieldButtonPressed()
    {
        _mesh.material = _protectedMaterial;
    }

    private void OnShieldButtonUnressed()
    {
        _mesh.material = _startMaterial;
    }
}
