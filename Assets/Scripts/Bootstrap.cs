using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private UiController _uiController;
    [SerializeField] private Player _player;
    [SerializeField] private MazeSpawner _mazeSpawner;
    [SerializeField] private SmallCubesCreator _smallCubesCreator;
    [SerializeField] private FinishSetup _finishSetup;
    [SerializeField] private NavMeshRebaker _navMeshRebaker;
    [SerializeField] private PlayerView _playerView;

    private void Awake()
    {
        _navMeshRebaker.Init(_mazeSpawner);
        _uiController.Init();
        _smallCubesCreator.Init(_player);
        _player.Init(_mazeSpawner);
        _finishSetup.Init(_mazeSpawner);
        _playerView.Init();
        _mazeSpawner.Init();
    }
}