using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleCompilerVersion2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            txtDebug.Clear();
            CSharpCodeProvider provider 
                = new CSharpCodeProvider(new Dictionary<string, string>()
                    { {"compilerVersion", txtFramework.Text } });
            CompilerParameters parameters 
                = new CompilerParameters(new[] {"mscorlib.dll","System.Core.dll"},
                    txtOutput.Text, true);

            parameters.GenerateExecutable = true;
            CompilerResults result = provider.CompileAssemblyFromSource(parameters, txtCode.Text);
            if (result.Errors.HasErrors)
            {
                result.Errors.Cast<CompilerError>()
                    .ToList()
                    .ForEach(error => txtDebug.Text += error.ErrorText + "\r\n");
            }
            else
            {
                txtDebug.Text = "----- Build Succeed -----";
                Process.Start(Application.StartupPath + "/" + txtOutput.Text);
            }
        }
    }
}
