Composite.WindowsAzure.Webrole
==============================

The deployment packages for Composite Windows Azure. Windows Azure SDK 2.2

### Install Guide
Fork, Download or clone the soruce. We have packed it under releases for downloading a stable release. Open the solution in visual studio and edit the settings according to comments.

This blob post give you alittle insigth into how the webrole works: http://www.s-innovations.net/Blog/2013/11/07/Composite-Windows-Azure-Webrole-version-1004-is-released

For SSL support, you need to specify a certificate with the deployment. You can specify more if you need to, and then use the thumbprint in the deployment configuration.


### Tools

At this point you will have to manually deploy and setup websites editing the xml files in the blob storage. We are working on two tools to simplify this. A .Net tool wher you specify the settings, log in to azure and deploy the webrole directly from this with no need for visual studio. Another tool is a management portal where you can, by oauth2, controll the webrole if the management plugin is installed. The management plugin is also soon done and will be released on nuget also when done. Stay tuned.

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

