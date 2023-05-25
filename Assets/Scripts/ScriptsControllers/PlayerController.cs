using ScriptsManager;
using UnityEngine;

namespace ScriptsControllers
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Transform _playerCamera;
        [SerializeField] private float _mouseSensitivity;
        [SerializeField] private float _walkSpeed;
        [SerializeField] private float _gravity;
        [SerializeField] [Range(0.0f, 0.5f)] private float _moveSmoothTime;
        [SerializeField] [Range(0.0f, 0.5f)] private float _mouseSmoothTime;
        [SerializeField] private Transform _positionGunStart;
        [SerializeField] private bool _lockCursor;
        [SerializeField] private bool _canNew;

        private float _cameraPitch;
        private float _velocityY;
        private Vector2 _currentDir;
        private Vector2 _currentDirVelocity;
        private Vector2 _currentMouseDelta;
        private Vector2 _currentMouseDeltaVelocity;
        private CharacterController _controller;
        private GunsManager _currentGun;

        public PlayerController()
        {
            _currentMouseDeltaVelocity = Vector2.zero;
            _currentMouseDelta = Vector2.zero;
            _currentDirVelocity = Vector2.zero;
            _currentDir = Vector2.zero;
            _controller = null;
            _velocityY = 0.0f;
            _cameraPitch = 0.0f;
            _canNew = true;
            _lockCursor = true;
            _mouseSmoothTime = 0.03f;
            _moveSmoothTime = 0.3f;
            _gravity = -13.0f;
            _walkSpeed = 6.0f;
            _mouseSensitivity = 3.5f;
            _playerCamera = null;
        }

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
            if(_lockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (_currentGun)
                {
                    _currentGun.ShootNow();
                }
            }
        
            UpdateMouseLook();
            UpdateMovement();
        }

        private void UpdateMouseLook()
        {
            var targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            _currentMouseDelta = Vector2.SmoothDamp(_currentMouseDelta, targetMouseDelta, ref _currentMouseDeltaVelocity, _mouseSmoothTime);

            _cameraPitch -= _currentMouseDelta.y * _mouseSensitivity;
            _cameraPitch = Mathf.Clamp(_cameraPitch, -90.0f, 90.0f);

            _playerCamera.localEulerAngles = Vector3.right * _cameraPitch;
            transform.Rotate(Vector3.up * (_currentMouseDelta.x * _mouseSensitivity));
        }

        private void UpdateMovement()
        {
            var targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            targetDir.Normalize();
            _currentDir = Vector2.SmoothDamp(_currentDir, targetDir, ref _currentDirVelocity, _moveSmoothTime);
        
            if (_controller.isGrounded)
            {
                _velocityY = 0.0f;
            }

            _velocityY += _gravity * Time.deltaTime;
            var velocity = (transform.forward * _currentDir.y + transform.right * _currentDir.x) * _walkSpeed + Vector3.up * _velocityY;
            _controller.Move(velocity * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.CompareTag("Gun"))
            {
                ClearOtherGuns();
                SetGunInPlayer(other.gameObject);
            }
        }

        private void ClearOtherGuns()
        {
            if (_currentGun)
            {
                var currentGunTemp = _currentGun.transform.GetComponent<GunsManager>();
                
                _currentGun.transform.position = currentGunTemp._positionInitial;
                _currentGun.transform.rotation = currentGunTemp._rotationInitial;
                _currentGun.transform.parent = null;
                _currentGun.transform.GetComponent<BoxCollider>().enabled = true;
                _currentGun = null;
            }
        }

        private void SetGunInPlayer(GameObject gun)
        {
            _currentGun = gun.GetComponent<GunsManager>();
            _currentGun.transform.position = _positionGunStart.position;
            _currentGun.transform.rotation = _positionGunStart.rotation;
            _currentGun.transform.GetComponent<BoxCollider>().enabled = false;
            _currentGun.transform.parent = transform.GetChild(0);
        }
    }
}
