﻿using SmartTek.ToolSchool.Behaviour.Interfaces;
using SmartTek.ToolSchool.Helpers;
using SmartTek.ToolSchool.Services.Interfaces;
using TMPro;
using UnityEngine;

namespace SmartTek.ToolSchool.Behaviour
{
    public class LobbyController : MonoBehaviour
    {
        private ILessonsService lessonsService;

        [SerializeField]
        private BezierPointer _buttonQuit;
        [SerializeField]
        private Transform _lessonsParent;
        [SerializeField]
        private BezierPointer _buttonPrefab;

        private void Awake()
        {
            _buttonQuit.SetActionOnClick(OnButtonQuitClick);
        }

        private void Start()
        {
            lessonsService = ServicesReferences.LessonsService;
            InitializeLessonsList();
        }

#if UNITY_EDITOR
        private void OnGUI()
        {
            foreach(var lesson in lessonsService.LessonsInstances)
            {
                if(GUILayout.Button(lesson.Name))
                {
                    lessonsService.Launch(lesson);
                }
            }    
        }
#endif

        private void InitializeLessonsList()
        {
            foreach (var lesson in lessonsService.LessonsInstances)
            {
                var closureLesson = lesson;
                var button = Instantiate(_buttonPrefab, _lessonsParent, false);
                button.SetActionOnClick(() => OnButtonStartLessonClick(closureLesson));
                button.GetComponentInChildren<TMP_Text>().text = lesson.Name;
            }
        }

        private void OnButtonStartLessonClick(IToolLesson lesson)
        {
            lessonsService.Launch(lesson);
        }

        private void OnButtonQuitClick()
        {
            Application.Quit();
        }
    }
}