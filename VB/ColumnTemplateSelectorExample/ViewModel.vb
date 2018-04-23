Imports System
Imports System.ComponentModel
Imports System.Xml.Serialization
Imports System.Collections.Generic
Imports System.Reflection
Imports DevExpress.Xpf.Grid
Imports System.ComponentModel.DataAnnotations

Namespace Model
    Public Class ViewModel
        Private privateIds As List(Of Integer)
        Public Property Ids() As List(Of Integer)
            Get
                Return privateIds
            End Get
            Private Set(ByVal value As List(Of Integer))
                privateIds = value
            End Set
        End Property
        Private privateCities As List(Of String)
        Public Property Cities() As List(Of String)
            Get
                Return privateCities
            End Get
            Private Set(ByVal value As List(Of String))
                privateCities = value
            End Set
        End Property
        Private privateTitles As List(Of String)
        Public Property Titles() As List(Of String)
            Get
                Return privateTitles
            End Get
            Private Set(ByVal value As List(Of String))
                privateTitles = value
            End Set
        End Property
        Private privateSource As IList(Of Employee)
        Public Property Source() As IList(Of Employee)
            Get
                Return privateSource
            End Get
            Private Set(ByVal value As IList(Of Employee))
                privateSource = value
            End Set
        End Property
        Public Sub New()
            Source = EmployeesData.DataSource

            Dim cities_Renamed As New List(Of String)()
            For Each employee As Employee In Source
                If Not cities_Renamed.Contains(employee.City) Then
                    cities_Renamed.Add(employee.City)
                End If
            Next employee
            Cities = cities_Renamed
            Titles = New List(Of String)() From {"Mr.", "Ms."}

            Dim ids_Renamed As New List(Of Integer)()
            For Each employee As Employee In Source
                If Not ids_Renamed.Contains(employee.Id) Then
                    ids_Renamed.Add(employee.Id)
                End If
            Next employee
            Ids = ids_Renamed
        End Sub
    End Class

    <XmlRoot("Employees")> _
    Public Class EmployeesData
        Inherits List(Of Employee)

        Public Shared ReadOnly Property DataSource() As IList(Of Employee)
            Get

                Dim s As New XmlSerializer(GetType(EmployeesData))
#If SILVERLIGHT Then
                Return DirectCast(s.Deserialize(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("SLColumnTemplateSelectorExample.EmployeesWithPhoto.xml")), List(Of Employee))
#Else
                Return DirectCast(s.Deserialize(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("ColumnTemplateSelectorExample.EmployeesWithPhoto.xml")), List(Of Employee))
#End If
            End Get
        End Property
    End Class

    Public Class Employee
        <ColumnGeneratorTemplateNameAttribute("IdTemplate")> _
        Public Property Id() As Integer
        Public Property FirstName() As String
        <ColumnGeneratorTemplateNameAttribute("CityTemplate")> _
        Public Property City() As String
        Public Property BirthDate() As Date
        <ColumnGeneratorTemplateNameAttribute("TitleTemplate")> _
        Public Property Title() As String
        <ColumnGeneratorTemplateNameAttribute("ImageTemplate"), Display(Name := "Photo")> _
        Public Property ImageData() As Byte()
    End Class
End Namespace
