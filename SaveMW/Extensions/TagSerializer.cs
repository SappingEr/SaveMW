using Newtonsoft.Json;
using SaveMW.Models;
using SaveMW.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaveMW.Extensions
{
    public class TagSerializer
    {
        private TagRepository tagRepository;

        public TagSerializer(TagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }       

        public string SerializeTags(Tag[] tags)
        {
            throw new NotImplementedException();
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

        public IList<Tag> GetTags(string tags)
        {
            IList<Tag> newTags = new List<Tag>();
            List<Tag> deserializedTags = DeserializeTags(tags);
            var deserializedTagsNames = deserializedTags.Select(n => n.Name);
            IList<Tag> savedTags = tagRepository.SavedTags(deserializedTagsNames.ToArray());
            
            if (savedTags.Count > 0)
            {
                newTags = savedTags;
                var savedTagsNames = savedTags.Select(n => n.Name);
                var newNames = deserializedTagsNames.Except(savedTagsNames);
                if (newNames.Any())
                {
                    foreach (var t in newNames)
                    {
                        Tag tag = deserializedTags.Where(i => i.Name == t).FirstOrDefault();
                        tagRepository.Save(tag);
                        newTags.Add(tag);
                    }
                }
                return newTags;
            }
            newTags = deserializedTags;
            return newTags;
        }
    }

    class TagVal
    {
        [JsonProperty(PropertyName = "value")]
        public string Value;
    }
}