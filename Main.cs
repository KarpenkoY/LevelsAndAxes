using System;
using System.Windows.Forms;
using System.Collections.Generic;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;

namespace LevelsAndAxesGridWithDimensions
{
    // Setting transaction in manual mode
    [Transaction(TransactionMode.Manual)]

    public partial class ExternalCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            // Get application, document and units objects
            UIApplication uiApp = commandData.Application;
            Document doc = uiApp.ActiveUIDocument.Document;
            Units units = doc.GetUnits();

            // Get length unit, using in project
            DisplayUnitType unit = units
                .GetFormatOptions(UnitType.UT_Length).DisplayUnits;

            // Show form for get data from user
            SetParametersForm form = new SetParametersForm(units);
            form.ShowDialog();

            // Stop running if user pressed cancel button or ESC
            if (form.DialogResult == DialogResult.Cancel)
            {
                return Result.Cancelled;
            }

            // Defining parameters sets
            GridParameters gridParameters = new GridParameters();
            LevelsParameters levelsParameters = new LevelsParameters();

            // Try initialize parameters set, using data entered by user
            try
            {
                InitializeParameters(form, ref gridParameters,  ref levelsParameters, unit);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

            using (TransactionGroup transactionGroup = new TransactionGroup(doc))
            {
                transactionGroup.Start("Create levels, and axes grid with dimensions");

                // Get from document highest level elevation
                double highestElevation = GetHighestElevation(doc);

                if (levelsParameters.CreateALevels)
                {
                    // Creating levels with user entered parameters 
                    List<Level> levels = CreateLevels(doc, levelsParameters, highestElevation);

                    // Creating plans for new levels
                    CreatePlans(doc, levels);
                }
                
                if (gridParameters.CreateAGrid)
                {
                    List<Grid> grid = new List<Grid>();

                    try
                    {
                        // Creating axes grid with user entered parameters
                        grid = CreateAxesGrid(doc, gridParameters);
                    }
                    catch (OperationCanceledException)
                    {
                        MessageBox.Show("The axes cant't have the same name");

                        return Result.Cancelled;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                    // Creating dimensions
                    CreateDimensions(doc, gridParameters, grid);
                }
                    
                transactionGroup.Assimilate();
            }

            // Notyfy app, that external command complete succeeded
            return Result.Succeeded;
        }
    }
}
