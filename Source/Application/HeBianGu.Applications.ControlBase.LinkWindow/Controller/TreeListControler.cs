﻿
using HeBianGu.Base.WpfBase;
using HeBianGu.General.WpfControlLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HeBianGu.Applications.ControlBase.LinkWindow.Controler
{
    [Route("TreeList")]
    public class TreeListController : Controller
    {
        [Route("TreeList")]
        public IActionResult TreeList()
        {
            return View();
        }
    }
}