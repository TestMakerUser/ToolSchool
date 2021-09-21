using UnityEngine;

namespace SmartTek.ToolSchool.Behaviour
{
    public class ScrewBehaviour : MonoBehaviour
    {
        [Tooltip("Use 0 and 1 for directions")]
        [SerializeField]
        private Vector3 stepValue;
        [SerializeField]
        private int screwDirection = -1;
        [SerializeField]
        private float screwStep = 0.0001f;
        [SerializeField]
        private Space moveSpace;

        [SerializeField]
        private float rotationAngle;
        [SerializeField]
        private Space rotationSpace;

        [SerializeField]
        private Transform[] screwEnds;

        [SerializeField]
        [Range(0, 1f)]
        private float initScrewPercentage = 0.4f;

        [SerializeField]
        private float screwHeight;

        private Vector3 startPosition;

        public bool IsScrewing { get; set; }
        public bool IsRotatingClockwise { get; set; }

        private void Awake()
        {
            Init();
            CalculateScrewHeight();
            ScrewByPercentage();
        }

        private void Init()
        {
            startPosition = moveSpace == Space.Self ? transform.localPosition : transform.position;
        }

        private void CalculateScrewHeight()
        {
            screwHeight = Vector3.Distance(screwEnds[0].position, screwEnds[1].position);
            Debug.Log("ScrewHeight: " + screwHeight);
        }

        private void ScrewByPercentage()
        {
            float targetMoveDistance = screwHeight * initScrewPercentage * screwDirection;

            if (moveSpace == Space.Self)
            {
                transform.localPosition += new Vector3(
                    stepValue.x * targetMoveDistance,
                    stepValue.y * targetMoveDistance,
                    stepValue.z * targetMoveDistance);
            }
            else
            {
                transform.position += new Vector3(
                    stepValue.x * targetMoveDistance,
                    stepValue.y * targetMoveDistance,
                    stepValue.z * targetMoveDistance);
            }
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
            if (!IsScrewedDistanceInBounds())
            {
                return;
            }

            if (IsRotatingClockwise)
            {
                transform.Rotate(stepValue, rotationAngle, rotationSpace);
            }
            else
            {
                transform.Rotate(stepValue, -rotationAngle, rotationSpace);
            }

            var modifier = screwStep * screwDirection;
            if (moveSpace == Space.Self)
            {
                transform.localPosition += new Vector3(
                    stepValue.x * modifier,
                    stepValue.y * modifier,
                    stepValue.z * modifier);
            }
            else
            {
                transform.position += new Vector3(
                    stepValue.x * modifier,
                    stepValue.y * modifier,
                    stepValue.z * modifier);
            }
        }

        private bool IsScrewedDistanceInBounds()
        {
            Vector3 currentPos = moveSpace == Space.Self ? transform.localPosition : transform.position;

            return !(Vector3.Distance(startPosition, currentPos) >= screwHeight);
        }
    }
}