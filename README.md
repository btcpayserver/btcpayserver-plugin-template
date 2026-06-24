# BTCPay Server Plugin Template

A template for your own [BTCPay Server](https://github.com/btcpayserver) plugin.

Learn more in our [plugin documentation](https://docs.btcpayserver.org/Development/Plugins/).

## Requirements

- .NET SDK 10.0 or later
- Git with submodule support
- Docker, for running the BTCPay Server development dependencies

## Getting started

First, clone this repository:

```bash
mkdir my-plugin && cd my-plugin
git clone --recurse-submodules https://github.com/btcpayserver/btcpayserver-plugin-template.git .
```

If you already cloned without submodules, initialize them with:

```bash
git submodule update --init --recursive
```

Rename the template to your plugin name. The name should follow .NET assembly naming conventions: PascalCase, optionally separated by dots. For example: `MyPlugin` or `MyCompany.MyPlugin`.

Then replace all occurrences of `BTCPayServer.Plugins.Template` with `PLUGIN_NAME`.

Here is a script you can run to do this automatically:
```bash
PLUGIN_NAME="MyPlugin"
OLD_NAME="BTCPayServer.Plugins.Template"
export OLD_NAME PLUGIN_NAME

git grep -l -z "$OLD_NAME" -- . ':!submodules' | xargs -0 perl -pi -e 's/\Q$ENV{OLD_NAME}\E/$ENV{PLUGIN_NAME}/g'

git mv "src/$OLD_NAME" "src/$PLUGIN_NAME"
git mv "src/$PLUGIN_NAME/$OLD_NAME.csproj" "src/$PLUGIN_NAME/$PLUGIN_NAME.csproj"

git mv "tests/$OLD_NAME.Tests" "tests/$PLUGIN_NAME.Tests"
git mv "tests/$PLUGIN_NAME.Tests/$OLD_NAME.Tests.csproj" "tests/$PLUGIN_NAME.Tests/$PLUGIN_NAME.Tests.csproj"

git mv "$OLD_NAME.slnx" "$PLUGIN_NAME.slnx"
```

Then update the plugin metadata in `src/<YourPluginName>/<YourPluginName>.csproj`:

- `Product`
- `Description`
- `Version`

Verify that the plugin builds:

```bash
dotnet build
```

Finally, clean up the remaining template project references:

```bash
git remote set-url origin git@github.com:<your-github-user>/<your-plugin-repository>.git
```

Then replace this README with documentation for your own plugin. At minimum, document what the plugin does, how to install it, how to configure it, and how to build or test it locally.

You may also want to review and update:

- [LICENSE](LICENSE)
- package metadata in your plugin `.csproj`

## Debugging the plugin

Register the plugin with the BTCPay Server development environment:

```bash
./plugin-register.sh
```

It will configure BTCPay Server for loading your plugin during debug.

Start the BTCPay Server development dependencies:

```bash
cd submodules/btcpayserver/BTCPayServer.Tests
docker compose up -d dev
```

Open the solution file in your IDE and use the `BTCPayServer: Bitcoin-HTTPS` launch profile. When the debugger starts BTCPay Server, it should load your plugin. A breakpoint in `src/<YourPluginName>/Plugin.cs`, such as inside `Plugin.Execute`, should be hit during startup.

## Adding plugin code

The template starts with a minimal `Plugin.cs` class:

Register your services, controllers, hosted services, migrations, or other plugin components from `Execute`.

If your plugin needs Entity Framework or bundled project dependencies, see the commented examples in the plugin `.csproj` file.

You can take examples from [other plugins](https://plugin-builder.btcpayserver.org/) and BTCPay Server's own system plugins in `submodules/btcpayserver/BTCPayServer/Plugins`.
