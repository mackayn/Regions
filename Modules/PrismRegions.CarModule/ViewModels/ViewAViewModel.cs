using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace PrismRegions.CarModule.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewAViewModel()
        {
            Title = "View A";
        }
    }
}
