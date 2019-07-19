﻿using HeBianGu.Applications.ControlBase.LinkWindow;
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using HeBianGu.General.WpfMvc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LoyoutViewModel vm = new LoyoutViewModel();

            vm.LoadedCommand.Execute(null);

            this.DataContext = vm;
        }
    }

    class LoyoutViewModel : MvcViewModelBase
    {
        public RelayCommand<string> LoadedCommand => new Lazy<RelayCommand<string>>(() =>
    new RelayCommand<string>(Loaded, CanLoaded)).Value;

        private void Loaded(string args)
        {
            this.Collection.Clear();

            var data = AssemblyDomain.Instance.GetTreeListData();

            foreach (var item in data)
            {
                this.Collection.Add(item);
            }  
        }


        private string _buttonContentText;
        /// <summary> 说明  </summary>
        public string ButtonContentText
        {
            get { return _buttonContentText; }
            set
            {
                _buttonContentText = value;
                RaisePropertyChanged("ButtonContentText");
            }
        }



        private bool CanLoaded(string args)
        {
            return true;
        }


        private ObservableCollection<TreeNodeEntity> _collection = new ObservableCollection<TreeNodeEntity>();
        /// <summary> 说明  </summary>
        public ObservableCollection<TreeNodeEntity> Collection
        {
            get { return _collection; }
            set
            {
                _collection = value;
                RaisePropertyChanged("Collection");
            }
        }

        /// <summary> 命令通用方法 </summary>
        protected override async void RelayMethod(object obj)

        {
            string command = obj?.ToString();

            //  Do：对话消息
            if (command == "Button.ShowDialogMessage")
            {
                await MessageService.ShowSumitMessge("这是消息对话框？");

            }
            //  Do：等待消息
            else if (command == "Button.ShowWaittingMessge")
            {

                await MessageService.ShowWaittingMessge(() => Thread.Sleep(2000));

            }
            //  Do：百分比进度对话框
            else if (command == "Button.ShowPercentProgress")
            {
                Action<IPercentProgress> action = l =>
                {
                    for (int i = 0; i < 100; i++)
                    {
                        l.Value = i;

                        Thread.Sleep(50);
                    }

                    Thread.Sleep(1000);

                    MessageService.ShowSnackMessageWithNotice("加载完成！");
                };
                await MessageService.ShowPercentProgress(action);

            }
            //  Do：文本进度对话框
            else if (command == "Button.ShowStringProgress")
            {
                Action<IStringProgress> action = l =>
                {
                    for (int i = 1; i <= 100; i++)
                    {
                        l.MessageStr = $"正在提交当前页第{i}份数据,共100份";

                        Thread.Sleep(50);
                    }

                    Thread.Sleep(1000);

                    MessageService.ShowSnackMessageWithNotice("提交完成：成功100条，失败0条！");
                };

                await MessageService.ShowStringProgress(action);

            }
            //  Do：确认取消对话框
            else if (command == "Button.ShowResultMessge")
            {
                Action<object, DialogClosingEventArgs> action = (l, k) =>
                {
                    if ((bool)k.Parameter)
                    {
                        MessageService.ShowSnackMessageWithNotice("你点击了取消");
                    }
                    else
                    {
                        MessageService.ShowSnackMessageWithNotice("你点击了确定");
                    }
                };

                MessageService.ShowResultMessge("确认要退出系统?", action);


            }
            //  Do：提示消息
            else if (command == "Button.ShowSnackMessage")
            {
                MessageService.ShowSnackMessageWithNotice("这是提示消息？");
            }
            //  Do：气泡消息
            else if (command == "Button.ShowNotifyMessage")
            {
                MessageService.ShowNotifyMessage("你有一条报警信息需要处理，请检查", "Notify By HeBianGu");
            }
        }
    }

}