Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports Example

Namespace WindowsApplication167

    Public Partial Class Form1
        Inherits Form

        Public Sub New()
            InitializeComponent()
            Dim matrix As Boolean(,) = New Boolean(9, 9) {}
            'int[,] matrix = new int[10, 10];
            'string[,] matrix = new string[10, 10];
            Dim wrapper As Array2DWrapper(Of Boolean) = New Array2DWrapper(Of Boolean)(matrix)
            gridControl1.DataSource = wrapper
        End Sub
    End Class
End Namespace
