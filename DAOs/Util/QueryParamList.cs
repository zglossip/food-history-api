using Npgsql;

namespace food_history_api.DAOs.Util;

public class QueryParamList<T>
{
    private string _paramString;
    private List<T> _queryList;

    public QueryParamList(string paramString, List<T> queryList)
    {
        this._paramString = paramString;
        this._queryList = queryList;
    }

    public string GetQueryString()
    {
        return string.Join(",", Enumerable.Range(0, _queryList.Count).Select(i => _generateVariableInstance(i)));
    }

    public void PopulateParamList(List<NpgsqlParameter> parameters)
    {
        for (int i = 0; i < _queryList.Count; i++)
        {
            parameters.Add(new NpgsqlParameter(_generateVariableInstance(i), _queryList[i]));
        }
    }

    private string _generateVariableInstance(int i)
    {
        return "@" + _paramString + $"{i}";
    }
}