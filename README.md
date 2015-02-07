<h1>Data Insights</h1>
An application that allows you to get better insight of your data by building reports, charts, and dashboard.  You can query data from data sources, currently Microsoft Sql Server is supported.

<h2>Goals</h2>
<ul>
  <li>Make it easier to data mine and get data insights.</li>
  <li>Allow users to easily, and quickly build reports and charts.</li>
  <li>Give organizations the power to quickly build flexible reports and charts to visual their data.</li>
</ul>

<h2>Roadmap</h2>
The project is currently in very early stages.

The following has been developed:
<ol>
  <li>Create/Update projects.</li>
  <li>Create new data source.</li>
  <li>Create new data connection to a Microsoft Sql Server, and query tables and columns from the schema, and import to the data source.</li>
</ol>

The following are ideas for v1:
<ol>
  <li>Create report builder interface and backend.</li>
  <li>Create chart builder interface and backend infrastructure.</li>
  <li>Add support for multiple users and security. Ex: certain users can build projects, and certain users have access to only some reports.</li>
  <li>Add support for export reports and charts to different to platforms, like SharePoint. User should also be able to add web widgets to any webpage.</li>
  <li>Build dashboards using existing reports and charts in a drag and drop UI.</li>
</ol>

In the farther future, I have the following ideas:
<ol>
  <li>Add support for more data sources, such as MongoDb, Oracle, SalesForce, SharePoint, and Google Analytics.</li>
</ol>

<h2>Tech Stack</h2>
This project is built on the following stack:
<ul>
  <li>Asp.Net Mvc</li>
  <li>AngularJs</li>
  <li>Sql Server</li>
  <li>AutoFac</li>
  <li>BootStrap</li>
</ul>

<h2>Architecture</h2>
The project is primarily based on the <ahref="https://www.develop.com/onionarchitecture" title="learn more">Onion Architecture</a>.  We also a multi-tier architecture with domain layer, business layer, and UI layer.  On the client side, we primarily use MVC with the help of AngularJs.  AutoFac is used for DI/IoC.
