﻿using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KtpAcs.WinForm.Jijian
{
  public    class MessageHelper
    {
        public static void Show(string msg)
        {
         
            //MessageHelper.Show(msg);
           // new MessagePrompt(msg).ShowDialog();
            XtraMessageBox.Show(msg);
        }

        public static void Show(Exception ex)
        {
            //new MessagePrompt(ex.Message).ShowDialog();
            XtraMessageBox.Show(ex.Message);
        }

    }
}
