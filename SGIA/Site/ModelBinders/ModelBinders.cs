using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Site.ModelBinders
{
    public class DateTimeModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            var modelName = bindingContext.ModelName;
            var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
            if (valueProviderResult == ValueProviderResult.None)
                return Task.CompletedTask;

            bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);

            var dateStr = valueProviderResult.FirstValue;

            if (bindingContext.ModelMetadata.IsNullableValueType && string.IsNullOrEmpty(dateStr))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }
            
            if (!DateTime.TryParse(dateStr, new CultureInfo("pt-BR"), DateTimeStyles.None, out DateTime date))
            {
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, "Data inválida");
                return Task.CompletedTask;
            }

            bindingContext.Result = ModelBindingResult.Success(date);
            return Task.CompletedTask;
        }
    }

    public class DateTimeModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.Metadata.ModelType == typeof(DateTime) || context.Metadata.ModelType == typeof(DateTime?))
                return new DateTimeModelBinder();

            return null;
        }
    }
}