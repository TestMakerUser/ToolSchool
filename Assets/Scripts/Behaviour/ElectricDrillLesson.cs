using SmartTek.ToolSchool.Behaviour.Interfaces;
using SmartTek.ToolSchool.Components;
using SmartTek.ToolSchool.Helpers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SmartTek.ToolSchool.Behaviour
{
    public class ElectricDrillLesson : BaseToolLesson
    {
        [SerializeField]
        private string _lessonName = "Electric Drill Lesson";
        [SerializeField]
        private BaseTool[] _toolsPrefabs;
        [SerializeField]
        private GameObject _environment;
        [SerializeField]
        private ScrewBehaviour _screwPrefab;
        [SerializeField]
        private GameObject _completePopupPrefab;
        [SerializeField]
        private float _delayToFinish = 5f;

        private GameObject instantiatedEnvironment;
        private DrillTool drillTool;
        private DrillBitTool drillBitTool;
        private ScrewBehaviour screw;
        private bool isFinished;
        private GameObject completePopup;

        public override IReadOnlyList<BaseTool> ToolsPrefabs => _toolsPrefabs;

        public override string Name => _lessonName;

        public override  string Description => string.Empty;

        protected virtual void Update()
        {
            if(!IsLaunching || isFinished)
            {
                return;
            }

            CheckDrillUsing();
        }

        private void CheckDrillUsing()
        {
            if(drillTool.IsDrillFullyReady && drillTool.IsDrillInUse)
            {
                if(IsScrewingComplete())
                {
                    InstantiateCompletePopupAndFinish();
                }
                else
                {
                    screw.IsScrewing = drillBitTool.IsBitInScrew;
                    screw.IsRotatingClockwise = drillTool.SpinClockwise;
                }
            }
        }

        private bool IsScrewingComplete()
        {
            return screw.CurrentScrewingPercent >= 1f;
        }

        private void InstantiateCompletePopupAndFinish()
        {
            isFinished = true;
            screw.IsScrewing = false;
            completePopup = Instantiate(_completePopupPrefab);
            StartCoroutine(CoroutineDelayedFinish());
        }

        private IEnumerator CoroutineDelayedFinish()
        {
            yield return new WaitForSeconds(_delayToFinish);
            ServicesReferences.LessonsService.FinishAndReturnToLobby();
        }

        public override void Dispose()
        {
            if(instantiatedEnvironment != null)
            {
                Destroy(instantiatedEnvironment);
            }
            if(screw != null)
            {
                Destroy(screw.gameObject);
                screw = null;
            }
            if(completePopup != null)
            {
                Destroy(completePopup);
            }
            isFinished = false;
            base.Dispose();
        }

        public override void LaunchLesson(LessonContext context)
        {
            base.LaunchLesson(context);
            instantiatedEnvironment = Instantiate(_environment);
            screw = Instantiate(_screwPrefab);
            drillTool = GetToolInstance<DrillTool>();
            drillBitTool = GetToolInstance<DrillBitTool>();
        }
    }
}