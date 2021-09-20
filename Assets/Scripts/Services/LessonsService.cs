using SmartTek.ToolSchool.Behaviour.Interfaces;
using SmartTek.ToolSchool.Helpers;
using SmartTek.ToolSchool.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SmartTek.ToolSchool.Services
{
    public class LessonsService : MonoBehaviour, ILessonsService
    {
        public static ILessonsService Instance { get; private set; }

        public IReadOnlyList<IToolLesson> LessonsInstances { get; private set; }

        private IToolLesson currentLesson;

        private bool isLoadingNow;

        private void Awake()
        {
            InitializeInstances();
            Instance = this;
        }

        private void InitializeInstances()
        {
            LessonsInstances = GetComponentsInChildren<MonoBehaviour>()
                .Where(i => i is IToolLesson)
                .Select(i => i as IToolLesson)
                .ToList();
        }

        public T GetLessonInstace<T>() where T : IToolLesson
        {
            return (T)LessonsInstances.FirstOrDefault(i => i is T);
        }

        public void Launch<T>() where T : IToolLesson
        {
            Launch(GetLessonInstace<T>());
        }

        public void Launch(IToolLesson lessonInstance)
        {
            if (isLoadingNow)
            {
                throw new System.InvalidOperationException("Another lesson is loading now");
            }

            isLoadingNow = true;
            if (currentLesson != null)
            {
                currentLesson.Dispose();
            }
            currentLesson = lessonInstance;
            ServicesReferences.SceneService.LoadScene(SceneService.SceneType.Workshop, OnWorkshopLoaded);
        }

        public void FinishAndReturnToLobby()
        {
            if(currentLesson == null)
            {
                throw new System.InvalidOperationException("Current lesson is null");
            }

            currentLesson.Dispose();
            ServicesReferences.SceneService.LoadScene(SceneService.SceneType.Lobby, null);
        }

        private void OnWorkshopLoaded()
        {
            currentLesson.LaunchLesson(null);
            isLoadingNow = false;
        }
    }
}