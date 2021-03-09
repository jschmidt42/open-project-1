using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor.Search;

static class PropertyDBProvider
{
	[SearchItemProvider]
    public static SearchProvider CreateProvider()
    {
		var qe = BuildQueryEngine();
		return new SearchProvider("pdb", "Property DB", (context, provider) => FetchItem(qe, context, provider)) { isExplicitProvider = true };
    }

    static QueryEngine<PropertyDatabaseRecordValue> BuildQueryEngine()
    {
		var queryEngineOptions = new QueryValidationOptions { validateFilters = false, skipNestedQueries = true };
		var qe = new QueryEngine<PropertyDatabaseRecordValue>(queryEngineOptions);
		qe.SetSearchDataCallback(e => null, s => s.Length < 2 ? null : s, StringComparison.Ordinal);
		return qe;
    }

	class PropertyDBEvaluator : IQueryHandler<PropertyDatabaseRecordValue, object>
	{
		private QueryGraph graph;

		public PropertyDBEvaluator(QueryGraph graph)
		{
			this.graph = graph;
		}

		public IEnumerable<PropertyDatabaseRecordValue> Eval(object payload)
		{
			if (graph.empty)
				return Enumerable.Empty<PropertyDatabaseRecordValue>();

			return EvalNode(graph.root);
		}

		private IEnumerable<PropertyDatabaseRecordValue> EvalNode(IQueryNode node)
		{
			switch (node.type)
			{
				case QueryNodeType.Search:
					return GetRecords(node);

				default:
					throw new NotSupportedException($"Node {node.type} not supported for evaluation");
			}
		}

		private IEnumerable<PropertyDatabaseRecordValue> GetRecords(IQueryNode node)
		{
			throw new NotImplementedException("TODO: Get all record value from a document key");
			/*SearchMonitor.propertyDatabase.TryLoad
			using (var view = SearchMonitor.GetView())
			{
				view.propertyDatabaseView.
			}*/
		}

		public bool Eval(PropertyDatabaseRecordValue element) => throw new NotSupportedException();
	}

	class PropertyDBQueryHandler : IQueryHandlerFactory<PropertyDatabaseRecordValue, PropertyDBEvaluator, object>
	{
		public PropertyDBEvaluator Create(QueryGraph graph, ICollection<QueryError> errors) => new PropertyDBEvaluator(graph);
	}

	static IEnumerable<SearchItem> FetchItem(QueryEngine<PropertyDatabaseRecordValue> qe, SearchContext context, SearchProvider provider)
    {
        var query = qe.Parse(context.searchQuery, new PropertyDBQueryHandler());
        if (!query.valid)
            yield break;

		foreach (var r in query.Apply())
			yield return provider.CreateItem(context, Guid.NewGuid().ToString("N"), r.propertyType.ToString(), r.ToString(), null, r);
    }
}
