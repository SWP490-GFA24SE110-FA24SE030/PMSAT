namespace PMSAT.BusinessTier.Utils
{
    public static class CustomListUtil
    {
        public static (List<Guid> idsToRemove, List<Guid> idsToAdd, List<Guid> idsToKeep) SplitIdsToAddAndRemove_v1(List<Guid> oldIds, List<Guid> newIds)
        {
            List<Guid> idsToAdd = new List<Guid>(newIds);
            List<Guid> idsToRemove = new List<Guid>(oldIds);
            List<Guid> idsToKeep = new List<Guid>();

            //A list of Id that is contain deleted ids but does not contain new ids added
            List<Guid> listWithOutIdsToAdd = new List<Guid>();

            //This logic help to split new ids, keep old ids and deleted ids
            newIds.ForEach(x =>
            {
                oldIds.ForEach(y =>
                {
                    if (x.Equals(y))
                    {
                        listWithOutIdsToAdd.Add(x);
                        idsToAdd.Remove(x);
                    }
                });
            });

            //This help clarify what ids to keep, using to update case
            idsToKeep = listWithOutIdsToAdd;

            //This logic help to remove old ids, keep only ids to remove
            oldIds.ForEach(x =>
            {
                listWithOutIdsToAdd.ForEach(y =>
                {
                    if (x.Equals(y)) idsToRemove.Remove(x);
                });
            });

            //foreach (var x in newIds)
            //{
            //    if (oldIds.Contains(x))
            //    {
            //        idsToRemove.Remove(x);
            //        idsToAdd.Remove(x);
            //        idsToKeep.Add(x);
            //    }
            //}

            return (idsToRemove, idsToAdd, idsToKeep);
        }

        public static (List<Guid> idsToRemove, List<Guid> idsToAdd, List<Guid> idsToKeep) SplitIdsToAddAndRemove(List<Guid> oldIds, List<Guid> newIds)
        {
            List<Guid> idsToAdd = newIds.Except(oldIds).ToList();
            List<Guid> idsToRemove = oldIds.Except(newIds).ToList();
            List<Guid> idsToKeep = oldIds.Intersect(newIds).ToList();

            return (idsToRemove, idsToAdd, idsToKeep);
        }
    }
}
