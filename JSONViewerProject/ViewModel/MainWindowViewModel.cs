using JSONViewerProject.Commands;
using JSONViewerProject.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace JSONViewerProject.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        // поле для хранения списка объединенных данных
        private ObservableCollection<CombinedData> combinedDataList;

        // свойство для доступа к списку объединенных данных
        public ObservableCollection<CombinedData> CombinedDataList
        {
            get => combinedDataList;
            set
            {
                combinedDataList = value;
                OnPropertyChanged(); // уведомление об изменении поля
            }
        }

        // команда для загрузки JSON (lcmsp6) файла 
        public ICommand LoadJsonCommand { get; }

        // конструктор
        public MainWindowViewModel()
        {
            // инициализация списка объединенных данных
            CombinedDataList = new ObservableCollection<CombinedData>();
            // инициализация команды для загрузки JSON файла
            LoadJsonCommand = new RelayCommand(LoadJson);
        }

        // метод для загрузки lcmsp6 файла
        private void LoadJson()
        {
            // выбор файла
            var dialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".lcmsp6", // Установка расширения по умолчанию
                Filter = "lcmsp6 Files (*.lcmsp6)|*.lcmsp6"
            };

            // показ диалогового окна
            bool? result = dialog.ShowDialog();
            if (result == true)
            {
                string filename = dialog.FileName; 
                string json = File.ReadAllText(filename); // чтение содержимого файла
                var rootObject = JsonConvert.DeserializeObject<RootObject>(json); // десериализация JSON в объект

                // создание списка объединенных данных
                var combinedData = rootObject.PlanItemProductItemRelations
                    .Select(p =>
                    {
                        // находим продукт для текущего PlanItemProductItemRelation
                        var product = rootObject.Products.FirstOrDefault(pr => pr.Id == p.ProductItemId);

                        // находим Id родительского элемента для текущего продукта
                        var parentItemId = rootObject.ProductItemProductItemRelations
                            .FirstOrDefault(pr => pr.ChildItemId == p.ProductItemId)?.ParentItemId;
                        // находим уровень для текущего продукта
                        var level = rootObject.Products.FirstOrDefault(pr => pr.Id == parentItemId)?.Name;

                        product.Level = level; // устанавливаем уровень для продукта

                        // создаем новый объект CombinedData
                        var combined = new CombinedData
                        {
                            PlanItemProductItemRelation = p,
                            Products = product != null ? new List<Product> { product } : new List<Product>(),
                        };

                        return combined; // возвращаем созданный объект
                    }).ToList();

                // устанавливаем список объединенных данных
                CombinedDataList = new ObservableCollection<CombinedData>(combinedData);
            }
        }

        // событие, возникающее при изменении свойства
        public event PropertyChangedEventHandler PropertyChanged;

        // метод для уведомления об изменении свойства
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
