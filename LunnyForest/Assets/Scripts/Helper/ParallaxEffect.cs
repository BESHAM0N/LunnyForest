using System;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
   [SerializeField] private Camera _camera;
   [SerializeField] private Transform _followTarget;
   private Vector2 _startPosition;
   private float _startingZ;
   private Vector2 _cameraMoveSinceStart => (Vector2)_camera.transform.position - _startPosition;
   private float zDistanceFromTarget => transform.position.z - _followTarget.position.z;
   private float _clippingPlane => (_camera.transform.position.z +
                                    (zDistanceFromTarget > 0 ? _camera.farClipPlane : _camera.nearClipPlane));
   private float _parallaxFactor => Math.Abs(zDistanceFromTarget) / _clippingPlane;
   private void Start()
   {
       _startPosition = transform.position;
       _startingZ = transform.position.z;
   }
   private void Update()
   {
       var newPosition = _startPosition + _cameraMoveSinceStart * _parallaxFactor;
       transform.position = new Vector3(newPosition.x, newPosition.y, _startingZ);
   }
}
