using System;
using System.Linq;
using Match;
using UI;
using UnityEngine;

namespace Cameras
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private CameraLocationsContainer _cameraLocationsContainer;
        [SerializeField] private TeamSelectionMenuController _teamSelectionMenuController;

        [SerializeField] private float _cameraSpeed = 20f;
        [SerializeField] private float _borderThickness = 10f;
        [SerializeField] private float _cameraMaxHeight = 50f;
        [SerializeField] private float _cameraMinHeight = 5f;
        [SerializeField] private bool _allowMoveCamera;

        public bool AllowMoveCamera
        {
            get => _allowMoveCamera;
            set => _allowMoveCamera = value;
        }

        private void Start()
        {
            _teamSelectionMenuController.PlayerTeamSelected += OnPlayerTeamSelected;
            _allowMoveCamera = false;
        }
        

        private void LateUpdate()
        {
            if (AllowMoveCamera)
            {
                HandleKeyboardInput();
                HandleMouseInput();
            }

            LimitHeight();
        }
        
        private void OnPlayerTeamSelected(PlayerTeam playerTeam)
        {
            switch (playerTeam)
            {
                case PlayerTeam.Blue:
                    MoveToLocation(CameraLocationId.BlueTeamThrone);
                    break;
                case PlayerTeam.Red:
                    MoveToLocation(CameraLocationId.RedTeamThrone);
                    break;
                case PlayerTeam.Green:
                    break;
                case PlayerTeam.Yellow:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerTeam), playerTeam, null);
            }

            _allowMoveCamera = true;
        }

        public void MoveToLocation(CameraLocationId locationId)
        {
            var cameraLocation = _cameraLocationsContainer.CameraLocations
                .Single(item => item._cameraLocationId == locationId)._transform;

            _cameraTransform.position = cameraLocation.position;
            Debug.Log($"Moved camera to location: {locationId}");
        }

        private void HandleKeyboardInput()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");
            var vector3 = new Vector3(horizontal, 0, vertical).normalized;

            _cameraTransform.Translate(vector3 * _cameraSpeed * Time.deltaTime, Space.World);
        }

        private void HandleMouseInput()
        {
            var mousePosition = Input.mousePosition;
            if (mousePosition.x < _borderThickness)
            {
                _cameraTransform.Translate(Vector3.left * _cameraSpeed * Time.deltaTime);
            }
            else if (mousePosition.x > Screen.width - _borderThickness)
            {
                _cameraTransform.Translate(Vector3.right * _cameraSpeed * Time.deltaTime);
            }

            if (mousePosition.y < _borderThickness)
            {
                _cameraTransform.Translate(Vector3.back * _cameraSpeed * Time.deltaTime, Space.World);
            }
            else if (mousePosition.y > Screen.height - _borderThickness)
            {
                _cameraTransform.Translate(Vector3.forward * _cameraSpeed * Time.deltaTime, Space.World);
            }
        }

        private void LimitHeight()
        {
            var scroll = Input.GetAxis("Mouse ScrollWheel");
            var cameraPosition = _cameraTransform.position;
            cameraPosition.y -= scroll * 1000 * _cameraSpeed * Time.deltaTime;
            cameraPosition.y = Mathf.Clamp(cameraPosition.y, _cameraMinHeight, _cameraMaxHeight);
            _cameraTransform.position = cameraPosition;
        }
    }
}