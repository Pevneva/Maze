using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshBuilder), typeof(PlayerView))]
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _finish;
    [SerializeField] private MazeSpawner _spawner;
    [SerializeField] private ScreenUiPanel _screenUiPanel;
    [SerializeField] private GameObject _winFx;
    [SerializeField] private TrailRenderer _trail;

    private NavMeshAgent _agent;
    private bool _isProtected;
    private bool _isInFinishEntered;
    private bool _isInTrapEntered;
    private PlayerView _playerView;

    public event Action Revived;
    public event Action Setup;
    public event Action Won;

    private void OnEnable()
    {
        _agent = GetComponent<NavMeshAgent>();
        _spawner.MazeSpawned += OnMazeSpawned;
        _playerView = GetComponent<PlayerView>();
        _screenUiPanel.ShieldButtonPressed += OnShieldButtonPressed;
        _screenUiPanel.ShieldButtonUnpressed += OnShieldButtonOnPressed;
    }

    private void OnTriggerStay(Collider other)
    {
        if (_isInTrapEntered == false && other.gameObject.TryGetComponent(out Trap trap))
        {
            _isInTrapEntered = true;
            if (_isProtected == false)
            {
                Die(trap);
                _isInFinishEntered = true;
            }
        }
        else if (_isInFinishEntered == false && other.gameObject.TryGetComponent(out FinishSetup finish))
        {
            _isInFinishEntered = true;
            Win();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out FinishSetup finish))
        {
            _isInFinishEntered = false;
        }

        if (other.gameObject.TryGetComponent(out Trap trap))
        {
            _isInTrapEntered = false;
        }
    }

    private void OnShieldButtonPressed()
    {
        _isProtected = true;
    }

    private void OnShieldButtonOnPressed()
    {
        _isProtected = false;
        _isInTrapEntered = false;
    }

    private void OnMazeSpawned()
    {
        SetStartState();
        Invoke(nameof(SetDestination), ParamsController.Player.DELAY_BEFORE_MOVING);
    }

    private void SetStartState()
    {
        LocateInBottomLeftCell();
        transform.rotation = Quaternion.identity;
        _agent.enabled = false;
        _playerView.SetBigCubeEnabling(true);
        _playerView.SetSmallCubesEnabling(false);
        _winFx.SetActive(false);
        _trail.Clear();
        _isInFinishEntered = false;
        _isInTrapEntered = false;
        Setup?.Invoke();
    }

    private void LocateInBottomLeftCell()
    {
        transform.localPosition = new Vector3(ParamsController.Maze.CELL_SIZE / 2, transform.localPosition.y, 0);
    }

    private void SetDestination()
    {
        _agent.enabled = true;
        _agent.SetDestination(_finish.transform.position);
    }

    private void Win()
    {
        _winFx.gameObject.SetActive(true);
        Invoke(nameof(StartFading), ParamsController.Player.DELAY_AFTER_WIN);
        Invoke(nameof(RespawnMaze), ParamsController.Player.DELAY_AFTER_WIN + ParamsController.Player.DELAY_FADE);
    }

    private void StartFading()
    {
        Won?.Invoke();
    }

    private void RespawnMaze()
    {
        _spawner.SpawnMaze();
    }

    private void Die(Trap trap)
    {
        _playerView.SetBigCubeEnabling(false);
        _playerView.SetSmallCubesEnabling(true);
        SetIsKinematic(false);
        Invoke(nameof(StopMoving), ParamsController.Player.DELAY_BEFORE_DIE);
        Invoke(nameof(Revive), ParamsController.Player.DELAY_AFTER_DIE);
    }

    private void SetIsKinematic(bool isEnabled)
    {
        Rigidbody[] rigidBodiesSmallCubes = GetComponentsInChildren<Rigidbody>();
        foreach (var rigidbodyItem in rigidBodiesSmallCubes)
            rigidbodyItem.isKinematic = isEnabled;
    }

    private void StopMoving()
    {
        _agent.enabled = false;
    }

    private void Revive()
    {
        Revived?.Invoke();
        SetIsKinematic(true);
        SetStartState();
        Invoke(nameof(SetDestination), ParamsController.Player.DELAY_BEFORE_MOVING);
    }
}