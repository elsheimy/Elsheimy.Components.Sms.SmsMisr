using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Elsheimy.Components.RemoteApi
{
  public class ParamProvider
  {
    public virtual IEnumerable<Parameter> ExtractQueryParameters(object targetObject)
    {
      return ExtractParameters<QueryAttribute>(targetObject);
    }

    public virtual IEnumerable<Parameter> ExtractHeaderParameters(object targetObject)
    {
      return ExtractParameters<HeaderAttribute>(targetObject);
    }

    public virtual object ExtractBodyParameter(object targetObject)
    {
      var memberAttributes = GetAttributeMembers<BodyAttribute>(targetObject);

      if (memberAttributes.Count() > 1)
      {
        throw new InvalidOperationException(Properties.Resources.InvalidOperationException_MultipleBody);
      }

      if (memberAttributes.Any())
      {
        object value = GetMemberValue(targetObject, memberAttributes.First().Member);
        return value;
      }

      return null;
    }

    public virtual IEnumerable<Parameter> ExtractParameters<T>(object targetObject) where T : ParamAttribute
    {
      IEnumerable<MemberAttributePair<T>> queryMembers = GetAttributeMembers<T>(targetObject);

      List<Parameter> queryParams = new List<Parameter>(queryMembers.Count());

      foreach (var memberAtt in queryMembers)
      {
        object value = null;
        value = GetMemberValue(targetObject, memberAtt.Member);

        string name = memberAtt.Attribute.Name ?? memberAtt.Member.Name;
        string valueStr = value?.ToString();


        if (null == valueStr && memberAtt.Attribute.IsRequired == false)
          continue;

        Parameter param = new Parameter();
        param.Name = name;
        param.Value = valueStr;

        queryParams.Add(param);
      }

      return queryParams;
    }

    private static object GetMemberValue(object targetObject, MemberInfo member)
    {
      object value;
      if (member is PropertyInfo)
        value = (member as PropertyInfo).GetValue(targetObject);
      else if (member is FieldInfo) // member is a field
        value = (member as FieldInfo).GetValue(targetObject);
      else
        throw new ArgumentException(Properties.Resources.ArgumentException_InvalidMemberType, nameof(member));
      return value;
    }

    protected virtual IEnumerable<MemberAttributePair<T>> GetAttributeMembers<T>(object targetObject) where T : Attribute
    {
      List<MemberInfo> queryMembers = new List<MemberInfo>();

      queryMembers.AddRange(targetObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance));
      queryMembers.AddRange(targetObject.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance));

      // Checks attribute existence
      IEnumerable<MemberAttributePair<T>> memberAttributes =
        queryMembers.Distinct().Where(a => a.GetCustomAttributes<T>().Any()).Select(mem => new MemberAttributePair<T>()
        {
          Member = mem,
          Attribute = mem.GetCustomAttributes<T>().FirstOrDefault()

        }).ToArray();
      return memberAttributes;
    }
  }
}
