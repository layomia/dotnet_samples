﻿'<Snippet1>
Imports System
Imports System.Threading
Imports System.Security.AccessControl
Imports System.Security.Principal

Public Class Example

    Public Shared Sub Main()

        ' Create a string representing the current user.
        Dim user As String = Environment.UserDomainName _ 
            & "\" & Environment.UserName

        ' Create a security object that grants no access.
        Dim mSec As New EventWaitHandleSecurity()

        ' Add a rule that grants the current user the 
        ' right to wait on or signal the event, and to 
        ' read its permissions.
        Dim rule As New EventWaitHandleAccessRule(user, _
            EventWaitHandleRights.Synchronize _
            Or EventWaitHandleRights.Modify _
            Or EventWaitHandleRights.ReadPermissions, _
            AccessControlType.Allow)
        mSec.AddAccessRule(rule)

        ' Add a rule that denies the current user the 
        ' right to change permissions on the event.
        rule = New EventWaitHandleAccessRule(user, _
            EventWaitHandleRights.ChangePermissions, _
            AccessControlType.Deny)
        mSec.AddAccessRule(rule)

        ' Display the rules in the security object.
        ShowSecurity(mSec)

        ' Create a rule that grants the current user 
        ' the right to read permissions on the event, and
        ' take ownership of the event. Use this rule to 
        ' remove the right to read permissions from the 
        ' Allow rule for the current user. The inclusion 
        ' of the right to take ownership has no effect.
        rule = New EventWaitHandleAccessRule(user, _
            EventWaitHandleRights.TakeOwnership _
            Or EventWaitHandleRights.ReadPermissions, _
            AccessControlType.Allow)
        mSec.RemoveAccessRule(rule)

        ShowSecurity(mSec)
        
    End Sub 

    Private Shared Sub ShowSecurity(ByVal security As EventWaitHandleSecurity)
        Console.WriteLine(vbCrLf & "Current access rules:" & vbCrLf)

        For Each ar As EventWaitHandleAccessRule In _
            security.GetAccessRules(True, True, GetType(NTAccount))

            Console.WriteLine("        User: {0}", ar.IdentityReference)
            Console.WriteLine("        Type: {0}", ar.AccessControlType)
            Console.WriteLine("      Rights: {0}", ar.EventWaitHandleRights)
            Console.WriteLine()
        Next

    End Sub
End Class 

'This code example produces output similar to following:
'
'Current access rules:
'
'        User: TestDomain\TestUser
'        Type: Deny
'      Rights: ChangePermissions
'
'        User: TestDomain\TestUser
'        Type: Allow
'      Rights: Modify, ReadPermissions, Synchronize
'
'
'Current access rules:
'
'        User: TestDomain\TestUser
'        Type: Deny
'      Rights: ChangePermissions
'
'        User: TestDomain\TestUser
'        Type: Allow
'      Rights: Modify, Synchronize
'</Snippet1>
