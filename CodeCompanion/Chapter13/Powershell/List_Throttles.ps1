Add-PSSnapin Microsoft.SharePoint.PowerShell -ErrorAction SilentlyContinue
$bdc = Get-SPServiceApplicationProxy | Where {$_ -match "Business Data Connectivity"}
Get-SPBusinessDataCatalogThrottleConfig -ThrottleType Connections -Scope Global -ServiceApplicationProxy $bdc

#Scope        : Global
#ThrottleType : Connections
#Enforced     : True
#Default      : 100
#Max          : 500

Get-SPBusinessDataCatalogThrottleConfig -ThrottleType Items -Scope Database -ServiceApplicationProxy $bdc

#Scope        : Database
#ThrottleType : Items
#Enforced     : True
#Default      : 2000
#Max          : 25000

Get-SPBusinessDataCatalogThrottleConfig -ThrottleType Timeout -Scope Database -ServiceApplicationProxy $bdc

#Scope        : Database
#ThrottleType : Timeout
#Enforced     : True
#Default      : 60000
#Max          : 600000

Get-SPBusinessDataCatalogThrottleConfig -ThrottleType Size -Scope Wcf -ServiceApplicationProxy $bdc

#Scope        : Wcf
#ThrottleType : Size
#Enforced     : True
#Default      : 3000000
#Max          : 150000000

Get-SPBusinessDataCatalogThrottleConfig -ThrottleType Timeout -Scope Wcf -ServiceApplicationProxy $bdc

#Scope        : Wcf
#ThrottleType : Timeout
#Enforced     : True
#Default      : 60000
#Max          : 600000