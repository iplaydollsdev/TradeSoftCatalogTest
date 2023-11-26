using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TradeSoftCatalogTest.MVVM.Model;

namespace TradeSoftCatalogTest.Core
{
    public static class RouteFinder
    {
        public static List<AnalogRoute> Find(string inputFrom, string inputTo, int recursionSteps)
        {
            string[] partsFrom = inputFrom.Split('/');
            string articleFrom = RemoveSeparators(partsFrom[0]);
            string manufacturerFrom = partsFrom.ElementAtOrDefault(1)?.ToLower();

            string[] partsTo = inputTo.Split('/');
            string articleTo = RemoveSeparators(partsTo[0]);
            string manufacturerTo = partsTo.ElementAtOrDefault(1)?.ToLower();

            List<AnalogRoute> result = new();
            HashSet<string> keys = new HashSet<string>();
            FindRoutes(articleFrom, manufacturerFrom, articleTo, manufacturerTo, recursionSteps, new List<AnalogChain>(), result, keys);
            return result;
        }

        private static void FindRoutes(string articleFrom, string manufacturerFrom, string articleTo, string manufacturerTo, int stepsLeft, List<AnalogChain> currentRoute, List<AnalogRoute> result, HashSet<string> visitedNodes)
        {
            articleFrom = RemoveSeparators(articleFrom);
            manufacturerFrom = manufacturerFrom.ToLower();
            articleTo = RemoveSeparators(articleTo);
            manufacturerTo = manufacturerTo.ToLower();

            if (articleFrom == articleTo && (manufacturerFrom == null || manufacturerFrom == manufacturerTo))
            {
                result.Add(new AnalogRoute { Chains = new List<AnalogChain>(currentRoute) });
                return;
            }

            string currentNodeKey = $"{articleFrom}/{manufacturerFrom}";

            if (visitedNodes.Contains(currentNodeKey) || stepsLeft < 0)
            {
                return; // Skip visited nodes and limit recursion steps
            }

            visitedNodes.Add(currentNodeKey);

            using (var context = new AnalogContext())
            {
                var nextAnalogModels = context.AnalogModels.ToList()
                    .Where(a => RemoveSeparators(a.Article1) == articleFrom &&
                                (manufacturerFrom == null || a.Manufacturer1.ToLower() == manufacturerFrom));

                foreach (var nextModel in nextAnalogModels)
                {
                    if (nextModel.TrustLevel == 0)
                    {
                        if (RemoveSeparators(nextModel.Article2) == articleTo &&
                        (manufacturerTo == null || nextModel.Manufacturer2.ToLower() == manufacturerTo))
                        {
                            currentRoute.Add(new AnalogChain
                            {
                                AtricleFrom = $"{nextModel.Article1}/{nextModel.Manufacturer1}",
                                AtricleTo = $"{nextModel.Article2}/{nextModel.Manufacturer2}"
                            });

                            result.Add(new AnalogRoute { Chains = new List<AnalogChain>(currentRoute) });
                            currentRoute.RemoveAt(currentRoute.Count - 1);
                        }
                    }
                    else
                    {
                        currentRoute.Add(new AnalogChain
                        {
                            AtricleFrom = $"{nextModel.Article1}/{nextModel.Manufacturer1}",
                            AtricleTo = $"{nextModel.Article2}/{nextModel.Manufacturer2}"
                        });

                        FindRoutes(nextModel.Article2, nextModel.Manufacturer2, articleTo, manufacturerTo, stepsLeft - 1, currentRoute, result, visitedNodes);
                        currentRoute.RemoveAt(currentRoute.Count - 1);
                    }
                }
            }
            //visitedNodes.Remove(currentNodeKey);
        }

        private static string RemoveSeparators(string input)
        {
            return input.Replace(" ", "").Replace("-", "").Replace("/", "").Replace("_", "").ToLower();
        }
    }
}
