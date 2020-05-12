using NHibernate;
using NHibernate.Criterion;
using SaveMW.Models.Filters;
using System.Collections.Generic;
using System.Linq;

namespace SaveMW.Models.Repositories
{
    [Repository]
    public class TagRepository : Repository<Tag, TagFilter>
    {
        public TagRepository(ISession session) :
           base(session)
        {
        }

        public override void SetupFilter(TagFilter filter, ICriteria crit)
        {
            base.SetupFilter(filter, crit);
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    crit.Add(Restrictions.Eq("Name", filter.Name));
                }                
            }
        }

        public IEnumerable<Tag> GetSavedTags(string[] tags)
        {
            var crit = session.CreateCriteria<Tag>()
                .Add(Restrictions.InG("Name", tags));
            return crit.List<Tag>();
        }

        public Tag FindTag(TagFilter filter)
        {
            var crit = session.CreateCriteria<Tag>();               
            SetupFilter(filter, crit);           
            return crit.UniqueResult<Tag>();
        }

        public IList<Tag> TagsToAdd(List<Tag> tags)
        {
            IList<Tag> newTags = new List<Tag>();
            List<Tag> deserializedTags = tags;
            var deserializedTagsNames = deserializedTags.Select(n => n.Name);
            IEnumerable<Tag> savedTags = GetSavedTags(deserializedTagsNames.ToArray());

            if (savedTags.Count() > 0)
            {
                newTags = savedTags.ToList();
                var savedTagsNames = savedTags.Select(n => n.Name);
                var newNames = deserializedTagsNames.Except(savedTagsNames);
                if (newNames.Any())
                {
                    foreach (var t in newNames)
                    {
                        Tag tag = deserializedTags.Where(i => i.Name == t).FirstOrDefault();
                        Save(tag);
                        newTags.Add(tag);
                    }
                }
                return newTags;
            }
            newTags = deserializedTags;
            return newTags;
        }       
    }
}
