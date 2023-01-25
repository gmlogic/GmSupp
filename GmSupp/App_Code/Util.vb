Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Imports System.Reflection
Imports System.Security.Cryptography
Imports System.Text
Imports System.Web.Security

Public Class Util

    'Public Shared Function GetUserNo(UserName As String) As Integer
    '    Dim userNo As Integer = 0
    '    Dim u As MembershipUser = Membership.GetUser()
    '    Select Case u.UserName
    '        Case "gmlogic"
    '            userNo = 99
    '        Case "admin"
    '            userNo = 1
    '    End Select
    '    Return userNo

    '    'Throw New NotImplementedException()
    'End Function
#Region "LinQ"
    'Public Shared Function LINQToDataSet(ByVal myDB As GmParkNetDataContext, ByVal item As IQueryable) As DataSet
    '    Dim cmd As SqlCommand = TryCast(myDB.GetCommand(item), SqlCommand)

    '    Dim oDataTable As New DataTable()
    '    Dim oDataAdapter As New SqlDataAdapter(cmd)
    '    oDataAdapter.Fill(oDataTable)

    '    Dim oDataSet As New DataSet()
    '    oDataSet.Tables.Add(oDataTable)
    '    Return oDataSet
    'End Function
    'Public Shared Function LINQToDataTable(ByVal myDB As GmParkNetDataContext, ByVal item As IQueryable) As DataTable
    '    Dim cmd As SqlCommand = TryCast(myDB.GetCommand(item), SqlCommand)

    '    Dim oDataTable As New DataTable()
    '    Dim oDataAdapter As New SqlDataAdapter(cmd)
    '    oDataAdapter.Fill(oDataTable)

    '    Return oDataTable
    'End Function
    Public Shared Function LINQToDataTable(Of T)(varlist As IEnumerable(Of T)) As DataTable
        Dim dtReturn As New DataTable()

        ' column names 
        Dim oProps As PropertyInfo() = Nothing

        If varlist Is Nothing Then
            Return dtReturn
        End If
        Dim dr As DataRow = Nothing
        Dim pi1 As PropertyInfo = Nothing
        Try
            For Each rec As T In varlist
                ' Use reflection to get property names, to create table, Only first time, others will follow 
                If oProps Is Nothing Then
                    oProps = DirectCast(rec.[GetType](), Type).GetProperties()
                    For Each pi As PropertyInfo In oProps
                        Dim colType As Type = pi.PropertyType

                        If (colType.IsGenericType) AndAlso (colType.GetGenericTypeDefinition() Is GetType(Nullable(Of ))) Then
                            colType = colType.GetGenericArguments()(0)
                        End If

                        dtReturn.Columns.Add(New DataColumn(pi.Name, colType))
                    Next
                End If

                dr = dtReturn.NewRow()

                For Each pi1 In oProps
                    If Not IsDBNull(dr("FINDOC")) AndAlso dr("FINDOC") = 34229 Then
                        dr("FINDOC") = 34229
                    End If
                    dr(pi1.Name) = If(pi1.GetValue(rec, Nothing) Is Nothing, DBNull.Value, pi1.GetValue(rec, Nothing))
                Next

                dtReturn.Rows.Add(dr)
            Next
        Catch ex As Exception

        End Try
        Return dtReturn
    End Function
    Public Shared Function DataTableFromIEnumerable(ien As IEnumerable) As DataTable
        Dim dt As New DataTable()
        Dim t As Type = Nothing
        Dim pis As PropertyInfo() = Nothing
        Dim pi As PropertyInfo = Nothing
        Try
            For Each obj As Object In ien
                t = obj.[GetType]()
                pis = t.GetProperties()
                If dt.Columns.Count = 0 Then
                    For Each pi In pis
                        dt.Columns.Add(pi.Name, pi.PropertyType)
                    Next
                End If

                Dim dr As DataRow = dt.NewRow()
                For Each pi In pis
                    Dim value As Object = pi.GetValue(obj, Nothing)
                    dr(pi.Name) = value
                Next

                dt.Rows.Add(dr)
            Next
        Catch ex As Exception

        End Try
        Return dt
    End Function
    Private Sub Create_DATASET()
        'test1()
        Try
            'Αντί για Object μπαίνει η class πχ AvmResults από τη οποία θέλουμε να δημιουργήσουμε την DataSet
            Dim avm As New Object 'AvmResults
            Dim Type1 As Type = avm.GetType()
            Dim properties As PropertyInfo() = avm.GetType().GetProperties() 'Type1.GetProperties()

            For Each prop As PropertyInfo In properties
                '<xs:element name="DataColumn1" msprop:Generator_ColumnVarNameInTable="columnDataColumn1" msprop:Generator_ColumnPropNameInRow="DataColumn1" msprop:Generator_ColumnPropNameInTable="DataColumn1Column" msprop:Generator_UserColumnName="DataColumn1" type="xs:string" minOccurs="0" />
                Dim ss As String = ""
                ss = "<xs:element name=""" & prop.Name & """ msprop:Generator_ColumnVarNameInTable=""" & "column" & prop.Name & """ msprop:Generator_ColumnPropNameInRow=""" & prop.Name & """ msprop:Generator_ColumnPropNameInTable=""" & prop.Name & "Column" & """ msprop:Generator_UserColumnName=""" & prop.Name & """ type=""" & "xs:" & prop.PropertyType.Name.ToString.ToLower & """ minOccurs=""0"" />"
                'Debug.Print("Name: " + prop.Name + " Type:" + prop.PropertyType.Name) ' + ", Value: " + prop.GetValue(obj, null))
                Debug.Print(ss)
            Next


        Catch ex As Exception

        End Try
    End Sub
#End Region
End Class
'''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
' SAMPLE: Symmetric key encryption and decryption using Rijndael algorithm.
' 
' To run this sample, create a new Visual Basic.NET project using the Console 
' Application template and replace the contents of the Module1.vb file with 
' the code below.
' 
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
' EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED 
' WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
' 
' Copyright (C) 2002 Obviex(TM). All rights reserved.
'
'Imports System
'Imports System.IO
'Imports System.Text
'Imports System.Security.Cryptography

'Module Module1

''' <summary>
''' This class uses a symmetric key algorithm (Rijndael/AES) to encrypt and 
''' decrypt data. As long as encryption and decryption routines use the same 
''' parameters to generate the keys, the keys are guaranteed to be the same.
''' The class uses static functions with duplicate code to make it easier to 
''' demonstrate encryption and decryption logic. In a real-life application, 
''' this may not be the most efficient way of handling encryption, so - as 
''' soon as you feel comfortable with it - you may want to redesign this class.
''' </summary>
Public  Class RijndaelSimple


    ''' <summary>
    ''' Encrypts specified plaintext using Rijndael symmetric key algorithm
    ''' and returns a base64-encoded result.
    ''' </summary>
    ''' <param name="plainText">
    ''' Plaintext value to be encrypted.
    ''' </param>
    ''' <param name="passPhrase">
    ''' Passphrase from which a pseudo-random password will be derived. The 
    ''' derived password will be used to generate the encryption key. 
    ''' Passphrase can be any string. In this example we assume that this 
    ''' passphrase is an ASCII string.
    ''' If passPhrase = "" Then regKey.GetValue("Key1").ToString
    ''' </param>
    ''' <param name="saltValue">
    ''' Salt value used along with passphrase to generate password. Salt can 
    ''' be any string. In this example we assume that salt is an ASCII string.
    ''' If saltValue = "" Then regKey.GetValue("Key2").ToString
    ''' </param>
    ''' <param name="hashAlgorithm">
    ''' Hash algorithm used to generate password. Allowed values are: "MD5" and
    ''' "SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
    ''' </param>
    ''' <param name="passwordIterations">
    ''' Number of iterations used to generate password. One or two iterations
    ''' should be enough.
    ''' </param>
    ''' <param name="initVector">
    ''' Initialization vector (or IV). This value is required to encrypt the 
    ''' first block of plaintext data. For RijndaelManaged class IV must be 
    ''' exactly 16 ASCII characters long.
    ''' </param>
    ''' <param name="keySize">
    ''' Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
    ''' Longer keys are more secure than shorter keys.
    ''' </param>
    ''' <returns>
    ''' Encrypted value formatted as a base64-encoded string.
    ''' </returns>
    Public Shared Function Encrypt(ByVal plainText As String,
                                       ByVal passPhrase As String,
                                       ByVal saltValue As String,
                                       ByVal hashAlgorithm As String,
                                       ByVal passwordIterations As Integer,
                                       ByVal initVector As String,
                                       ByVal keySize As Integer) _
                               As String
        'regKey.SetValue("MyApp", RijndaelSimple.Encrypt(myApp, "", "", "SHA1", 2, "@1B2c3D4e5F6g7H8", 256)) ', Microsoft.Win32.RegistryValueKind.String)
        'Pass Standart passPhrase saltValue
        If passPhrase = String.Empty Or saltValue = String.Empty Then
            Dim regKey As Microsoft.Win32.RegistryKey
            regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\GmLogic\GmWinNet\CONTRACT") ', True)
            passPhrase = "5Fs6F+UBqAEPLSItSrkcxg==" 'regKey.GetValue("Key1").ToString
            saltValue = "UwmyTTIAdRmYBlQarD7e4w==" 'regKey.GetValue("Key2").ToString
            If Not IsNothing(regKey) Then
                passPhrase = regKey.GetValue("Key1").ToString
                saltValue = regKey.GetValue("Key2").ToString
            End If
        End If
        ' Convert strings into byte arrays.
        ' Let us assume that strings only contain ASCII codes.
        ' If strings include Unicode characters, use Unicode, UTF7, or UTF8 
        ' encoding.
        Dim initVectorBytes As Byte()
        initVectorBytes = Encoding.ASCII.GetBytes(initVector)

        Dim saltValueBytes As Byte()
        saltValueBytes = Encoding.ASCII.GetBytes(saltValue)

        ' Convert our plaintext into a byte array.
        ' Let us assume that plaintext contains UTF8-encoded characters.
        Dim plainTextBytes As Byte()
        plainTextBytes = Encoding.UTF8.GetBytes(plainText)

        ' First, we must create a password, from which the key will be derived.
        ' This password will be generated from the specified passphrase and 
        ' salt value. The password will be created using the specified hash 
        ' algorithm. Password creation can be done in several iterations.

        'Dim pdb As PasswordDeriveBytes
        'pdb = New PasswordDeriveBytes(passPhrase,
        '                                       saltValueBytes,
        '                                       hashAlgorithm,
        '                                       passwordIterations)

        '' Use the password to generate pseudo-random bytes for the encryption
        '' key. Specify the size of the key in bytes (instead of bits).
        'Dim keyBytes As Byte() ' = Nothing
        'keyBytes = pdb.GetBytes(keySize / 8)

        Dim password As Rfc2898DeriveBytes = New Rfc2898DeriveBytes(passPhrase, saltValueBytes, passwordIterations)
        Dim keyBytes As Byte()
        keyBytes = password.GetBytes(CInt(keySize \ 8))


        ' Create uninitialized Rijndael encryption object.
        Dim symmetricKey As RijndaelManaged
        symmetricKey = New RijndaelManaged()

        ' It is reasonable to set encryption mode to Cipher Block Chaining
        ' (CBC). Use default options for other symmetric key parameters.
        symmetricKey.Mode = CipherMode.CBC

        ' Generate encryptor from the existing key bytes and initialization 
        ' vector. Key size will be defined based on the number of the key 
        ' bytes.
        Dim encryptor As ICryptoTransform
        encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes)

        ' Define memory stream which will be used to hold encrypted data.
        Dim memoryStream As MemoryStream
        memoryStream = New MemoryStream()

        ' Define cryptographic stream (always use Write mode for encryption).
        Dim cryptoStream As CryptoStream
        cryptoStream = New CryptoStream(memoryStream,
                                            encryptor,
                                            CryptoStreamMode.Write)
        ' Start encrypting.
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length)

        ' Finish encrypting.
        cryptoStream.FlushFinalBlock()

        ' Convert our encrypted data from a memory stream into a byte array.
        Dim cipherTextBytes As Byte()
        cipherTextBytes = memoryStream.ToArray()

        ' Close both streams.
        memoryStream.Close()
        cryptoStream.Close()

        ' Convert encrypted data into a base64-encoded string.
        Dim cipherText As String
        cipherText = Convert.ToBase64String(cipherTextBytes)

        ' Return encrypted string.
        Encrypt = cipherText
    End Function

    '''<summary>
    '''Decrypts specified ciphertext using Rijndael symmetric key algorithm.
    '''</summary>
    '''<param name="cipherText">
    '''Base64-formatted ciphertext value.
    '''</param>
    '''<param name="passPhrase">
    '''Passphrase from which a pseudo-random password will be derived. The 
    '''derived password will be used to generate the encryption key. 
    '''Passphrase can be any string. In this example we assume that this 
    '''passphrase is an ASCII string.
    '''If passPhrase = "" Then regKey.GetValue("Key1").ToString
    '''</param>
    '''<param name="saltValue">
    '''Salt value used along with passphrase to generate password. Salt can 
    '''be any string. In this example we assume that salt is an ASCII string.
    '''If saltValue = "" Then regKey.GetValue("Key2").ToString
    '''</param>
    '''<param name="hashAlgorithm">
    '''Hash algorithm used to generate password. Allowed values are: "MD5" and
    '''"SHA1". SHA1 hashes are a bit slower, but more secure than MD5 hashes.
    '''</param>
    '''<param name="passwordIterations">
    '''Number of iterations used to generate password. One or two iterations
    '''should be enough.
    '''</param>
    '''<param name="initVector">
    '''Initialization vector (or IV). This value is required to encrypt the 
    '''first block of plaintext data. For RijndaelManaged class IV must be 
    '''exactly 16 ASCII characters long.
    '''</param>
    '''<param name="keySize">
    '''Size of encryption key in bits. Allowed values are: 128, 192, and 256. 
    '''Longer keys are more secure than shorter keys.
    '''</param>
    '''<returns>
    '''Decrypted string value.
    '''</returns>
    '''<remarks>
    '''Most of the logic in this function is similar to the Encrypt 
    '''logic. In order for decryption to work, all parameters of this function
    '''- except cipherText value - must match the corresponding parameters of 
    '''the Encrypt function which was called to generate the 
    '''ciphertext.
    '''</remarks>
    Public Shared Function Decrypt(ByVal cipherText As String,
                                       ByVal passPhrase As String,
                                       ByVal saltValue As String,
                                       ByVal hashAlgorithm As String,
                                       ByVal passwordIterations As Integer,
                                       ByVal initVector As String,
                                       ByVal keySize As Integer) _
                               As String

        'Pass Standart passPhrase saltValue
        If passPhrase = String.Empty Or saltValue = String.Empty Then
            Dim regKey As Microsoft.Win32.RegistryKey
            regKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\GmLogic\GmWinNet\CONTRACT") ', True)
            passPhrase = "5Fs6F+UBqAEPLSItSrkcxg==" 'regKey.GetValue("Key1").ToString
            saltValue = "UwmyTTIAdRmYBlQarD7e4w==" 'regKey.GetValue("Key2").ToString
            If Not IsNothing(regKey) Then
                passPhrase = regKey.GetValue("Key1").ToString
                saltValue = regKey.GetValue("Key2").ToString
            End If
        End If
        ' Convert strings defining encryption key characteristics into byte
        ' arrays. Let us assume that strings only contain ASCII codes.
        ' If strings include Unicode characters, use Unicode, UTF7, or UTF8
        ' encoding.
        Dim initVectorBytes As Byte()
        initVectorBytes = Encoding.ASCII.GetBytes(initVector)

        Dim saltValueBytes As Byte()
        saltValueBytes = Encoding.ASCII.GetBytes(saltValue)

        ' Convert our ciphertext into a byte array.
        Dim cipherTextBytes As Byte()
        cipherTextBytes = Convert.FromBase64String(cipherText)

        ' First, we must create a password, from which the key will be 
        ' derived. This password will be generated from the specified 
        ' passphrase and salt value. The password will be created using
        ' the specified hash algorithm. Password creation can be done in
        ' several iterations.
        'Dim password As PasswordDeriveBytes
        'password = New PasswordDeriveBytes(passPhrase,
        '                                       saltValueBytes,
        '                                       hashAlgorithm,
        '                                       passwordIterations)

        '' Use the password to generate pseudo-random bytes for the encryption
        '' key. Specify the size of the key in bytes (instead of bits).
        'Dim keyBytes As Byte() = Nothing
        'keyBytes = password.GetBytes(keySize / 8)

        Dim password As Rfc2898DeriveBytes = New Rfc2898DeriveBytes(passPhrase, saltValueBytes, passwordIterations)
        Dim keyBytes As Byte()
        keyBytes = password.GetBytes(CInt(keySize \ 8))

        ' Create uninitialized Rijndael encryption object.
        Dim symmetricKey As RijndaelManaged
        symmetricKey = New RijndaelManaged()

        ' It is reasonable to set encryption mode to Cipher Block Chaining
        ' (CBC). Use default options for other symmetric key parameters.
        symmetricKey.Mode = CipherMode.CBC

        ' Generate decryptor from the existing key bytes and initialization 
        ' vector. Key size will be defined based on the number of the key 
        ' bytes.
        Dim decryptor As ICryptoTransform
        decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes)

        ' Define memory stream which will be used to hold encrypted data.
        Dim memoryStream As MemoryStream
        memoryStream = New MemoryStream(cipherTextBytes)

        ' Define memory stream which will be used to hold encrypted data.
        Dim cryptoStream As CryptoStream
        cryptoStream = New CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)

        ' Since at this point we don't know what the size of decrypted data
        ' will be, allocate the buffer long enough to hold ciphertext;
        ' plaintext is never longer than ciphertext.
        Dim plainTextBytes As Byte()
        ReDim plainTextBytes(cipherTextBytes.Length)

        ' Start decrypting.
        Dim decryptedByteCount As Integer
        Try
            decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length)

        Catch ex As Exception
            Return ex.Message
        End Try

        ' Close both streams.
        memoryStream.Close()
        cryptoStream.Close()

        ' Convert decrypted data into a string. 
        ' Let us assume that the original plaintext string was UTF8-encoded.
        Dim plainText As String
        plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount)

        ' Return decrypted string.
        Decrypt = plainText
    End Function

End Class

''' <summary>
''' Summary description for CryptoUtils.
''' </summary>
Public Class CryptoUtils
    Public Shared ReadOnly Property CryptoKey() As String
        Get
            Return My.Settings.CryptoKey ' ConfigurationManager.AppSettings("WebsitePanel.CryptoKey")
        End Get
    End Property

    Public Shared ReadOnly Property EncryptionEnabled() As Boolean
        Get
            Return True ' If((ConfigurationManager.AppSettings("WebsitePanel.EncryptionEnabled") IsNot Nothing), [Boolean].Parse(ConfigurationManager.AppSettings("WebsitePanel.EncryptionEnabled")), True)
        End Get
    End Property

    Public Shared Function Encrypt(InputText As String) As String
        Dim Password As String = CryptoKey

        If Not EncryptionEnabled Then
            Return InputText
        End If

        If InputText Is Nothing Then
            Return InputText
        End If

        ' We are now going to create an instance of the 
        ' Rihndael class.
        Dim RijndaelCipher As New RijndaelManaged()

        ' First we need to turn the input strings into a byte array.
        Dim PlainText As Byte() = System.Text.Encoding.Unicode.GetBytes(InputText)


        ' We are using salt to make it harder to guess our key
        ' using a dictionary attack.
        Dim Salt As Byte() = Encoding.ASCII.GetBytes("UwmyTTIAdRmYBlQarD7e4w==") 'Password.Length.ToString())


        ' The (Secret Key) will be generated from the specified 
        ' password and salt.
        Dim SecretKey As New Rfc2898DeriveBytes(Password, Salt, 2) 'PasswordDeriveBytes(Password, Salt)
        Dim keyBytes = SecretKey.GetBytes(32)

        'Dim password As New Rfc2898DeriveBytes(passPhrase, saltValueBytes, passwordIterations)
        'keyBytes = password.GetBytes(CInt(keySize \ 8))

        Dim initVectorBytes = SecretKey.GetBytes(16) ' ByVal initVector As String "@1B2c3D4e5F6g7H8"

        ' Create a encryptor from the existing SecretKey bytes.
        ' We use 32 bytes for the secret key 
        ' (the default Rijndael key length is 256 bit = 32 bytes) and
        ' then 16 bytes for the IV (initialization vector),
        ' (the default Rijndael IV length is 128 bit = 16 bytes)
        Dim Encryptor As ICryptoTransform = RijndaelCipher.CreateEncryptor(keyBytes, initVectorBytes) 'SecretKey.GetBytes(32), SecretKey.GetBytes(16))


        ' Create a MemoryStream that is going to hold the encrypted bytes 
        Dim memoryStream As New MemoryStream()


        ' Create a CryptoStream through which we are going to be processing our data. 
        ' CryptoStreamMode.Write means that we are going to be writing data 
        ' to the stream and the output will be written in the MemoryStream
        ' we have provided. (always use write mode for encryption)
        Dim cryptoStream As New CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write)

        ' Start the encryption process.
        cryptoStream.Write(PlainText, 0, PlainText.Length)


        ' Finish encrypting.
        cryptoStream.FlushFinalBlock()

        ' Convert our encrypted data from a memoryStream into a byte array.
        Dim CipherBytes As Byte() = memoryStream.ToArray()



        ' Close both streams.
        memoryStream.Close()
        cryptoStream.Close()



        ' Convert encrypted data into a base64-encoded string.
        ' A common mistake would be to use an Encoding class for that. 
        ' It does not work, because not all byte values can be
        ' represented by characters. We are going to be using Base64 encoding
        ' That is designed exactly for what we are trying to do. 
        Dim EncryptedData As String = Convert.ToBase64String(CipherBytes)



        ' Return encrypted string.
        Return EncryptedData
    End Function


    Public Shared Function Decrypt(InputText As String) As String
        Try
            If Not EncryptionEnabled Then
                Return InputText
            End If

            If InputText Is Nothing OrElse InputText = "" Then
                Return InputText
            End If

            Dim Password As String = CryptoKey
            Dim RijndaelCipher As New RijndaelManaged()


            Dim EncryptedData As Byte() = Convert.FromBase64String(InputText)
            Dim Salt As Byte() = Encoding.ASCII.GetBytes("UwmyTTIAdRmYBlQarD7e4w==") 'Password.Length.ToString())


            'Dim SecretKey As New PasswordDeriveBytes(Password, Salt)
            Dim SecretKey As New Rfc2898DeriveBytes(Password, Salt, 2) 'PasswordDeriveBytes(Password, Salt)
            Dim keyBytes = SecretKey.GetBytes(32)

            Dim initVectorBytes = SecretKey.GetBytes(16)

            ' Create a decryptor from the existing SecretKey bytes.
            Dim Decryptor As ICryptoTransform = RijndaelCipher.CreateDecryptor(keyBytes, initVectorBytes) ' SecretKey.GetBytes(32), SecretKey.GetBytes(16))


            Dim memoryStream As New MemoryStream(EncryptedData)

            ' Create a CryptoStream. (always use Read mode for decryption).
            Dim cryptoStream As New CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read)


            ' Since at this point we don't know what the size of decrypted data
            ' will be, allocate the buffer long enough to hold EncryptedData;
            ' DecryptedData is never longer than EncryptedData.
            Dim PlainText As Byte() = New Byte(EncryptedData.Length - 1) {}

            ' Start decrypting.
            Dim DecryptedCount As Integer = cryptoStream.Read(PlainText, 0, PlainText.Length)


            memoryStream.Close()
            cryptoStream.Close()

            ' Convert decrypted data into a string. 
            Dim DecryptedData As String = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount)


            ' Return decrypted string.   
            Return DecryptedData
        Catch
            Return ""
        End Try
    End Function

    Public Shared Function SHA1(plainText As String) As String
        ' Convert plain text into a byte array.
        Dim plainTextBytes As Byte() = Encoding.UTF8.GetBytes(plainText)

        Dim hash As HashAlgorithm = New SHA1Managed()



        ' Compute hash value of our plain text with appended salt.
        Dim hashBytes As Byte() = hash.ComputeHash(plainTextBytes)

        ' Return the result.
        Return Convert.ToBase64String(hashBytes)
    End Function
End Class
