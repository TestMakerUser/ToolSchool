using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SmartTek.ToolSchool.Components
{
    public class DrillBitTool : BaseTool
    {
        private readonly string[] namesOfScrewTriggers = { "HatTriggerBottom", "HatTriggerTop" };

        [SerializeField]
        private string _toolName = "Drill Bit";

        private List<string> collidedScrewTriggers = new List<string>();
             
        public override string Name => _toolName;

        public bool IsBitInScrew { get => collidedScrewTriggers.Count == namesOfScrewTriggers.Length; }

        private void OnTriggerEnter(Collider other)
        {
            if (namesOfScrewTriggers.Contains(other.name) && !collidedScrewTriggers.Contains(other.name))
            {
                collidedScrewTriggers.Add(other.name);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (namesOfScrewTriggers.Contains(other.name) && !collidedScrewTriggers.Contains(other.name))
            {
                collidedScrewTriggers.Remove(other.name);
            }
        }
    }
}