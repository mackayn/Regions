using System;
using System.Windows.Input;
using Prism.Mvvm;

namespace PrismRegions.Framework.Model
{
    public class NavigationItem : BindableBase, ICloneable
    {
        private string _imageName;
        public string ImageName
        {
            get => _imageName;
            set => SetProperty(ref _imageName, value);
        }

        private string _kindName;
        public string KindName
        {
            get => _kindName;
            set => SetProperty(ref _kindName, value);
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        private object _commandParameter;
        public object CommandParameter
        {
            get => _commandParameter;
            set => SetProperty(ref _commandParameter, value);
        }

        private ICommand _commandToExecute;
        public ICommand CommandToExecute
        {
            get => _commandToExecute;
            set => SetProperty(ref _commandToExecute, value);
        }

        public object Clone()
        {
            return (NavigationItem)this.MemberwiseClone();
        }

        object ICloneable.Clone() { return Clone(); }
    }
}
