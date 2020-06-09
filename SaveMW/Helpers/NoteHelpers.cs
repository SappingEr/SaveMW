using SaveMW.Models;
using SaveMW.Models.NoteViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SaveMW.Helpers
{
    public static class NoteHelpers
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
                    t += tag.Name + " ";
                    tags.SetInnerText(t);                    
                }
                row.InnerHtml += tags.ToString();



                TagBuilder pub = new TagBuilder("td");
                if (note.Show == true)
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

        public static MvcHtmlString TrimText(int stringLenght, string text)
        {
            if (stringLenght <= 0)
            {
                throw new ArgumentOutOfRangeException("maxChars");
            }
            if (text == null || text.Length < stringLenght)
            {
                return new MvcHtmlString(text);
            }
            int lastSpaceIndex = text.LastIndexOf(" ", stringLenght);
            int substringLength = (lastSpaceIndex > 0) ? lastSpaceIndex : stringLenght;
            var trimString = text.Substring(0, substringLength).Trim() + "...";
            return new MvcHtmlString(trimString);
        }

        public static MvcHtmlString ShowAvatar(int? avatarId, int width)
        {
            string src = null;
            if (avatarId != null)
            {
                src = "/DBFile/GetImage?id=" + avatarId;
            }
            else
            {
                src = "/Content/Pic/avatar.png";
            }
            TagBuilder avatar = new TagBuilder("img");
            avatar.AddCssClass("img-thumbnail");
            avatar.MergeAttribute("src", src);
            avatar.MergeAttribute("style", "width:" + width + "px");
            return MvcHtmlString.Create(avatar.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString ShowFiles(FSFile file)
        {           
            TagBuilder link = new TagBuilder("a");            
            string url = "/FSFile/Download/" + file.Id;
            link.MergeAttribute("href", url);
            string src;
            switch (file.Extention)
            {
                case ".docx":
                    src = "/Content/Pic/word ico.png";
                    break;
                case ".pdf":
                    src = "/Content/Pic/PDF ico.png";
                    break;
                default:
                    src = "/Content/Pic/file.png";
                    break;
            }
            TagBuilder icon = new TagBuilder("img");
            icon.MergeAttribute("src", src);
            icon.MergeAttribute("style", "width: 20px");            
            link.InnerHtml += icon.ToString();
            TagBuilder text = new TagBuilder("span");
            text.SetInnerText(" " + file.Name);
            link.InnerHtml += text.ToString();           
            return MvcHtmlString.Create(link.ToString());
        }
    }
}