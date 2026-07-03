# Create and retrieve secrets from Azure Key Vault

## Step 1: Create resource group

```sh
$ az group create --name myResourceGroup --location eastus
```

## Step 2: Create variable to use

```sh
resourceGroup=myResourceGroup
location=eastus
keyVaultName=mykeyvaultname$RANDOM

# Print the name 🖨️
$ echo $keyVaultName 
```

## Step 3: Create keyvault resource

```sh
$ az keyvault create --name $keyVaultName \
    --resource-group $resourceGroup --location $location
```

## Step 4: Set userPrincipalName variable

```sh
$ userPrincipal=$(az rest --method GET --url https://graph.microsoft.com/v1.0/me \
    --headers 'Content-Type=application/json' \
    --query userPrincipalName --output tsv)
```

## Step 5: Set resourceID variable

```sh
$ resourceID=$(az keyvault show --resource-group $resourceGroup \
    --name $keyVaultName --query id --output tsv)
```

## Step 6: Create an assign the key

```sh
$ az role assignment create --assignee $userPrincipal \
    --role "Key Vault Secrets Officer" \
    --scope $resourceID
```

## Step 7: Adding the secret in KeyVault 🔑

```sh
$ az keyvault secret set --vault-name $keyVaultName \
    --name "MySecret" --value "My secret value"
```

## Step 8: Validate the secret

```sh
$ az keyvault secret show --name "MySecret" --vault-name $keyVaultName
```