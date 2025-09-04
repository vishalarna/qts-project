using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Localization;
using QTD2.Domain.Interfaces.Validation;

namespace QTD2.Domain.Validation
{
    public class Validation<TEntity> : IValidation<TEntity>
        where TEntity : class
    {
        private readonly Dictionary<string, IValidationRule<TEntity>> _validationsRules;
        private readonly IStringLocalizerFactory _factory;

        protected IStringLocalizer _validationStringLocalizer { get; set; }

        public Validation(IStringLocalizerFactory stringLocalizerFactory)
        {
            _validationsRules = new Dictionary<string, IValidationRule<TEntity>>();
            _factory = stringLocalizerFactory;

            var type = typeof(Validation<>);

            string assemblyName = type.GetTypeInfo().Assembly.GetName().Name;
            string typeName = type.Name.Remove(type.Name.IndexOf('`'));
            string baseName = (type.Namespace + "." + typeName).Substring(assemblyName.Length).Trim('.');

            _validationStringLocalizer = _factory.Create("Resources.Validation.Validation", assemblyName);
        }

        public virtual ValidationResult Valid(TEntity entity)
        {
            var result = new ValidationResult();
            foreach (var key in _validationsRules.Keys)
            {
                var rule = _validationsRules[key];
                if (!rule.Valid(entity))
                {
                    result.Add(new ValidationError(rule.ErrorMessage));
                }
            }

            return result;
        }

        protected virtual void AddRule(IValidationRule<TEntity> validationRule)
        {
            var ruleName = validationRule.GetType() + Guid.NewGuid().ToString("D");
            _validationsRules.Add(ruleName, validationRule);
        }

        protected virtual void RemoveRule(string ruleName)
        {
            _validationsRules.Remove(ruleName);
        }
    }
}
