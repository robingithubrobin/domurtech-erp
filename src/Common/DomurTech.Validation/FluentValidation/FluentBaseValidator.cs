using System.Collections.Generic;
using AutoMapper;
using DomurTech.Core.Abstract;
using DomurTech.Validation.Abstract;
using DomurTech.Validation.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace DomurTech.Validation.FluentValidation
{
    public class FluentBaseValidator<TModel,TRules > : ICustomValidator
        where TModel : class, IBaseModel, new()
        where TRules : AbstractValidator<TModel>, new()
        
    {
        private readonly TModel _model;
        private TRules _rules;
        
        public bool IsValid { get; set; }
        protected readonly List<CustomValidationResult> ValidationResults;
        public FluentBaseValidator(TModel model)
        {
            _model = model;         
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<ValidationFailure, CustomValidationResult>());
            mapperConfiguration.CreateMapper();
            ValidationResults = new List<CustomValidationResult>();
        }
        public List<CustomValidationResult> Validate()
        {
            _rules = new TRules();
            var results = _rules.Validate(_model);
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<ValidationFailure, CustomValidationResult>());
            var mapper = mapperConfiguration.CreateMapper();
            foreach (var error in results.Errors)
            {
                ValidationResults.Add(mapper.Map<ValidationFailure, CustomValidationResult>(error));
            }
            IsValid = results.IsValid;
            return ValidationResults;
        }
    }
}
