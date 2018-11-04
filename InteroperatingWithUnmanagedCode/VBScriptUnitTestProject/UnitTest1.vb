Imports Task2

<TestClass()> Public Class UnitTest1

    <TestMethod()> Public Sub TestDeleteHibernationFile()
        'This is just an example string and could be anything, it maps to fileToCopy in your code.
        Dim sourcePath As String = "c:\hiberfil.sys"
        Dim saveDirectory As String = "c:\"

        Dim wrapper As New PowrProfWrapper
        wrapper.ReserveHibernationFile(False)


        'Get the filename of the original file without the directory on it
        Dim filename As String = System.IO.Path.GetFileName(sourcePath)

        'Combines the saveDirectory and the filename to get a fully qualified path.
        Dim savePath As String = System.IO.Path.Combine(saveDirectory, filename)

        If System.IO.File.Exists(savePath) Then
            Assert.Fail()
        Else
        End If
    End Sub

    <TestMethod()> Public Sub TestTurnOnSleepMode()
        Dim wrapper As New PowrProfWrapper
        wrapper.TurnOnSleepMode()
    End Sub

End Class