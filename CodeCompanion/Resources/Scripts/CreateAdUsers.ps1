function CreateNewAdUser([string]$username, [string]$givenName, [string]$surName, [string]$displayName)
{
    Write-Host ""
    
    $WingtipDomain = "DC=wingtip,DC=com"
    $WingtipOUUsers = "CN=Users,"+$WingtipDomain
    $RDPGroup = "Remote Desktop Users"
    $password = ConvertTo-SecureString -AsPlainText "Password1" -Force

    $existingUser = Get-ADUser -Filter {SamAccountName -eq $username}

    Write-Host "username-" $username
    Write-Host "givenName-" $givenName
    Write-Host "surName-" $surName
    Write-Host "display name-" $displayName

    if ($existingUser -eq $null)   # if user does not exist
    {
        Write-Host "   Creating user $username in $WingtipDomain" -ForegroundColor Gray
        # create user
        $userPrincipalName = $username+"@wingtip.com"
        $newUser = New-ADUser -Path $WingtipOUUsers -SamAccountName $username -Name $displayName -DisplayName $displayName -GivenName $givenName -Surname $surName -EmailAddress $userPrincipalName -UserPrincipalName $userPrincipalName -Enabled $true -ChangePasswordAtLogon $false -PassThru -PasswordNeverExpires $true -AccountPassword $password
        Write-Host "   User $newUsername created." -ForegroundColor Green
        
        # add user to RDP group
        Write-Host "   Adding user to RDP group" -ForegroundColor Gray
        Add-ADGroupMember -Identity $RDPGroup -Members $username
        Write-Host "   User $newUsername added to AD group '$RDPGroup'" -ForegroundColor Green
    } else { Write-Host "   User $newUsername already exists as '$existingUser.Name' ... skipping this user." -ForegroundColor Gray }
}


Import-Module ActiveDirectory

# create user WINGTIP\ken
CreateNewAdUser "ken" "Ken" "Sanchez" "Ken Sanchez"
CreateNewAdUser "michael" "Michael" "Sullivan" "Michael Sullivan"
CreateNewAdUser "rob" "Rob" "Walters" "Rob Walters"
CreateNewAdUser "janice" "Janice" "Galvin" "Janice Galvin"
CreateNewAdUser "jill" "Jill" "Williams" "Jill Williams"