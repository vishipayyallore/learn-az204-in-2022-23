az deployment group create --resource-group rg-womd-robbie-001 --template-file windowsvm.deploy.json --parameters windowsvm.parameters.json --mode Incremental
