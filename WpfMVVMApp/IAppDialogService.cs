using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfMVVMApp
{
    public interface IAppDialogService
    {
        void ShowMessage(string message);
        string FileName { set; get; }
        bool OpenFileDialog();
        bool SaveFileDialog();
    }

    public class ApplicationDialogService : IAppDialogService
    {
        public string FileName { get; set; }

        public bool OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if(openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.FileName;
                return true;
            }
            return false;
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog saveFileDialog = new();
            if(saveFileDialog.ShowDialog() == true)
            {
                FileName = saveFileDialog.FileName;
                return true;
            }
            return false;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }

}
