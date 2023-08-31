Imports System.ComponentModel
Imports System.Xml.Serialization
Imports System.Collections.Generic
Imports System.Reflection
Imports DevExpress.Xpf.Grid
Imports System.ComponentModel.DataAnnotations

Namespace Model

    Public Class ViewModel

        Private _Ids As List(Of Integer), _Cities As List(Of String), _Titles As List(Of String), _Source As IList(Of Model.Employee)

        Public Property Ids As List(Of Integer)
            Get
                Return _Ids
            End Get

            Private Set(ByVal value As List(Of Integer))
                _Ids = value
            End Set
        End Property

        Public Property Cities As List(Of String)
            Get
                Return _Cities
            End Get

            Private Set(ByVal value As List(Of String))
                _Cities = value
            End Set
        End Property

        Public Property Titles As List(Of String)
            Get
                Return _Titles
            End Get

            Private Set(ByVal value As List(Of String))
                _Titles = value
            End Set
        End Property

        Public Property Source As IList(Of Employee)
            Get
                Return _Source
            End Get

            Private Set(ByVal value As IList(Of Employee))
                _Source = value
            End Set
        End Property

        Public Sub New()
            Source = EmployeesData.DataSource
            Dim cities As List(Of String) = New List(Of String)()
            For Each employee As Employee In Source
                If Not cities.Contains(employee.City) Then cities.Add(employee.City)
            Next

            Me.Cities = cities
            Titles = New List(Of String)() From {"Mr.", "Ms."}
            Dim ids As List(Of Integer) = New List(Of Integer)()
            For Each employee As Employee In Source
                If Not ids.Contains(employee.Id) Then ids.Add(employee.Id)
            Next

            Me.Ids = ids
        End Sub
    End Class

    <XmlRoot("Employees")>
    Public Class EmployeesData
        Inherits List(Of Employee)

        Public Shared ReadOnly Property DataSource As IList(Of Employee)
            Get
                Dim s As XmlSerializer = New XmlSerializer(GetType(EmployeesData))
#If SILVERLIGHT
                return (List<Employee>)s.Deserialize(Assembly.GetExecutingAssembly().GetManifestResourceStream("SLColumnTemplateSelectorExample.EmployeesWithPhoto.xml"));
#Else
                Return CType(s.Deserialize(Assembly.GetExecutingAssembly().GetManifestResourceStream("ColumnTemplateSelectorExample.EmployeesWithPhoto.xml")), List(Of Employee))
#End If
            End Get
        End Property
    End Class

    Public Class Employee

        <ColumnGeneratorTemplateNameAttribute("IdTemplate")>
        Public Property Id As Integer

        Public Property FirstName As String

        <ColumnGeneratorTemplateNameAttribute("CityTemplate")>
        Public Property City As String

        Public Property BirthDate As Date

        <ColumnGeneratorTemplateNameAttribute("TitleTemplate")>
        Public Property Title As String

        <ColumnGeneratorTemplateNameAttribute("ImageTemplate"), Display(Name:="Photo")>
        Public Property ImageData As Byte()
    End Class
End Namespace
