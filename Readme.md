# ALS.Printify.Net.Catalog
This package will allow you to get elements from the Printify Catalog.

Instantiate a PrintifyClient(string apiToken). Then you will have access to all needed methods from the client object.

Example:

    var client = new PrintifyClient([PRINTIFY_TOKEN]);
    var blueprints = await client.GetBlueprints();
    
    //Get Providers
    var providers = await bluerint[0].GetProviders(fullResponse:true);
    // OR
    var providers = await client.GetProviders(blueprint, fullResponse:true);
