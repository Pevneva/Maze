using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private UiController _uiController;
    
    private void Awake()
    {
        _uiController.Init();
    }
}
