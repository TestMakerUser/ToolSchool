using UnityEngine;
using VRTK;

namespace SmartTek.ToolSchool.Components
{
    public class DrillTool : BaseTool
    {
        [SerializeField]
        private VRTK_InteractableObject linkedObject;
        [SerializeField]
        private VRTK_SnapDropZone[] _bitSnapDropZone;
        [SerializeField]
        private VRTK_SnapDropZone[] _hoseSnapDropZone;
        [SerializeField]
        private VRTK_InteractableObject _vrtkInteractableObject;
        [Space]
        [SerializeField]
        private string toolName = "Electric drill";
        [SerializeField]
        private float spinSpeed = 360f;
        [SerializeField]
        private Transform spinner;
        [SerializeField]
        private bool spinClockwise = true;

        public bool BitIsSnapped { get; private set; }
        public bool HoseIsSnapped { get; private set; }
        public bool IsDrillInUse { get; private set; }

        private bool isSpinning;

        public bool SpinClockwise { get => spinClockwise; }

        public override string Name => toolName;

        protected virtual void Awake()
        {
            Init();
        }

        protected virtual void OnEnable()
        {
            linkedObject = (linkedObject == null ? GetComponent<VRTK_InteractableObject>() : linkedObject);

            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed += InteractableObjectUsed;
                linkedObject.InteractableObjectUnused += InteractableObjectUnused;
            }
        }

        protected virtual void Update()
        {
            if (isSpinning)
            {
                if (spinClockwise)
                {
                    spinner.transform.Rotate(new Vector3(0f, 0f, -spinSpeed * Time.deltaTime));
                }
                else
                {
                    spinner.transform.Rotate(new Vector3(0f, 0f, spinSpeed * Time.deltaTime));
                }
            }
        }

        protected virtual void OnDisable()
        {
            if (linkedObject != null)
            {
                linkedObject.InteractableObjectUsed -= InteractableObjectUsed;
                linkedObject.InteractableObjectUnused -= InteractableObjectUnused;
            }
        }

        protected virtual void OnDestroy()
        {
            foreach (var objToSnap in _bitSnapDropZone)
            {
                objToSnap.ObjectSnappedToDropZone -= SetBitSnapState();
                objToSnap.ObjectUnsnappedFromDropZone -= ResetBitSnapState();
            }
            foreach (var objToSnap in _hoseSnapDropZone)
            {
                objToSnap.ObjectSnappedToDropZone -= SetHoseSnapState();
                objToSnap.ObjectUnsnappedFromDropZone -= ResetHoseSnapState();
            }

            _vrtkInteractableObject.InteractableObjectUsed -= SetUseState();
            _vrtkInteractableObject.InteractableObjectUnused -= ResetUseState();
        }

        private void Init()
        {
            foreach (var objToSnap in _bitSnapDropZone)
            {
                objToSnap.ObjectSnappedToDropZone += SetBitSnapState();
                objToSnap.ObjectUnsnappedFromDropZone += ResetBitSnapState();
            }
            foreach (var objToSnap in _hoseSnapDropZone)
            {
                objToSnap.ObjectSnappedToDropZone += SetHoseSnapState();
                objToSnap.ObjectUnsnappedFromDropZone += ResetHoseSnapState();
            }

            _vrtkInteractableObject.InteractableObjectUsed += SetUseState();
            _vrtkInteractableObject.InteractableObjectUnused += ResetUseState();
        }

        public bool IsDrillFullyReady { get => BitIsSnapped && HoseIsSnapped; }

        #region SnapStates

        private SnapDropZoneEventHandler SetBitSnapState()
        {
            return (sender, args) =>
            {
                BitIsSnapped = true;
            };
        }

        private SnapDropZoneEventHandler ResetBitSnapState()
        {
            return (sender, args) =>
            {
                BitIsSnapped = false;
            };
        }

        private SnapDropZoneEventHandler SetHoseSnapState()
        {
            return (sender, args) =>
            {
                Debug.Log("Hose is snapped");
                HoseIsSnapped = true;
                _vrtkInteractableObject.isUsable = true;
            };
        }

        private SnapDropZoneEventHandler ResetHoseSnapState()
        {
            return (sender, args) =>
            {
                Debug.Log("Hose is unsnapped");
                HoseIsSnapped = false;
                _vrtkInteractableObject.isUsable = false;
            };
        }

        #endregion

        private InteractableObjectEventHandler SetUseState()
        {
            return (sender, args) =>
            {
                Debug.Log("Drill is using");
                IsDrillInUse = true;
            };
        }

        private InteractableObjectEventHandler ResetUseState()
        {
            return (sender, args) =>
            {
                Debug.Log("Drill is not using");
                IsDrillInUse = false;
            };
        }

        protected virtual void InteractableObjectUsed(object sender, InteractableObjectEventArgs e)
        {
            isSpinning = true;
        }

        protected virtual void InteractableObjectUnused(object sender, InteractableObjectEventArgs e)
        {
            isSpinning = false;
        }
    }
}