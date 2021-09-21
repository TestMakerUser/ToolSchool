using UnityEngine;

namespace SmartTek.ToolSchool.Behaviour
{
    public class ScrewBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Vector3 stepValue;
        [SerializeField]
        private float rotationAngle;
        [SerializeField]
        private Space rotationSpace;

        [SerializeField]
        private float screwHeight = 0.015f;
        [SerializeField]
        private float screwSpeed = 1f;

        public float CurrentScrewingPercent { get; private set; }

        public bool IsScrewing { get; set; }
        public bool IsRotatingClockwise { get; set; }

        private Vector3 startPosition;
        private Vector3 endPosition;

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            startPosition = transform.position;
            endPosition = startPosition + (-transform.up * screwHeight);
        }

        void Update()
        {
            if (IsScrewing)
            {
                MoveDown();
            }
        }

        private void MoveDown()
        {
            if (IsRotatingClockwise)
            {
                CurrentScrewingPercent += Time.deltaTime * screwSpeed;
                CurrentScrewingPercent = Mathf.Clamp01(CurrentScrewingPercent);
                transform.Rotate(stepValue, rotationAngle, rotationSpace);
            }
            else
            {
                CurrentScrewingPercent -= Time.deltaTime * screwSpeed;
                CurrentScrewingPercent = Mathf.Clamp01(CurrentScrewingPercent);
                transform.Rotate(stepValue, -rotationAngle, rotationSpace);
            }

            transform.position = Vector3.Lerp(startPosition, endPosition, CurrentScrewingPercent);
        }
    }
}