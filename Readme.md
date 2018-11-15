<!-- default file list -->
*Files to look at*:

* [Array2DWrapper.cs](./CS/WindowsApplication167/Array2DWrapper.cs) (VB: [Array2DWrapper.vb](./VB/WindowsApplication167/Array2DWrapper.vb))
* [Form1.cs](./CS/WindowsApplication167/Form1.cs) (VB: [Form1.vb](./VB/WindowsApplication167/Form1.vb))
<!-- default file list end -->
# How to bind the XtraGrid to a two-dimensional array


<p>The XtraGrid doesn't natively support this kind of datasource. However, you can create a wrapper class, which implements the necessary interfaces and provides access to array data via them. In this example, we create a generic wrapper for 2-dimensional arrays and implement essential members of IList and ITypedList interfaces.</p>

<br/>


