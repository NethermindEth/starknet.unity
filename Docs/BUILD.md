# Building the project into a package

A unity package is simply a modified compressed file containg Unity assets(i.e images, scripts, configs, etc.).

To build the project and export it as a unity package.
You need to identify the version of unity you need to support.
Due to the changes being made across versions, especially across major versions.
You might need to make changes tailored to those versions.
This is assuming the current version of the source code hasn't already made those changes
for you.

First download the source code.

## Manually from Editor
- Open the editor or unity hub, from there open the source code as a project.
- Wait for the project to be loaded in and all dependencies fetched.
- **Make all desired modifications.**
- Ensure the project builds with no errors.
- Open the projects tab.
- Right-click on the Assets Folder in the Project tab. (Or click on the Assets folder in the project tab,
selecting it and then click on Assets in the main menu bar)
- Select Export Package.
- Check the boxes of all required Items.
- Deselect Include dependencies (so it doesn't pack unity version dependent files)
- Select Include all scripts
- Click on Export
- Name the package and specify location to export package to.


## Build from command line


## Build using CI