### Description
LevelsAndAxes is a C# .NET Autodesk Revit API add-in. Depending on the entered data, it builds:
1. Levels. The building comes from the highest existing level.
2. Axes of the grid. Construction is possible both in both orientations, and in any one (vertical or horizontal). Axes are named automatically: horizontal axes - in alphabetical letters, vertical - in numbers.
If there are axes before drawing, prefixes for the names of the axes, each of the orientations (for each new mesh) will be requested.
3. Dimensional annotations between the grid axes.

### Requirements
Revit 2019 or 2020

### Installation
1. Get manifest and assembly files:
    * Download both file in `*.zip` archive (Download Add-in button).
    or
    * Compile from source code (require RevitAPI.dll and RevitAPIUI.dll from Revit root directory)
      * Download `*.zip` archive within source code (Download SRC button), load the solution file in Visual Studio, compile.
      or
      * Fork the repository, clone to your local system, load the solution file in Visual Studio, compile.

2. Install the add-in manifest file and the .NET DLL assembly in the standard Revit add-in location, for example by copying to `%ProgramData%\Autodesk\Revit\Addins\2020` (for all users at PC).

### Launch

1. Run Revit, allow load this add-in, open your project.
2. Add-ins **>** External Tools **>** Create levels and axes gris

### License
This sample is licensed under the terms of the [MIT License](http://opensource.org/licenses/MIT).
Please see the [LICENSE](LICENSE) file for full details.
