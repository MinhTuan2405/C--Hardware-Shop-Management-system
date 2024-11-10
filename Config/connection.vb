Imports MySql.Data.MySqlClient
Module connection
    Public Function mysqldb() As MySqlConnection
        Return New MySqlConnection("server=localhost;user id=root;database=dbtreeways")
    End Function
    Public con As MySqlConnection = mysqldb()
End Module
