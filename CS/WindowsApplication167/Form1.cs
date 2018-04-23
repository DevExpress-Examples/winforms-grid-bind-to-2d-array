using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Example;

namespace WindowsApplication167
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            bool[,] matrix = new bool[10, 10];
            //int[,] matrix = new int[10, 10];
            //string[,] matrix = new string[10, 10];

            Array2DWrapper<bool> wrapper = new Array2DWrapper<bool>(matrix);
            gridControl1.DataSource = wrapper;
        }
    }
}