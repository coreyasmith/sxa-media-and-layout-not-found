# SXA Handle Media and Layout Not Found

This is a sample repository to demonstrate how to handle media and layout not
found in [Sitecore Experience Accelerator][1].

## Setup

Clone this repository. The rest of the setup assumes you cloned to
`C:\Projects\Sitecore\SxaNotFound`.

### Connected Mode

1. Install an instance of [Sitecore 9.0 Update-2][2].
   - The default install path is
    `C:\inetpub\wwwroot\sxanotfound.localhost`.
   - The default URL is `sxanotfound.localhost`.
2. Install [Sitecore PowerShell Extensions 4.7.2 or above][3].
3. Install [Sitecore Experience Accelerator 1.7 Update-1][1].
4. If you used a clone path, install directory, or URL different than the
   defaults above, open
   [SxaNotFound.sln](SxaNotFound.sln) and modify
   the following files in the `.config` folder:
   - `CoreySmith.Project.Common.Dev.config`
     - Change `sourceFolder` to your repository directory.
   - `PublishSettings.targets`
     - Change `publishUrl` to the path of your Sitecore instance.
5. Build the solution in Visual Studio.
   - This will publish all code to your instance thanks to
     [Helix Publishing Pipeline][4].
   - Note: you may need to reload the solution and build a second time if you
     get errors about missing assemblies/references when you load Sitecore.
6. Perform a Unicorn sync at `/unicorn.aspx?verb=sync`.
7. Run the Unicorn sync again.
8. Login to Sitecore and do a full site publish.
9. Navigate to your site at <http://hostname.localhost>.

[1]: https://dev.sitecore.net/Downloads/Sitecore_Experience_Accelerator/17/Sitecore_Experience_Accelerator_17_Update1.aspx
[2]: https://dev.sitecore.net/Downloads/Sitecore_Experience_Platform/90/Sitecore_Experience_Platform_90_Update2.aspx
[3]: https://marketplace.sitecore.net/Modules/Sitecore_PowerShell_console.aspx?sc_lang=en
[4]: https://github.com/richardszalay/helix-publishing-pipeline
