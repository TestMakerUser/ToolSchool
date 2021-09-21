using SmartTek.ToolSchool.Behaviour.Interfaces;
using SmartTek.ToolSchool.Helpers;
using SmartTek.ToolSchool.Services.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace SmartTek.ToolSchool.Behaviour
{
    public class LobbyController : MonoBehaviour
    {
        private ILessonsService lessonsService;

        [SerializeField]
        private Button _buttonQuit;
        [SerializeField]
        private Transform _lessonsParent;
        [SerializeField]
        private Button _buttonPrefab;

        private void Awake()
        {
            _buttonQuit.onClick.AddListener(OnButtonQuitClick);
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
                button.onClick.AddListener(() => OnButtonStartLessonClick(closureLesson));
                button.GetComponentInChildren<Text>().text = lesson.Name;
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