using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace DomurTech.ERP.UI.Web.Common.Helpers
{

    public static class Html5Helper
    {
        public enum MediaPreloadOptions
        {
            Auto,
            Metadata,
            None
        }
        public static MvcHtmlString CustomVideo(this HtmlHelper htmlHelper, string src, bool? autoplay = false, bool? controls = true, bool? loop = false, bool? muted = false, int? width = 640, int? height = 360, MediaPreloadOptions? preload = MediaPreloadOptions.None, string poster = null, IDictionary<string, object> htmlAttributes = null)
        {
            var tagBuilder = new TagBuilder("video");
            tagBuilder.Attributes.Add("src", src);
            tagBuilder.Attributes.Add("width", width.ToString());
            tagBuilder.Attributes.Add("height", height.ToString());
            tagBuilder.Attributes.Add("preload", preload.ToString().ToLower());

            if (autoplay == true)
            {
                tagBuilder.Attributes.Add("autoplay", "autoplay");
            }
            if (controls == true)
            {
                tagBuilder.Attributes.Add("controls", "controls");
            }
            if (loop == true)
            {
                tagBuilder.Attributes.Add("loop", "loop");
            }

            if (muted == true)
            {
                tagBuilder.Attributes.Add("muted", "muted");
            }
            if (poster != null)
            {
                tagBuilder.Attributes.Add("poster", poster);

            }

            if (htmlAttributes != null)
            {
                foreach (var htmlAttribute in htmlAttributes)
                {
                    tagBuilder.Attributes.Add(htmlAttribute.Key, htmlAttribute.Value.ToString());
                }

            }

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(tagBuilder);
            return MvcHtmlString.Create(stringBuilder.ToString());
        }
        public static MvcHtmlString CustomAudio(this HtmlHelper htmlHelper, string src, bool? autoplay = false, bool? controls = true, bool? loop = false, bool? muted = false, MediaPreloadOptions? preload = MediaPreloadOptions.None, IDictionary<string, object> htmlAttributes = null)
        {
            var tagBuilder = new TagBuilder("audio");
            tagBuilder.Attributes.Add("src", src);
            tagBuilder.Attributes.Add("preload", preload.ToString().ToLower());

            if (autoplay == true)
            {
                tagBuilder.Attributes.Add("autoplay", "autoplay");
            }
            if (controls == true)
            {
                tagBuilder.Attributes.Add("controls", "controls");
            }
            if (loop == true)
            {
                tagBuilder.Attributes.Add("loop", "loop");
            }

            if (muted == true)
            {
                tagBuilder.Attributes.Add("muted", "muted");
            }
            if (htmlAttributes != null)
            {
                foreach (var htmlAttribute in htmlAttributes)
                {
                    tagBuilder.Attributes.Add(htmlAttribute.Key, htmlAttribute.Value.ToString());
                }

            }
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(tagBuilder);
            return MvcHtmlString.Create(stringBuilder.ToString());
        }
        public static MvcHtmlString CustomAlert(this HtmlHelper helper, string tag, string innerHtml, string cssClass)
        {
            var tagBuilder = new TagBuilder(tag);
            tagBuilder.AddCssClass(cssClass);
            tagBuilder.InnerHtml = innerHtml;
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString CustomAlertFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string tag, string innerHtml, object htmlAttributes = null)
        {
            return innerHtml == null ? null : new MvcHtmlString(htmlHelper.LabelFor(expression, innerHtml, htmlAttributes).ToString().Replace("label", tag));
        }
        public static MvcHtmlString CustomTextBox(this HtmlHelper htmlHelper, string name, object htmlAttributes = null, object value = null, string format = null)
        {
            return htmlHelper.TextBox(name, value, format, htmlAttributes);
        }
        public static MvcHtmlString CustomTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, string format = null)
        {
            return htmlHelper.TextBoxFor(expression, format, htmlAttributes);
        }
        public static MvcHtmlString CustomEmail(this HtmlHelper htmlHelper, string name, object htmlAttributes = null, object value = null, string format = null)
        {
            return new MvcHtmlString(htmlHelper.TextBox(name, value, format, htmlAttributes).ToString().Replace("type=\"text\"", "type=\"email\""));
        }
        public static MvcHtmlString CustomEmailFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, string format = null)
        {
            return new MvcHtmlString(htmlHelper.TextBoxFor(expression, format, htmlAttributes).ToString().Replace("type=\"text\"", "type=\"email\""));
        }

        public static MvcHtmlString CustomFileFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, string format = null)
        {
            return new MvcHtmlString(htmlHelper.TextBoxFor(expression, format, htmlAttributes).ToString().Replace("type=\"text\"", "type=\"file\""));
        }

        public static MvcHtmlString CustomUrl(this HtmlHelper htmlHelper, string name, object htmlAttributes = null, object value = null, string format = null)
        {
            return new MvcHtmlString(htmlHelper.TextBox(name, value, format, htmlAttributes).ToString().Replace("type=\"text\"", "type=\"url\""));
        }
        public static MvcHtmlString CustomUrlFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, string format = null)
        {
            return new MvcHtmlString(htmlHelper.TextBoxFor(expression, format, htmlAttributes).ToString().Replace("type=\"text\"", "type=\"url\""));
        }
        public static MvcHtmlString CustomNumber(this HtmlHelper htmlHelper, string name, object htmlAttributes = null, object value = null,  string format = null)
        {
            return new MvcHtmlString(htmlHelper.TextBox(name, value, format, htmlAttributes).ToString().Replace("type=\"text\"", "type=\"number\""));
        }
        public static MvcHtmlString CustomNumberFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, string format = null)
        {
            return new MvcHtmlString(htmlHelper.TextBoxFor(expression, format, htmlAttributes).ToString().Replace("type=\"text\"", "type=\"number\""));
        }
        public static MvcHtmlString CustomCheckBox(this HtmlHelper htmlHelper, string name, bool lessHiddenField = false, object htmlAttributes = null)
        {
            var checkBoxWithHidden = htmlHelper.CheckBox(name, htmlAttributes).ToHtmlString().Trim();
            var pureCheckBox = checkBoxWithHidden.Substring(0, checkBoxWithHidden.IndexOf("<input", 1, StringComparison.Ordinal));
            return lessHiddenField ? new MvcHtmlString(pureCheckBox) : new MvcHtmlString(checkBoxWithHidden);
        }
        public static MvcHtmlString CustomCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, bool lessHiddenField = false, object htmlAttributes = null)
        {
            var checkBoxWithHidden = htmlHelper.CheckBoxFor(expression, htmlAttributes).ToHtmlString().Trim();
            var pureCheckBox = checkBoxWithHidden.Substring(0, checkBoxWithHidden.IndexOf("<input", 1, StringComparison.Ordinal));
            return lessHiddenField ? new MvcHtmlString(pureCheckBox) : new MvcHtmlString(checkBoxWithHidden);
        }
        public static MvcHtmlString CustomHidden(this HtmlHelper htmlHelper, string name, object htmlAttributes = null, object value = null)
        {
            return htmlHelper.Hidden(name, value, htmlAttributes);
        }
        public static MvcHtmlString CustomHiddenFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return htmlHelper.HiddenFor(expression, htmlAttributes);
        }
        public static MvcHtmlString CustomPassword(this HtmlHelper htmlHelper, string name, object htmlAttributes = null, object value = null)
        {
            return htmlHelper.Password(name, value, htmlAttributes);
        }
        public static MvcHtmlString CustomPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return htmlHelper.PasswordFor(expression, htmlAttributes);
        }
        public static MvcHtmlString CustomRadioButton(this HtmlHelper htmlHelper, string name, object htmlAttributes = null, bool isChecked = false, object value = null)
        {
            return htmlHelper.RadioButton(name, value, isChecked, htmlAttributes);
        }
        public static MvcHtmlString CustomRadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null, object value = null)
        {
            return htmlHelper.RadioButtonFor(expression, value ?? "", htmlAttributes);
        }
        public static MvcHtmlString CustomDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes, string optionLabel = null)
        {
            return htmlHelper.DropDownListFor(expression, selectList, optionLabel, htmlAttributes);
        }
        public static MvcHtmlString CustomListBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
        {
            return htmlHelper.ListBoxFor(expression, selectList, htmlAttributes);
        }
        public static MvcHtmlString CustomTextArea(this HtmlHelper htmlHelper, string name, string value = null, object htmlAttributes = null)
        {
            return htmlHelper.TextArea(name, value, htmlAttributes);
        }

        public static MvcHtmlString CustomTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            return htmlHelper.TextAreaFor(expression, htmlAttributes);
        }



    }
}