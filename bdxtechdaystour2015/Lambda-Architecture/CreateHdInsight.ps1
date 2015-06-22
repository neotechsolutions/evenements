##################### Begin Edits ####################
#region edits
param
(
    # NOTE: All the storage accounts and containers need to be created on the same data center as the HDInsight cluster and would need to be created prior to running the script
    # They can be created from the Azure Management Portal

    # This is the name of your Azure Subscription that will be used for provisiong Azure HDInsight
    [string]$PrimarySubscriptionName="<name of your subscription>",
    # This is the primary storage account that needs to be created on the same data center as your HDInsight Cluster
    # This needs to be pre-provisioned prior to running the script
    [string]$PrimaryStorageAccount="<replace with the name of your storage account where hdinisght will be install>",
	[string]$SecondaryStorageAccount="<replace with the name of your storage account where data are stored>", 
    # This is the name of the container in the primary storage account. This needs to be pre-provisioned prior to running this script.
    [string]$PrimaryStorageContainer="hadoop",
    # This is the data center where the HDInsight cluster and storage accounts are provisioned
    [string]$HDInsightClusterLocation="West Europe",
    #This is the name of the HDInsight cluster
    [string]$HDInsightClusterName="techdaystour",
    [string]$HiveSQLDatabaseServerName = "<database_server_name>",
    [string]$HiveSQLUserName = "<sql user>",
    [string]$HiveSQLPassword = "<sql password>",
    [string]$HiveSQLDatabaseName = "HiveMetaDataStore",
    #This is the size of the cluster # of data nodes
    [string]$HDIClusterSize = "2",
    #This is the version of the HDInsight cluster. Default version is 2.1, however you can choose to create 3.0 or 3.1 clusters
    # Please refer to this page for HDInsight/HDP/Hadoop version mapping - http://azure.microsoft.com/en-us/documentation/articles/hdinsight-component-versioning/
    [string]$HDInsightClusterVersion="3.2",
    # HDInsight cluster admin user name and password need to be specified here. This will be used for subsequent admin logins to the cluster for job submissions. 
    [string]$MyHDInsightUserName = "admin",
    [string]$MyHDInsightPwd = "#T3chdaysT0ur!!"
)
#endregion edits
#################### End Edits ####################### 

# Credentials
Add-AzureAccount
Select-AzureSubscription $PrimarySubscriptionName
 
$HdInsightPwd = ConvertTo-SecureString $MyHDInsightPwd -AsPlainText -Force
$HiveSQLPassword = ConvertTo-SecureString $HiveSQLPassword -AsPlainText -Force
$creds= New-Object System.Management.Automation.PSCredential ($MyHDInsightUserName, $HdInsightPwd)
$hiveCreds =  New-Object System.Management.Automation.PSCredential ($HiveSQLUserName, $HiveSQLPassword)
 
$PrimarySubscriptionID = (Select-AzureSubscription -SubscriptionName $PrimarySubscriptionName).SubscriptionId
$PrimaryStorageAccountKey = Get-AzureStorageKey $PrimaryStorageAccount | %{ $_.Primary }
$SecondaryStorageAccountKey = Get-AzureStorageKey $SecondaryStorageAccount | %{ $_.Primary }
#This will be the credential used for the admin account of the HDInsight Cluster
 
# Set Custom Configuration
$configvalues = new-object 'Microsoft.WindowsAzure.Management.HDInsight.Cmdlet.DataObjects.AzureHDInsightHiveConfiguration'

$configvalues.Configuration = @{}
$configValues.Configuration.Add('hive.execution.engine','tez')
$configValues.Configuration.Add('hive.vectorized.execution.enabled','true')
$configValues.Configuration.Add('hive.vectorized.execution.reduce.enabled','true')
$configValues.Configuration.Add('hive.cbo.enable','true')
$configValues.Configuration.Add('hive.compute.query.using.stats','true')
$configValues.Configuration.Add('hive.stats.fetch.column.stats','true')
$configValues.Configuration.Add('hive.stats.fetch.partition.stats','true')
$configValues.Configuration.Add('hive.stats.autogather','true')
$configValues.Configuration.Add('hive.exec.dynamic.partition','true')
$configValues.Configuration.Add('hive.exec.dynamic.partition.mode','nonstrict')

# Create Azure HDInsight Cluster
$creds = Get-Credential -Credential $creds
$hiveCreds =  Get-Credential -Credential $hiveCreds
# $config = New-AzureHDInsightClusterConfig -HeadNodeVMSize 'Standard_DS3' -DataNodeVMSize 'Standard_DS3'
try {
$config = New-AzureHDInsightClusterConfig -ClusterSizeInNodes $HDIClusterSize -ClusterType Hadoop |
Set-AzureHDInsightDefaultStorage -StorageAccountName "$PrimaryStorageAccount.blob.core.windows.net" -StorageAccountKey $PrimaryStorageAccountKey -StorageContainerName $PrimaryStorageContainer |
Add-AzureHDInsightStorage -StorageAccountName "$SecondaryStorageAccount.blob.core.windows.net" -StorageAccountKey $SecondaryStorageAccountKey |
Add-AzureHDInsightMetastore -SqlAzureServerName "$HiveSQLDatabaseServerName.database.windows.net" -DatabaseName $HiveSQLDatabaseName -Credential $hiveCreds -MetastoreType HiveMetastore |
Add-AzureHDInsightConfigValues -Hive $configvalues
New-AzureHDInsightCluster -Subscription $PrimarySubscriptionID -Config $config -Credential $creds -Name $HDInsightClusterName  -Location $HDInsightClusterLocation -Version $HDInsightClusterVersion
}
catch [Exception] {
    return $_.Exception.Message
}
#endregion code
