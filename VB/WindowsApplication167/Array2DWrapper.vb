Imports Microsoft.VisualBasic
Imports System
Imports System.Text
Imports System.Collections.Generic
Imports System.Collections
Imports System.ComponentModel

Namespace Example
	Public Class Array2DWrapper(Of T)
		Implements IList, ITypedList
		Private Class WrapperPropertyDescriptor
			Inherits PropertyDescriptor
			Private index As Integer
			Private elementType As Type

			Public Sub New(ByVal name As String, ByVal index As Integer, ByVal elementType As Type)
				MyBase.New(name, Nothing)
				Me.index = index
				Me.elementType = elementType
			End Sub
			Public Overrides Function CanResetValue(ByVal component As Object) As Boolean
				Return False
			End Function

			Public Overrides ReadOnly Property ComponentType() As Type
				Get
					Return GetType(RowWrapper)
				End Get
			End Property

			Public Overrides Function GetValue(ByVal component As Object) As Object
				If TypeOf component Is RowWrapper Then
					Return (CType(component, RowWrapper)).GetValue(Me.index)
				End If
				Return Nothing
			End Function

			Public Overrides ReadOnly Property IsReadOnly() As Boolean
				Get
					Return False
				End Get
			End Property

			Public Overrides ReadOnly Property PropertyType() As Type
				Get
					Return elementType
				End Get
			End Property

			Public Overrides Sub ResetValue(ByVal component As Object)
			End Sub

			Public Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
				If TypeOf component Is RowWrapper Then
					CType(component, RowWrapper).SetValue(Me.index, value)
				End If
			End Sub

			Public Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
				Return False
			End Function
		End Class

		Private Class RowWrapper
			Inherits CustomTypeDescriptor
			Private owner As Array2DWrapper(Of T)

			Public Sub New(ByVal owner As Array2DWrapper(Of T))
				Me.owner = owner
			End Sub
			Public Overrides Overloads Function GetProperties() As PropertyDescriptorCollection
				Return MyBase.GetProperties()
			End Function

			Public Function GetValue(ByVal index As Integer) As Object
				Return owner.array.GetValue(owner.list.IndexOf(Me), index)
			End Function
			Public Sub SetValue(ByVal index As Integer, ByVal value As Object)
				owner.array.SetValue(value, owner.list.IndexOf(Me), index)
			End Sub

			Public Overrides Overloads Function GetProperties(ByVal attributes() As Attribute) As PropertyDescriptorCollection
				Return owner.pdc
			End Function
		End Class

		Private array(,) As T
		Private list As List(Of RowWrapper)

		Public Sub New(ByVal array(,) As T)
			Me.array = array
			Dim count As Integer = array.GetLength(0)
			list = New List(Of RowWrapper)(count)
			For i As Integer = 0 To count - 1
				list.Add(New RowWrapper(Me))
			Next i
		End Sub

		#Region "IList Members"

		Private Function Add(ByVal value As Object) As Integer Implements IList.Add
			Throw New Exception("The method or operation is not implemented.")
		End Function

		Private Sub Clear() Implements IList.Clear
			Throw New Exception("The method or operation is not implemented.")
		End Sub

		Private Function Contains(ByVal value As Object) As Boolean Implements IList.Contains
			If TypeOf value Is RowWrapper Then
				Return list.Contains(CType(value, RowWrapper))
			End If
			Return False
		End Function

		Private Function IndexOf(ByVal value As Object) As Integer Implements IList.IndexOf
			If TypeOf value Is RowWrapper Then
				Return list.IndexOf(CType(value, RowWrapper))
			End If
			Return -1
		End Function

		Private Sub Insert(ByVal index As Integer, ByVal value As Object) Implements IList.Insert
			Throw New Exception("The method or operation is not implemented.")
		End Sub

		Private ReadOnly Property IsFixedSize() As Boolean Implements IList.IsFixedSize
			Get
				Return True
			End Get
		End Property

		Private ReadOnly Property IsReadOnly() As Boolean Implements IList.IsReadOnly
			Get
				Return True
			End Get
		End Property

		Private Sub Remove(ByVal value As Object) Implements IList.Remove
			Throw New Exception("The method or operation is not implemented.")
		End Sub

		Private Sub RemoveAt(ByVal index As Integer) Implements IList.RemoveAt
			Throw New Exception("The method or operation is not implemented.")
		End Sub

		Public Property IList_Item(ByVal index As Integer) As Object Implements IList.Item
			Get
				Return list(index)
			End Get
			Set(ByVal value As Object)
				Throw New Exception("The method or operation is not implemented.")
			End Set
		End Property

		#End Region

		#Region "ICollection Members"

		Private Sub CopyTo(ByVal array As Array, ByVal index As Integer) Implements ICollection.CopyTo
			If TypeOf array Is RowWrapper() Then
				list.CopyTo(CType(array, RowWrapper()), index)
			End If
		End Sub

		Private ReadOnly Property Count() As Integer Implements ICollection.Count
			Get
				Return list.Count
			End Get
		End Property

		Private ReadOnly Property IsSynchronized() As Boolean Implements ICollection.IsSynchronized
			Get
				Return False
			End Get
		End Property

		Private ReadOnly Property SyncRoot() As Object Implements ICollection.SyncRoot
			Get
				Return Me
			End Get
		End Property

		#End Region

		#Region "IEnumerable Members"

		Private Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
			Return list.GetEnumerator()
		End Function

		#End Region

		#Region "ITypedList Members"

		Private pdc As PropertyDescriptorCollection

		Private Function GetItemProperties(ByVal listAccessors() As PropertyDescriptor) As PropertyDescriptorCollection Implements ITypedList.GetItemProperties
			If pdc Is Nothing Then
				Dim pd(array.GetLength(1) - 1) As PropertyDescriptor
				For i As Integer = 0 To pd.Length - 1
					pd(i) = New WrapperPropertyDescriptor(AscW("C" )+ i, i, GetType(T))
				Next i
				pdc = New PropertyDescriptorCollection(pd)
			End If
			Return pdc
		End Function

		Private Function GetListName(ByVal listAccessors() As PropertyDescriptor) As String Implements ITypedList.GetListName
			Return String.Empty
		End Function

		#End Region

	End Class
End Namespace