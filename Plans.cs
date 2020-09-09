using System.Linq;
using System.Collections.Generic;
using Autodesk.Revit.DB;
using System;

namespace LevelsAndAxesGridWithDimensions
{
    public partial class ExternalCommand
    {
        private List<ViewPlan> CreatePlans(Document doc, List<Level> levels)
        {
            /*
             * This method create three view plans for levels:
             * stractural plan, floor plan and ceileng plan
             */

            List<ViewPlan> viewPlans = new List<ViewPlan>();

            using (Transaction transaction = new Transaction(doc))
            {
                transaction.Start("Create plans");

                List<ElementId> plansId = new List<ElementId>()
                {
                    GetViewFamilyTypeId(doc, ViewFamily.StructuralPlan),
                    GetViewFamilyTypeId(doc, ViewFamily.FloorPlan),
                    GetViewFamilyTypeId(doc, ViewFamily.CeilingPlan)
                };

                foreach (Level level in levels)
                {
                    foreach (ElementId planId in plansId)
                    {
                        ViewPlan plan = ViewPlan.Create(doc, planId, level.Id);
                        viewPlans.Add(plan);
                    }
                }
                transaction.Commit();
            }
            return viewPlans;
        }

        private View GetView(Document doc)
        {
            /*
             * This method return active view or first of floor plans
             */

            View view = doc.ActiveView;

            // Get first floor plan view if non active
            if (view is ViewSection)
            {
                FilteredElementCollector viewCollector = new FilteredElementCollector(doc)
                    .OfClass(typeof(View));

                IEnumerable<ViewPlan> views
                    = from elem in viewCollector
                      let type = elem as ViewPlan
                      where type?.ViewType == ViewType.FloorPlan
                      select type;

                view = views.First();
            }

            return view;
        }

        private ElementId GetViewFamilyTypeId(Document doc, ViewFamily viewFamily)
        {
            /*
             * This method return first view family type id
             */

            IEnumerable<ViewFamilyType> viewFamilyTypes
                    = from elem in new FilteredElementCollector(doc)
                        .OfClass(typeof(ViewFamilyType))
                      let type = elem as ViewFamilyType
                      where type.ViewFamily == viewFamily
                      select type;

            return viewFamilyTypes.First().Id;
        }
    }
}
