using System.Collections.Generic;
using UnityEngine;

namespace Cameras
{
    public class CameraLocationsContainer : MonoBehaviour
    {
        [SerializeField] private List<CameraLocationItem> _cameraLocations;

        public List<CameraLocationItem> CameraLocations => _cameraLocations;
    }
}