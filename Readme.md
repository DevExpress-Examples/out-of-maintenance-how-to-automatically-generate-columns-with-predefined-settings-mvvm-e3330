# How to automatically generate columns with predefined settings (MVVM)


<p><em><strong>Update:</strong></em><br><em>In version 13.2, we significantly improved Data Annotation attribute support for our controls. You can use standard Data Annotation attributes as well as custom ones provided with our MVVM Framework to specify editors and other settings for grid columns. Take a look at  the <a href="https://documentation.devexpress.com/#WPF/CustomDocument16863">Data Annotation Attributes</a> and <a href="https://documentation.devexpress.com/#WPF/CustomDocument8834">Applying Data Annotations</a> articles to learn more.</em><br><br><br>If the grid's AutoPopulateColumns option is turned on, the grid automatically generates columns for all fields in a data source. In this instance, cell editors are automatically created based on the type of column values. For instance, if a column is bound to a field that contains DateTime values, the grid creates a date editor for it. If a column is bound to a field that contains numeric data, the numeric editor is used. Otherwise, the text editor is used. Default editors are created dynamically when requested and always have the default behavior.</p>
<p><br> This example shows how to automatically generate columns with predefined settings.<br> Column settings are specified via templates defined within Grid Control's resources:</p>


```xml
<UserControl.Resources>
    <dxg:ColumnTemplateSelector x:Key="TemplateSelector"/>
    <Style x:Key="ColumnStyle" TargetType="dxg:GridColumn">
        <Setter Property="MinWidth" Value="100"/>
    </Style>
</UserControl.Resources>
 <dxg:GridControl x:Name="grid" AutoGenerateColumns="AddNew"
                    ColumnGeneratorStyle="{StaticResource ColumnStyle}"
                    ColumnGeneratorTemplateSelector="{StaticResource ResourceKey=TemplateSelector}"
                    ItemsSource="{Binding Source}">
    <dxg:GridControl.Resources>
    <DataTemplate x:Key="CityTemplate">
        <ContentControl>
            <dxg:GridColumn x:Name="c1" Visible="True">
                <dxg:GridColumn.EditSettings>
                    <dxe:ComboBoxEditSettings ItemsSource="{Binding Control.DataContext.Cities}"/>
                </dxg:GridColumn.EditSettings>
            </dxg:GridColumn>
        </ContentControl>
    </DataTemplate>
...  

```


<p>In a model that describes a data item, use <strong>ColumnGeneratorTemplateNameAttribute</strong> to specify which column template is used to present the corresponding data field:</p>


```cs
[ColumnGeneratorTemplateNameAttribute("CityTemplate")]
public string City { get; set; }


```


<p>As a result, when the Grid Control creates a column, the ColumnTemplateSelector object specified via the grid's ColumnGeneratorTemplateSelector property returns the predefined column template based on <em>ColumnGeneratorTemplateNameAttribute</em> applied to the data field. If this attribute is not applied, the grid creates a column with default settings.</p>
<p><br><br>To learn more on how to implement similar functionality in Silverlight, refer to the <a href="https://www.devexpress.com/Support/Center/p/T246737">T246737</a> example.</p>

<br/>


