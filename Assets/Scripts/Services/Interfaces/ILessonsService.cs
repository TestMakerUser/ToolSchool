﻿using SmartTek.ToolSchool.Behaviour.Interfaces;
using System.Collections.Generic;

namespace SmartTek.ToolSchool.Services.Interfaces
{
    public interface ILessonsService
    {
        IReadOnlyList<IToolLesson> LessonsInstances { get; }

        void Launch<T>() where T : IToolLesson;
        T GetLessonInstace<T>() where T : IToolLesson;
        void Launch(IToolLesson lessonInstance);
        void FinishAndReturnToLobby();
    }
}