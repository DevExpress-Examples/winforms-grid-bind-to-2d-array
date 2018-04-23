Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Collections
Imports Example

Namespace WindowsApplication167
	Partial Public Class Form1
		Inherits Form
		Public Sub New()
			InitializeComponent()

			Dim matrix(9, 9) As Boolean
			'int[,] matrix = new int[10, 10];
			'string[,] matrix = new string[10, 10];

			Dim wrapper As Array2DWrapper(Of Boolean) = New Array2DWrapper(Of Boolean)(matrix)
			gridControl1.DataSource = wrapper
		End Sub
	End Class
End Namespace