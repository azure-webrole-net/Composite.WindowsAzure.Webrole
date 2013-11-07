Composite.WindowsAzure.Webrole
==============================

The deployment packages for Composite Windows Azure. Windows Azure SDK 2.2

### Composite Windows Azure Simpleboot

This is the deployment for the very basic setup with simplest set of configuration settings.
```
<?xml version="1.0" encoding="utf-8"?>
<ServiceConfiguration serviceName="Composite.WindowsAzure.Webrole.SimpleBoot.Cloud" xmlns="http://schemas.microsoft.com/ServiceHosting/2008/10/ServiceConfiguration" osFamily="3" osVersion="*" schemaVersion="2013-10.2.2">
  <Role name="Composite.WindowsAzure.Webrole.SimpleBoot">
    <Instances count="1" />
    <ConfigurationSettings>
      <!-- Add your storage connection to where you want your diagnostics logs sendt-->
      <Setting name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" value="UseDevelopmentStorage=true" />
      <!-- Change the value of Composite.WindowsAzure.WebRole.DeploymentName: Sugested is to fill out the pattern.-->
      <Setting name="Composite.WindowsAzure.WebRole.DeploymentName" value="deployment-{name}-{yymmdd}" />
      <!-- The dispaly name used ect by composite azure publisher -->
      <Setting name="Composite.WindowsAzure.WebRole.DisplayName" value="Composite Windows Azure Webrole" />
      <!--The connection setting of where to have websites and deployment files.-->
      <Setting name="Composite.WindowsAzure.WebRole.Storage.ConnectionString" value="UseDevelopmentStorage=true" />
    </ConfigurationSettings>
  </Role>
</ServiceConfiguration>
```


### Composite Windows Azure Simpleboot with SSL
You need to add a certificate, upload it to azure or use publish in VS.

```
<?xml version="1.0" encoding="utf-8"?>
<ServiceConfiguration serviceName="Composite.WindowsAzure.Webrole.SimpleBoot.Ssl.Cloud" xmlns="http://schemas.microsoft.com/ServiceHosting/2008/10/ServiceConfiguration" osFamily="3" osVersion="*" schemaVersion="2013-10.2.2">
  <Role name="Composite.WindowsAzure.Webrole.SimpleBoot">
    <Instances count="1" />
    <ConfigurationSettings>
      <!-- Add your storage connection to where you want your diagnostics logs sendt-->
      <Setting name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" value="UseDevelopmentStorage=true" />
      <!-- Change the value of Composite.WindowsAzure.WebRole.DeploymentName: Sugested is to fill out the pattern.-->
      <Setting name="Composite.WindowsAzure.WebRole.DeploymentName" value="deployment-{name}-{yymmdd}" />
      <!-- The dispaly name used ect by composite azure publisher -->
      <Setting name="Composite.WindowsAzure.WebRole.DisplayName" value="Composite Windows Azure Webrole" />
      <!--The connection setting of where to have websites and deployment files.-->
      <Setting name="Composite.WindowsAzure.WebRole.Storage.ConnectionString" value="UseDevelopmentStorage=true" />
    </ConfigurationSettings>
    <Certificates>
      <Certificate name="Composite.WindowsAzure.Certificates.Ssl" thumbprint="" thumbprintAlgorithm="sha1" />
    </Certificates>
  </Role>
</ServiceConfiguration>
```

