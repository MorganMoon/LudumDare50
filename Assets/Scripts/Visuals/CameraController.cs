using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LudumDare50.Client.Visuals
{
    [RequireComponent(typeof(Camera))]
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        private Camera _mainCamera;

        // Start is called before the first frame update
        void Awake()
        {
            _mainCamera = GetComponent<Camera>();
            CalculateCamSize();
        }

        private void CalculateCamSize()
        {
            _mainCamera.orthographicSize = Mathf.Min(_spriteRenderer.bounds.extents.y, _spriteRenderer.bounds.extents.x / _mainCamera.aspect);
        }
    }
}
