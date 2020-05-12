using SaveMW.Models.NoteViewModels;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;

namespace SaveMW.Helpers
{
    public static class NoteHelper
    {
        public static MvcHtmlString NoteList(NotesListViewModel model)
        {
            TagBuilder table = new TagBuilder("table");
            table.AddCssClass("table");
            TagBuilder tHeader = new TagBuilder("tr");

            TagBuilder hName = new TagBuilder("th");
            hName.SetInnerText("Название");
            tHeader.InnerHtml += hName.ToString();

            TagBuilder hFiles = new TagBuilder("th");
            hFiles.SetInnerText("Количество файлов");
            tHeader.InnerHtml += hFiles.ToString();

            TagBuilder hTags = new TagBuilder("th");
            hTags.SetInnerText("Теги");
            tHeader.InnerHtml += hTags.ToString();

            TagBuilder hPub = new TagBuilder("th");
            hPub.SetInnerText("Публикация");
            tHeader.InnerHtml += hPub.ToString();

            TagBuilder hDate = new TagBuilder("th");
            hDate.SetInnerText("Дата создания");
            tHeader.InnerHtml += hDate.ToString();

            table.InnerHtml += tHeader.ToString();

            foreach (var note in model.Notes)
            {
                TagBuilder row = new TagBuilder("tr");

                TagBuilder name = new TagBuilder("td");               
                var link = new TagBuilder("a");
                link.SetInnerText(note.Name);
                string url = "/Note/Note/" + note.Id;
                link.MergeAttribute("href", url);
                name.InnerHtml += link.ToString();
                row.InnerHtml += name.ToString();

                TagBuilder files = new TagBuilder("td");
                files.SetInnerText(note.Files.Count.ToString());
                row.InnerHtml += files.ToString();

                TagBuilder tags = new TagBuilder("td");
                string t = null;
                foreach (var tag in note.Tags)
                {
                    t = tag.Name + " ";
                }
                tags.SetInnerText(t);
                row.InnerHtml += tags.ToString();

                TagBuilder pub = new TagBuilder("td");
                if (note.Show ==true)
                {
                    TagBuilder span = new TagBuilder("span");
                    span.AddCssClass("glyphicon glyphicon-check");
                    pub.InnerHtml += span.ToString();
                }
                row.InnerHtml += pub.ToString();

                TagBuilder date = new TagBuilder("td");
                date.SetInnerText(note.CreationDate.ToString("d"));
                row.InnerHtml += date.ToString();
                    
                table.InnerHtml += row.ToString();
            }
            return new MvcHtmlString(table.ToString());
        }
    }
}