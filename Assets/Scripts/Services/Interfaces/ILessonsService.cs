using SmartTek.ToolSchool.Behaviour.Intefaces;

namespace SmartTek.ToolSchool.Services.Interfaces
{
    public interface ILessonsService
    {
        void Launch<IToolLesson>();
        T GetLessonInstace<T>() where T : IToolLesson;
    }
}