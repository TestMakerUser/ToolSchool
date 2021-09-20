using SmartTek.ToolSchool.Behaviour.Intefaces;
using SmartTek.ToolSchool.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SmartTek.ToolSchool.Services
{
    public class LessonsService : MonoBehaviour, ILessonsService
    {
        public static ILessonsService Instance { get; private set; }

        private IToolLesson currentLesson;
        private IReadOnlyList<IToolLesson> lessonsInstances;

        private void Awake()
        {
            InitializeInstances();
            Instance = this;
        }

        private void InitializeInstances()
        {
            lessonsInstances = GetComponentsInChildren<MonoBehaviour>()
                .Where(i => i is IToolLesson)
                .Select(i => i as IToolLesson)
                .ToList();
        }

        public T GetLessonInstace<T>() where T : IToolLesson
        {
            return (T)lessonsInstances.FirstOrDefault(i => i is T);
        }

        public void Launch<IToolLesson>()
        {
            //todo: dispose
            throw new System.NotImplementedException();
        }
    }
}