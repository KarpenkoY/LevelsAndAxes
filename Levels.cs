using System;
using System.Linq;
using System.Windows;
using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace LevelsAndAxesGridWithDimensions
{
    public partial class ExternalCommand
    {
        private List<Level> CreateLevels(Document doc, LevelsParameters p, double elevation)
        {
            /*
             * This method create levels considering the existing
             */

            List<Level> list = new List<Level>();
            Level level;

            using (Transaction transaction = new Transaction(doc))
            {
                try
                {
                    transaction.Start("Create levels");

                    for (int i = 1, n = p.numLevel; i <= n; i++)
                    {
                        level = Level.Create(doc, elevation + (p.distLevel * i));
                        list.Add(level);
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

            return list;
        }
        private double GetHighestElevation(Document doc)
        {
            /*
             * This method return elevation highest level from exist
             */

            FilteredElementCollector existingLevels = new FilteredElementCollector(doc)
                .WhereElementIsNotElementType()
                .OfCategory(BuiltInCategory.INVALID)
                .OfClass(typeof(Level));

            List<double> levelElevations = new List<double>();
            foreach (Level level in existingLevels)
            {
                levelElevations.Add(level.Elevation);
            }

            return levelElevations.ToArray().Max();
        }
        private double GetElevationIndent()
        {
            /*
             * This method return indent for grid axes elevation
             */

            return 10;
        }
    }
}
