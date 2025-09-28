# SendEMail-AzureFunc
Azure Function for Sending Emails - Useful for Contact Forms and any Jobs for Automated Email triggers.


## 1. Create Azure Function App from Azure Portal

* Go to the Azure Portal and select Create a resource.
* Search for and choose Function App.
* On the Basics tab, provide:
    - Subscription
    - Resource Group (create new if needed)
    - Function App name (must be globally unique)
    - Choose Code for Publish
    - Select the Runtime stack (NET)
    - Version and Region as appropriate
* Select Review + create, verify details, and click Create.
* Wait for deployment, then click Go to Resource to access your new Function App.
* In the Function App pane, select Functions, then Add to create your function (HTTP trigger).
* Set up your trigger type, name, and authorization level, then review or edit code under Code + Test.

## 2. Deploy Azure Function Code from VSCode
* Ensure VSCode has the Azure Functions and language extensions (such as C#).
* Open VSCode and sign in to your Azure account using the Azure extension.
* Use F1 (or Ctrl+Shift+P) and type Azure Functions: Create New Project to generate a local function project.
* Select the folder where this code is downloaded, language, template (HTTP trigger), provide function details.
* test your function locally by running (F5).
* When ready to deploy:
    - In the Azure sidebar, right-click your function and select Deploy to Function App.
    - Choose your subscription and the target Function App created earlier.
    - Confirm overwrite if prompted—deployment will push your code to Azure.
* Your function is now live in Azure and can be tested using its Azure endpoint.

Your function is now live and accessible from the Azure portal or via its HTTP endpoint!

## 3. Test the code
* To know the endpoint. 
    - Goto the created Function App in Azure portal select the Function Name "SendEmail".
    - Click on "Get Function URL" which will provide with Master and Default Keys.
* Test the endpoint in Postman or Insomnia with the following form parameters
    > contactName, email, subject, message

* When the function is called from web application, Browser security Cross Origin Resource Sharing (CORS) will block the request. you can white-list the host URL from where the HTML/JS application runs. 
    - Goto the created Function App in Azure Portal and search CORS within it or navigate to API menu and select CORS. Add the URL on Allowed Origins section. 
## Tech Stack

Dotnet 8.0

