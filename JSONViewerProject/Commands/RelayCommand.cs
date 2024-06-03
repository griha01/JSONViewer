using System;
using System.Windows.Input;

namespace JSONViewerProject.Commands
{
    class RelayCommand : ICommand
    {
        // действие, которое будет выполняться при вызове команды
        private readonly Action execute;

        // может ли команда выполняться
        private readonly Func<bool> canExecute;

        // событие, которое вызывается при изменении состояния, влияющего на то, может ли команда выполняться
        public event EventHandler CanExecuteChanged;

        // конструктор
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        // метод, определяющий, может ли команда выполняться
        public bool CanExecute(object parameter) => canExecute == null || canExecute();

        // метод, выполняющий команду
        public void Execute(object parameter) => execute();

        // метод для уведомления о том, что состояние выполнения команды изменилось
        public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
    }
}
