using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace IPtor.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged //реализация и интерфейсов тоже вполне стандартная, весь класс как шаблон по сути можно вставлять на будущее без изменений
    {
        // Событие — View подписывается на него и обновляет интерфейс
        public event PropertyChangedEventHandler? PropertyChanged;

        //метод для вызова события
        //[CallerMemberName] — компилятор сам подставит имя свойства, которое вызвало метод
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Универсальный метод установки значения свойства
        // Проверяет изменилось ли значение, если да — обновляет и вызывает событие
        protected bool Set<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(field, value))
                return false;  // Значение не изменилось, ничего не делаем

            field = value;
            OnPropertyChanged(propertyName);  // Сообщаем View: "я изменился, обновись!"
            return true;
        }
    }
}
