using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private UiController _uiController;
    [SerializeField] private Player _player;
    [SerializeField] private MazeSpawner _mazeSpawner;
    [SerializeField] private SmallCubesCreator _smallCubesCreator;
    [SerializeField] private FinishSetup _finishSetup;
    
    private void Awake()
    {
        _uiController.Init();
        _smallCubesCreator.Init(_player);
        _player.Init(_mazeSpawner);
        _finishSetup.Init(_mazeSpawner);
    }
}
