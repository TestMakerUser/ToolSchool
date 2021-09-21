using SmartTek.ToolSchool.Behaviour.Interfaces;
using SmartTek.ToolSchool.Components;
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

        private GameObject instantiatedEnvironment;
        private DrillTool drillTool;
        private DrillBitTool drillBitTool;
        private ScrewBehaviour screw;

        public override IReadOnlyList<BaseTool> ToolsPrefabs => _toolsPrefabs;

        public override string Name => _lessonName;

        public override  string Description => string.Empty;

        protected virtual void Update()
        {
            if(!IsLaunching)
            {
                return;
            }

            CheckDrillUsing();
        }

        private void CheckDrillUsing()
        {
            if(drillTool.IsDrillFullyReady && drillTool.IsDrillInUse)
            {
                screw.IsScrewing = drillBitTool.IsBitInScrew;
                screw.IsRotatingClockwise = drillTool.SpinClockwise;
            }
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