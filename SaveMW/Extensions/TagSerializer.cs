using Newtonsoft.Json;
using SaveMW.Models;
using System.Collections.Generic;
using System.Linq;

namespace SaveMW.Extensions
{
    public class TagSerializer
    {
        public string SerializeTags(Tag[] tags)
        {
            string[] tagNames = tags.Select(t => t.Name).ToArray();
            string stringTags = null;
            List<TagVal> tagValues = new List<TagVal>();
            foreach (var name in tagNames)
            {
                tagValues.Add(new TagVal { Value = name });
            }
            stringTags = JsonConvert.SerializeObject(tagValues);
            return stringTags;
        }

        public List<Tag> DeserializeTags(string tags)
        {
            List<Tag> dTags = new List<Tag>();
            var tagVals = JsonConvert.DeserializeObject<TagVal[]>(tags);
            foreach (var t in tagVals)
            {
                dTags.Add(new Tag { Name = t.Value });
            }
            return dTags;
        }
    }

    class TagVal
    {
        [JsonProperty(PropertyName = "value")]
        public string Value;
    }
}